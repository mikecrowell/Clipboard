using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.Bsi.Models;
using FieldTool.BLL;
using Clipboard.UI.DirectInstall;
using Clipboard.UI._Helper;
using Clipboard.Helper.Apis.HES;
using Clipboard.UI.Theme;
using Clipboard.UI.Home.Help;
using Clipboard.UI.DirectInstall.Settings;

namespace Clipboard.UI.Home
{
    public partial class ucHome : DevExpress.XtraEditors.XtraUserControl
    {
        public ucHome()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public void SetSkin()
        {
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }

        public bool LogOffEnabled
        {
            set
            {
                tileLogOff.Enabled = value;
            }
        }

        #region Direct Install

        private void btnDirectInstall_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                DirecInstallClickHandle(autoTransferPage: true);
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

        public void DirecInstallClickHandle(bool autoTransferPage)
        {
            List<ApiProgramMetadata> programs = DataStore.GetPrograms();
            var dis = DataStore.GetAllDIs();

            if (programs.Count > 0)
            {
                if (SelectedItems.ClipboardConfiguration == null)
                {
                    MessageBox.Show("ClipboardConfiguration is null... This will most likely cause an error, so i cannot take you to this page. Please download data again and make sure the branding files are downloaded correctly. ");
                    return;
                }

                if (MainFromUserControls.UcDirectInstall == default(ucDirectInstall))
                {
                    InitializeDirectInstall();
                }

                if (MainFromUserControls.UcDirectInstall.InitializedProperly)
                {
                    if (autoTransferPage)
                    {
                        MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDirectInstall);
                    }
                }
                else
                {
                    MessageBox.Show("There was a problem initializing direct install so i cannot send you to this page at this time.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Looks like you don't have a any programs. Please click the download data button on this page before entering Direct Install.");
                return;
            }
        }

        public void InitializeDirectInstall()
        {
            MainFromUserControls.MainForm.PleaseWait = true;
            MainFromUserControls.UcDirectInstall = new ucDirectInstall();
            MainFromUserControls.UcDirectInstall.Init();
            DirectInstallUserControls.UcMenu.SetConfiguration(SelectedItems.ClipboardConfiguration);
            MainFormHelper.SkinApplication();
        }

        #endregion

        // Theme
        private void tileTheme_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(new ucTheme());
        }

        // Login
        private void tileLogOff_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                MainFromUserControls.UcLogIn.Init();
                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcLogIn);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        // Download
        private void btnDownload_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure?", "Download Data", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return;
                }

                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDownload);
                MainFromUserControls.UcDownload.BeginDownload(autoTransferToNextPage:false);
                MainFromUserControls.UcDirectInstall = null;
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        // Help
        private void tileHelp_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHelp);
        }

        // Settings
        private void btnSettings_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcSettings);
        }
       
    }
}
