using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FieldTool.DAL.DTO.LogIn;
using Clipboard.Helper.LogIn;
using FieldTool.Bsi.Helpers;
using Clipboard.Helper.Utilities;
using Clipboard.UI._Helper;

namespace Clipboard.UI.LogIn
{
    public partial class ucPinCreation : DevExpress.XtraEditors.XtraUserControl
    {
        public ucPinCreation()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.LookAndFeel.SkinName = MainFromUserControls.MainForm.SkinName;


            btnOne.Click += btnNumber_Click;
            btnTwo.Click += btnNumber_Click;
            btnThree.Click += btnNumber_Click;
            btnFour.Click += btnNumber_Click;
            btnFive.Click += btnNumber_Click;
            btnSix.Click += btnNumber_Click;
            btnSeven.Click += btnNumber_Click;
            btnEight.Click += btnNumber_Click;
            btnNine.Click += btnNumber_Click;
            btnZero.Click += btnNumber_Click;

            txtPin.Focus();
        }

        private void btnSaveAndContinue_Click(object sender, EventArgs e)
        {
            try
            {
                bool goodToGo = true;
                int pin = 0;

                try
                {
                    pin = Convert.ToInt32(txtPin.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Pin Can only be numbers.");
                    goodToGo = false;
                }

                if (txtPin.Text != txtConfirmPin.Text)
                {
                    MessageBox.Show("Pin Did not Match");
                    goodToGo = false;
                }

                if (goodToGo)
                {
                    List<LogInDTO> list = LogInHelper.GetReportXmlObject();
                    LogInDTO oDto = list.Where(x => x.UserName == TAUserSetting.UserName).FirstOrDefault();
                    LogInDTO dto = list.Where(x => x.UserName == TAUserSetting.UserName).FirstOrDefault();

                    if (dto != default(LogInDTO))
                    {
                        dto.HashedPin = StringCipher.Encrypt(txtPin.Text, "");
                        list.Remove(oDto);
                        list.Add(dto);
                        LogInHelper.SaveToXml(list);

                        BackHandle();
                    }
                }
                else
                {
                    txtPin.Text = "";
                    txtConfirmPin.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }



        private void btnNumber_Click(object sender, EventArgs e)
        {
            try
            {
                SimpleButton btn = (SimpleButton)sender;

                if (txtPin.Text.Length == 4 && txtConfirmPin.Text.Length < 4 && txtConfirmPin.Text.Length != 4)
                {
                    txtConfirmPin.Text += btn.Text;
                }

                if (txtPin.Text.Length < 4)
                {
                    txtPin.Text += btn.Text;
                }

                if (txtPin.Text.Length == 4)
                {
                    txtConfirmPin.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                BackHandle();
            }
            catch (Exception ex)
            {
                ErrorHelper.PresentError(ex);
            }
        }


        private void BackHandle()
        {
            MainFromUserControls.MainForm.SetUserControl(MainFromUserControls.UcLogIn);
            MainFromUserControls.UcLogIn.Init();
            this.Dispose();
        }






    }
}
