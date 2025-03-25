using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Clipboard.UI._Helper;
using System.IO;
using Clipboard.Helper.Home.Help;
using FieldTool.BLL.Home.Help;
using Clipboard.UI.DirectInstall;
using FieldTool.Bsi.Helpers;

namespace Clipboard.UI.Home.Help
{
    public partial class ucSendDataFiles : DevExpress.XtraEditors.XtraUserControl
    {
        public ucSendDataFiles(string errorText = "")
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            btnBack.Click += btnBack_Click;
            btnSendData.Click += btnSendData_Click;

            _errorText = errorText;

            if (!string.IsNullOrEmpty(_errorText))
            {
                _errorText = _errorText.Replace(Environment.NewLine, "<br/>");
            }
        }


        public string _errorText = "";

        public string ZIP_FILENAME = "DataFiles_{0}_{1:yyyy_MM_dd_hh_mm_ss}.zip";

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                //dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                dialog.Multiselect = false;

                if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
                {
                    string path = dialog.FileName; // get name of file
                    txtAttachmentList.Text += path + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }


        #region Bottom Buttons

        private void btnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;

                txtStatus.Text = "";
                var nl = Environment.NewLine;
                txtStatus.Text += "Starting" + nl;

                // Get attachments by splitting on NewLine
                txtStatus.Text += "Parsing Attachments" + nl;
                List<string> attachments = new List<string>();
                attachments = txtAttachmentList.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

                // User input
                txtStatus.Text += "Parsing User Questions" + nl;
                SendDataFiles sd = PopulateSendDataFiles(_errorText);

                txtStatus.Text += "Preparing to zip files" + nl;
                string zipFilePath = SendDataFilesHelper.CreateZip(attachments, sd);
                if (File.Exists(zipFilePath))
                {
                    string filename = Path.GetFileName(zipFilePath);
                    txtStatus.Text += "Zipping completed" + nl;
                    txtStatus.Text += "Preparing to upload zip" + nl;
                    SendDataFilesHelper.SendZipToAzure(zipFilePath);
                    txtStatus.Text += "Upload Complete" + nl;

                    txtStatus.Text += "Removing temp files" + nl;
                    File.Delete(zipFilePath);

                    string fromEmail = SelectedItems.IsTaEnv ? TAUserSetting.UserName : SelectedItems.User.Email;
                    SendDataFilesHelper.SendEmailNotice(fromEmail, _errorText, filename, sd, attachments);

                    MessageBox.Show("Files Sent Successfully.");
                }
                else
                {
                    txtStatus.Text += "Zipping FAILED" + nl;
                    MessageBox.Show("Could not create zipped file. Files cannot be sent.");
                }

                txtStatus.Text += "Finished" + nl;
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

        private SendDataFiles PopulateSendDataFiles(string errorText = "")
        {
            SendDataFiles sd = new SendDataFiles() {
                Description = txtDescription.Text,
                TicketNumber = txtTicketNumber.Text,
                ErrorText = errorText,
                Version = MainFromUserControls.MainForm.Title
            };
            return sd;
        }

        private void btnBack_Click(object sender, EventArgs e)
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







        #endregion


    }
}
