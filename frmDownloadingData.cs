using Clipboard.Helper.Configuration;
using DevExpress.XtraEditors;
using FieldTool.BLL;
using FieldTool.BLL.Utilities;
using FieldTool.Bsi.Models;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Paths;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmDownloadingData : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmDownloadingData(frmMain parent, string userEmail)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            btnClose.Visible = false;

            _parent = parent;
            _userEmail = userEmail;

            this.Shown += FrmDownloadingData_Shown;
        }

        public frmMain _parent { get; set; }
        public string _userEmail { get; set; }

        private void FrmDownloadingData_Shown(object sender, EventArgs e)
        {
            DownloadDataHelper(_parent, _userEmail);
        }

        private string StatusText
        {
            get
            {
                return txtStatus.Text;
            }
            set
            {
                if (value == "")
                {
                    txtStatus.Text = "";
                }
                txtStatus.Text += value + Environment.NewLine;
                txtStatus.Invalidate();
                txtStatus.Update();
                txtStatus.Refresh();
                Application.DoEvents();
            }
        }

        private async void DownloadDataHelper(frmMain parent, string userEmail)
        {
            StatusText = "";
            int projectCount = 0;
            string msg = "";
            List<ApiProjectDownload> projects = new List<ApiProjectDownload>();

            try
            {
                StatusText = "Initializing Download Helper...";
                var downloadHelper = new DownloadHelper(ConfigurationManager.AppSettings);

                StatusText = "Getting Gloabal Meta Data...";
                await downloadHelper.GetGlobalMetadata();

                StatusText = "Getting Programs...";
                await downloadHelper.GetAllPrograms(userEmail);

                StatusText = "Getting Projects...";
                projects = parent.bsiService.GetProjects(userEmail);

                // StatusText = "Getting projects...";
                //projectCount = downloadHelper.GetAllProjects(userEmail);

                StatusText = "Getting Configuration folder...";
                await LoadDatFolder(PathConstant.ConfigurationFolderPath);
                ConfigurationHelper.ResetApiBrandingConfigurationData();

                //Don't download engineering data files from Azure.
                StatusText = "Getting Engineering folder... Skipped";
                //if (ConfigurationHelper.ShouldIDownloadEngineeringConfig)
                //{
                //    StatusText = "Getting Engineering folder...";
                //    await LoadDatFolder(PathConstant.EngineeringFolderPath);
                //}
                //else
                //{
                //    StatusText = "Getting Engineering folder... Skipped";
                //}

                StatusText = "Getting Branding Files";
                ConfigurationHelper.CreateBrandingDirectoriesFromPrograms();
                await LoadBrandingFiles();

                StatusText = Environment.NewLine + "Processing Audit Projects...";
                projectCount = downloadHelper.ProcessAuditProjects(projects);

                StatusText = "Getting xml files...";
                DownloadXmlData(parent);

                StatusText = "Initializing Main Form...";
                _parent.frmMainInitializeAfterDownload();

                StatusText = " ";

                if (projectCount == 1)
                {
                    msg = "1 new project downloaded.";
                }
                else if (projectCount > 0)
                {
                    msg = projectCount + " new projects downloaded.";
                }
                else
                {
                    msg = "No new projects were found in Efficiency Manager.";
                }

                StatusText = msg;
            }
            catch (JsonReaderException ex)
            {
                string clipboardText = ex.Data["JsonMessage"] + "\n\n" + ex.ToString() + "\n\n" + ex.Data["JsonString"];
                Constants.Utilities.SetClipboardText(clipboardText);

                string message = "Your data was not properly downloaded!\n\nNone of your current data was affected.\n\n" + ex.Data["JsonMessage"] + "\n\n" + ex.ToString() +
                    "\n\nThis message has been copied to your clipboard for you to paste and send in an email to the help desk.";
                MessageBox.Show(message, "Data Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                StatusText = " ";
                StatusText = "@@@ ERROR @@@";
                StatusText = message;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString(), "Data Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                StatusText = " ";
                StatusText = "@@@ ERROR @@@";
                StatusText = ex.InnerException.ToString();
            }
            finally
            {
                btnClose.Visible = true;
                this.Cursor = Cursors.Default;
            }
        }

        private async Task<bool> LoadDatFolder(string folderToDownloadPath)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                //MainFromUserControls.MainForm.PleaseWait = true;
                UserSettings user = UserSettings.GetUserSettings();
                string username = user.UserName?.Replace("@", "");
                string tempDirectory = DataStore.DIReportDirectoryTemp;
                string directory = System.Environment.CurrentDirectory + folderToDownloadPath;
                string endpoint = ConfigurationManager.AppSettings["lookupServiceUrl"];
                // endpoint = "http://localhost:51455";

                if (folderToDownloadPath == PathConstant.EngineeringFolderPath && ApplicationDeployment.IsNetworkDeployed)
                {
                    directory = ApplicationDeployment.CurrentDeployment.DataDirectory + folderToDownloadPath;
                }

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var helper = new AzureDirectoryDownloadHelper
                    (
                        endpoint: endpoint,
                        userName: username,
                        tempDirectory: tempDirectory
                    );

                await helper.DownloadExtractAndCopyByFolderName(directory);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> LoadBrandingFiles()
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                UserSettings user = UserSettings.GetUserSettings();
                List<ApiProgramMetadata> programsList = DataStore.GetPrograms();
                string username = user.UserName?.Replace("@", "");
                string tempDirectory = DataStore.DIReportDirectoryTemp;
                string brandingDirectory = System.Environment.CurrentDirectory + PathConstant.ClientBrandingDirectory;
                string endpoint = ConfigurationManager.AppSettings["lookupServiceUrl"];
                //endpoint = "http://localhost:51455";

                var helper = new AzureDirectoryDownloadHelper
                    (
                        endpoint: endpoint,
                        userName: username,
                        tempDirectory: tempDirectory
                    );

                await helper.DownloadExtractAndCopyBrandingFilesByProgramList(ConfigurationHelper.ApiBrandingConfigurationData, programsList, brandingDirectory, new MemoEdit(), txtStatus, "Branding");
                return true;
            }
            catch (Exception ex)
            {
                Lg.FatalError(ex, "LoadBrandingFiles()");
                return false;
            }
        }

        private void DownloadXmlData(frmMain parent)
        {
            if (!this.DesignMode)
            {
                parent.cvApptCalendarView.CalendarModel.Appointments.Clear();

                Audit.AuditCollection audits = DataStore.GetAuditsByDate(parent.calApptMonthCalendar.SelectedDate);

                if (audits != null && audits.Count > 0)
                {
                    parent.AddAuditsToCalendar(audits);
                }
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}