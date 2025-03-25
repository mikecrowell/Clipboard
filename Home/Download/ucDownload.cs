using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.Bsi.Helpers;
using FieldTool.BLL.Utilities;
using System.Configuration;
using FieldTool.Constants.Logging;
using Newtonsoft.Json;
using System.Threading;
using Clipboard.UI._Helper;
using FieldTool.BLL;
using FieldTool.Constants.Paths;
using Clipboard.UI.DirectInstall;
using FieldTool.Bsi.Models;
using Clipboard.Helper.Configuration;
using FieldTool.BLL.ClipboardConfiguration;
using System.IO;
using Clipboard.Helper.Help;
using DevExpress.XtraEditors;
using System.Deployment.Application;

namespace Clipboard.UI.Home.Download
{
    public partial class ucDownload : DevExpress.XtraEditors.XtraUserControl
    {
        private IBsiService _bsiService = default(IBsiService);
        private string _userEmail = "";

        public ucDownload()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }


        public void SetSkin()
        {
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }

        public void Init(IBsiService bsiService, string userEmail)
        {
            Lg.Info("ucDownload Init(IBsiService bsiService, string userEmail)");
            _bsiService = bsiService;
            _userEmail = userEmail;
        }

        public string MyProperty { get; set; }


        public async void BeginDownload(bool autoTransferToNextPage = false, bool skipProgramDownload = false)
        {
            DownloadHelper.ProcessingReport result = new DownloadHelper.ProcessingReport();
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                txtStatus.Text += Environment.NewLine;

                btnHome.Enabled = false;
                btnDownloadAgain.Enabled = false;

                txtStatus.Text = "";

                

                if (skipProgramDownload == false)
                {
                    txtStatus.Text += "Initializing Download..." + Environment.NewLine;
                    var downloadHelper = new DownloadHelper(ConfigurationManager.AppSettings);
                    downloadHelper.bsiService = _bsiService;

                    txtStatus.Text += "Getting Global Meta Data.." + Environment.NewLine;
                    await downloadHelper.GetGlobalMetadata();

                    txtStatus.Text += "Getting Programs..." + Environment.NewLine;

                    MainFromUserControls.MainForm.PleaseWait = true;

                    if (SelectedItems.IsTaEnv)
                    {
                        await downloadHelper.GetAllPrograms(TAUserSetting.UserName, TAUserSetting.TradeAllyBensightId, TAUserSetting.ProgramCodeList);
                    }
                    else
                    {
                        await downloadHelper.GetAllPrograms(_userEmail);
                    }

                    var programs = DataStore.GetPrograms();
                    foreach (ApiProgramMetadata p in programs)
                    {
                        txtStatus.Text += p.program.Name + "," + Environment.NewLine;
                    }
                }
                else
                {
                    txtStatus.Text += "Program & MetaData... - Skipped.." + Environment.NewLine;
                }

                
                txtStatus.Text += Environment.NewLine + "Getting Configuration folder..." + Environment.NewLine;

                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

                await LoadDatFolder(PathConstant.ConfigurationFolderPath);
                ConfigurationHelper.ResetApiBrandingConfigurationData();

                if (ConfigurationHelper.ShouldIDownloadEngineeringConfig)
                {
                    txtStatus.Text += "Getting Engineering folder..." + Environment.NewLine;
                    await LoadDatFolder(PathConstant.EngineeringFolderPath);
                }
                else
                {
                    txtStatus.Text += "Getting Engineering folder... - Skipped" + Environment.NewLine;
                }

                txtStatus.Text += "Creating Branding Directories..." + Environment.NewLine;

                ConfigurationHelper.CreateBrandingDirectoriesFromPrograms();

                txtStatus.Text += "Getting Branding Files..." + Environment.NewLine;
                await LoadBrandingFiles();

                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

                MainFromUserControls.MainForm.DownloadInProgress = false;
                //MainFormHelper.SetClipboardConfiguration();


                txtStatus.Text += Environment.NewLine + "Download Finished..." + Environment.NewLine + Environment.NewLine;
                btnHome.Enabled = true;
                btnDownloadAgain.Enabled = true;

                var txt = new MemoEdit();
                if (ConflictResolverHelper.RunAll(txt, updateData:false, runMinor:false))
                {
                    string warning = $"WARNING!! Conflicts Found...{Environment.NewLine}You may experience errors.{Environment.NewLine}To fix this go to Home > Help > Resolve Confilcts. {Environment.NewLine} {txt.Text}";
                    txtStatus.Text += warning;
                    MessageBox.Show(warning);
                }


                txtStatus.SelectionStart = txtStatus.Text.Length;
                txtStatus.ScrollToCaret();

                if (autoTransferToNextPage)
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex,  $"--- Download Programs --- {Environment.NewLine} {txtStatus.Text}");
                btnHome.Enabled = true;
            }
            finally
            {
                MainFromUserControls.MainForm.DownloadInProgress = false;
                MainFromUserControls.MainForm.PleaseWait = false;
                Lg.Info(JsonConvert.SerializeObject(result));
            }
        }

  

        private async Task<bool> LoadBrandingFiles()
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                MainFromUserControls.MainForm.PleaseWait = true;
                UserSettings user = MainFromUserControls.MainForm.User;
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

                await helper.DownloadExtractAndCopyBrandingFilesByProgramList(ConfigurationHelper.ApiBrandingConfigurationData, programsList, brandingDirectory, txtStatus, new TextBox(), "Branding");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<bool> LoadDatFolder(string folderToDownloadPath)
        {
            try
            {
                //Cursor.Current = Cursors.WaitCursor;
                MainFromUserControls.MainForm.PleaseWait = true;
                UserSettings user = MainFromUserControls.MainForm.User;
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



        private void btnHome_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
        }

  

        private void btnDownloadAgain_Click(object sender, EventArgs e)
        {
            try
            {
                BeginDownload();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }
    }
}
