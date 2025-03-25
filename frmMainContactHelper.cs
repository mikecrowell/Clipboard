using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using FieldTool.BLL;
using FieldTool.Constants;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public static class frmMainContactHelper
    {
        #region Private member variables

        private static Contact _editContact = null;
        private static Contact _selectedContact;
        private static Enumerations.PanelDisplayMode _mode;
        private static string _companyId;
        private static Company _company = null;

        // Hidden fields
        private static TextBoxX _id;

        private static TextBoxX _externalId;
        private static TextBoxX _middleName;

        private static MetroTileItem _contactAdd;
        private static MetroTileItem _contactEdit;
        private static MetroTileItem _contactDelete;
        private static LabelX _addIcon;
        private static LabelX _editIcon;
        private static LabelX _nameLabel;
        private static LabelX _fullNameValue;
        private static TextBoxX _firstName;
        private static TextBoxX _lastName;
        private static LabelX _jobRoleLabel;
        private static LabelX _jobRoleValue;
        private static ComboBoxEx _jobRole;
        private static ListViewEx _phoneList;
        private static LabelX _phoneValue;
        private static MetroTilePanel _phoneButtons;
        private static MetroTileItem _phoneAddButton;
        private static MetroTileItem _phoneEditButton;
        private static MetroTileItem _phoneDeleteButton;
        private static ListViewEx _emailList;
        private static LabelX _emailValue;
        private static MetroTilePanel _emailButtons;
        private static MetroTileItem _emailAddButton;
        private static MetroTileItem _emailEditButton;
        private static MetroTileItem _emailDeleteButton;
        private static LabelX _noteLabel;
        private static LabelX _noteValue;
        private static TextBoxX _note;
        private static MetroTilePanel _saveButtons;
        private static MetroTileItem _cancelButton;
        private static MetroTileItem _saveButton;
        private static MetroTabItem _homePanel;
        private static ListViewEx _contactList;
        private static MetroTilePanel _contactButtons;

        #endregion Private member variables

        #region Properties

        internal static Contact.ContactCollection Contacts
        {
            get
            {
                Contact.ContactCollection result = new Contact.ContactCollection();

                foreach (ListViewItem li in _contactList.Items)
                {
                    object o = li.Tag;
                    if (o != null)
                    {
                        Contact c = o as Contact;
                        if (c != null)
                        {
                            result.Add(c);
                        }
                    }
                }

                return result;
            }
        }

        public static bool LoadingContactDetails { get; set; }
        public static bool IsContactLockingForm { get; set; }

        public static bool IsRollYourOwn
        {
            get
            {
                return _mode == Enumerations.PanelDisplayMode.RollYourOwnAudit;
            }
        }

        private static Contact SelectedContact
        {
            get
            {
                //_selectedContact = GetSelectedContactFromList();
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
            }
        }

        #endregion Properties

        #region Exposed class methods (static)

        internal static void AddEmail()
        {
            Email e = new Email();

            using (frmContactInformationEdit frm = new frmContactInformationEdit(e))
            {
                frm.SuspendLayout();
                // Set to popup right over the selected item.
                frm.Location = _emailList.PointToScreen(Point.Empty);
                //frm.Size = new Size(471, 37);
                frm.ResumeLayout();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _editContact.AddEmail(e);
                    LoadEmails(_editContact.Emails);

                    SelectPhone(e.Id);
                }
            }
        }

        internal static void AddPhone()
        {
            Phone p = new Phone();

            using (frmContactInformationEdit frm = new frmContactInformationEdit(p))
            {
                frm.SuspendLayout();
                // Set to popup right over the selected item.
                frm.Location = _phoneList.PointToScreen(Point.Empty);
                //frm.Size = new Size(471, 37);
                frm.ResumeLayout();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    _editContact.AddPhone(p);
                    LoadPhones(_editContact.Phones);

                    SelectPhone(p.Id);
                }
            }
        }

        internal static void Delete()
        {
            if (ValidateControls())
            {
                Contact selectedContact = frmMainContactHelper.GetSelectedContactFromList();

                if (selectedContact != null)
                {
                    string msg = String.Format("Are you sure you wish to permanently remove the selected contact '{0}'?\n\nIf you select Yes, this contact as well as their phone numbers, emails and notes will be removed from this company and you will never, ever, EVER be able to recover it.\n\nGONE!\n\nYa know in all those horror movies when they think they finally killed the bad guy but he keeps coming back?  Yeah, not gonna happen like that.\n\nSo are you sure you want to delete this contact?",
                        selectedContact.ToString("LF"));
                    DialogResult dlg = MessageBox.Show(msg, "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (dlg == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (Contact c in Contacts)
                        {
                            if (c.Id == selectedContact.Id)
                            {
                                DataStore.DeleteContact(c, true);
                                Reset(false);
                                RefreshContactList();
                                SetContactListButtons();

                                break;
                            }
                        }
                    }
                }
            }
        }

        internal static void DeleteSelectedEmail()
        {
            Email e = GetSelectedEmailFromList();

            if (e != null)
            {
                string msg = String.Format("This will remove the email '{0}' from this contact.\n\nThis will not be permanent until you save this contact.\n\nDo you wish to remove this email? ", e.ToString("LD"));
                DialogResult dlg = MessageBox.Show(msg, "Delete Email", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dlg == DialogResult.Yes)
                {
                    DeleteEmail(e);
                    LoadEmails(_editContact.Emails);
                }
            }
        }

        internal static void DeleteSelectedPhone()
        {
            Phone p = GetSelectedPhoneFromList();

            if (p != null)
            {
                string msg = String.Format("This will remove the phone '{0}' from this contact.\n\nThis will not be permanent until you save this contact.\n\nDo you wish to remove this phone? ", p.ToString("LD"));
                DialogResult dlg = MessageBox.Show(msg, "Delete Phone", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (dlg == DialogResult.Yes)
                {
                    DeletePhone(p);
                    LoadPhones(_editContact.Phones);
                }
            }
        }

        internal static void EditSelectedEmail()
        {
            Email e = GetSelectedEmailFromList();

            if (e != null)
            {
                Email copy = new Email(e);

                using (frmContactInformationEdit frm = new frmContactInformationEdit(copy))
                {
                    frm.SuspendLayout();
                    // Set to popup right over the selected item.
                    frm.Location = _emailList.PointToScreen(Point.Empty);
                    //frm.Size = new Size(471, 37);
                    frm.ResumeLayout();

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        UpdateEmail(e.Id, copy);
                        LoadEmails(_editContact.Emails);

                        SelectEmail(e.Id);
                    }
                }
            }
        }

        internal static void EditSelectedPhone()
        {
            Phone p = GetSelectedPhoneFromList();

            if (p != null)
            {
                Phone copy = new Phone(p);

                using (frmContactInformationEdit frm = new frmContactInformationEdit(copy))
                {
                    frm.SuspendLayout();
                    // Set to popup right over the selected item.
                    frm.Location = _phoneList.PointToScreen(Point.Empty);
                    //frm.Size = new Size(471, 37);
                    frm.ResumeLayout();

                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        UpdatePhone(p.Id, copy);
                        LoadPhones(_editContact.Phones);

                        SelectPhone(p.Id);
                    }
                }
            }
        }

        internal static void Initialize()
        {
            frmMainContactHelper.LoadJobRoles();
        }

        internal static void Load(Company company)
        {
            _mode = Enumerations.PanelDisplayMode.RollYourOwnAudit;

            if (company != null)
            {
                _company = company;

                if (_contactList.Items != null)
                {
                    _contactList.Items.Clear();
                }

                _editContact = new Contact();
                SyncData(Enumerations.SyncDirection.EditObjectToControls);
                SetControlDisplayMode();
                SetContactListButtons();
            }
        }

        internal static void Load(string companyId, Contact.ContactCollection contacts)
        {
            _companyId = companyId;

            if (_contactList.Items != null)
            {
                _contactList.Items.Clear();
            }

            if (contacts != null)
            {
                foreach (Contact contact in contacts)
                {
                    if (contact.IsValid)
                    {
                        ListViewItem li = new ListViewItem(contact.ToString("LF"));
                        li.Tag = contact;
                        _contactList.Items.Add(li);
                    }
                }
            }

            frmMainContactHelper.LoadDetails(Enumerations.PanelDisplayMode.ReadOnly);
        }

        internal static void LoadDetails(Enumerations.PanelDisplayMode mode)
        {
            _mode = mode;
            Contact contact = GetSelectedContactFromList();

            if (contact == null || mode == Enumerations.PanelDisplayMode.Add)
            {
                _editContact = new Contact();
            }
            else
            {
                _editContact = new Contact(contact);
            }

            SyncData(Enumerations.SyncDirection.EditObjectToControls);
            SetControlDisplayMode();
            SetContactListButtons();
        }

        internal static void Cancel()
        {
            Reset(true);
        }

        internal static bool Save()
        {
            bool result = false;

            if (ValidateData())
            {
                SyncData(Enumerations.SyncDirection.ControlsToSelectedObject);

                if (_mode == Enumerations.PanelDisplayMode.Add)
                {
                    DataStore.AddContact(_companyId, _editContact, true);
                    SelectedContact = new Contact();
                    SelectedContact.Update(_editContact);

                    Reset(false);
                    RefreshContactList();
                    SelectContactInList(SelectedContact);
                    SetContactListButtons();
                }
                else if (_mode == Enumerations.PanelDisplayMode.RollYourOwnAudit)
                {
                    _company.AddContact(_editContact);
                    if (_company.HasAudits)
                    {
                        if (_company.HasContacts)
                        {
                            _company.Audits[0].CompanyContact = _company.Contacts[0].ToString("FL");
                        }
                        _company.Audits[0].EnergyAdvisorName = UserSettings.GetUserSettings().FullName;
                    }
                    DataStore.AddCompany(_company, true);
                }
                else
                {
                    DataStore.UpdateContact(_companyId, _editContact, true);
                    SelectedContact = new Contact();
                    SelectedContact.Update(_editContact);

                    Reset(false);
                    RefreshContactList();
                    SelectContactInList(SelectedContact);
                    SetContactListButtons();
                }

                result = true;
            }

            return result;
        }

        internal static void SelectEmail()
        {
            Email e = GetSelectedEmailFromList();

            if (e == null)
            {
                _emailEditButton.Enabled = false;
                _emailDeleteButton.Enabled = false;
            }
            else
            {
                _emailEditButton.Enabled = true;
                _emailDeleteButton.Enabled = true;
            }
        }

        internal static void SelectPhone()
        {
            Phone p = GetSelectedPhoneFromList();

            if (p == null)
            {
                _phoneEditButton.Enabled = false;
                _phoneDeleteButton.Enabled = false;
            }
            else
            {
                _phoneEditButton.Enabled = true;
                _phoneDeleteButton.Enabled = true;
            }
        }

        #region Control methods

        internal static void RegisterControls(TextBoxX id, TextBoxX externalId, MetroTileItem contactAdd, MetroTileItem contactEdit, MetroTileItem contactDelete,
            LabelX addIcon, LabelX editIcon,
            LabelX nameLabel, LabelX fullNameValue, TextBoxX firstName, TextBoxX middleName, TextBoxX lastName, LabelX jobRoleLabel, LabelX jobRoleValue, ComboBoxEx jobRole,
            ListViewEx phoneList, LabelX phoneValue, MetroTilePanel phoneButtons, MetroTileItem phoneAddButton, MetroTileItem phoneEditButton, MetroTileItem phoneDeleteButton,
            ListViewEx emailList, LabelX emailValue, MetroTilePanel emailButtons, MetroTileItem emailAddButton, MetroTileItem emailEditButton, MetroTileItem emailDeleteButton,
            LabelX noteLabel, LabelX noteValue, TextBoxX note, MetroTilePanel saveButtons, MetroTileItem cancelButton, MetroTileItem saveButton, MetroTabItem homePanel, ListViewEx contactList, MetroTilePanel contactButtons)
        {
            _id = id;
            _externalId = externalId;

            _contactAdd = contactAdd;
            _contactEdit = contactEdit;
            _contactDelete = contactDelete;
            _addIcon = addIcon;
            _editIcon = editIcon;
            _nameLabel = nameLabel;
            _fullNameValue = fullNameValue;
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _jobRoleLabel = jobRoleLabel;
            _jobRoleValue = jobRoleValue;
            _jobRole = jobRole;
            _phoneList = phoneList;
            _phoneValue = phoneValue;
            _phoneButtons = phoneButtons;
            _phoneAddButton = phoneAddButton;
            _phoneEditButton = phoneEditButton;
            _phoneDeleteButton = phoneDeleteButton;
            _emailList = emailList;
            _emailValue = emailValue;
            _emailButtons = emailButtons;
            _emailAddButton = emailAddButton;
            _emailEditButton = emailEditButton;
            _emailDeleteButton = emailDeleteButton;
            _noteLabel = noteLabel;
            _noteValue = noteValue;
            _note = note;
            _saveButtons = saveButtons;
            _cancelButton = cancelButton;
            _saveButton = saveButton;
            _homePanel = homePanel;
            _contactList = contactList;
            _contactButtons = contactButtons;
        }

        internal static bool ValidateControls()
        {
            return (
                _id != null &&
                _externalId != null &&
                _contactAdd != null &&
                _contactEdit != null &&
                _contactDelete != null &&
                _addIcon != null &&
                _editIcon != null &&
                _nameLabel != null &&
                _fullNameValue != null &&
                _firstName != null &&
                _middleName != null &&
                _lastName != null &&
                _jobRoleLabel != null &&
                _jobRoleValue != null &&
                _jobRole != null &&
                _phoneList != null &&
                _phoneValue != null &&
                _phoneButtons != null &&
                _phoneAddButton != null &&
                _phoneEditButton != null &&
                _phoneDeleteButton != null &&
                _emailList != null &&
                _emailValue != null &&
                _emailButtons != null &&
                _emailAddButton != null &&
                _emailEditButton != null &&
                _emailDeleteButton != null &&
                _noteLabel != null &&
                _noteValue != null &&
                _note != null &&
                _saveButtons != null &&
                _cancelButton != null &&
                _saveButton != null &&
                _homePanel != null &&
                _contactList != null &&
                _contactButtons != null);
        }

        #endregion Control methods

        #endregion Exposed class methods (static)

        #region Private methods

        private static bool ContactExists()
        {
            bool result = false;

            foreach (Contact c in Contacts)
            {
                if (c.FirstName.ToUpper() == _firstName.Text.ToUpper().Trim() &&
                    c.LastName.ToUpper() == _lastName.Text.ToUpper().Trim())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private static void DeleteEmail(Email email)
        {
            if (_editContact != null && email != null)
            {
                foreach (Email e in _editContact.Emails)
                {
                    if (e.Id == email.Id)
                    {
                        _editContact.RemoveEmail(e);
                        break;
                    }
                }
            }
        }

        private static void DeletePhone(Phone phone)
        {
            if (_editContact != null && phone != null)
            {
                foreach (Phone p in _editContact.Phones)
                {
                    if (p.Id == phone.Id)
                    {
                        _editContact.RemovePhone(p);
                        break;
                    }
                }
            }
        }

        private static Contact GetSelectedContactFromList()
        {
            Contact result = null;

            if (_contactList != null)
            {
                foreach (ListViewItem li in _contactList.SelectedItems)
                {
                    object o = li.Tag;

                    if (o != null)
                    {
                        Contact c = o as Contact;

                        if (c != null)
                        {
                            result = c;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private static Email GetSelectedEmailFromList()
        {
            Email result = null;

            if (_emailList != null)
            {
                foreach (ListViewItem li in _emailList.SelectedItems)
                {
                    object o = li.Tag;

                    if (o != null)
                    {
                        Email e = o as Email;

                        if (e != null)
                        {
                            result = e;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private static Phone GetSelectedPhoneFromList()
        {
            Phone result = null;

            if (_phoneList != null)
            {
                foreach (ListViewItem li in _phoneList.SelectedItems)
                {
                    object o = li.Tag;

                    if (o != null)
                    {
                        Phone p = o as Phone;

                        if (p != null)
                        {
                            result = p;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private static void LoadEmails(List<IContactMedium> emails)
        {
            string emailString = "Emails:\n\n";

            if (_emailList != null)
            {
                _emailList.Items.Clear();

                if (emails != null)
                {
                    foreach (Email email in emails)
                    {
                        ListViewItem li = new ListViewItem(email.EmailAddress);
                        li.SubItems.Add(email.ContactMedium);
                        li.Tag = email;
                        _emailList.Items.Add(li);

                        emailString += email.ToString("LD") + "\n";
                    }
                }
            }

            _emailValue.Text = emailString;
        }

        private static void LoadJobRoles()
        {
            if (_jobRole != null)
            {
                _jobRole.Items.Add("Manager");
                _jobRole.Items.Add("Owner");
                _jobRole.Items.Add("President");
                _jobRole.Items.Add("HR");
            }
        }

        private static void LoadPhones(List<IContactMedium> phones)
        {
            string phoneString = "Phones:\n\n";

            if (_phoneList != null)
            {
                _phoneList.Items.Clear();

                if (phones != null)
                {
                    foreach (Phone phone in phones)
                    {
                        ListViewItem li = new ListViewItem(phone.ToString("F"));
                        li.SubItems.Add(phone.ContactMedium);
                        li.Tag = phone;
                        _phoneList.Items.Add(li);

                        phoneString += phone.ToString("LD") + "\n";
                    }
                }
            }

            _phoneValue.Text = phoneString;
        }

        private static void LockFormForContactEdit()
        {
            _homePanel.Enabled = false;
            _contactList.Enabled = false;
            _contactButtons.Enabled = false;

            frmMainContactHelper.IsContactLockingForm = true;
        }

        private static void RefreshContactList()
        {
            if (!String.IsNullOrWhiteSpace(_companyId))
            {
                List<Contact> contacts = DataStore.GetContacts(_companyId);

                _contactList.Items.Clear();
                foreach (Contact contact in contacts)
                {
                    ListViewItem li = new ListViewItem(contact.ToString("LF"));
                    li.Tag = contact;
                    _contactList.Items.Add(li);
                }
            }
        }

        private static void Reset(bool canceling)
        {
            if (!canceling)
            {
                _editContact = null;
            }

            SyncData(Enumerations.SyncDirection.SelectedObjectToControls);
            _mode = Enumerations.PanelDisplayMode.ReadOnly;
            SetControlDisplayMode();
        }

        private static void ResumeControls()
        {
            if (ValidateControls())
            {
                //_id.ResumeDrawing();
                //_externalId.ResumeDrawing();
                _addIcon.ResumeDrawing();
                _editIcon.ResumeDrawing();
                _nameLabel.ResumeDrawing();
                _firstName.ResumeDrawing();
                //_middleName.ResumeDrawing();
                _lastName.ResumeDrawing();
                _fullNameValue.ResumeDrawing();
                _jobRoleLabel.ResumeDrawing();
                _jobRole.ResumeDrawing();
                _jobRoleValue.ResumeDrawing();
                _phoneValue.ResumeDrawing();
                _phoneButtons.ResumeDrawing();
                _emailValue.ResumeDrawing();
                _emailButtons.ResumeDrawing();
                _noteLabel.ResumeDrawing();
                _note.ResumeDrawing();
                _noteValue.ResumeDrawing();
                _saveButtons.ResumeDrawing();
            }
        }

        private static void SelectContactInList(Contact contact)
        {
            if (contact != null)
            {
                foreach (ListViewItem li in _contactList.Items)
                {
                    object o = li.Tag;
                    if (o != null)
                    {
                        Contact c = o as Contact;
                        if (c != null)
                        {
                            if (c.Id == contact.Id)
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void SelectPhone(string id)
        {
            foreach (ListViewItem li in _phoneList.Items)
            {
                object o = li.Tag;

                if (o != null)
                {
                    Phone p = o as Phone;

                    if (p != null)
                    {
                        if (p.Id == id)
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private static void SelectEmail(string id)
        {
            if (_emailList != null)
            {
                foreach (ListViewItem li in _emailList.Items)
                {
                    object o = li.Tag;

                    if (o != null)
                    {
                        Email e = o as Email;

                        if (e != null)
                        {
                            if (e.Id == id)
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void SetContactListButtons()
        {
            if (_contactButtons.Enabled)
            {
                if (_contactList.SelectedItems.Count > 0)
                {
                    _contactAdd.Enabled = true;
                    _contactEdit.Enabled = true;
                    _contactDelete.Enabled = true;
                }
                else
                {
                    _contactAdd.Enabled = true;
                    _contactEdit.Enabled = false;
                    _contactDelete.Enabled = false;
                }
            }
        }

        private static void SetControlDisplayMode()
        {
            //SuspendControls();

            switch (_mode)
            {
                case Enumerations.PanelDisplayMode.Add:
                case Enumerations.PanelDisplayMode.RollYourOwnAudit:
                    LockFormForContactEdit();

                    _addIcon.Visible = true;
                    _editIcon.Visible = false;

                    _nameLabel.Visible = true;
                    _firstName.Visible = true;
                    _lastName.Visible = true;
                    _fullNameValue.Visible = false;

                    _jobRoleLabel.Visible = true;
                    _jobRole.Visible = true;
                    _jobRoleValue.Visible = false;

                    _phoneValue.Visible = false;
                    _phoneList.Visible = true;

                    _phoneButtons.Visible = true;
                    _phoneAddButton.Enabled = true;
                    _phoneEditButton.Enabled = false;
                    _phoneDeleteButton.Enabled = false;

                    _emailValue.Visible = false;
                    _emailList.Visible = true;

                    _emailButtons.Visible = true;
                    _emailAddButton.Enabled = true;
                    _emailEditButton.Enabled = false;
                    _emailDeleteButton.Enabled = false;

                    _noteLabel.Visible = true;
                    _note.Visible = true;
                    _noteValue.Visible = false;

                    _saveButtons.Visible = true;
                    _cancelButton.Enabled = true;
                    _saveButton.Enabled = true;

                    _firstName.Focus();

                    break;

                case Enumerations.PanelDisplayMode.Edit:
                    LockFormForContactEdit();

                    _addIcon.Visible = false;
                    _editIcon.Visible = true;

                    _nameLabel.Visible = true;
                    _firstName.Visible = true;
                    _lastName.Visible = true;
                    _fullNameValue.Visible = false;

                    _jobRoleLabel.Visible = true;
                    _jobRole.Visible = true;
                    _jobRoleValue.Visible = false;

                    _phoneValue.Visible = false;
                    _phoneList.Visible = true;

                    _phoneButtons.Visible = true;
                    _phoneAddButton.Enabled = true;
                    _phoneEditButton.Enabled = false;
                    _phoneDeleteButton.Enabled = false;

                    _emailValue.Visible = false;
                    _emailList.Visible = true;

                    _emailButtons.Visible = true;
                    _emailAddButton.Enabled = true;
                    _emailEditButton.Enabled = false;
                    _emailDeleteButton.Enabled = false;

                    _noteLabel.Visible = true;
                    _note.Visible = true;
                    _noteValue.Visible = false;

                    _saveButtons.Visible = true;
                    _cancelButton.Enabled = true;
                    _saveButton.Enabled = true;

                    _firstName.Focus();

                    break;

                case Enumerations.PanelDisplayMode.ReadOnly:
                    UnlockFormForContactEdit();

                    bool contactValid = (_editContact != null && _editContact.IsValid);

                    _addIcon.Visible = false;
                    _editIcon.Visible = false;

                    _firstName.Visible = false;
                    _lastName.Visible = false;
                    _fullNameValue.Visible = contactValid;
                    _nameLabel.Visible = contactValid && !String.IsNullOrWhiteSpace(_fullNameValue.Text);

                    _jobRole.Visible = false;
                    _jobRoleValue.Visible = contactValid;
                    _jobRoleLabel.Visible = contactValid && !String.IsNullOrWhiteSpace(_jobRoleValue.Text);

                    _phoneValue.Visible = contactValid;
                    _phoneList.Visible = false;

                    _phoneButtons.Visible = false;
                    _phoneAddButton.Enabled = false;
                    _phoneEditButton.Enabled = false;
                    _phoneDeleteButton.Enabled = false;

                    _emailValue.Visible = contactValid;
                    _emailList.Visible = false;

                    _emailButtons.Visible = false;
                    _emailAddButton.Enabled = false;
                    _emailEditButton.Enabled = false;
                    _emailDeleteButton.Enabled = false;

                    _note.Visible = false;
                    _noteValue.Visible = contactValid;
                    _noteLabel.Visible = contactValid && !String.IsNullOrWhiteSpace(_noteValue.Text);

                    _saveButtons.Visible = false;
                    _cancelButton.Enabled = false;
                    _saveButton.Enabled = false;

                    break;
            }

            //ResumeControls();
        }

        private static void SuspendControls()
        {
            if (ValidateControls())
            {
                //_id.SuspendDrawing();
                //_externalId.SuspendDrawing();
                _addIcon.SuspendDrawing();
                _editIcon.SuspendDrawing();
                _nameLabel.SuspendDrawing();
                _firstName.SuspendDrawing();
                //_middleName.SuspendDrawing();
                _lastName.SuspendDrawing();
                _fullNameValue.SuspendDrawing();
                _jobRoleLabel.SuspendDrawing();
                _jobRole.SuspendDrawing();
                _jobRoleValue.SuspendDrawing();
                _phoneValue.SuspendDrawing();
                _phoneButtons.SuspendDrawing();
                _emailValue.SuspendDrawing();
                _emailButtons.SuspendDrawing();
                _noteLabel.SuspendDrawing();
                _note.SuspendDrawing();
                _noteValue.SuspendDrawing();
                _saveButtons.SuspendDrawing();
            }
        }

        private static void SyncData(Enumerations.SyncDirection direction)
        {
            LoadingContactDetails = true;

            switch (direction)
            {
                case Enumerations.SyncDirection.ControlsToEditObject:
                case Enumerations.SyncDirection.ControlsToSelectedObject:

                    // Controls to object

                    _editContact = new Contact();
                    _editContact.Id = _id.Text;

                    _editContact.ExternalId = _externalId.Text;
                    _editContact.FirstName = _firstName.Text;
                    _editContact.MiddleName = _middleName.Text;
                    _editContact.LastName = _lastName.Text;
                    _editContact.JobRole = _jobRole.Text;

                    foreach (ListViewItem li in _phoneList.Items)
                    {
                        object o = li.Tag;
                        if (o != null)
                        {
                            Phone p = o as Phone;
                            if (p != null)
                            {
                                _editContact.AddPhone(p);
                            }
                        }
                    }

                    foreach (ListViewItem li in _emailList.Items)
                    {
                        object o = li.Tag;
                        if (o != null)
                        {
                            Email e = o as Email;
                            if (e != null)
                            {
                                _editContact.AddEmail(e);
                            }
                        }
                    }

                    _editContact.Note = _note.Text;

                    break;

                case Enumerations.SyncDirection.EditObjectToControls:
                case Enumerations.SyncDirection.SelectedObjectToControls:

                    // Object to controls

                    if (_editContact == null)
                    {
                        _id.Text = "";
                        _externalId.Text = "";

                        _firstName.Text = "";
                        _middleName.Text = "";
                        _lastName.Text = "";
                        _fullNameValue.Text = "";

                        _jobRole.SelectedIndex = -1;
                        _jobRoleValue.Text = "";

                        _phoneValue.Text = "";
                        _phoneList.Items.Clear();
                        _emailValue.Text = "";
                        _emailList.Items.Clear();

                        _note.Text = "";
                        _noteValue.Text = "";
                    }
                    else
                    {
                        _id.Text = _editContact.Id;
                        _externalId.Text = _editContact.ExternalId;

                        _firstName.Text = _editContact.FirstName;
                        _middleName.Text = _editContact.MiddleName;
                        _lastName.Text = _editContact.LastName;
                        _fullNameValue.Text = _editContact.ToString("LF");

                        // Set default values for Job Role
                        _jobRole.Text = string.Empty;
                        _jobRoleValue.Text = string.Empty;

                        // Only populate Job Role combobox and label text if the value matches one of the items in the dropdown
                        foreach (var item in _jobRole.Items)
                        {
                            if (item.ToString() == _editContact.JobRole)
                            {
                                _jobRole.Text = _editContact.JobRole;
                                _jobRoleValue.Text = _editContact.JobRole;
                                break;
                            }
                        }

                        LoadPhones(_editContact.Phones);
                        LoadEmails(_editContact.Emails);

                        _note.Text = _editContact.Note;
                        _noteValue.Text = _editContact.Note;
                    }

                    break;
            }

            LoadingContactDetails = false;
        }

        private static void UnlockFormForContactEdit()
        {
            if (_homePanel != null && _contactList != null && _contactButtons != null)
            {
                _homePanel.Enabled = true;
                _contactList.Enabled = true;
                _contactButtons.Enabled = true;
            }

            frmMainContactHelper.IsContactLockingForm = false;
        }

        private static void UpdateEmail(string id, Email newEmail)
        {
            if (_editContact != null && !String.IsNullOrWhiteSpace(id) && newEmail != null)
            {
                foreach (Email e in _editContact.Emails)
                {
                    if (e.Id == id)
                    {
                        e.EmailAddress = newEmail.EmailAddress;
                        e.ContactMedium = newEmail.ContactMedium;

                        break;
                    }
                }
            }
        }

        private static void UpdatePhone(string id, Phone newPhone)
        {
            if (_editContact != null && !String.IsNullOrWhiteSpace(id) && newPhone != null)
            {
                foreach (Phone p in _editContact.Phones)
                {
                    if (p.Id == id)
                    {
                        p.PhoneNumber = newPhone.PhoneNumber;
                        p.PhoneExtension = newPhone.PhoneExtension;
                        p.ContactMedium = newPhone.ContactMedium;

                        break;
                    }
                }
            }
        }

        private static bool ValidateData()
        {
            bool result = false;
            string msg = "";

            if (String.IsNullOrWhiteSpace(_firstName.Text))
            {
                msg = "Please enter a valid first name.";
                _firstName.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_lastName.Text))
            {
                msg = "Please enter a valid last name.";
                _lastName.Focus();
            }
            else if (ContactExists() && _mode == Enumerations.PanelDisplayMode.Add)
            {
                msg = "There is already a contact with that first and last name.  Please enter a unique contact name.";
                _firstName.Focus();
            }

            if (msg == "")
            {
                result = true;
            }
            else
            {
                result = false;
                MessageBox.Show(msg, "Invalid Contact Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        #endregion Private methods

        internal static void SetSampleData()
        {
            _id.Text = BusinessObjectBase.GetNewId();
            _externalId.Text = "ZYXWVUTSRQPONMLKJIHGFEDCBA";
            _middleName.Text = "M.";

            _firstName.Text = "Scott";
            _lastName.Text = "Malik";
            _jobRole.Text = "Manager";

            Phone p = new Phone();
            //p.Id = BusinessObjectBase.GetNewId();
            p.PhoneNumber = "2626338021";
            p.PhoneExtension = "12";
            p.ContactMedium = "Home";
            //p.Type = Constants.Enumerations.ContactMediumType.Phone;
            ListViewItem li = new ListViewItem(p.PhoneNumber);
            li.SubItems.Add(p.ContactMedium);
            li.Tag = p;
            _phoneList.Items.Add(li);

            Email e = new Email();
            //e.Id = BusinessObjectBase.GetNewId();
            e.EmailAddress = "smmasm@yahoo.com";
            e.ContactMedium = "Home";
            //e.Type = Constants.Enumerations.ContactMediumType.Email;
            li = new ListViewItem(e.EmailAddress);
            li.SubItems.Add(e.ContactMedium);
            li.Tag = e;
            _emailList.Items.Add(li);

            _note.Text = "New client";
        }
    }
}