using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.BLL;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using FieldTool.Constants.Logging;
using Clipboard.UI._Helper;
using Clipboard.Helper.LogIn;
using FieldTool.DAL.DTO.LogIn;
using Clipboard.Helper.Utilities;
using DevExpress.XtraEditors;

namespace Clipboard.UI.LogIn
{
    public partial class ucLogIn : DevExpress.XtraEditors.XtraUserControl
    {
        public ucLogIn()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            btnUserOne.Click += btnUserOne_Click;
            btnUserTwo.Click += btnUserTwo_Click;
        }

        #region Init

        public void Init()
        {
            Lg.Info("ucLogIn Init()");
            txtPassword.Text = "";
            CenterControls();
            SetPreviousUsers();
            txtEmail.Focus();
            lblWarning.Visible = false;
            lblWarning.Text = "";
        }

        public void SetSkin()
        {
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;
        }

        public void CenterControls()
        {
            int left = (this.Width - pnlCredentials.Width) / 2;
            pnlCredentials.Location = new Point(left, pnlCredentials.Location.Y);
            txtEmail.Focus();
        }

        private void SetPreviousUsers()
        {
            List<LogInDTO> users = LogInHelper.GetReportXmlObject().OrderBy(x=> x.LastLoggedInOnEngage).Take(2).ToList();

            if (users.Count == 1)
            {
                btnUserOne.Visible = true;
                btnUserTwo.Visible = false;

                btnUserOne.Text = users?[0].UserName;
            }
            else if (users.Count == 2)
            {
                btnUserOne.Text = users?[0].UserName;
                btnUserTwo.Text = users?[1].UserName;

                btnUserOne.Visible = true;
                btnUserTwo.Visible = true;
            }
            else
            {
                btnUserOne.Visible = false;
                btnUserTwo.Visible = false;
            }

        }

        #endregion

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogInHelper.IsRequredInformationFilledOut(lblWarning, txtPassword, txtEmail, chkAccept))
                {
                    MainFromUserControls.MainForm.PleaseWait = true;
                    btnLogIn.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPassword.Enabled = false;
                    chkAccept.Enabled = false;

                    string password = txtPassword.Text;
                    string username = txtEmail.Text;

                    LogInDTO dto = LogInHelper.GetReportXmlObject().Where(x => x.UserName == username).FirstOrDefault();
                    password = LogInHelper.DecryptPassword(dto, password);

                    bool successfullyLoggedIn = await LogInHelper.LogIn(lblWarning, password, username);

                    if (successfullyLoggedIn)
                    {
                        if (dto != default(LogInDTO) && string.IsNullOrEmpty(dto.HashedPin))
                        {
                            MainFromUserControls.MainForm.SetUserControl(new ucPinCreation());
                        }
                        else
                        {
                            if (MainFormHelper.AutoDownloadPrograms)
                            {
                                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcDownload);
                                MainFromUserControls.UcDownload.BeginDownload();
                            }
                            else
                            {
                                MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHome);
                            }
                        }
                    }
                    else
                    {
                        lblWarning.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            finally
            {
                MainFromUserControls.MainForm.PleaseWait = false;
                btnLogIn.Enabled = true;
                txtEmail.Enabled = true;
                txtPassword.Enabled = true;
                chkAccept.Enabled = true;
            }
        }

        

        private void lblTermsAndConditions_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.SetUserControl(new ucTermsOfService());
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnUserOne_Click(object sender, EventArgs e)
        {
            try
            {
                UserButtonHandel(btnUserOne);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnUserTwo_Click(object sender, EventArgs e)
        {
            try
            {
                UserButtonHandel(btnUserTwo);
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void UserButtonHandel(CheckButton btn)
        {
            LogInDTO dto = LogInHelper.GetReportXmlObject().Where(x => x.UserName == btn.Text).FirstOrDefault();

            //if (dto != default(LogInDTO) && dto.AcceptedTermsAndConditions)
            //{
            //    chkAccept.Checked = true;
            //}
            //else
            //{
            //    chkAccept.Checked = false;
            //}

            btnUserOne.Checked = false;
            btnUserTwo.Checked = false;
            txtEmail.Text = btn.Text;
            txtPassword.Focus();
        }

        private void lblClearPin_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmResult = MessageBox.Show("Are you sure you want to clear your pin?",
                                     "Yes Clear Pin",
                                     MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    string username = txtEmail.Text;

                    List<LogInDTO> list = LogInHelper.GetReportXmlObject();
                    LogInDTO Odto = list.Where(x => x.UserName == username).FirstOrDefault();
                    LogInDTO dto = list.Where(x => x.UserName == username).FirstOrDefault();

                    dto.HashedPin = string.Empty;

                    list.Remove(Odto);
                    list.Add(dto);
                    LogInHelper.SaveToXml(list);
                }
                else
                {
                    // If 'No', do something here.
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }











    }
}
