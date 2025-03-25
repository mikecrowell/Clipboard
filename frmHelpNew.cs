using FieldTool.BLL;
using FieldTool.BLL.BusinessObjects;
using FieldTool.Constants.Paths;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmHelpNew : DevExpress.XtraEditors.XtraForm
    {
        private frmMain _parent;

        public frmHelpNew(frmMain parent)
        {
            InitializeComponent();
            _parent = parent;

            btnViewDocument.Click += btnViewDocument_Click;

            InitializeDocuments();
        }

        private void btnSendDataFiles_Click(object sender, EventArgs e)
        {
            string userEmail = UserSettings.GetUserSettings().Email;
            string title = _parent.Text;
            var frm = new frmSendDataFiles(userEmail, title);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnInstallatiionFolder_Click(object sender, EventArgs e)
        {
            string path = GetInstallPath();
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void btnDataFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", DataStore.XmlDataDirectory);
        }

        private void btnBrandingFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", System.Environment.CurrentDirectory + PathConstant.ClientBrandingDirectory);
        }

        private void btnForceBrandingRefresh_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to update your branding files? This will require a restart of clipboard", "Refresh branding files.", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            string dir = System.Environment.CurrentDirectory + PathConstant.ClientBrandingDirectory;

            if (Directory.Exists(dir))
            {
                DirectoryInfo d = new DirectoryInfo(dir);
                d.Delete(true);
            }

            _parent.Close();
        }

        private void btnDataFileRecovery_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmDataFileRecovery(_parent);
                frm.ShowDialog();
            }
            catch (Exception)
            {
                throw;
            }
        }









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
                    throw;
                }
            }
            else
            {
                // Not running from installation.
                result = System.Environment.CurrentDirectory;
            }

            return result;
        }


        private void InitializeDocuments()
        {

            this.cboDocuments.DataSource = UserDocs.GetDocs();
            cboDocuments.DisplayMember = "DocName";
            cboDocuments.ValueMember = "FileNamePath";
            if (this.cboDocuments.Items.Count > 0)
            {
                this.cboDocuments.SelectedIndex = 0;
                btnViewDocument.Enabled = true;
            }
            else
            {
                btnViewDocument.Enabled = false;
            }

        }

        private void btnViewDocument_Click(object sender, EventArgs e)
        {
            if (File.Exists(cboDocuments.SelectedValue.ToString()))
            {
                System.Diagnostics.Process.Start(cboDocuments.SelectedValue.ToString());
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
