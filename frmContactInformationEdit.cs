using FieldTool.BLL;
using FieldTool.Constants;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmContactInformationEdit : Form
    {
        #region Private member variables

        private Email _email;
        private Phone _phone;
        private Enumerations.ContactMediumType _type;

        #endregion Private member variables

        #region Constructors

        public frmContactInformationEdit(IContactMedium item)
        {
            this.InitializeComponent();

            if (item != null)
            {
                this.Type = item.Type;

                if (!this.DesignMode)
                {
                    if (item.Type == Enumerations.ContactMediumType.Email)
                    {
                        this.InternalEmail = item as Email;
                        this.InternalPhone = null;
                    }
                    else
                    {
                        this.InternalEmail = null;
                        this.InternalPhone = item as Phone;
                    }
                }
            }
        }

        #endregion Constructors

        #region Properties

        [Browsable(false)]
        private Email InternalEmail
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
                if (!this.DesignMode && this._email != null)
                {
                    this.txtEmail.Text = this._email.EmailAddress;
                    this.txtContactMedium.Text = this._email.ContactMedium;
                }
            }
        }

        [Browsable(false)]
        private Phone InternalPhone
        {
            get
            {
                return this._phone;
            }
            set
            {
                this._phone = value;
                if (!this.DesignMode && this._phone != null)
                {
                    this.txtPhoneNumber.Text = this._phone.PhoneNumber;
                    this.txtPhoneExtension.Text = this._phone.PhoneExtension;
                    this.txtContactMedium.Text = this._phone.ContactMedium;
                }
            }
        }

        [Browsable(false)]
        public string EmailAddress
        {
            get
            {
                string result = String.Empty;

                if (this.InternalEmail != null)
                {
                    result = this.InternalEmail.EmailAddress;
                }

                return result;
            }
            set
            {
                if (this.Type == Enumerations.ContactMediumType.Phone)
                {
                    throw new ArgumentException("This control is set to handle a phone number but you are trying to use it for an email.");
                }

                if (!this.DesignMode)
                {
                    this.InternalEmail.EmailAddress = value;
                }
            }
        }

        [Browsable(false)]
        public string PhoneAll
        {
            get
            {
                string result = String.Empty;

                if (this.InternalPhone != null)
                {
                    result = this.InternalPhone.ToString("A");
                }

                return result;
            }
        }

        [Browsable(false)]
        public string PhoneExtension
        {
            get
            {
                string result = String.Empty;

                if (this.InternalPhone != null)
                {
                    result = this.InternalPhone.PhoneExtension;
                }

                return result;
            }
            set
            {
                if (this.Type == Enumerations.ContactMediumType.Email)
                {
                    throw new ArgumentException("This control is set to handle email but you are trying to use it for a phone number.");
                }

                if (!this.DesignMode)
                {
                    this.InternalPhone.PhoneExtension = value;
                }
            }
        }

        [Browsable(false)]
        public string PhoneNumber
        {
            get
            {
                string result = String.Empty;

                if (this.InternalPhone != null)
                {
                    result = this.InternalPhone.PhoneNumber;
                }

                return result;
            }
            set
            {
                if (this.Type == Enumerations.ContactMediumType.Email)
                {
                    throw new ArgumentException("This control is set to handle email but you are trying to use it for a phone number.");
                }

                if (!this.DesignMode)
                {
                    this.InternalPhone.PhoneNumber = value;
                }
            }
        }

        public Enumerations.ContactMediumType Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;

                this.ClearTextControls();
                this.SetControlVisibility();
            }
        }

        #endregion Properties

        #region Internal events

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (this.Type == Enumerations.ContactMediumType.Email)
                {
                    this.InternalEmail.EmailAddress = this.txtEmail.Text;
                    this.InternalEmail.ContactMedium = this.txtContactMedium.Text;
                    this.InternalPhone = null;
                }
                else
                {
                    this.InternalEmail = null;
                    this.InternalPhone.PhoneNumber = this.txtPhoneNumber.Text;
                    this.InternalPhone.PhoneExtension = this.txtPhoneExtension.Text;
                    this.InternalPhone.ContactMedium = this.txtContactMedium.Text;
                }
            }
        }

        private bool ValidateData()
        {
            bool result = false;
            string msg = "";

            if (this.Type == Enumerations.ContactMediumType.Email)
            {
                if (String.IsNullOrWhiteSpace(this.txtEmail.Text))
                {
                    msg = "Please enter a valid email address.";
                    this.txtEmail.Focus();
                }
                else if (String.IsNullOrWhiteSpace(this.txtContactMedium.Text))
                {
                    msg = "Please enter a contact medium type.";
                    this.txtContactMedium.Focus();
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.txtPhoneNumber.Text))
                {
                    msg = "Please enter a phone number.";
                    this.txtPhoneNumber.Focus();
                }
                else if (String.IsNullOrWhiteSpace(this.txtContactMedium.Text))
                {
                    msg = "Please enter a contact medium type.";
                    this.txtContactMedium.Focus();
                }
            }

            if (msg == "")
            {
                result = true;
                this._okToClose = true;
            }
            else
            {
                result = false;
                this._okToClose = false;
                MessageBox.Show(msg, "Invalid Contact Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        private void frmContactInformationEdit_Load(object sender, EventArgs e)
        {
            ;
        }

        #endregion Internal events

        #region Private helper methods

        private void ClearTextControls()
        {
            this.txtEmail.Clear();
            this.txtPhoneNumber.Clear();
            this.txtPhoneExtension.Clear();
            this.txtContactMedium.Clear();
        }

        private void SetControlVisibility()
        {
            if (this._type == Enumerations.ContactMediumType.Email)
            {
                this.txtEmail.Visible = true;
                this.txtPhoneNumber.Visible = false;
                this.lblExtension.Visible = false;
                this.txtPhoneExtension.Visible = false;
            }
            else
            {
                this.txtEmail.Visible = false;
                this.txtPhoneNumber.Visible = true;
                this.lblExtension.Visible = true;
                this.txtPhoneExtension.Visible = true;
            }
        }

        #endregion Private helper methods

        private bool _okToClose = false;

        private void frmContactInformationEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this._okToClose)
            {
                e.Cancel = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _okToClose = true;
        }
    }
}