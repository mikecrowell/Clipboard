using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FieldTool.DAL.DTO;
using FieldTool.BLL;
using System.IO;
using FieldTool.Bsi.Models;
using Clipboard.Helper;
using Clipboard.UI._Helper;
using Clipboard.Helper.Utilities;

namespace Clipboard.UI.Home.Help
{
    public partial class ucUserInfo : DevExpress.XtraEditors.XtraUserControl
    {
        public ucUserInfo()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;

            pnlButtonContainer.Visible = false;
            pnlCreateAuthorizationGroup.Visible = MainFromUserControls.MainForm.IsDebug && Environment.UserName?.ToLower() == "kgrittner";
            
            

            btnRefreshApiUser.Click += btnRefreshApiUser_Click;
            btnRefreshSettings.Click += btnRefreshSettings_Click;
            btnUpdateUserInfo.Click += btnUpdateUserInfo_Click;

            ReadUserSettingsFile();
            ReadApiUserFile();
            PopulatePrograms();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcHelp);
            this.Dispose();
        }

        private void PopulatePrograms()
        {
            var nl = Environment.NewLine;

            foreach (ApiProgramMetadata p in DataStore.GetPrograms())
            {
                txtProgramName.Text += $"{p.program.Name}{nl}";
                txtProgramCodes.Text += $"{p.program.ProgramCode}{nl}";
            }
        }

        private void ReadApiUserFile()
        {

            if (!File.Exists(DataStore.JsonApiUserFile) || MainFromUserControls.MainForm.ApiUser == null)
            {
                txtFileExistsApiUser.Text = "FALSE";
                return;
            }

            txtFileExistsApiUser.Text = "TRUE";
            txtEmailApiUser.Text = MainFromUserControls.MainForm.ApiUser.Email;
            txtIDApiUser.Text = MainFromUserControls.MainForm.ApiUser.Id;
            txtNameApiUser.Text = MainFromUserControls.MainForm.ApiUser.Name;
            txtUsernameApiUser.Text = MainFromUserControls.MainForm.ApiUser.UserName;

        }

        private void ReadUserSettingsFile()
        {
            UserSettingsDTO dto = UserSettingsDTO.GetUserSettings(DataStore.XmlDataDirectory);
            if (dto == default(UserSettingsDTO))
            {
                txtFileExistsSetting.Text = "FALSE";
                return;
            }

            txtFileExistsSetting.Text = "TRUE";
            txtEmailSetting.Text = dto.Email;
            txtFullNameSetting.Text = dto.FullName;
            txtUserNameSetting.Text = dto.UserName;
        }

        private void btnRefreshSettings_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                if (!FieldTool.Constants.Utilities.IsNetworkAvailable())
                {
                    MessageBox.Show("Could not find internet connection or franklin network. Could not refresh file");
                    return;
                }

                UserSettingsHelper.LoadUserSettings(true);
                MessageBox.Show("Success");
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

        private void btnRefreshApiUser_Click(object sender, EventArgs e)
        {
            try
            {
                MainFromUserControls.MainForm.PleaseWait = true;
                if (!FieldTool.Constants.Utilities.IsNetworkAvailable())
                {
                    MessageBox.Show("Could not find internet connection or franklin network. Could not refresh file");
                    return;
                }

                MainFormHelper.GetApiUser(true);
                MessageBox.Show("Success");
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

        private void btnUpdateUserInfo_Click(object sender, EventArgs e)
        {
            txtEmailSetting.ReadOnly = false;
            txtFullNameSetting.ReadOnly = false;
            txtUserNameSetting.ReadOnly = false;

            txtEmailApiUser.ReadOnly = false;
            txtIDApiUser.ReadOnly = false;
            txtNameApiUser.ReadOnly = false;
            txtUsernameApiUser.ReadOnly = false;

            MessageBox.Show("User Fields are now editable. Edit what you need to and then come back to this page and click save.");
        }

        private void btnCreateKeyCode_Click(object sender, EventArgs e)
        {
            try
            {
                string code = DateTime.Now.ToShortDateString();
                code = StringCipher.Encrypt(code, "Karl");
                txtCreatedKeyCode.Text = code;
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
            
        }

        private void btnKeyCodeSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string code = txtKeyCode.Text;
                code = StringCipher.Decrypt(code, "Karl");

                if (code == DateTime.Now.ToShortDateString())
                {
                    pnlButtonContainer.Visible = true;
                    pnlKeyCode.Visible = false;
                    return;
                }

                MessageBox.Show("Incorrect.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed or incorrect.");
            }
        }






    }
}
