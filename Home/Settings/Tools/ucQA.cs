using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.BLL.ClipboardConfiguration;
using Clipboard.UI.DirectInstall;
using Clipboard.UI._Helper;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucQA : DevExpress.XtraEditors.XtraUserControl
    {

        public QAUserConfig _qaUserConfig { get; set; }


        public ucQA()
        {
            InitializeComponent();
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
            this.Dock = DockStyle.Fill;

            btnSaveAndClose.Click += BtnSaveAndClose_Click;

            if (SelectedItems.UserClipboardConfig.ProjectUserConfig == null)
            {
                _qaUserConfig = new QAUserConfig();
            }
            else
            {
                _qaUserConfig = SelectedItems.UserClipboardConfig.QAUserConfig;
            }

            SetUserConfig();
        }

        

        private void SetUserConfig()
        {
            tglSkipDownload.IsOn = _qaUserConfig.SkipProgramDownload;
        }



        private void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                if (txtPassCode.Text == "Alex1")
                {
                    _qaUserConfig.SkipProgramDownload = tglSkipDownload.IsOn;

                    SelectedItems.UserClipboardConfig.QAUserConfig = _qaUserConfig;
                    SelectedItems.UserClipboardConfiguration.SaveToXml();
                }
                else
                {
                    MessageBox.Show("You must enter the correct passcode to edit this section. If you don't know it then you probably shouldn't be messing with it :) ");
                }

                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.MainForm.PreviousUserControl);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally
            {
                MainFromUserControls.MainForm.PleaseWait = false;
                this.Dispose();
            }
        }






    }
}
