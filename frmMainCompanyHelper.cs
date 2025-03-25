using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.Editors.DateTimeAdv;
using FieldTool.BLL;
using FieldTool.Bsi.Models;
using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public class frmMainCompanyHelper
    {
        #region Private member variables

        private static Enumerations.PanelDisplayMode _currentMode;

        #region Registered controls

        private static LabelX _addIcon;
        private static LabelX _editIcon;
        private static Company _selectedObject;
        private static Company _editObject = null;

        private static TextBoxX _id;
        private static TextBoxX _externalId;

        private static LabelX _nameCaption;
        private static LabelX _nameValue;
        private static TextBoxX _name;
        private static LabelX _propertyManagementCaption;
        private static LabelX _propertyManagementValue;
        private static TextBoxX _propertyManagement;
        private static LabelX _addressCaption;
        private static LabelX _addressValue;
        private static TextBoxX _address;
        private static LabelX _address2Value;
        private static TextBoxX _address2;
        private static LabelX _cityValue;
        private static TextBoxX _city;
        private static LabelX _stateValue;
        private static ComboBoxEx _state;
        private static LabelX _zipValue;
        private static TextBoxX _zip;
        private static LabelX _zipExtensionValue;
        private static TextBoxX _zipExtension;

        private static MetroTilePanel _editButtons;
        private static MetroTileItem _edit;

        private static LabelX _utilityCaption;
        private static LabelX _utilityValue;
        private static ComboBoxEx _utility;
        private static LabelX _programCaption;
        private static LabelX _programValue;
        private static ComboBoxEx _program;
        private static LabelX _accountTypeCaption;
        private static LabelX _accountTypeValue;
        private static ComboBoxEx _accountType;

        private static LabelX _startDateTimeCaption;
        private static LabelX _startDateTimeValue;
        private static DateTimeInput _startDateTime;

        private static LabelX _electricAccountNumberCaption;
        private static LabelX _electricAccountNumberValue;
        private static TextBoxX _electricAccountNumber;
        private static LabelX _electricRateCodeCaption;
        private static LabelX _electricRateCodeValue;
        private static TextBoxX _electricRateCode;

        private static LabelX _gasAccountNumberCaption;
        private static LabelX _gasAccountNumberValue;
        private static TextBoxX _gasAccountNumber;
        private static LabelX _gasRateCodeCaption;
        private static LabelX _gasRateCodeValue;
        private static TextBoxX _gasRateCode;

        private static MetroTilePanel _saveButtons;
        private static MetroTileItem _cancel;
        private static MetroTileItem _save;

        private static MetroTabItem _homePanel;
        private static TextBoxX _recordType;
        private static LabelX _companyInfo;

        private static LabelX _companyEmailValue;
        private static TextBoxX _companyEmail;
        private static LabelX _companyPhoneValue;
        private static TextBoxX _companyPhone;

        #endregion Registered controls

        #endregion Private member variables

        #region Properties

        public static string AuditId
        {
            get
            {
                if (_editObject != null && _editObject.HasAudits)
                {
                    return _editObject.Audits[0].Id;
                }
                else
                {
                    return "";
                }
            }
        }

        public static string AuditName
        {
            get
            {
                if (_editObject != null && _editObject.HasAudits)
                {
                    return _editObject.Audits[0].Name;
                }
                else
                {
                    return "";
                }
            }
        }

        public static bool HasAudits
        {
            get
            {
                return _editObject.HasAudits;
            }
        }

        public static Company Company
        {
            get
            {
                return _editObject;
            }
        }

        /// <summary>
        /// Get a flag that indicates whether or not this panel is currently loading its data.
        /// </summary>
        internal static bool IsLoading { get; private set; }

        /// <summary>
        /// Get a flag that indicates whether or not the panel is currently in an edit state.
        /// </summary>
        internal static bool IsLockedForEdit { get; private set; }

        /// <summary>
        /// Gets a flag that indicates whether or not this panel is currently in a Roll Your Own state.
        /// </summary>
        internal static bool IsRollYourOwn
        {
            get
            {
                return _currentMode == Enumerations.PanelDisplayMode.RollYourOwnAudit;
            }
        }

        #endregion Properties

        #region Exposed class methods (static)

        internal static void AddAudit(Company company)
        {
            if (company != null)
            {
                Audit a = new Audit();

                a.ActualStartTimeStamp = "";
                a.AuditCompleteDate = null;
                a.AuditStatus = "";
                //a.Buildings = new Building.BuildingCollection();
                a.ClientAccountId = "";
                a.CompanyContact = "";
                a.Description = "";
                a.ElectricDisplayAs = "";
                a.EndTimeStamp = "";
                a.EnergyAdvisorName = "";
                a.GasDisplayAs = "";
                a.Id = GuidGenerator.Generate();
                a.ExternalId = a.Id;
                a.Name = company.Name + " - " + company.AddressLine1;
                a.ProgramId = DataStore.GetProgramCodeForProgramName(_program.Text);
                a.ClientAccountId = DataStore.GetClientNameForProgramName(_program.Text);
                //a.Report = new ReportParameters();
                //a.RetrofitEstimate = new Retrofit.RetrofitCollection();

                // always save to XML as UTC for consistency
                a.ScheduledStartTimeStamp = _startDateTime.Value.ToUniversalTime();

                ApiProgramMetadata api = DataStore.GetProgramsByProgramCode(a.ProgramId).FirstOrDefault();
                if (api == default(ApiProgramMetadata))
                {
                    throw new Exception(String.Format("Should have found a program for ({0})", a.ProgramId));
                }
                a.ProgramType = ProgramType.Parse(api.program.ProgramType);

                _editObject.AddAudit(a);
            }
        }

        /// <summary>
        /// Put the form into a state where it can be edited, but the user must commit the save separately.
        /// </summary>
        internal static void Edit()
        {
            if (Company != null)
            {
                Load(Company, Enumerations.PanelDisplayMode.Edit);
            }
        }

        private static string FatalExceptionLogHelper(string errorMessage, string path)
        {
            string result = "";
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Efficiency Clipboard has encountered an error and become unstable.  It is recommended that you do not use the application until the following error is addressed:");
            sb.AppendLine();
            sb.AppendLine(errorMessage);
            sb.AppendLine();
            sb.AppendLine("Path: " + path);
            sb.AppendLine();
            sb.AppendLine("This message has been copied to your clipboard so that you can easily include it in a bug report.");

            result = sb.ToString();

            Constants.Utilities.SetClipboardText(result);

            Lg.Error(result);

            return result;
        }

        /// <summary>
        /// Gets the panel ready for use by setting up all controls, filling lists, and setting any defaults.
        /// </summary>
        internal static string Initialize()
        {
            // Lg.MethodStart("frmMainCompanyHelper.Initialize()");

            string result = "";

            // Load dropdowns
            try
            {
                //Lg.Info("Getting States from XML file.");

                _state.Items.AddRange(DataStore.GetActiveStates().Select(x => x.Name).ToArray());

                if (_state == null || _state.Items == null || _state.Items.Count == 0)
                {
                    result = FatalExceptionLogHelper("The states in your metadata file are missing or the metadata file is missing, corrupted, or incomplete.", DataStore.XmlMetaDataFile);
                }
                else
                {
                    //Lg.Info("Success!");
                }
            }
            catch (Exception ex)
            {
                result = FatalExceptionLogHelper(ex.ToString(), DataStore.XmlMetaDataFile);
                _state = null;
            }

            try
            {
                //Lg.Info("Getting Programs from XML file.");

                _utility.Items.AddRange(DataStore.GetUtilities().ToArray());

                if (_utility == null || _utility.Items == null || _utility.Items.Count == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Your Programs file is missing, corrupted, or incomplete.  You can attempt to fix this by doing the following when connected to the Franklin Energy network:");
                    sb.AppendLine();
                    sb.AppendLine("1) Continue through any exceptions");
                    sb.AppendLine("2) From the Efficiency Clipboard main screen, press the Download Data button");
                    sb.AppendLine("3) Restart Efficiency Clipboard");
                    sb.AppendLine();
                    sb.AppendLine("If these steps fail to resolve your issue, please submit an Efficiency Clipboard bug report.");

                    result = FatalExceptionLogHelper(sb.ToString(), DataStore.XmlProgramsFile);
                }
                else
                {
                    // Lg.Info("Success!");
                }
            }
            catch (Exception ex)
            {
                result = FatalExceptionLogHelper(ex.ToString(), DataStore.XmlProgramsFile);
                _utility = null;
            }

            _program.WatermarkText = "Please select a Utility";

            //Lg.MethodEnd("frmMainCompanyHelper.Initialize()");

            return result.ToString();
        }

        /// <summary>
        /// Populates the panel with the specified object's  data.
        /// </summary>
        /// <param name="company">The object whose data will fill the panel.</param>
        /// <param name="mode">The display mode in which the panel will open</param>
        internal static void Load(Company company, Enumerations.PanelDisplayMode mode)
        {
            IsLoading = true;

            // Useful for when the Save or Cancel is clicked, we know what mode we are in so
            //  we can take the appropriate actions.
            _currentMode = mode;

            switch (mode)
            {
                case Enumerations.PanelDisplayMode.Add:
                case Enumerations.PanelDisplayMode.RollYourOwnAudit:
                    _selectedObject = company;
                    _editObject = null;
                    SetControlDisplayMode(mode);
                    SyncData(Enumerations.SyncDirection.EditObjectToControls);

                    _companyInfo.Text = "";

                    break;

                case Enumerations.PanelDisplayMode.Edit:
                case Enumerations.PanelDisplayMode.ReadOnly:
                case Enumerations.PanelDisplayMode.Unvalidated:

                    _selectedObject = company;

                    _editObject = new Company(company, true);

                    SetControlDisplayMode(mode);
                    SyncData(Enumerations.SyncDirection.EditObjectToControls);

                    _companyInfo.Text = company.ToString("I");

                    break;

                default:
                    _selectedObject = company;
                    _editObject = null;
                    SetControlDisplayMode(Enumerations.PanelDisplayMode.Blank);
                    SyncData(Enumerations.SyncDirection.EditObjectToControls);

                    _companyInfo.Text = "";

                    break;
            }

            IsLoading = false;
        }

        /// <summary>
        /// Undo any changes made since the object was last saved and return the panel to a read only state.
        /// </summary>
        internal static void Cancel()
        {
            SyncData(Enumerations.SyncDirection.SelectedObjectToControls);
            SetControlDisplayMode(Enumerations.PanelDisplayMode.ReadOnly);
        }

        /// <summary>
        /// Commit all panel changes to the underlying data and return the panel to a read only state.
        /// </summary>
        internal static bool Save()
        {
            bool result = false;

            if (ValidateData())
            {
                switch (_currentMode)
                {
                    case Enumerations.PanelDisplayMode.Add:
                        SyncData(Enumerations.SyncDirection.ControlsToEditObject);
                        DataStore.AddCompany(_editObject, true);
                        break;

                    case Enumerations.PanelDisplayMode.RollYourOwnAudit:
                        SyncData(Enumerations.SyncDirection.ControlsToEditObject);
                        //AddAudit();
                        break;

                    case Enumerations.PanelDisplayMode.Edit:
                        SyncData(Enumerations.SyncDirection.ControlsToEditObject);
                        DataStore.UpdateCompany(_editObject, true);
                        break;

                    case Enumerations.PanelDisplayMode.Unvalidated:
                        SyncData(Enumerations.SyncDirection.ControlsToEditObject);
                        DataStore.UpdateCompany(_editObject, true);
                        break;

                    default:
                        break;
                }

                SyncData(Enumerations.SyncDirection.EditObjectToControls);

                if (_currentMode == Enumerations.PanelDisplayMode.Unvalidated)
                {
                    SetControlDisplayMode(Enumerations.PanelDisplayMode.Unvalidated);
                }
                else
                {
                    SetControlDisplayMode(Enumerations.PanelDisplayMode.ReadOnly);
                }

                result = true;
            }

            return result;
        }

        internal static void SaveToXml(Company company)
        {
            if (company != null)
            {
                DataStore.AddCompany(company, true);
            }
        }

        internal static void UpdateProgramsFromUtility(bool selectFirstItem)
        {
            _program.Items.Clear();
            List<ApiProgramMetadata> programs = DataStore.GetPrograms(_utility.Text);

            if (programs != null && programs.Count > 0)
            {
                List<string> sortedPrograms = new List<string>();
                sortedPrograms = programs.Select(x => x.program.Name).Distinct().ToList();
                sortedPrograms.Sort();
                _program.Items.AddRange(sortedPrograms.ToArray());

                if (selectFirstItem)
                {
                    _program.SelectedIndex = 0;
                }
            }
        }

        #region Control methods

        /// <summary>
        /// Identify all controls used by this panel.
        /// </summary>
        internal static void RegisterControls(
            TextBoxX id, TextBoxX externalId, LabelX addIcon, LabelX editIcon,
            LabelX nameCaption, LabelX nameValue, TextBoxX name,
            LabelX propertyManagementCaption, LabelX propertyManagementValue, TextBoxX propertyManagement,
            LabelX addressCaption, LabelX addressValue, TextBoxX address,
            LabelX address2Value, TextBoxX address2,
            LabelX cityValue, TextBoxX city, LabelX stateValue, ComboBoxEx state,
            LabelX zipValue, TextBoxX zip, LabelX zipExtensionValue, TextBoxX zipExtension,
            MetroTilePanel editButtons, MetroTileItem edit,
            LabelX utilityCaption, LabelX utilityValue, ComboBoxEx utility,
            LabelX programCaption, LabelX programValue, ComboBoxEx program,
            LabelX accountTypeCaption, LabelX accountTypeValue, ComboBoxEx accountType,
            LabelX electricAccountNumberCaption, LabelX electricAccountNumberValue, TextBoxX electricAccountNumber,
            LabelX electricRateCodeCaption, LabelX electricRateCodeValue, TextBoxX electricRateCode,
            LabelX gasAccountNumberCaption, LabelX gasAccountNumberValue, TextBoxX gasAccountNumber,
            LabelX gasRateCodeCaption, LabelX gasRateCodeValue, TextBoxX gasRateCode,
            LabelX startDateTimeCaption, LabelX startDateTimeValue, DateTimeInput startDateTime,
            MetroTilePanel saveButtons, MetroTileItem cancel, MetroTileItem save,
            MetroTabItem homePanel, TextBoxX recordType, LabelX companyInfo,
            LabelX companyEmailValue, TextBoxX companyEmail, LabelX companyPhoneValue, TextBoxX companyPhone)
        {
            //Lg.MethodStart("frmMainCompanyHelper.RegisterControls(...)");

            _id = id;
            _externalId = externalId;

            _addIcon = addIcon;
            _editIcon = editIcon;

            _nameCaption = nameCaption;
            _nameValue = nameValue;
            _name = name;
            _propertyManagementCaption = propertyManagementCaption;
            _propertyManagementValue = propertyManagementValue;
            _propertyManagement = propertyManagement;
            _addressCaption = addressCaption;
            _addressValue = addressValue;
            _address = address;
            _address2Value = address2Value;
            _address2 = address2;
            _cityValue = cityValue;
            _city = city;
            _stateValue = stateValue;
            _state = state;
            _zipValue = zipValue;
            _zip = zip;
            _zipExtensionValue = zipExtensionValue;
            _zipExtension = zipExtension;

            _editButtons = editButtons;
            _edit = edit;

            _utilityCaption = utilityCaption;
            _utilityValue = utilityValue;
            _utility = utility;
            _programCaption = programCaption;
            _programValue = programValue;
            _program = program;
            _accountTypeCaption = accountTypeCaption;
            _accountTypeValue = accountTypeValue;
            _accountType = accountType;

            _startDateTimeCaption = startDateTimeCaption;
            _startDateTimeValue = startDateTimeValue;
            _startDateTime = startDateTime;

            _electricAccountNumberCaption = electricAccountNumberCaption;
            _electricAccountNumberValue = electricAccountNumberValue;
            _electricAccountNumber = electricAccountNumber;
            _electricRateCodeCaption = electricRateCodeCaption;
            _electricRateCodeValue = electricRateCodeValue;
            _electricRateCode = electricRateCode;

            _gasAccountNumberCaption = gasAccountNumberCaption;
            _gasAccountNumberValue = gasAccountNumberValue;
            _gasAccountNumber = gasAccountNumber;
            _gasRateCodeCaption = gasRateCodeCaption;
            _gasRateCodeValue = gasRateCodeValue;
            _gasRateCode = gasRateCode;

            _saveButtons = saveButtons;
            _cancel = cancel;
            _save = save;

            _homePanel = homePanel;
            _recordType = recordType;
            _companyInfo = companyInfo;

            _companyEmailValue = companyEmailValue;
            _companyEmail = companyEmail;
            _companyPhoneValue = companyPhoneValue;
            _companyPhone = companyPhone;

            //Lg.MethodEnd("frmMainCompanyHelper.RegisterControls(...)");
        }

        /// <summary>
        /// Verify that all controls used by this panel are registered with this class and initialized.
        /// </summary>
        /// <returns>'true' if all necessary controls are present and accounted for; 'false' otherwise.</returns>
        public static bool ValidateControls()
        {
            //Lg.MethodStart("frmMainCompanyHelper.ValidateControls()");

            return (
                _id != null &&
                _externalId != null &&
                _addIcon != null &&
                _editIcon != null &&
                _nameCaption != null &&
                _nameValue != null &&
                _name != null &&
                _propertyManagementCaption != null &&
                _propertyManagementValue != null &&
                _propertyManagement != null &&
                _addressCaption != null &&
                _addressValue != null &&
                _address != null &&
                _address2Value != null &&
                _address2 != null &&
                _cityValue != null &&
                _city != null &&
                _stateValue != null &&
                _state != null &&
                _zipValue != null &&
                _zip != null &&
                _zipExtensionValue != null &&
                _zipExtension != null &&
                _editButtons != null &&
                _edit != null &&
                _utilityCaption != null &&
                _utilityValue != null &&
                _utility != null &&
                _programCaption != null &&
                _programValue != null &&
                _program != null &&
                _accountTypeCaption != null &&
                _accountTypeValue != null &&
                _accountType != null &&
                _startDateTimeCaption != null &&
                _startDateTimeValue != null &&
                _startDateTime != null &&
                _electricAccountNumberCaption != null &&
                _electricAccountNumberValue != null &&
                _electricAccountNumber != null &&
                _electricRateCodeCaption != null &&
                _electricRateCodeValue != null &&
                _electricRateCode != null &&
                _gasAccountNumberCaption != null &&
                _gasAccountNumberValue != null &&
                _gasAccountNumber != null &&
                _gasRateCodeCaption != null &&
                _gasRateCodeValue != null &&
                _gasRateCode != null &&
                _saveButtons != null &&
                _cancel != null &&
                _save != null &&
                _homePanel != null &&
                _recordType != null &&
                _companyInfo != null &&
                _companyEmailValue != null &&
                _companyEmail != null &&
                _companyPhoneValue != null &&
                _companyPhone != null
            );

            //Lg.MethodEnd("frmMainCompanyHelper.ValidateControls()");
        }

        #endregion Control methods

        #endregion Exposed class methods (static)

        #region Private methods

        private static void SetControlDisplayMode(Enumerations.PanelDisplayMode mode)
        {
            //SuspendControls();

            switch (mode)
            {
                case Enumerations.PanelDisplayMode.Add:
                case Enumerations.PanelDisplayMode.RollYourOwnAudit:

                    #region Add/Roll Your Own Audit modes

                    _id.Visible = false;
                    _externalId.Visible = false;
                    _recordType.Visible = false;

                    _addIcon.Visible = true;
                    _editIcon.Visible = false;

                    _nameCaption.Visible = true;
                    _nameValue.Visible = false;
                    _name.Visible = true;
                    _propertyManagementCaption.Visible = true;
                    _propertyManagementValue.Visible = false;
                    _propertyManagement.Visible = true;
                    _addressCaption.Visible = true;
                    _addressValue.Visible = false;
                    _address.Visible = true;
                    _address2Value.Visible = false;
                    _address2.Visible = true;
                    _cityValue.Visible = false;
                    _city.Visible = true;
                    _stateValue.Visible = false;
                    _state.Visible = true;
                    _zipValue.Visible = false;
                    _zip.Visible = true;
                    _zipExtensionValue.Visible = false;
                    _zipExtension.Visible = true;

                    _editButtons.Visible = false;
                    _edit.Visible = false;

                    _utilityCaption.Visible = true;
                    _utilityValue.Visible = false;
                    _utility.Visible = true;
                    _programCaption.Visible = true;
                    _programValue.Visible = false;
                    _program.Visible = true;
                    _accountTypeCaption.Visible = true;
                    _accountTypeValue.Visible = false;
                    _accountType.Visible = true;

                    _startDateTimeCaption.Visible = false;
                    _startDateTimeValue.Visible = false;
                    _startDateTime.Visible = false;

                    _electricAccountNumberCaption.Visible = true;
                    _electricAccountNumberValue.Visible = false;
                    _electricAccountNumber.Visible = true;
                    _electricRateCodeCaption.Visible = true;
                    _electricRateCodeValue.Visible = false;
                    _electricRateCode.Visible = true;

                    _gasAccountNumberCaption.Visible = true;
                    _gasAccountNumberValue.Visible = false;
                    _gasAccountNumber.Visible = true;
                    _gasRateCodeCaption.Visible = true;
                    _gasRateCodeValue.Visible = false;
                    _gasRateCode.Visible = true;

                    _companyEmailValue.Visible = false;
                    _companyEmail.Visible = true;
                    _companyPhoneValue.Visible = false;
                    _companyPhone.Visible = true;

                    _saveButtons.Visible = true;
                    _cancel.Visible = true;
                    _save.Visible = true;

                    _name.Focus();

                    SetFormLock(Enumerations.LockStatus.Lock);

                    #endregion Add/Roll Your Own Audit modes

                    break;

                case Enumerations.PanelDisplayMode.Edit:
                case Enumerations.PanelDisplayMode.Unvalidated:

                    #region Edit mode

                    _id.Visible = false;
                    _externalId.Visible = false;
                    _recordType.Visible = false;

                    _addIcon.Visible = false;
                    _editIcon.Visible = true;

                    _nameCaption.Visible = true;
                    _nameValue.Visible = false;
                    _name.Visible = true;
                    _propertyManagementCaption.Visible = true;
                    _propertyManagementValue.Visible = false;
                    _propertyManagement.Visible = true;
                    _addressCaption.Visible = true;
                    _addressValue.Visible = false;
                    _address.Visible = true;
                    _address2Value.Visible = false;
                    _address2.Visible = true;
                    _cityValue.Visible = false;
                    _city.Visible = true;
                    _stateValue.Visible = false;
                    _state.Visible = true;
                    _zipValue.Visible = false;
                    _zip.Visible = true;
                    _zipExtensionValue.Visible = false;
                    _zipExtension.Visible = true;

                    _editButtons.Visible = false;
                    _edit.Visible = false;

                    _utilityCaption.Visible = true;
                    _utilityValue.Visible = false;
                    _utility.Visible = true;
                    _programCaption.Visible = true;
                    _programValue.Visible = false;
                    _program.Visible = true;
                    _accountTypeCaption.Visible = true;
                    _accountTypeValue.Visible = false;
                    _accountType.Visible = true;

                    _startDateTimeCaption.Visible = false;
                    _startDateTimeValue.Visible = false;
                    _startDateTime.Visible = false;

                    _electricAccountNumberCaption.Visible = true;
                    _electricAccountNumberValue.Visible = false;
                    _electricAccountNumber.Visible = true;
                    _electricRateCodeCaption.Visible = true;
                    _electricRateCodeValue.Visible = false;
                    _electricRateCode.Visible = true;

                    _gasAccountNumberCaption.Visible = true;
                    _gasAccountNumberValue.Visible = false;
                    _gasAccountNumber.Visible = true;
                    _gasRateCodeCaption.Visible = true;
                    _gasRateCodeValue.Visible = false;
                    _gasRateCode.Visible = true;

                    _companyEmailValue.Visible = false;
                    _companyEmail.Visible = true;
                    _companyPhoneValue.Visible = false;
                    _companyPhone.Visible = true;

                    _saveButtons.Visible = true;
                    _cancel.Visible = true;
                    _save.Visible = true;

                    _name.Focus();

                    if (mode == Enumerations.PanelDisplayMode.Edit)
                    {
                        SetFormLock(Enumerations.LockStatus.Lock);
                    }
                    else
                    {
                        // Unvalidated
                        SetFormLock(Enumerations.LockStatus.Unlock);
                    }

                    #endregion Edit mode

                    break;

                case Enumerations.PanelDisplayMode.ReadOnly:

                    #region Read only mode

                    _id.Visible = false;
                    _externalId.Visible = false;
                    _recordType.Visible = false;

                    _addIcon.Visible = false;
                    _editIcon.Visible = false;

                    _nameCaption.Visible = true;
                    _nameValue.Visible = true;
                    _name.Visible = false;
                    _propertyManagementCaption.Visible = true;
                    _propertyManagementValue.Visible = true;
                    _propertyManagement.Visible = false;
                    _addressCaption.Visible = true;
                    _addressValue.Visible = true;
                    _address.Visible = false;
                    _address2Value.Visible = true;
                    _address2.Visible = false;
                    _cityValue.Visible = true;
                    _city.Visible = false;
                    _stateValue.Visible = true;
                    _state.Visible = false;
                    _zipValue.Visible = true;
                    _zip.Visible = false;
                    _zipExtensionValue.Visible = true;
                    _zipExtension.Visible = false;

                    if (Company != null && Company.IsValidated)
                    {
                        _editButtons.Visible = false;
                        _edit.Visible = false;
                    }

                    _utilityCaption.Visible = true;
                    _utilityValue.Visible = true;
                    _utility.Visible = false;
                    _programCaption.Visible = true;
                    _programValue.Visible = true;
                    _program.Visible = false;
                    _accountTypeCaption.Visible = true;
                    _accountTypeValue.Visible = true;
                    _accountType.Visible = false;

                    _startDateTimeCaption.Visible = false;
                    _startDateTimeValue.Visible = false;
                    _startDateTime.Visible = false;

                    _electricAccountNumberCaption.Visible = true;
                    _electricAccountNumberValue.Visible = true;
                    _electricAccountNumber.Visible = false;
                    _electricRateCodeCaption.Visible = true;
                    _electricRateCodeValue.Visible = true;
                    _electricRateCode.Visible = false;

                    _gasAccountNumberCaption.Visible = true;
                    _gasAccountNumberValue.Visible = true;
                    _gasAccountNumber.Visible = false;
                    _gasRateCodeCaption.Visible = true;
                    _gasRateCodeValue.Visible = true;
                    _gasRateCode.Visible = false;

                    _companyEmailValue.Visible = true;
                    _companyEmail.Visible = false;
                    _companyPhoneValue.Visible = true;
                    _companyPhone.Visible = false;

                    _saveButtons.Visible = false;
                    //_cancel.Visible = true;
                    //_save.Visible = true;

                    SetFormLock(Enumerations.LockStatus.Unlock);

                    #endregion Read only mode

                    break;

                case Enumerations.PanelDisplayMode.Blank:

                    #region Blank mode

                    _id.Visible = false;
                    _externalId.Visible = false;
                    _recordType.Visible = false;

                    _addIcon.Visible = false;
                    _editIcon.Visible = false;

                    _nameCaption.Visible = false;
                    _nameValue.Visible = false;
                    _name.Visible = false;
                    _propertyManagementCaption.Visible = false;
                    _propertyManagementValue.Visible = false;
                    _propertyManagement.Visible = false;
                    _addressCaption.Visible = false;
                    _addressValue.Visible = false;
                    _address.Visible = false;
                    _address2Value.Visible = false;
                    _address2.Visible = false;
                    _cityValue.Visible = false;
                    _city.Visible = false;
                    _stateValue.Visible = false;
                    _state.Visible = false;
                    _zipValue.Visible = false;
                    _zip.Visible = false;
                    _zipExtensionValue.Visible = false;
                    _zipExtension.Visible = false;

                    _editButtons.Visible = false;
                    _edit.Visible = false;

                    _utilityCaption.Visible = false;
                    _utilityValue.Visible = false;
                    _utility.Visible = false;
                    _programCaption.Visible = false;
                    _programValue.Visible = false;
                    _program.Visible = false;
                    _accountTypeCaption.Visible = false;
                    _accountTypeValue.Visible = false;
                    _accountType.Visible = false;

                    _startDateTimeCaption.Visible = false;
                    _startDateTimeValue.Visible = false;
                    _startDateTime.Visible = false;

                    _electricAccountNumberCaption.Visible = false;
                    _electricAccountNumberValue.Visible = false;
                    _electricAccountNumber.Visible = false;
                    _electricRateCodeCaption.Visible = false;
                    _electricRateCodeValue.Visible = false;
                    _electricRateCode.Visible = false;

                    _gasAccountNumberCaption.Visible = false;
                    _gasAccountNumberValue.Visible = false;
                    _gasAccountNumber.Visible = false;
                    _gasRateCodeCaption.Visible = false;
                    _gasRateCodeValue.Visible = false;
                    _gasRateCode.Visible = false;

                    _companyEmailValue.Visible = false;
                    _companyEmail.Visible = false;
                    _companyPhoneValue.Visible = false;
                    _companyPhone.Visible = false;

                    _saveButtons.Visible = false;
                    //_cancel.Visible = true;
                    //_save.Visible = true;

                    SetFormLock(Enumerations.LockStatus.Unlock);

                    #endregion Blank mode

                    break;
            }

            //ResumeControls();
        }

        private static void SetFormLock(Enumerations.LockStatus status)
        {
            if (status == Enumerations.LockStatus.Lock)
            {
                _homePanel.Enabled = false;

                frmMainCompanyHelper.IsLockedForEdit = true;
            }
            else
            {
                _homePanel.Enabled = true;

                frmMainCompanyHelper.IsLockedForEdit = false;
            }
        }

        private static void SyncData(Enumerations.SyncDirection direction)
        {
            IsLoading = true;

            switch (direction)
            {
                case Enumerations.SyncDirection.ControlsToEditObject:

                    #region Controls to edit object

                    _editObject = new Company();

                    if (String.IsNullOrWhiteSpace(_id.Text))
                    {
                        string newId = GuidGenerator.Generate();
                        _editObject.Id = newId;
                        _editObject.CompanyId = newId;
                        _editObject.ExternalId = ""; // leave this blank until it is validated
                    }
                    else
                    {
                        _editObject.Id = _id.Text;
                        _editObject.CompanyId = _id.Text;
                        _editObject.ExternalId = _externalId.Text;
                    }

                    _editObject.RecordType = _recordType.Text;

                    _editObject.Name = _name.Text;
                    _editObject.PropertyManagement = _propertyManagement.Text;
                    _editObject.AddressLine1 = _address.Text;
                    _editObject.AddressLine2 = _address2.Text;
                    _editObject.City = _city.Text;
                    _editObject.State = _state.Text;
                    _editObject.PostalCode = _zip.Text;
                    _editObject.PostalCodeExtension = _zipExtension.Text;

                    _editObject.Utility = _utility.Text;
                    _editObject.Program = _program.Text;
                    _editObject.AccountType = _accountType.Text;

                    _editObject.ElectricAccountNumber = _electricAccountNumber.Text;
                    _editObject.ElectricRateCode = _electricRateCode.Text;
                    _editObject.GasAccountNumber = _gasAccountNumber.Text;
                    _editObject.GasRateCode = _gasRateCode.Text;

                    if (_selectedObject != null)
                    {
                        // Stuff to copy from the original object.

                        _editObject.Audits = _selectedObject.Audits;

                        if (!_selectedObject.IsValidated)
                        {
                            _editObject.UpdateAuditName();
                        }

                        _editObject.Contacts = _selectedObject.Contacts;
                        _editObject.AddressLine3 = _selectedObject.AddressLine3;
                    }

                    _editObject.Email = _companyEmail.Text;
                    _editObject.Phone = _companyPhone.Text;

                    #endregion Controls to edit object

                    break;

                case Enumerations.SyncDirection.EditObjectToControls:

                    #region Edit object to controls

                    if (_editObject == null)
                    {
                        _id.Text = "";
                        _externalId.Text = "";
                        _recordType.Text = "";

                        _name.Text = "";
                        _nameValue.Text = "";
                        _propertyManagement.Text = "";
                        _propertyManagementValue.Text = "";
                        _address.Text = "";
                        _addressValue.Text = "";
                        _address2.Text = "";
                        _address2Value.Text = "";
                        _city.Text = "";
                        _cityValue.Text = "";
                        _state.SelectedIndex = -1;
                        _stateValue.Text = "";
                        _zip.Text = "";
                        _zipValue.Text = "";
                        _zipExtension.Text = "";
                        _zipExtensionValue.Text = "";

                        _utility.SelectedIndex = -1;
                        _utilityValue.Text = "";
                        _program.SelectedIndex = -1;
                        _programValue.Text = "";
                        _accountType.SelectedIndex = -1;
                        _accountTypeValue.Text = "";

                        _startDateTime.Value = DateTime.Now;
                        _startDateTimeValue.Text = "";

                        _electricAccountNumber.Text = "";
                        _electricAccountNumberValue.Text = "";
                        _electricRateCode.Text = "";
                        _electricRateCodeValue.Text = "";
                        _gasAccountNumber.Text = "";
                        _gasAccountNumberValue.Text = "";
                        _gasRateCode.Text = "";
                        _gasRateCodeValue.Text = "";

                        _companyEmail.Text = "";
                        _companyEmailValue.Text = "";
                        _companyPhone.Text = "";
                        _companyPhoneValue.Text = "";
                    }
                    else
                    {
                        _id.Text = _editObject.Id;
                        _externalId.Text = _editObject.ExternalId;
                        _recordType.Text = _editObject.RecordType;

                        _name.Text = _editObject.Name;
                        _nameValue.Text = _editObject.Name;
                        _propertyManagement.Text = _editObject.PropertyManagement;
                        _propertyManagementValue.Text = _editObject.PropertyManagement;
                        _address.Text = _editObject.AddressLine1;
                        _addressValue.Text = _editObject.AddressLine1;
                        _address2.Text = _editObject.AddressLine2;
                        _address2Value.Text = _editObject.AddressLine2;
                        _city.Text = _editObject.City;
                        _cityValue.Text = _editObject.City;
                        _state.Text = _editObject.State;
                        _stateValue.Text = _editObject.State;
                        _zip.Text = _editObject.PostalCode;
                        _zipValue.Text = _editObject.PostalCode;
                        _zipExtension.Text = _editObject.PostalCodeExtension;
                        _zipExtensionValue.Text = _editObject.PostalCodeExtension;

                        _utility.Text = _editObject.Utility;
                        _utilityValue.Text = _editObject.Utility;
                        _program.Text = _editObject.Program;
                        _programValue.Text = _editObject.Program;
                        _accountType.Text = _editObject.AccountType;
                        _accountTypeValue.Text = _editObject.AccountType;

                        //_startDateTime.Value = _editObject.RollYourOwnStartDateTime;
                        //if (_startDateTime.Value == default(DateTime)) {
                        //    _startDateTimeValue.Text = "";
                        //}
                        //else {
                        //    _startDateTimeValue.Text = _editObject.RollYourOwnStartDateTime.ToString("MM/dd/yyyy hh:mm tt");
                        //}

                        _electricAccountNumber.Text = _editObject.ElectricAccountNumber;
                        _electricAccountNumberValue.Text = _editObject.ElectricAccountNumber;
                        _electricRateCode.Text = _editObject.ElectricRateCode;
                        _electricRateCodeValue.Text = _editObject.ElectricRateCode;
                        _gasAccountNumber.Text = _editObject.GasAccountNumber;
                        _gasAccountNumberValue.Text = _editObject.GasAccountNumber;
                        _gasRateCode.Text = _editObject.GasRateCode;
                        _gasRateCodeValue.Text = _editObject.GasRateCode;

                        _companyEmail.Text = _editObject.Email;
                        _companyEmailValue.Text = _editObject.Email;
                        _companyPhone.Text = _editObject.Phone;
                        _companyPhoneValue.Text = _editObject.Phone;
                    }

                    #endregion Edit object to controls

                    break;

                case Enumerations.SyncDirection.ControlsToSelectedObject:

                    #region Controls to selected object

                    if (_selectedObject == null)
                    {
                        _selectedObject = new Company();
                    }

                    _selectedObject.Id = _id.Text;
                    _selectedObject.CompanyId = _id.Text;

                    // never set the external id from anything but a validate call

                    //if (String.IsNullOrWhiteSpace(_externalId.Text)) {
                    //    _selectedObject.ExternalId = _id.Text;
                    //}
                    //else {
                    _selectedObject.ExternalId = _externalId.Text;
                    //}

                    _selectedObject.RecordType = _recordType.Text;

                    _selectedObject.Name = _name.Text;
                    _selectedObject.PropertyManagement = _propertyManagement.Text;
                    _selectedObject.AddressLine1 = _address.Text;
                    _selectedObject.AddressLine2 = _address2.Text;
                    _selectedObject.City = _city.Text;
                    _selectedObject.State = _state.Text;
                    _selectedObject.PostalCode = _zip.Text;
                    _selectedObject.PostalCodeExtension = _zipExtension.Text;

                    _selectedObject.Utility = _utility.Text;
                    _selectedObject.Program = _program.Text;
                    _selectedObject.AccountType = _accountType.Text;

                    //_selectedObject.RollYourOwnStartDateTime = _startDateTime.Value;

                    _selectedObject.ElectricAccountNumber = _electricAccountNumber.Text;
                    _selectedObject.ElectricRateCode = _electricRateCode.Text;
                    _selectedObject.GasAccountNumber = _gasAccountNumber.Text;
                    _selectedObject.GasRateCode = _gasRateCode.Text;

                    _selectedObject.Email = _companyEmail.Text;
                    _selectedObject.Phone = _companyPhone.Text;

                    #endregion Controls to selected object

                    break;

                case Enumerations.SyncDirection.SelectedObjectToControls:

                    #region Selected object to controls

                    if (_selectedObject == null)
                    {
                        _id.Text = "";
                        _externalId.Text = "";
                        _recordType.Text = "";

                        _name.Text = "";
                        _nameValue.Text = "";
                        _propertyManagement.Text = "";
                        _address.Text = "";
                        _addressValue.Text = "";
                        _address2.Text = "";
                        _address2Value.Text = "";
                        _city.Text = "";
                        _cityValue.Text = "";
                        _state.SelectedIndex = -1;
                        _stateValue.Text = "";
                        _zip.Text = "";
                        _zipValue.Text = "";
                        _zipExtension.Text = "";
                        _zipExtensionValue.Text = "";

                        _utility.SelectedIndex = -1;
                        _utilityValue.Text = "";
                        _program.SelectedIndex = -1;
                        _programValue.Text = "";
                        _accountType.SelectedIndex = -1;
                        _accountTypeValue.Text = "";

                        _startDateTime.Value = DateTime.Now;
                        _startDateTimeValue.Text = "";

                        _electricAccountNumber.Text = "";
                        _electricAccountNumberValue.Text = "";
                        _electricRateCode.Text = "";
                        _electricRateCodeValue.Text = "";
                        _gasAccountNumber.Text = "";
                        _gasAccountNumberValue.Text = "";
                        _gasRateCode.Text = "";
                        _gasRateCodeValue.Text = "";

                        _companyEmail.Text = "";
                        _companyEmailValue.Text = "";
                        _companyPhone.Text = "";
                        _companyPhoneValue.Text = "";

                        _companyInfo.Text = "";
                    }
                    else
                    {
                        _id.Text = _selectedObject.Id;
                        _externalId.Text = _selectedObject.ExternalId;
                        _recordType.Text = _selectedObject.RecordType;

                        _name.Text = _selectedObject.Name;
                        _nameValue.Text = _selectedObject.Name;
                        _propertyManagement.Text = _selectedObject.PropertyManagement;
                        _propertyManagementValue.Text = _selectedObject.PropertyManagement;
                        _address.Text = _selectedObject.AddressLine1;
                        _addressValue.Text = _selectedObject.AddressLine1;
                        _address2.Text = _selectedObject.AddressLine2;
                        _address2Value.Text = _selectedObject.AddressLine2;
                        _city.Text = _selectedObject.City;
                        _cityValue.Text = _selectedObject.City;
                        _state.Text = _selectedObject.State;
                        _stateValue.Text = _selectedObject.State;
                        _zip.Text = _selectedObject.PostalCode;
                        _zipValue.Text = _selectedObject.PostalCode;
                        _zipExtension.Text = _selectedObject.PostalCodeExtension;
                        _zipExtensionValue.Text = _selectedObject.PostalCodeExtension;

                        _utility.Text = _selectedObject.Utility;
                        _utilityValue.Text = _selectedObject.Utility;
                        _program.Text = _selectedObject.Program;
                        _programValue.Text = _selectedObject.Program;
                        _accountType.Text = _selectedObject.AccountType;
                        _accountTypeValue.Text = _selectedObject.AccountType;

                        //_startDateTime.Value = _selectedObject.RollYourOwnStartDateTime;
                        //if (_startDateTime.Value == default(DateTime)) {
                        //    _startDateTimeValue.Text = "";
                        //}
                        //else {
                        //    _startDateTimeValue.Text = _selectedObject.RollYourOwnStartDateTime.ToString("MM/dd/yyyy hh:mm tt");
                        //}

                        _electricAccountNumber.Text = _selectedObject.ElectricAccountNumber;
                        _electricAccountNumberValue.Text = _selectedObject.ElectricAccountNumber;
                        _electricRateCode.Text = _selectedObject.ElectricRateCode;
                        _electricRateCodeValue.Text = _selectedObject.ElectricRateCode;
                        _gasAccountNumber.Text = _selectedObject.GasAccountNumber;
                        _gasAccountNumberValue.Text = _selectedObject.GasAccountNumber;
                        _gasRateCode.Text = _selectedObject.GasRateCode;
                        _gasRateCodeValue.Text = _selectedObject.GasRateCode;

                        _companyEmail.Text = _selectedObject.Email;
                        _companyEmailValue.Text = _selectedObject.Email;
                        _companyPhone.Text = _selectedObject.Phone;
                        _companyPhoneValue.Text = _selectedObject.Phone;

                        _companyInfo.Text = _selectedObject.ToString("I");
                    }

                    #endregion Selected object to controls

                    break;
            }

            IsLoading = false;
        }

        private static bool ValidateData()
        {
            bool result = false;
            string msg = "";

            if (String.IsNullOrWhiteSpace(_name.Text))
            {
                msg = "Please enter a valid name.";
                _name.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_address.Text))
            {
                msg = "Please enter a valid address.";
                _address.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_city.Text))
            {
                msg = "Please enter a city.";
                _city.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_state.Text))
            {
                msg = "Please selecte a state.";
                _state.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_zip.Text))
            {
                msg = "Please enter a valid ZIP code.";
                _zip.Focus();
            }
            //else if (String.IsNullOrWhiteSpace(_startDateTime.Text)) {
            //    msg = "Please enter a valid start time.";
            //    _startDateTime.Focus();
            //}
            else if (String.IsNullOrWhiteSpace(_utility.Text))
            {
                msg = "Please select a utility.";
                _utility.Focus();
            }
            else if (String.IsNullOrWhiteSpace(_program.Text))
            {
                msg = "Please select a program.";
                _utility.Focus();
            }
            //else if (String.IsNullOrWhiteSpace(_electricAccountNumber.Text) && String.IsNullOrWhiteSpace(_gasAccountNumber.Text)) {
            //    msg = "Please enter an electric account number, a gas account number, or both.";
            //    _electricAccountNumber.Focus();
            //}

            if (msg == "")
            {
                result = true;
            }
            else
            {
                result = false;
                MessageBox.Show(msg, "Invalid Company Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        #endregion Private methods

        public static void SetSampleData()
        {
            _id.Text = GuidGenerator.Generate();
            _externalId.Text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            _name.Text = "Scott's LLC";
            _propertyManagement.Text = "John Q. Manager";
            _address.Text = "1727 N. Main Street";
            _address2.Text = "Upper Level";
            _city.Text = "Racine";
            _state.Text = "WI";
            _zip.Text = "53402";
            _zipExtension.Text = "1727";

            _utility.Text = "";
            _program.Text = "";
            _accountType.Text = "";

            _startDateTime.Value = DateTime.Now;

            _electricAccountNumber.Text = "1234567";
            _electricRateCode.Text = "2.54";

            _gasAccountNumber.Text = "7654321";
            _gasRateCode.Text = "4.52";

            _companyEmail.Text = "SLL@noneyo.com";
            _companyPhone.Text = "(312) 555-1212";
        }
    }
}