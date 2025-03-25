using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Entity.Model;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using System.Deployment.Application;
using FieldTool.BLL;
using Clipboard.UI._Helper;
using FieldTool.Constants.Paths;
using System.IO;
using Clipboard.UI.Home.Download;

namespace Clipboard.UI.Home.Help
{
    public partial class ucHelp : DevExpress.XtraEditors.XtraUserControl
    {
        public ucHelp()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            btnDataFolder.Click += btnDataFolder_Click;
            btnInstallatiionFolder.Click += btnInstallatiionFolder_Click;
            btnBack.Click += btnBack_Click;
            btnDataFileRecovery.Click += btnDataFileRecovery_Click;
            btnInfoDataRecovery.Click += btnInfoDataRecovery_Click;
            //btnDataFileRecovery.Visible = false;
            if (!MainFromUserControls.MainForm.IsDebug)
            {
                tabDocumentation.PageVisible = false;
            }
        }

        #region Init

        public void Init()
        {
            Lg.Info("ucHelp Init()");
            SetSkin();
        }

        public void SetSkin()
        {
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }

        #endregion


        #region Button Clicks

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
        }

        private void btnInstallatiionFolder_Click(object sender, EventArgs e)
        {
            string path = GetInstallPath();

            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally {
                MainFromUserControls.MainForm.PleaseWait = false;
            }
        }

        private void btnDataFolder_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                System.Diagnostics.Process.Start("explorer.exe", DataStore.XmlDataDirectory);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally
            {
                MainFromUserControls.MainForm.PleaseWait = false;
            }
        }

        private void btnBrandingFolder_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                System.Diagnostics.Process.Start("explorer.exe", System.Environment.CurrentDirectory + PathConstant.ClientBrandingDirectory);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally
            {
                MainFromUserControls.MainForm.PleaseWait = false;
            }
        }

        #endregion



        private string GetInstallPath()
        {
            string result = "Undetermined";

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                try
                {
                    // Running from installation.
                    result = ApplicationDeployment.CurrentDeployment.DataDirectory;
                }
                catch (Exception ex)
                {
                    ErrorHelper.PresentError(ex);
                }
            }
            else
            {
                // Not running from installation.
                result = System.Environment.CurrentDirectory;
            }

            return result;
        }

        private void btnConflictResolver_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucConflictResolver());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnSendDataFiles_Click(object sender, EventArgs e)
        {
            try
            {
                //MainFromUserControls.MainForm.SetUserControl(new ucSendDataFiles());
                MainFromUserControls.MainForm.SetUserControl(new ucSendDataFilesContainer());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnDataFileRecovery_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("You may lose all your data... You should only use this if directed.", "WARNING!!!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                DialogResult dialogResult2 = MessageBox.Show("This will destroy your current data and load from a backup.", "SUPER WARNING!!!", MessageBoxButtons.YesNo);
                if (dialogResult2 == DialogResult.No)
                {
                    return;
                }

                DialogResult dialogResult3 = MessageBox.Show("You super duper sure?", "SUPER DUPER WARNING!!!", MessageBoxButtons.YesNo);
                if (dialogResult3 == DialogResult.No)
                {
                    return;
                }

                var uc = new ucDataFileRecovery();
                MainFromUserControls.MainForm.SetUserControl(uc);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }
        
        private void btnForceBrandingRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update your branding files? This will require a restart of clipboard", "Refresh branding files.", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                string dir = System.Environment.CurrentDirectory + PathConstant.ClientBrandingDirectory;
                string dirEngineering = System.Environment.CurrentDirectory + PathConstant.EngineeringFolderPath;

                if (Directory.Exists(dir))
                {
                    DirectoryInfo d = new DirectoryInfo(dir);
                    d.Delete(true);
                }
                if (Directory.Exists(dirEngineering))
                {
                    DirectoryInfo dirEng = new DirectoryInfo(dirEngineering);
                    dirEng.Delete(true);
                }



                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDownload);
                MainFromUserControls.UcDownload.BeginDownload(autoTransferToNextPage: false, skipProgramDownload: true);
                MainFromUserControls.UcDirectInstall = null;

                //MainFromUserControls.MainForm.Close();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnUserInfo_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(new ucUserInfo());
        }

        private void btnInfoDataRecovery_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Data File Recovery", 
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "This function will show you a list of all your backup files, "
                + "with the most recent at the top. Selecting a row and clicking restore will return clipboard's data to the selected row. "
                + "Clipboard will then restart and the selected backup file will be restored. " 
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "You should only use this when you have a corrupted file, or as instructed." 
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "Your current data will be lost. You will also see this at start up if your data file is corrupted and you MUST do a backup restore."
                );
        }

        private void btnInfoBrandingRefresh_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Branding Refresh",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "I will delete your current branding files and re-download them. " + Environment.NewLine
                + "I will SKIP program and metedata files." 
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) If clipboard branding is not correct. (Buttons, Logos, Buttons Options, etc...)" + Environment.NewLine
                + "2) If report branding is not correct." + Environment.NewLine
                + "3) If your are getting config errors." 
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "Data and Program files will NOT be affected by this. It's a very safe option."
                );
        }

        private void btnInfoConflictResolver_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Conflict Resolver",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "I will look for data related issues that may cause errors during normal use or upload. " + Environment.NewLine
                + "If i find any i will list them and enable the option to fix it."
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) If directed to during startup." + Environment.NewLine
                + "2) If you are experiencing data related errors." + Environment.NewLine
                + "3) If instructed."
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "This will modify your data file, and re-load direct install."
                );
        }

        private void btnInfoSendDataFiles_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Send Data Files",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "Will direct you to a new page where you can send the following information via email." + Environment.NewLine
                + "1) Data Files - Auto" + Environment.NewLine
                + "2) Attachments - Manual" + Environment.NewLine
                + "3) Description of issue - Manual" + Environment.NewLine
                + "4) Ticket Number - Manual" 
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) If you recieve and error. (Only once per error, please do not spam)" + Environment.NewLine
                + "2) If you created a data releated ticket." + Environment.NewLine
                + "3) If instructed."
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "Dev team may respond to you regarding the issue. However you should create a ticket or contact your program manager regarding clipboard related issues."
                );
        }

        private void btnInfoInstalltionFolder_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Installation Folder",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "I will open a file explorer where clipboard was installed."
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) Only as directed." 
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "Modifying ANY file on your own could corrupt clipboard and require a re-install."
                );
        }

        private void btnInfoDataFolder_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("Data Folder",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "I will open a file explorer where clipboard keeps your data."
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) Only as directed."
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "Modifying ANY file on your own could corrupt clipboard and require a re-install."
                );
        }

        private void btnInfoBrandingFolder_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

               ("Branding Folder",
               "-- WHAT DOES IT DO? --" + Environment.NewLine
               + "I will open a file explorer where clipboard keeps branding files."
               + Environment.NewLine + Environment.NewLine

               + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
               + "1) Only as directed."
               + Environment.NewLine + Environment.NewLine

               + "-- NOTES --" + Environment.NewLine
               + "Modifying ANY file on your own could corrupt clipboard and require a re-install."
               );
        }

        private void btnInfoUserInfo_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.CustomMessageBox

                ("User Info",
                "-- WHAT DOES IT DO? --" + Environment.NewLine
                + "Direct you to a page where you can view:" + Environment.NewLine
                + "1) User data pulled from Franklin." + Environment.NewLine
                + "2) User data pulled from eManager." + Environment.NewLine
                + "3) Programs & Codes assigned to you." 
                + Environment.NewLine + Environment.NewLine

                + "-- WHEN SHOUD I USE IT? --" + Environment.NewLine
                + "1) As directed for user setup." + Environment.NewLine
                + "2) As directed by support for confirmation." + Environment.NewLine
                + "3) Personal confirmation of user data & program information."
                + Environment.NewLine + Environment.NewLine

                + "-- NOTES --" + Environment.NewLine
                + "1) Advanced Settings requires a key code to unlock." + Environment.NewLine
                + "2) Program information will update after clicking Home > Download OR re-opening clipboard." + Environment.NewLine,
                650
                );
        }
    }
}
