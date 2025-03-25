using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard.UI._Helper;
using FieldTool.BLL.ClipboardConfiguration;
using Clipboard.UI.DirectInstall;

namespace Clipboard.UI.Home.Settings.Tools
{
    public partial class ucSavingData : DevExpress.XtraEditors.XtraUserControl
    {
        private SavingDataConfig _savingDataConfig { get; set; }

        public ucSavingData()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            btnSaveAndClose.Click += BtnSaveAndClose_Click;
            btnDefault.Click += BtnDefault_Click;
            

            txtAbout.Text = "This section will allow you to update when clipboard saves the data file."
                +" If you have many projects, reducing save writes may increase performance."
                +" However, if you experience, System Crashes, or Loss of data frequently,"
                +" then saving more often may be the best option" ;


            if (SelectedItems.UserClipboardConfig.ProjectUserConfig == null)
            {
                _savingDataConfig = new SavingDataConfig();
            }
            else
            {
                _savingDataConfig = SelectedItems.UserClipboardConfig.SavingDataConfig;
            }

            SetUserConfig();

        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                _savingDataConfig = new SavingDataConfig();
                chkSaveOnMenu.Checked = _savingDataConfig.SaveOnMenuChange;
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void SetUserConfig()
        {
            chkSaveOnMenu.Checked= _savingDataConfig.SaveOnMenuChange;
        }

        private void BtnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                _savingDataConfig.SaveOnMenuChange = chkSaveOnMenu.Checked;

                SelectedItems.UserClipboardConfig.SavingDataConfig = _savingDataConfig;
                SelectedItems.UserClipboardConfiguration.SaveToXml();

                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcSettings);
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
