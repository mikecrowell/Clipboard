using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.MultiMedia.SendDataFiles;
using Clipboard.UI.DirectInstall;
using Clipboard.UI._Helper;

namespace Clipboard.UI.Home.Help
{
    public partial class ucSendDataFilesContainer : DevExpress.XtraEditors.XtraUserControl, ISendDataFiles
    {
        public ucSendDataFilesContainer()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            var uc = new Clipboard.MultiMedia.SendDataFiles.ucSendDataFiles(
                this, 
                SelectedItems.IsTaEnv, 
                MainFromUserControls.MainForm.User.Email, 
                MainFromUserControls.MainForm.Title, 
                MainFromUserControls.MainForm.SkinName
                );

            this.Controls.Add(uc);
        }


        public void BackClick()
        {
            try
            {
                if (MainFromUserControls.MainForm.PreviousUserControl?.Tag?.ToString() == "Error")
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
                }
                else
                {
                    MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.MainForm.PreviousUserControl);
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        public void Error(Exception ex)
        {
            ErrorHelper.PresentError(ex);
        }

        public void Loading(bool show)
        {
            MainFromUserControls.MainForm.PleaseWait = show;
        }


    }
}
