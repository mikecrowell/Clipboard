using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Schedule;
using DevComponents.Schedule.Model;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FieldTool.BLL;
using FieldTool.BLL.BusinessObjects;
using FieldTool.BLL.Emails;
using FieldTool.BLL.Utilities;
using FieldTool.Bsi.Helpers;
using FieldTool.Bsi.Models;
using FieldTool.Constants;
using FieldTool.Constants.Config;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Models.CB;
using FieldTool.Constants.Paths;
using FieldTool.UI.AuditWalkThrough;
using FieldTool.UI.AuditWalkThrough.Recomendations;
using FieldTool.UI.AuditWalkThrough.Referral;
using FieldTool.UI.AuditWalkThrough.Survey;
using FieldTool.UI.DirectInstall.Reports;
using FieldTool.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    public partial class frmMain : DevComponents.DotNetBar.Metro.MetroAppForm
    {
        #region Constants

        private const int EIP_GRID_COLUMN_IMAGE = 0;
        private const int EIP_GRID_COLUMN_ID = 1;
        private const int EIP_GRID_COLUMN_NAME = 2;
        private const int EIP_GRID_COLUMN_QUANTITY = 3;

        private const int DI_GRID_COLUMN_PROGRAM_CODE = 1;
        private const int DI_GRID_COLUMN_NAME = 2;
        private const int DI_GRID_COLUMN_ID = 4;
        private const int DI_GRID_COLUMN_INCREMENT_QTY = 13;
        private const int DI_GRID_COLUMN_QUANTITY = 14;
        private const int DI_GRID_COLUMN_DECREMENT_QTY = 15;
        private const int DI_GRID_COLUMN_SPACE = 16;
        private const int DI_GRID_COLUMN_KWH = 17;
        private const int DI_GRID_COLUMN_THERMS = 18;
        private const int DI_GRID_COLUMN_KGALLONS = 19;
        private const int DI_GRID_COLUMN_SAVINGS = 21;
        private const int DI_GRID_COLUMN_ICON_FILE_NAME = 22;
        private const int DI_GRID_COLUMN_KWH_UNIT = 23;
        private const int DI_GRID_COLUMN_THERMS_UNIT = 24;
        private const int DI_GRID_COLUMN_COMPONENT_TYPE = 25;

        private const int UNIT_DI_GRID_COLUMN_PROGRAM_CODE = 1;
        private const int UNIT_DI_GRID_COLUMN_NAME = 2;
        private const int UNIT_DI_GRID_COLUMN_ID = 4;
        private const int UNIT_DI_GRID_COLUMN_INCREMENT_QTY = 13;
        private const int UNIT_DI_GRID_COLUMN_QUANTITY = 14;
        private const int UNIT_DI_GRID_COLUMN_DECREMENT_QTY = 15;
        private const int UNIT_DI_GRID_COLUMN_SPACE = 16;
        private const int UNIT_DI_GRID_COLUMN_KWH = 17;
        private const int UNIT_DI_GRID_COLUMN_THERMS = 18;
        private const int UNIT_DI_GRID_COLUMN_INCENTIVE = 19;
        private const int UNIT_DI_GRID_COLUMN_KGALLONS = 20;
        private const int UNIT_DI_GRID_COLUMN_SAVINGS = 21;
        private const int UNIT_DI_GRID_COLUMN_ICON_FILE_NAME = 22;
        private const int UNIT_DI_GRID_COLUMN_KWH_UNIT = 23;
        private const int UNIT_DI_GRID_COLUMN_THERMS_UNIT = 24;
        private const int UNIT_DI_GRID_COLUMN_COMPONENT_TYPE = 25;

        private const string DEFAULT_COMPONENT_ICON = "default.png";

        #endregion Constants

        #region Private member variables

        private Audit _selectedAudit;
        private Building _selectedBuilding;
        private ClientMultifamilyReportBranding _selectedAuditMultiFamilyReportBranding;
        private ClientCommercialReportBranding _selectedAuditCommercialReportBranding;

        private bool _isLoadingForm = false;
        private bool _isLoadingAudit = false;
        private bool _savingBuilding = false;
        public bool _savingGasHistory = false;
        public bool _savingElectricHistory = false;
        public bool _savingEnergyUseIndex = false;
        private string _userFullName = "";
        public string _userEmail = "";
        public string _userName = "";
        public bool _logTrace = false;
        private bool _unitNumberingChangedByUser = true;
        private IBsiService _bsiService = default(IBsiService);
        private ILookupService _lookupService = default(ILookupService);
        private Company _rollYourOwn = null;
        private bool _allowBuildingPropertyTypeEdit = true;
        private frmDirectInstall frmDI = default(frmDirectInstall);
        private static readonly System.Windows.Forms.Timer memoryCheckTimer = new System.Windows.Forms.Timer();
        private double totalMemory = 0.0;

        #endregion Private member variables

        #region Constructors

        public frmMain()
        {
            this.LoadUserSettings(true);

            try
            {
                //this.FormClosing += frmMain_FormClosing;
                //this.Load += frmMain_Load;
                this._isLoadingForm = true;

                InitializeComponent();
                this.Padding = new System.Windows.Forms.Padding(5, 5, 15, 5);
                this.BringToFront();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this._isLoadingForm = false;
            }
        }

        #endregion Constructors

        #region Properties

        public IBsiService bsiService
        {
            get
            {
                if (this._bsiService == default(IBsiService))
                {
                    this._bsiService = new BsiService(ConfigurationManager.AppSettings);
                }
                return this._bsiService;
            }
            set
            {
                this._bsiService = value;
            }
        }

        public ILookupService lookupService
        {
            get
            {
                if (this._lookupService == default(ILookupService))
                {
                    this._lookupService = new LookupService(ConfigurationManager.AppSettings);
                }
                return this._lookupService;
            }
            set
            {
                this._lookupService = value;
            }
        }

        private bool DoDebug
        {
            get
            {
                bool result = false;
                Boolean.TryParse(ConfigurationManager.AppSettings["DoDebug"], out result);
                return result;
            }
        }

        private Enumerations.Mode EditMode { get; set; }

        private bool OkToSave
        {
            get
            {
                return this.EditMode == Enumerations.Mode.Add || this.EditMode == Enumerations.Mode.Edit;
            }
        }

        public Audit SelectedAudit
        {
            get
            {
                return this._selectedAudit;
            }
            set
            {
                if (!this.DesignMode)
                {
                    this._selectedAudit = value;
                }
            }
        }

        public Building SelectedBuilding
        {
            get
            {
                return this._selectedBuilding;
            }
            set
            {
                if (!this.DesignMode)
                {
                    this._selectedBuilding = value;
                    if (this._selectedBuilding != null && this._selectedBuilding.IsValid)
                    {
                        this.LoadBuilding();
                    }
                }
            }
        }

        private ClientMultifamilyReportBranding SelectedAuditMultiFamilyReportBranding
        {
            get
            {
                return this._selectedAuditMultiFamilyReportBranding;
            }
            set
            {
                if (!this.DesignMode)
                {
                    this._selectedAuditMultiFamilyReportBranding = value;
                }
            }
        }

        private ClientCommercialReportBranding SelectedAuditCommercialReportBranding
        {
            get
            {
                return this._selectedAuditCommercialReportBranding;
            }
            set
            {
                if (!this.DesignMode)
                {
                    this._selectedAuditCommercialReportBranding = value;
                }
            }
        }

        private ClientReportBranding CurrentAuditReportReportBranding
        {
            get
            {
                if (this.SelectedAudit != null)
                {
                    if (this.SelectedAudit.ProgramType != null)
                    {
                        switch (this.SelectedAudit.ProgramType)
                        {
                            case FieldTool.Constants.ProgramType.MultiFamily:
                                return this._selectedAuditMultiFamilyReportBranding;

                            case FieldTool.Constants.ProgramType.Commercial:
                                return this._selectedAuditCommercialReportBranding;

                            default:
                                return null;
                        }
                    }
                }
                return null;
            }
        }

        public ucAuditSurvey UcAuditSurvey
        {
            get
            {
                ucAuditSurvey u = FindControlByName<ucAuditSurvey>(tabControlPanelAuditSurveyQuestions.Controls, AuditUserControlNames.Survey);
                if (u == default(ucAuditSurvey))
                {
                    u = new ucAuditSurvey(this);
                    u.Dock = DockStyle.Fill;
                    u.Name = AuditUserControlNames.Survey;
                    tabControlPanelAuditSurveyQuestions.Controls.Add(u);
                }
                return u;
            }
        }

        public ucAuditReferral UcAuditReferral
        {
            get
            {
                ucAuditReferral u = FindControlByName<ucAuditReferral>(tabControlPanelAuditSurveyReferrals.Controls, AuditUserControlNames.Referral);
                if (u == default(ucAuditReferral))
                {
                    u = new ucAuditReferral(this);
                    u.Dock = DockStyle.Fill;
                    u.Name = AuditUserControlNames.Referral;
                    tabControlPanelAuditSurveyReferrals.Controls.Add(u);
                }
                return u;
            }
        }

        public ucAuditRecommendations UcAuditRecommendations
        {
            get
            {
                ucAuditRecommendations u = FindControlByName<ucAuditRecommendations>(tabControlPanelAuditSurveyRecommendations.Controls, AuditUserControlNames.Recommendation);
                if (u == default(ucAuditRecommendations))
                {
                    u = new ucAuditRecommendations(this);
                    u.Dock = DockStyle.Fill;
                    u.Name = AuditUserControlNames.Recommendation;
                    tabControlPanelAuditSurveyRecommendations.Controls.Add(u);
                }
                return u;
            }
        }

        public double ParentWidth { get { return this.Width; } }

        #endregion Properties

        #region Events

        private void btnAddRecommendation_Click(object sender, EventArgs e)
        {
            List<string> spaces = new List<string>();
            spaces.Add("Whole Building");
            foreach (ListViewItem li in this.lstSelectedSpaces.Items)
            {
                spaces.Add(li.Text);
            }
            spaces.Sort();

            string companyId = "";
            string programCode = "";

            if (this.SelectedAudit != null)
            {
                Company c = DataStore.GetCompanyByAudit(this.SelectedAudit);
                if (c != null)
                {
                    companyId = c.CompanyId;
                }
                programCode = this.SelectedAudit.ProgramId;
            }

            //List<RecommendationLibraryParameter> parameters = DataStore.GetAllRecommendationLibraryParameters();

            using (frmAdditionalRecommendations frm = new frmAdditionalRecommendations(companyId, this.SelectedBuilding, spaces, this.recRecommendations.HighestRank, programCode))
            {
                DialogResult dlg = frm.ShowDialog();

                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                    DataStore.SaveData();
                    this.recRecommendations.RepopulateControlByBuildingId(this.SelectedBuilding);
                }
            }
        }

        private void btnRollYourOwnAudit_Click(object sender, EventArgs e)
        {
            this.ClearBuildingControls();
            this.cboActiveBuilding.Items.Clear();
            ClientBranding branding = this.ApplyBranding("", ""); //Set branding back to default
            this.NavigateToCompanyPanel();
            frmMainCompanyHelper.Load(null, Enumerations.PanelDisplayMode.RollYourOwnAudit);
        }

        private void btnHomeValidateAudit_Click(object sender, EventArgs e)
        {
            this.LaunchValidateCompanyForm();
        }

        private void btnApptDayView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.cvApptCalendarView.SelectedView = eCalendarView.Day;
            this.cvApptCalendarView.HighlightCurrentDay = false;

            this.ClearButtonChecks();
            this.btnApptDayView.Checked = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnApptLaunch_Click(object sender, EventArgs e)
        {
            this.DoLaunch();
        }

        private void btnApptMonthView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.cvApptCalendarView.SelectedView = eCalendarView.Month;
            this.cvApptCalendarView.HighlightCurrentDay = true;

            this.ClearButtonChecks();
            this.btnApptMonthView.Checked = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnApptWeekView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.cvApptCalendarView.SelectedView = eCalendarView.Week;
            this.cvApptCalendarView.HighlightCurrentDay = true;

            this.ClearButtonChecks();
            this.btnApptWeekView.Checked = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnApptYearView_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.cvApptCalendarView.SelectedView = eCalendarView.Year;
            this.cvApptCalendarView.HighlightCurrentDay = true;

            this.ClearButtonChecks();
            this.btnApptYearView.Checked = true;

            Cursor.Current = Cursors.Default;
        }

        private void btnHomeDashboard_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetHomeNavButtons(true, false, false);
            this.pslHomePageSlider.SelectedPageIndex = 0;

            Cursor.Current = Cursors.Default;
        }

        private void btnHomeMyDay_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetHomeNavButtons(false, true, false);
            this.pslHomePageSlider.SelectedPageIndex = 1;

            Cursor.Current = Cursors.Default;
        }

        private void calApptMonthCalendar_DateChanged(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Cursor.Current = Cursors.WaitCursor;

                // Keep the calendar view control in sync with the calendar control on the left side of the panel.
                this.cvApptCalendarView.ShowDate(this.calApptMonthCalendar.SelectedDate);
                this.calApptMonthCalendar.DisplayMonth = this.calApptMonthCalendar.SelectedDate;

                Cursor.Current = Cursors.Default;
            }
        }

        private void cboBuildingCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = "";

            this.cboBuildingType.Items.Clear();
            if (this.cboBuildingCategory.SelectedIndex >= 0)
            {
                ListViewItem li = this.cboBuildingCategory.Items[this.cboBuildingCategory.SelectedIndex] as ListViewItem;
                if (li != null)
                {
                    object o = li.Tag;
                    if (o != null)
                    {
                        BuildingCategoryDropDownItem catItem = o as BuildingCategoryDropDownItem;
                        if (catItem != null)
                        {
                            category = catItem.Category;

                            foreach (BuildingTypeDropDownItem item in catItem.Items)
                            {
                                ListViewItem newLi = new ListViewItem(item.Type);
                                newLi.Tag = item.HoursEquivalent;
                                this.cboBuildingType.Items.Add(newLi);
                            }
                            if (this.cboBuildingType.Items.Count > 0)
                            {
                                this.cboBuildingType.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }

            this.FillBuildingSpaces(this.SelectedAudit.ProgramType);
            this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces
        }

        private void cboBuildingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtBuildingHoursEquivalent.Text = "";

            if (this.cboBuildingType.SelectedIndex >= 0)
            {
                ListViewItem li = this.cboBuildingType.Items[this.cboBuildingType.SelectedIndex] as ListViewItem;
                if (li != null)
                {
                    object o = li.Tag;
                    if (o != null)
                    {
                        string hoursEquivalent = o as string;
                        if (hoursEquivalent != null)
                        {
                            this.txtBuildingHoursEquivalent.Text = hoursEquivalent;
                        }
                    }
                }
            }
        }

        private void cvApptCalendarView_DayViewDateChanged(object sender, DateChangeEventArgs e)
        {
            if (!this.DesignMode)
            {
                Cursor.Current = Cursors.WaitCursor;

                // When navigating off the current day, clear the details.
                this.FillAuditDetails(null);

                Cursor.Current = Cursors.Default;
            }
        }

        private void cvApptCalendarView_ItemClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetNewAudit(false);

            Cursor.Current = Cursors.Default;
        }

        private void cvApptCalendarView_ItemDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetNewAudit(true);

            Cursor.Current = Cursors.Default;
        }

        private void dnavApptDateNav_DateChanged(object sender, DateNavigator.DateChangedEventArgs e)
        {
            if (!this.DesignMode)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.SelectCalendarDate(e.NewStartDate);

                Cursor.Current = Cursors.Default;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._isLoadingForm = true;
                    totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;

                    try
                    {
                        DataStore.LoadInstance();
                    }
                    catch (Exception)
                    {
                        var frm = new frmDataFileRecovery(this);
                        frm.ShowDialog();
                        return;
                    }

                    if (ConfigurationManager.AppSettings[AppConstants.AppKeys.ENV] != AppConstants.Enviorment.DEBUG)
                    {
                        DownloadDataHelper(true);
                    }
                    else
                    {
                        frmMainInitializeAfterDownload();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this._isLoadingForm = false;
            }
        }

        public void frmMainInitializeAfterDownload()
        {
            this.InitializeForm();
            this.CopyUserDocs();
            this.NavigateToDashboardPanel();
            InitMemoryCheckTimer(Constants.MemoryMonitor.MONITOR_INTERVAL);
            Cursor.Current = Cursors.Default;
        }

        private void btnAuditWalkUnits_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetAuditNavButtons(true, false, false, false, false);
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;

            //this.LoadUnits(SelectedAudit);
            if (this.SelectedBuilding != null)
            {
                this.LoadUnits(this.SelectedBuilding);
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnAuditWalkSpaces_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetAuditNavButtons(false, true, false, false, false);
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;

            Cursor.Current = Cursors.Default;
        }

        private void btnAuditWalkSchedules_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetAuditNavButtons(false, false, true, false, false);
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 3;

            Cursor.Current = Cursors.Default;
        }

        private void btnAuditWalkInventory_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetAuditNavButtons(false, false, false, true, false);
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 4;

            Cursor.Current = Cursors.Default;
        }

        private void btnAuditWalkSurvey_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetAuditNavButtons(false, false, false, false, true);
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 0;

            Cursor.Current = Cursors.Default;
        }

        private void btnAuditScheduleSave_Click(object sender, EventArgs e)
        {
            if (this.SelectedBuilding == null)
            {
                MessageBox.Show("Please select a building to assign these schedules to.", "No Building Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                this.SaveAuditSchedule();
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnUnitDetailCancel_Click(object sender, EventArgs e)
        {
            this.tabBuildingUnits.SelectedTabIndex = 0;
            this.ClearUnitDetails();
            this.cboActiveBuilding.Enabled = true;
        }

        private void btnUnitDetailDelete_Click(object sender, EventArgs e)
        {
            this.DeleteUnit();
            SetUnitSummaryView();
        }

        private void btnUnitDetailSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.txtUnitQty.Text))
            {
                this.txtUnitQty.Text = "1";
            }

            if (this.ValidateData())
            {
                this.SaveUnit();
                this.SetUnitSummaryView();
            }
        }

        private bool ValidateData()
        {
            bool result = false;
            string msg = "";

            if (this.lstUnitBedroom.SelectedItems.Count == 0)
            {
                msg = "Please select a bedroom.";
                this.lstUnitBedroom.Focus();
            }
            else if (this.lstUnitBathroom.SelectedItems.Count == 0)
            {
                msg = "Please select a bathroom.";
                this.lstUnitBathroom.Focus();
            }
            else if (this.lstUnitHeating.SelectedItems.Count == 0)
            {
                msg = "Please select a heating item.";
                this.lstUnitHeating.Focus();
            }
            else if (this.lstUnitCooling.SelectedItems.Count == 0)
            {
                msg = "Please select a cooling item.";
                this.lstUnitCooling.Focus();
            }
            else if (this.lstUnitLocation.SelectedItems.Count == 0)
            {
                msg = "Please select a location.";
                this.lstUnitLocation.Focus();
            }
            else if (String.IsNullOrEmpty(this.txtUnitType.Text))
            {
                msg = "Unit Type is required.  Please select an option from each list to fill in this field.";
            }

            if (msg == "")
            {
                result = true;
            }
            else
            {
                MessageBox.Show(msg, "Validate Unit Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }

            return result;
        }

        private void SetUnitSummaryView()
        {
            this.tabBuildingUnits.SelectedTabIndex = 0;
            if (this.SelectedBuilding != null)
            {
                this.LoadUnits(this.SelectedBuilding);
            }
            this.ClearUnitDetails();
            this.cboActiveBuilding.Enabled = true;
        }

        private void ClearUnitDetails()
        {
            this.txtUnitType.Text = "";
            this.txtUnitTypeName.Text = "";
            this.txtUnitSqFt.ResetText();
            this.txtUnitQty.Text = "";
            this.txtUnitId.Text = "";
            ClearListViewSelection(lstUnitBedroom);
            ClearListViewSelection(lstUnitBathroom);
            ClearListViewSelection(lstUnitHeating);
            ClearListViewSelection(lstUnitCooling);
            ClearListViewSelection(lstUnitLocation);
            this.ClearUnitDirectInstallGridData();
        }

        private void ClearListViewSelection(ListViewEx listView)
        {
            foreach (ListViewItem item in listView.Items)
            {
                item.Selected = false;
            }
        }

        private void btnUnitAdd_Click(object sender, EventArgs e)
        {
            this.ClearUnitDetails();
            this.tabBuildingUnits.SelectedTabIndex = 1;
            this.cboActiveBuilding.Enabled = false;
            this.txtUnitQty.Text = "1";
            this.txtUnitSqFt.ResetText();
        }

        private void SaveUnitsGrid()
        {
            if (this.cboUnitNumbering.SelectedIndex >= 0)
            {
                this.SelectedBuilding.UnitNumbering = this.cboUnitNumbering.Items[this.cboUnitNumbering.SelectedIndex].ToString();
            }
            else
            {
                this.SelectedBuilding.UnitNumbering = "";
            }
            if (!(String.IsNullOrEmpty(this.txtUnitNumberOfUnits.Text)))
            {
                this.SelectedBuilding.NumberOfUnits = Convert.ToInt32(this.txtUnitNumberOfUnits.Text.ToString());
            }
            else
            {
                this.SelectedBuilding.NumberOfUnits = 0;
            }
            this.SaveAllUnits();
            DataStore.SaveBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
        }

        private void btnUnitSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SaveUnitsGrid();
            Cursor.Current = Cursors.Default;
        }

        private void cboUnitNumbering_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._unitNumberingChangedByUser)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.cboUnitNumbering.SelectedIndex >= 0)
                {
                    this.SelectedBuilding.UnitNumbering = this.cboUnitNumbering.Items[this.cboUnitNumbering.SelectedIndex].ToString();
                }
                else
                {
                    this.SelectedBuilding.UnitNumbering = "";
                }
                DataStore.SaveBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnBuildingAdd_Click(object sender, EventArgs e)
        {
            // Add a new, blank building.
            this.AddNewBuilding();
            if (this.SelectedAudit.ProgramType.Equals(FieldTool.Constants.ProgramType.MultiFamily))
            {
                this.btnAuditWalkUnits.Enabled = this.SelectedAudit.Buildings.Count > 0;
                if (this.btnAuditWalkUnits.Enabled)
                {
                    this.SetAuditNavButtons(true, false, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                }
                else
                {
                    this.SetAuditNavButtons(false, true, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;
                }
            }
        }

        private void btnBuildingCancel_Click(object sender, EventArgs e)
        {
            this.CancelBuilding();
        }

        private void btnBuildingCopy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon!", "Not yet implemented", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void btnBuildingDelete_Click(object sender, EventArgs e)
        {
            this.DeleteExistingBuilding();
            if (this.SelectedAudit.ProgramType.Equals(FieldTool.Constants.ProgramType.MultiFamily))
            {
                this.btnAuditWalkUnits.Enabled = this.SelectedAudit.Buildings.Count > 0;
                if (this.btnAuditWalkUnits.Enabled)
                {
                    this.SetAuditNavButtons(true, false, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                }
                else
                {
                    this.SetAuditNavButtons(false, true, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;
                }
            }
        }

        private void btnBuildingEdit_Click(object sender, EventArgs e)
        {
            this.EditExistingBuilding();
        }

        private void btnBuildingSave_Click(object sender, EventArgs e)
        {
            this.SaveBuilding();
            if (this.SelectedAudit.ProgramType.Equals(FieldTool.Constants.ProgramType.MultiFamily))
            {
                this.btnAuditWalkUnits.Enabled = this.SelectedAudit.Buildings.Count > 0;
                if (this.btnAuditWalkUnits.Enabled)
                {
                    this.SetAuditNavButtons(true, false, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                }
                else
                {
                    this.SetAuditNavButtons(false, true, false, false, false);
                    this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;
                }
            }
        }

        private void btnEipDecrement_Click(object sender, EventArgs e)
        {
            ChangeEquipmentQuantity(-1);
        }

        private void btnEipDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int index = this.grdEipItems.ActiveSheet.ActiveRowIndex;

            string equipmentName = this.grdEipItems.ActiveSheet.Cells[index, EIP_GRID_COLUMN_NAME].Text;

            string msg = "Are you sure you want to delete the selected equipment (" + equipmentName +
                   ")?\n\nIf you delete this equipment, it will be permanently removed from all related recommendations and they will be recalculated.\n\n" +
                   "Click the Yes button to permanently remove this equipment.  Click the No button to cancel this action without removing the equipment.";

            DialogResult r = MessageBox.Show(msg, "Delete Selected Equipment", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (r == DialogResult.Yes)
            {
                object tag = this.grdEipItems.ActiveSheet.ActiveRow.Tag;

                if (tag != null)
                {
                    EquipmentMaster equipment = tag as EquipmentMaster;
                    if (equipment != null)
                    {
                        this.grdEipItems.ActiveSheet.RemoveRows(index, 1);

                        if (this.SelectedAudit != null && this.SelectedBuilding != null)
                        {
                            DataStore.RemoveEquipmentFromBuilding(this.SelectedAudit.Id, this.SelectedBuilding.Id, equipment, true);

                            this.recRecommendations.RemoveEquipment(equipment, this.SelectedAudit.Id);
                        }
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnEipEdit_Click(object sender, EventArgs e)
        {
            this.ItemEditHelper();
        }

        private void grdEipItems_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            int activeIndex = grdEipItems.ActiveSheet.ActiveRowIndex;
            int actualIndex = e.Row;

            this.ItemEditHelper();
        }

        private void btnEipHeating_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.OpenEaForm(Enumerations.EnergyType.Heating, 1);

            Cursor.Current = Cursors.Default;
        }

        private void btnEipCooling_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.OpenEaForm(Enumerations.EnergyType.Cooling, 15);

            Cursor.Current = Cursors.Default;
        }

        private void btnEipIncrement_Click(object sender, EventArgs e)
        {
            ChangeEquipmentQuantity(1);
        }

        private void ChangeEquipmentQuantity(int offset)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.grdEipItems != null & this.grdEipItems.ActiveSheet != null && this.grdEipItems.ActiveSheet.Cells != null && this.grdEipItems.ActiveSheet.ActiveRow != null &&
                 this.grdEipItems.ActiveSheet.ActiveRowIndex >= 0 && this.grdEipItems.ActiveSheet.Cells[this.grdEipItems.ActiveSheet.ActiveRowIndex, EIP_GRID_COLUMN_QUANTITY] != null)
            {
                string sQty = this.grdEipItems.ActiveSheet.Cells[this.grdEipItems.ActiveSheet.ActiveRowIndex, EIP_GRID_COLUMN_QUANTITY].Text;

                if (String.IsNullOrWhiteSpace(sQty))
                {
                    sQty = "1";
                }

                int qty = 0;

                if (int.TryParse(sQty, out qty))
                {
                    qty += offset;

                    if (qty <= 0)
                    {
                        qty = 1;
                    }
                }

                this.grdEipItems.ActiveSheet.Cells[this.grdEipItems.ActiveSheet.ActiveRowIndex, EIP_GRID_COLUMN_QUANTITY].Text = qty.ToString();

                object tag = this.grdEipItems.ActiveSheet.ActiveRow.Tag;

                if (tag != null)
                {
                    EquipmentMaster equipment = tag as EquipmentMaster;

                    if (equipment != null)
                    {
                        equipment.Quantity = qty;
                        DataStore.UpdateEquipment(equipment, SelectedAudit, SelectedBuilding, true);
                        this.recRecommendations.MergeEquipment(equipment);
                        //DataStore.SaveData();
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnEipLighting_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.OpenEaForm(Enumerations.EnergyType.Lighting, 24);

            Cursor.Current = Cursors.Default;
        }

        private void btnEipDHW_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.OpenEaForm(Enumerations.EnergyType.DomesticWaterHeating, 5225);

            Cursor.Current = Cursors.Default;
        }

        private void btnSpacesSave_Click(object sender, EventArgs e)
        {
            this.SaveAllSpaces();
        }

        private void cboActiveBuilding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this._savingBuilding)
            {
                Cursor.Current = Cursors.WaitCursor;

                object o = this.cboActiveBuilding.SelectedItem;
                if (o != null)
                {
                    ListViewItem li = o as ListViewItem;
                    if (li != null)
                    {
                        object tag = li.Tag;
                        if (tag != null)
                        {
                            this.SelectedBuilding = tag as Building;
                            this.SelectBuildingInList(false);
                        }
                    }
                }

                this.selectedBuildingChanged();
                this.setBuildingListSelectedBuilding();
                Cursor.Current = Cursors.Default;
            }
        }

        private void selectedBuildingChanged()
        {
            this.ClearUnits();
            this.txtUnitNumberOfUnits.Text = "";

            this._unitNumberingChangedByUser = false;
            this.cboUnitNumbering.SelectedIndex = -1;
            this._unitNumberingChangedByUser = true;

            this.LoadUnits(this.SelectedBuilding);
            this.LoadAuditDirectInstallGrids(this.SelectedAudit.ProgramId);
            this.LoadSavedRetrofits(this.SelectedAudit);

            tabControlPanelAuditSurveyQuestions.Controls.Remove(FindControlByName<ucAuditSurvey>(tabControlPanelAuditSurveyQuestions.Controls, AuditUserControlNames.Survey));
            tabControlPanelAuditSurveyReferrals.Controls.Remove(FindControlByName<ucAuditReferral>(tabControlPanelAuditSurveyReferrals.Controls, AuditUserControlNames.Referral));
            tabControlPanelAuditSurveyRecommendations.Controls.Remove(FindControlByName<ucAuditRecommendations>(tabControlPanelAuditSurveyRecommendations.Controls, AuditUserControlNames.Recommendation));

            if (SelectedBuilding != null)
            {
                this.UcAuditSurvey.Initialize();
                this.UcAuditReferral.Init();
                this.UcAuditRecommendations.Init();

                this.UcAuditSurvey.ResizeGrids();
                this.UcAuditReferral.ResizeGrids();
                this.UcAuditRecommendations.ResizeGrids();
                tabAuditSurvey.Enabled = true;

                if (this.UcAuditSurvey.surveyTable.RowCount > 0)
                {
                    tabControlPanelAuditSurveyQuestions.Enabled = true;
                    this.UcAuditSurvey.EnableControls();
                }
                else
                {
                    tabControlPanelAuditSurveyQuestions.Enabled = false;
                    this.UcAuditSurvey.DisableControls();
                }

                if (this.UcAuditReferral.referralTable.rowCount > 0)
                {
                    tabControlPanelAuditSurveyReferrals.Enabled = true;
                    this.UcAuditReferral.referralInit.Table.Enabled = true;
                    this.UcAuditReferral.refResponseInit.Table.Visible = true;
                }
                else
                {
                    tabControlPanelAuditSurveyReferrals.Enabled = false;
                    this.UcAuditReferral.referralInit.Table.Enabled = false;
                    this.UcAuditReferral.refResponseInit.Table.Visible = false;
                }

                if (this.UcAuditRecommendations.recommendationTable.rowCount > 0)
                {
                    tabControlPanelAuditSurveyRecommendations.Enabled = true;
                    this.UcAuditRecommendations.recommendationsInit.Table.Enabled = true;
                    this.UcAuditRecommendations.recResponseInit.Table.Visible = true;
                }
                else
                {
                    tabControlPanelAuditSurveyRecommendations.Enabled = false;
                    this.UcAuditRecommendations.recommendationsInit.Table.Enabled = false;
                    this.UcAuditRecommendations.recResponseInit.Table.Visible = false;
                }
            }
            else
            {
                tabAuditSurvey.Enabled = false;
            }
        }

        private void setBuildingListSelectedBuilding()
        {
            if (!this._savingBuilding)
            {
                if (this.SelectedBuilding != null)
                {
                    for (int x = 0; x < this.lstBuildingList.Items.Count; x++)
                    {
                        Building b = this.lstBuildingList.Items[x].Tag as Building;

                        if (b.Id.Equals(this.SelectedBuilding.Id))
                        {
                            this.lstBuildingList.Items[x].Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void setBuildingComboSelectedBuilding()
        {
            if (!this._savingBuilding)
            {
                this.lblRecommenationSelectedBuilding.Text = "";
                if (this.SelectedBuilding != null)
                {
                    foreach (ListViewItem li in this.cboActiveBuilding.Items)
                    {
                        Building b = li.Tag as Building;

                        if (b.Id.Equals(this.SelectedBuilding.Id))
                        {
                            this.cboActiveBuilding.SelectedItem = li;
                            this.lblRecommenationSelectedBuilding.Text = "Active Building: " + li.Text;
                            break;
                        }
                    }
                }
            }
        }

        private void lstBuildingList_DoubleClick(object sender, EventArgs e)
        {
            //this.SelectAudit();

            if (lstBuildingList.SelectedIndices.Count > 0)
            {
                if (this.lstBuildingList.SelectedItems[0] != null)
                {
                    this.SelectedBuilding = this.lstBuildingList.SelectedItems[0].Tag as Building;
                    this.SelectBuildingInList(false);
                }
            }

            if (this.SelectedBuilding == null)
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.None);
            }
            else
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.Edit);
            }

            this.tabAuditMain.SelectedTabIndex = 0;
            this.selectedBuildingChanged();
            this.setBuildingComboSelectedBuilding();
        }

        private void lstBuildingList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstBuildingList.SelectedItems.Count > 0)
            {
                this.SelectBuildingInListHelper();
            }
            this.selectedBuildingChanged();
            this.setBuildingComboSelectedBuilding();
        }

        private void lstBuildingList_Click(object sender, EventArgs e)
        {
            if (this.lstBuildingList.SelectedItems.Count > 0)
            {
                this.SelectBuildingInListHelper();
            }
            this.selectedBuildingChanged();
            this.setBuildingComboSelectedBuilding();
        }

        [DebuggerStepThrough()]
        private void timer1_Tick(object sender, EventArgs e)
        {
            int index = this.sliHomeImages.SelectedPageIndex;
            index++;
            if (index == 3)
            {
                index = 0;
            }
            this.sliHomeImages.SelectedPageIndex = index;
        }

        private void btnReportRun_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (this.SelectedAudit != null)
                {
                    var program = DataStore.GetProgramByAuditId(SelectedAudit.Id);

                    if ((string.IsNullOrEmpty(txtReportContactName.Text) && string.IsNullOrEmpty(SelectedAudit.CompanyContact)) && ApiSetting.GetByKey<bool>(program.settings, ApiSetting.DataKeys.ContactNameRequiredForReport, false))
                    {
                        MessageBox.Show("Contact name is required");
                    }
                    else
                    {
                        List<string> filters = new List<string>();
                        int checkedItemCount = this.lstCustomReports.CheckedItems.Count;

                        for (int i = 0; i < checkedItemCount; i++)
                        {
                            filters.Add(this.lstCustomReports.CheckedItems[i].Text);
                        }

                        if (filters.Count > 0)
                        {
                            bool isSignOff = false; //Report is not being generated from the Sign Off tab.
                            Company.CompanyCollection coList = DataStore.GetAllCompanies();
                            string reportXMLFilePath = DataStore.XmlReportFile;
                            string reportXMLDirectory = Path.GetDirectoryName(DataStore.XmlReportFile);

                            //JCS 2016_01_15 - Report PDF saved automatically upon running the report
                            string reportFileName = ReportHelper.GetReportFileName(ReportHelper.ReportType.AuditReport, this.SelectedAudit.Id);
                            string reportFilePath = Path.Combine(reportXMLDirectory, reportFileName);

                            switch (this.SelectedAudit.ProgramType)
                            {
                                case FieldTool.Constants.ProgramType.MultiFamily:
                                    if (this.CurrentAuditReportReportBranding.IsFirstEnergyOH)
                                    {
                                        using (frmMultiFamilyCustomFirstEnergyOHReport frm = new frmMultiFamilyCustomFirstEnergyOHReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding))
                                        {
                                            frm.SaveReportAsPDF(reportFilePath);
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        using (frmMultiFamilyReport frm = new frmMultiFamilyReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding))
                                        {
                                            frm.SaveReportAsPDF(reportFilePath);
                                            frm.ShowDialog();
                                        }
                                    }

                                    break;

                                case FieldTool.Constants.ProgramType.Commercial:
                                    if (this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                                    {
                                        using (frmReport frm = new frmReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value))
                                        {
                                            frm.SaveReportAsPDF(reportFilePath);
                                            frm.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        using (frmCommercialReport frm = new frmCommercialReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.SelectedAuditCommercialReportBranding, this.numReportecommendations.Value))
                                        {
                                            frm.SaveReportAsPDF(reportFilePath);
                                            frm.ShowDialog();
                                        }
                                    }

                                    break;

                                default:
                                    using (frmReport frm = new frmReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value))
                                    {
                                        frm.SaveReportAsPDF(reportFilePath);
                                        frm.ShowDialog();
                                    }

                                    break;
                            }

                            DataStore.SaveAuditReportProperties(this.SelectedAudit.Id, filters, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text,
                                this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnReportEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.SelectedAudit != null)
                {
                    var program = DataStore.GetProgramByAuditId(SelectedAudit.Id);

                    if (string.IsNullOrEmpty(txtReportContactName.Text) && ApiSetting.GetByKey<bool>(program.settings, ApiSetting.DataKeys.ContactNameRequiredForReport, false))
                    {
                        MessageBox.Show("Contact name is required");
                    }
                    else
                    {
                        List<string> filters = new List<string>();
                        int checkedItemCount = this.lstCustomReports.CheckedItems.Count;

                        for (int i = 0; i < checkedItemCount; i++)
                        {
                            filters.Add(this.lstCustomReports.CheckedItems[i].Text);
                        }

                        if (filters.Count > 0)
                        {
                            EmailHelper emailHelper = new EmailHelper();
                            EmailMessage message = emailHelper.buildAssessmentReportEmailMessage(this.SelectedAudit.Id);
                            bool isSignOff = false; //Report is not being generated from the Sign Off tab.
                            Company.CompanyCollection coList = DataStore.GetAllCompanies();
                            string reportXMLFilePath = DataStore.XmlReportFile;
                            switch (this.SelectedAudit.ProgramType)
                            {
                                case FieldTool.Constants.ProgramType.MultiFamily:
                                    if (this.CurrentAuditReportReportBranding.IsFirstEnergyOH)
                                    {
                                        message.attachmentFilePath = emailHelper.createMultiFamilyFirstEnergyReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding);
                                    }
                                    else
                                    {
                                        message.attachmentFilePath = emailHelper.createMultiFamilyReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding);
                                    }
                                    break;

                                case FieldTool.Constants.ProgramType.Commercial:
                                    if (this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                                    {
                                        message.attachmentFilePath = emailHelper.createReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value);
                                    }
                                    else
                                    {
                                        message.attachmentFilePath = emailHelper.createCommercialReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.SelectedAuditCommercialReportBranding, this.numReportecommendations.Value);
                                    }
                                    break;

                                default:
                                    message.attachmentFilePath = emailHelper.createReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value);
                                    break;
                            }
                            if (message.isAttachmentExists())
                            {
                                var frm = new frmSendEmail("DevExpress Style", message, UserSettings.GetUserSettings().Email, null);
                                frm.ShowDialog();

                                //emailHelper.outlookDisplayEmail(message);
                                DataStore.SaveAuditReportProperties(this.SelectedAudit.Id, filters, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text,
                                    this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, true);
                            }
                            else
                            {
                                MessageBox.Show("Unable to create audit report pdf file to attach to the email.", "No Report File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnConsumersEnergyAutoEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.SelectedAudit != null)
                {
                    var program = DataStore.GetProgramByAuditId(SelectedAudit.Id);

                    if (string.IsNullOrEmpty(txtReportContactName.Text) && ApiSetting.GetByKey<bool>(program.settings, ApiSetting.DataKeys.ContactNameRequiredForReport, false))
                    {
                        MessageBox.Show("Contact name is required");
                    }
                    else
                    {
                        List<string> filters = new List<string>();
                        int checkedItemCount = this.lstCustomReports.CheckedItems.Count;

                        for (int i = 0; i < checkedItemCount; i++)
                        {
                            filters.Add(this.lstCustomReports.CheckedItems[i].Text);
                        }

                        if (filters.Count > 0)
                        {
                            EmailHelper emailHelper = new EmailHelper();
                            EmailMessage message = emailHelper.buildConsumersEnergyEmailMessage(this.SelectedAudit.Id);
                            bool isSignOff = false; //Report is not being generated from the Sign Off tab.
                            Company.CompanyCollection coList = DataStore.GetAllCompanies();
                            string reportXMLFilePath = DataStore.XmlReportFile;
                            message.attachmentFilePath = emailHelper.createReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value);
                            if (message.isAttachmentExists())
                            {
                                string emailTo;
                                string emailCc;
                                string emailBcc;

                                DialogResult emailDialogResult = ShowEmailDialog("Verify or enter the email address where the assessment report should be sent",
                                    message.emailTo,
                                    out emailTo,
                                    out emailCc,
                                    out emailBcc
                                    );

                                if (emailDialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    message.emailTo = emailTo;
                                    message.emailCc = emailCc;
                                    message.emailBcc = emailBcc;

                                    if (emailHelper.smtpSendEmail(message))
                                    {
                                        DataStore.UpdateReportEmailSent(this.SelectedAudit.Id, true, true);
                                        MessageBox.Show("Email successfully sent.");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Unable to send email.");
                                    }
                                }
                                DataStore.SaveAuditReportProperties(this.SelectedAudit.Id, filters, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text,
                                    this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, true);
                            }
                            else
                            {
                                MessageBox.Show("Unable to create audit report pdf file to attach to the email.", "No Report File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSignatureReportRun_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.SelectedAudit != null)
                {
                    List<string> filters = new List<string>();
                    int checkedItemCount = this.lstCustomReports.CheckedItems.Count;
                    filters.Add("Signature");
                    bool isSignOff = true; //Report is being generated from the Sign Off tab.
                    Company.CompanyCollection coList = DataStore.GetAllCompanies();
                    string reportXMLFilePath = DataStore.XmlReportFile;
                    switch (this.SelectedAudit.ProgramType)
                    {
                        case FieldTool.Constants.ProgramType.MultiFamily:
                            if (this.CurrentAuditReportReportBranding.IsFirstEnergyOH)
                            {
                                using (frmMultiFamilyCustomFirstEnergyOHReport frm = new frmMultiFamilyCustomFirstEnergyOHReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding))
                                {
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                using (frmMultiFamilyReport frm = new frmMultiFamilyReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding))
                                {
                                    frm.ShowDialog();
                                }
                            }

                            break;

                        case FieldTool.Constants.ProgramType.Commercial:
                            if (this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                            {
                                using (frmReport frm = new frmReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value))
                                {
                                    frm.ShowDialog();
                                }
                            }
                            else
                            {
                                using (frmCommercialReport frm = new frmCommercialReport(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.SelectedAuditCommercialReportBranding, this.numReportecommendations.Value))
                                {
                                    frm.ShowDialog();
                                }
                            }

                            break;

                        default:
                            using (frmReport frm = new frmReport(DataStore.UserFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value))
                            {
                                frm.SaveReportAsPDF(ReportHelper.GetReportFilePath(ReportHelper.ReportType.SignatureReport, reportXMLFilePath, this.SelectedAudit.Id));
                                frm.ShowDialog();
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnSignatureReportEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.SelectedAudit != null)
                {
                    List<string> filters = new List<string>();
                    int checkedItemCount = this.lstCustomReports.CheckedItems.Count;
                    filters.Add("Signature");
                    EmailHelper emailHelper = new EmailHelper();
                    EmailMessage message = emailHelper.buildSignatureReportEmailMessage(this.SelectedAudit.Id);
                    bool isSignOff = true; //Report is being generated from the Sign Off tab.
                    Company.CompanyCollection coList = DataStore.GetAllCompanies();
                    string reportXMLFilePath = DataStore.XmlReportFile;
                    switch (this.SelectedAudit.ProgramType)
                    {
                        case FieldTool.Constants.ProgramType.MultiFamily:
                            if (this.CurrentAuditReportReportBranding.IsFirstEnergyOH)
                            {
                                message.attachmentFilePath = emailHelper.createMultiFamilyFirstEnergyReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding);
                            }
                            else
                            {
                                message.attachmentFilePath = emailHelper.createMultiFamilyReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding);
                            }

                            break;

                        case FieldTool.Constants.ProgramType.Commercial:
                            if (this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                            {
                                message.attachmentFilePath = emailHelper.createReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value);
                            }
                            else
                            {
                                message.attachmentFilePath = emailHelper.createCommercialReportFileAttachment(this._userFullName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.SelectedAuditCommercialReportBranding, this.numReportecommendations.Value);
                            }
                            break;

                        default:
                            message.attachmentFilePath = emailHelper.createReportFileAttachment(message.attachmentFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, this.numReportecommendations.Value);
                            break;
                    }
                    if (message.isAttachmentExists())
                    {
                        var frm = new frmSendEmail("DevExpress Style", message, UserSettings.GetUserSettings().Email, null);
                        frm.ShowDialog();
                        //emailHelper.outlookDisplayEmail(message);
                    }
                    else
                    {
                        MessageBox.Show("Unable to create audit report pdf file to attach to the email.", "No Report File", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnEfficiencyNavigatorEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (this.SelectedAudit != null)
                {
                    var company = DataStore.GetCompanyByAudit(this.SelectedAudit.Id);
                    string companyEmail = company != null ? company.Email : string.Empty;
                    EfficiencyNavigatorEmailDialogResult dialogResult = ShowEfficiencyNavigatorRegistrationEmailDialog(companyEmail, company.SendRegistrationEmail);
                    if (dialogResult.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        if (dialogResult.SendRegistrationEmail)
                        {
                            company.Email = dialogResult.EmailAddress;
                            company.SendRegistrationEmail = true;
                        }
                        else
                        {
                            company.SendRegistrationEmail = false;
                        }

                        DataStore.UpdateCompany(company, true);
                    }
                }
                else
                {
                    MessageBox.Show("No audit has been selected.", "Audit Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnHomeUpload_Click(object sender, EventArgs e)
        {
            this.uploadAuditData();
        }

        private void DownloadDataHelper(bool isAppLoading)
        {
            if (Constants.Utilities.IsNetworkAvailable())
            {
                Cursor.Current = Cursors.WaitCursor;
                var frmDownloading = new frmDownloadingData(this, _userEmail);

                try
                {
                    frmDownloading.ShowDialog();
                    Lg.Info("--- Completed Branding Download and unpacking ---");
                }
                finally
                {
                    this.lblStatusText.Text = "READY";
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                Lg.Info("No network connection.  Skipping check and update.");

                if (!isAppLoading)
                {
                    MessageBox.Show("You are not connected to the Franklin Energy network.", "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnHomeDownload_Click(object sender, EventArgs e)
        {
            this.DownloadDataHelper(false);
        }

        private void btnHomeSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon!", "Not yet implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHomeHelp_Click(object sender, EventArgs e)
        {
            using (frmHelpNew frm = new frmHelpNew(this))
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                DialogResult dlg = frm.ShowDialog();

                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            //using (frmHelp frm = new frmHelp(this))
            //{
            //    DialogResult dlg = frm.ShowDialog();

            //    if (dlg == System.Windows.Forms.DialogResult.OK)
            //    {
            //    }
            //}
        }

        private void sigSignOffSignature_Signed(object sender, EventArgs e)
        {
            if (this.SelectedAudit == null)
            {
                MessageBox.Show("Please select an audit from which to sign off.", "No Audit Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;

                string signatureAsXml = "";
                string filename = "sig" + this.SelectedAudit.Id + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".xml";

                Microsoft.TabletPC.Samples.InkSecureSignature.InkSecureSignature obj = sender as Microsoft.TabletPC.Samples.InkSecureSignature.InkSecureSignature;
                if (obj != null)
                {
                    signatureAsXml = obj.Value;

                    if (!String.IsNullOrWhiteSpace(signatureAsXml))
                    {
                        //Save signature to a separate signature xml file
                        string signatureXmlFilePath = Path.Combine(DataStore.XmlSignatureDirectory, filename);
                        using (StreamWriter writer = new StreamWriter(signatureXmlFilePath))
                        {
                            using (TextWriter ssw = TextWriter.Synchronized(writer))
                            {
                                ssw.Write(signatureAsXml);
                            }
                        }

                        var sig = new InkSecureSignatureData();
                        sig.Serialize(signatureAsXml);
                        this.SelectedAudit.InkSecureSignatureData = sig;

                        DataStore.SaveData();

                        //JCS 2016_01_15 - Signature PDF saved automatically upon saving the sig
                        string reportXMLFilePath = DataStore.XmlReportFile;
                        var reportXMLDirectory = Path.GetDirectoryName(DataStore.XmlReportFile);
                        var signatureFileName = ReportHelper.GetReportFileName(ReportHelper.ReportType.SignatureReport, this.SelectedAudit.Id);
                        string signatureReportFilePath = Path.Combine(reportXMLDirectory, signatureFileName);
                        List<string> filters = new List<string>();
                        int checkedItemCount = this.lstCustomReports.CheckedItems.Count;
                        filters.Add("Signature");
                        EmailHelper emailHelper = new EmailHelper();
                        bool isSignOff = true; //Report is being generated from the Sign Off tab.
                        int numRecommendations = 0;  //No recommendations in signoff report.
                        Company.CompanyCollection coList = DataStore.GetAllCompanies();
                        switch (this.SelectedAudit.ProgramType)
                        {
                            case FieldTool.Constants.ProgramType.MultiFamily:
                                emailHelper.createMultiFamilyReportFileAttachment(signatureFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, this.txtTopRecommendation.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAuditMultiFamilyReportBranding);
                                break;

                            default:
                                emailHelper.createReportFileAttachment(signatureFileName, this.SelectedAudit.Id, this.txtReportCompanyName.Text, this.txtReportContactName.Text, this.txtReportEaName.Text, this.txtEnergyAdvisorPhone.Text, this.txtEnergyAdvisorEmail.Text, filters, isSignOff, coList, reportXMLFilePath, this.SelectedAudit.CompanyContact, numRecommendations);
                                break;
                        }
                        MessageBox.Show("Signature saved for audit: " + this.SelectedAudit.Name, "Save Signature", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnRunSignatureReport.Enabled = true;
                        btnEmailSignatureReport.Enabled = true;
                    }
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void sigSignOffSignature_Click(object sender, EventArgs e)
        {
            this.sigSignOffSignature.Reset();
            DataStore.DeleteSignatureFromAudit(this.SelectedAudit.Id, true);
            btnRunSignatureReport.Enabled = false;
            btnEmailSignatureReport.Enabled = false;
        }

        private void btnAddAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.lstAvailableSpaces.BeginUpdate();
            this.lstSelectedSpaces.BeginUpdate();

            foreach (ListViewItem item in this.lstAvailableSpaces.Items)
            {
                this.lstAvailableSpaces.Items.Remove(item);
                this.lstSelectedSpaces.Items.Add(item);
            }

            this.lstSelectedSpaces.EndUpdate();
            this.lstAvailableSpaces.EndUpdate();

            this.SetButtonsEnabled();

            Cursor.Current = Cursors.Default;
        }

        private void btnAddOne_Click(object sender, EventArgs e)
        {
            if (this.lstAvailableSpaces.SelectedItems.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.lstAvailableSpaces.BeginUpdate();
                this.lstSelectedSpaces.BeginUpdate();

                foreach (ListViewItem item in this.lstAvailableSpaces.SelectedItems)
                {
                    this.lstAvailableSpaces.Items.Remove(item);
                    this.lstSelectedSpaces.Items.Add(item);
                }

                this.lstSelectedSpaces.EndUpdate();
                this.lstAvailableSpaces.EndUpdate();

                this.SetButtonsEnabled();

                Cursor.Current = Cursors.Default;
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.lstAvailableSpaces.BeginUpdate();
            this.lstSelectedSpaces.BeginUpdate();

            foreach (ListViewItem item in this.lstSelectedSpaces.Items)
            {
                this.lstSelectedSpaces.Items.Remove(item);
                this.lstAvailableSpaces.Items.Add(item);
            }

            this.lstSelectedSpaces.EndUpdate();
            this.lstAvailableSpaces.EndUpdate();

            this.SetButtonsEnabled();

            Cursor.Current = Cursors.Default;
        }

        private void btnRemoveOne_Click(object sender, EventArgs e)
        {
            if (this.lstSelectedSpaces.SelectedItems.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                this.lstAvailableSpaces.BeginUpdate();
                this.lstSelectedSpaces.BeginUpdate();

                foreach (ListViewItem item in this.lstSelectedSpaces.SelectedItems)
                {
                    this.lstSelectedSpaces.Items.Remove(item);
                    this.lstAvailableSpaces.Items.Add(item);
                }

                this.lstSelectedSpaces.EndUpdate();
                this.lstAvailableSpaces.EndUpdate();

                this.SetButtonsEnabled();

                Cursor.Current = Cursors.Default;
            }
        }

        private void lstAvailableSpaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtonsEnabled();
        }

        private void lstSelectedSpaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetButtonsEnabled();
        }

        private void grdDirectInstall_CellClick(object sender, CellClickEventArgs e)
        {
            this.ChangeDirectInstallQuantity(e, false, false);
        }

        private void grdUnitDirectInstall_CellClick(object sender, CellClickEventArgs e)
        {
            this.ChangeDirectInstallQuantity(e, false, true);
        }

        private void ChangeDirectInstallQuantity(CellClickEventArgs e, bool isManualEdit, bool isMultiFamily)
        {
            // Column indices are 1-based!

            int incrementColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_INCREMENT_QTY : DI_GRID_COLUMN_INCREMENT_QTY);
            int decrementColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_DECREMENT_QTY : DI_GRID_COLUMN_DECREMENT_QTY);
            int quantityColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_QUANTITY : DI_GRID_COLUMN_QUANTITY);

            //int currentQty = Convert.ToInt32(e.View.Sheets[0].Cells[e.Row, quantityColumnIndex].Value);
            double currentQty = Convert.ToDouble(e.View.Sheets[0].Cells[e.Row, quantityColumnIndex].Value);

            Cursor.Current = Cursors.WaitCursor;

            if (isManualEdit)
            {
                // Quantity column
                if (e.Column == quantityColumnIndex)
                {
                    this.CalculateDirectInstallValues(e, currentQty, false);
                }
            }
            else
            {
                if (e.Column == incrementColumnIndex || e.Column == decrementColumnIndex)
                {
                    if (e.View.Sheets[0].Columns[e.Column].Tag.ToString() == "Add")
                    {
                        currentQty++;
                    }
                    else
                    {
                        currentQty--;

                        if (currentQty < 0)
                        {
                            currentQty = 0;
                        }
                    }

                    this.CalculateDirectInstallValues(e, currentQty, true);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void CalculateDirectInstallValues(CellClickEventArgs e, double currentQuantity, bool isMultiFamily)
        {
            int kwhColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_KWH : DI_GRID_COLUMN_KWH);
            int kwhUnitColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_KWH_UNIT : DI_GRID_COLUMN_KWH_UNIT);
            int thermsColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_THERMS : DI_GRID_COLUMN_THERMS);
            int thermsUnitColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_THERMS_UNIT : DI_GRID_COLUMN_THERMS_UNIT);
            int quanityColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_QUANTITY : DI_GRID_COLUMN_QUANTITY);
            int savingsColumnIndex = (isMultiFamily ? UNIT_DI_GRID_COLUMN_SAVINGS : DI_GRID_COLUMN_SAVINGS);

            double kwhUnit = Convert.ToDouble(e.View.Sheets[0].Cells[e.Row, kwhUnitColumnIndex].Value);
            e.View.Sheets[0].Cells[e.Row, kwhColumnIndex].Value = kwhUnit * currentQuantity;
            double mcfUnit = Convert.ToDouble(e.View.Sheets[0].Cells[e.Row, thermsUnitColumnIndex].Value);
            e.View.Sheets[0].Cells[e.Row, thermsColumnIndex].Value = mcfUnit * currentQuantity;
            double saving = currentQuantity * ((8 * mcfUnit) + (.1 * kwhUnit));

            e.View.Sheets[0].Cells[e.Row, quanityColumnIndex].Value = currentQuantity;
            e.View.Sheets[0].Cells[e.Row, savingsColumnIndex].Value = saving;
        }

        private void grdUnits_CellClick(object sender, CellClickEventArgs e)
        {
            int iCurrRow;
            int iCurrCol;
            int CurrentQty = 0;

            iCurrRow = e.Row;
            iCurrCol = e.Column;

            // Only adjust the quantity if there's data in this row.
            if (e.View.Sheets[0].Cells[iCurrRow, 0].Value != null)
            {
                if (!e.ColumnHeader)
                {
                    if (iCurrCol == 3 || iCurrCol == 5)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        CurrentQty = Convert.ToInt32(e.View.Sheets[0].Cells[iCurrRow, 4].Value);

                        if (e.View.Sheets[0].Columns[iCurrCol].Tag.ToString() == "Add")
                        {
                            CurrentQty++;
                        }
                        else
                        {
                            if (CurrentQty > 0)
                            {
                                CurrentQty--;
                            }
                        }
                        e.View.Sheets[0].Cells[iCurrRow, 4].Value = CurrentQty;

                        BuildingUnitType b = this.GetSelectedBuildingUnitType(e.Row);
                        if (b != null)
                        {
                            b.UnitTypeQty = CurrentQty;
                            DataStore.SaveBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
                        }

                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        private void grdUnits_EditModeStarting(object sender, EditModeStartingEventArgs e)
        {
            if (this.grdUnits_Sheet1.Cells[this.grdUnits_Sheet1.ActiveRowIndex, 0].Value == null)
            {
                e.Cancel = true;
            }
        }

        private void grdUnits_Change(object sender, ChangeEventArgs e)
        {
            // Quantity column
            if (e.Column == 4)
            {
                Cursor.Current = Cursors.WaitCursor;

                BuildingUnitType b = this.GetSelectedBuildingUnitType(e.Row);
                if (b != null)
                {
                    int qty = Convert.ToInt32(e.View.Sheets[0].Cells[e.Row, 4].Value);
                    b.UnitTypeQty = qty;
                    DataStore.SaveBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void grdDirectInstall_Change(object sender, ChangeEventArgs e)
        {
            CellClickEventArgs c = new CellClickEventArgs(e.View, e.Row, e.Column, Cursor.Position.X, Cursor.Position.Y, MouseButtons.Left, false, false);
            this.ChangeDirectInstallQuantity(c, true, false);
        }

        private void grdUnitDirectInstall_Change(object sender, ChangeEventArgs e)
        {
            CellClickEventArgs c = new CellClickEventArgs(e.View, e.Row, e.Column, Cursor.Position.X, Cursor.Position.Y, MouseButtons.Left, false, false);
            this.ChangeDirectInstallQuantity(c, true, true);
        }

        private BuildingUnitType GetSelectedBuildingUnitType(int rowIndex)
        {
            BuildingUnitType result = null;

            if (this.SelectedBuilding != null && this.SelectedBuilding.MultiFamily != null)
            {
                string unitId = this.grdUnits_Sheet1.Cells[rowIndex, 6].Text;

                foreach (BuildingUnitType unit in this.SelectedBuilding.MultiFamily.BuildingUnitTypes)
                {
                    if (unit.Id == unitId)
                    {
                        result = unit;
                        break;
                    }
                }
            }

            return result;
        }

        private void grdUnits_ButtonDoubleClicked(object sender, CellClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // don't do this if they checked plus or minus or are clicking on column header
            if (e.Column != 3 && e.Column != 5 && !e.ColumnHeader)
            {
                BuildingUnitType b = this.GetSelectedBuildingUnitType(e.Row);
                if (b != null)
                {
                    this.txtUnitId.Text = b.Id;
                    this.tabBuildingUnits.SelectedTabIndex = 1;
                    this.txtUnitType.Text = b.UnitType;
                    this.txtUnitTypeName.Text = b.UnitTypeName;
                    this.txtUnitSqFt.Value = b.SquareFeet;
                    this.txtUnitQty.Text = b.UnitTypeQty.ToString();
                    this.cboActiveBuilding.Enabled = false;
                    this.SetListViewSelectedValueFromText(lstUnitBedroom, b.NumBedrooms.ToString());
                    this.SetListViewSelectedValueFromText(lstUnitBathroom, b.NumBathrooms.ToString());
                    this.SetListViewSelectedValueFromText(lstUnitHeating, b.HeatingType);
                    this.SetListViewSelectedValueFromText(lstUnitCooling, b.CoolingType);
                    this.SetListViewSelectedValueFromText(lstUnitLocation, b.Location);
                    this.LoadSavedUnitRetrofits(b);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void SetListViewSelectedValueFromText(ListViewEx listView, string itemText)
        {
            if (listView != null && !String.IsNullOrWhiteSpace(itemText))
            {
                ListViewItem li = listView.FindItemWithText(itemText.Trim());
                if (li != null)
                {
                    li.Selected = true;
                }
            }
        }

        private void btnClearDI_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ClearAuditDirectInstallGridData();
            Cursor.Current = Cursors.Default;
        }

        private void btnSaveDI_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SaveDI();
            Cursor.Current = Cursors.Default;
        }

        private void btnElectricHistorySave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SaveElectricHistory();
            Cursor.Current = Cursors.Default;
        }

        private void btnGasHistorySave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SaveGasHistory();
            Cursor.Current = Cursors.Default;
        }

        private void btnElectricHistoryCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ClearElectricHistory();
            Cursor.Current = Cursors.Default;
        }

        private void btnGasHistoryCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ClearGasHistory();
            Cursor.Current = Cursors.Default;
        }

        private void btnClearGasHistory_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.ClearGasHistory();
            Cursor.Current = Cursors.Default;
        }

        private void btnUtilitiesSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this._savingEnergyUseIndex = true;

            DataStore.DeleteHistory(this.SelectedAudit.Id, "EUI");

            EnergyUsageIndex EUI = new BLL.EnergyUsageIndex();

            EUI.EUIDescription = fpSpread1_Sheet1.Cells[2, 0].Value.ToString();
            EUI.EUIRankText = fpSpread1_Sheet1.Cells[6, 1].Value.ToString();
            EUI.FacilityType = fpSpread1_Sheet1.Cells[8, 7].Value.ToString();
            EUI.TargetCost = Convert.ToDouble(fpSpread1_Sheet1.Cells[32, 8].Value.ToString());
            EUI.TargetSavings = Convert.ToDouble(fpSpread1_Sheet1.Cells[33, 8].Value.ToString());
            EUI.TargetUsage = Convert.ToDouble(fpSpread1_Sheet1.Cells[31, 8].Value.ToString());
            EUI.TotalGasUsage = Convert.ToDouble(fpSpread1_Sheet1.Cells[10, 7].Value.ToString());
            EUI.TotalElectricUsage = Convert.ToDouble(fpSpread1_Sheet1.Cells[9, 7].Value.ToString());
            EUI.TotalEnergyCosts = Convert.ToDouble(fpSpread1_Sheet1.Cells[11, 7].Value.ToString());
            EUI.TotalFloorArea = Convert.ToDouble(fpSpread1_Sheet1.Cells[12, 7].Value.ToString());
            EUI.YourCost = Convert.ToDouble(fpSpread1_Sheet1.Cells[33, 3].Value.ToString());
            EUI.YourUsage = Convert.ToDouble(fpSpread1_Sheet1.Cells[32, 3].Value.ToString());
            EUI.AverageUsage = Convert.ToDouble(fpSpread1_Sheet1.Cells[33, 3].Value.ToString());

            this.SelectedBuilding.AddEnergyUsagehistory(EUI, true);

            if (grdElectricHist_Sheet1.Rows.Count > 0)
            {
                //save chart image
                SaveChartImage("Electric");
            }

            if (fpSpread1_Sheet1.Cells[8, 7].Text.ToString() != "")//EUI gauge- sorry about the grid name 'fpSpread1'.  It wasn't saving the correct chart name in the designer and this was rushed
            {
                SaveChartImage("EUI");
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnEUIRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool bFound = false;

            int fRow = 0;
            // int fCol=0;
            string strType = "";
            string strValue = "";

            //Get Faciltiy Type

            // strType =GetFieldValue("EUI_Facility_Type");
            // Get Facility Calculated Use value
            strType = fpSpread1_Sheet1.Cells[8, 7].Text.ToString();//    GetFieldValue("EUI_Your_Use");
            strValue = fpSpread1_Sheet1.Cells[33, 3].Value.ToString();

            if (strType.Length > 0 && strValue.Length > 0)
            {
                //Find and set the Gauge value
                //With FpSpread1.Sheets("Energy Use Index")

                // Find row for facility type
                for (int i = 56; i <= 73; i++)
                {
                    if (fpSpread1_Sheet1.Cells[i, 0].Text == strType)
                    {
                        fRow = i;
                        bFound = true;
                        //break;
                    }

                    if (bFound)
                    {
                        bFound = false;
                        for (int x = 6; x <= 105; x++)
                        {
                            if (Convert.ToDouble(strValue) >= Convert.ToDouble(fpSpread1_Sheet1.Cells[fRow, x].Text) && Convert.ToDouble(strValue) < Convert.ToDouble(fpSpread1_Sheet1.Cells[fRow, x + 1].Text))
                            {
                                strValue = fpSpread1_Sheet1.Cells[55, x].Text;

                                bFound = true;
                                //   break;
                            }
                        }

                        if (bFound == true)
                        {
                            EUIChartRadialGauge.Value = Convert.ToDouble(strValue);
                        }
                    }

                    // Get an image of the gauge
                    Image image = EUIChartGauge.GetImage();
                    fpSpread1_Sheet1.Cells[15, 3].Value = image;

                    System.Windows.Forms.Clipboard.SetImage(image);
                    string chartfilepath = DataStore.ChartImagePath;
                    string sChartName = "EUI" + this.SelectedAudit.Id + ".bmp";

                    if (!File.Exists(chartfilepath))
                    {
                        Directory.CreateDirectory(chartfilepath.ToString());
                        //  File.Create(filePath).Close();
                    }

                    // Bitmap bmp = new Bitmap(400, 400);

                    //bmp.Save(chartfilepath + "\\" + sChartName);
                    image.Save(chartfilepath + "\\" + sChartName);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void pslHomePageSlider_SelectedPageChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                PageSlider ctl = sender as PageSlider;
                if (ctl != null)
                {
                    switch (ctl.SelectedPageIndex)
                    {
                        case 0:
                            this.SetHomeNavButtons(true, false, false);
                            break;

                        case 1:
                            this.SetHomeNavButtons(false, true, false);
                            break;

                        case 2:
                            this.SetHomeNavButtons(false, false, true);
                            break;
                    }
                }
            }
        }

        private void pslAuditWalkthruPageSlider_SelectedPageChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                PageSlider ctl = sender as PageSlider;
                if (ctl != null)
                {
                    switch (ctl.SelectedPageIndex)
                    {
                        case 0:
                            this.SetAuditNavButtons(false, false, false, false, true);
                            break;

                        case 1:
                            if (this.btnAuditWalkUnits.Enabled)
                            {
                                this.SetAuditNavButtons(true, false, false, false, false);
                                this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                            }
                            else
                            {
                                this.SetAuditNavButtons(false, true, false, false, false);
                                this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;
                            }
                            break;

                        case 2:
                            this.SetAuditNavButtons(false, true, false, false, false);
                            break;

                        case 3:
                            this.SetAuditNavButtons(false, false, true, false, false);
                            break;

                        case 4:
                            this.SetAuditNavButtons(false, false, false, true, false);
                            break;
                    }
                }
            }
        }

        private void btnCopyAddress_Click(object sender, EventArgs e)
        {
            if (this.SelectedAudit != null)
            {
                Cursor.Current = Cursors.WaitCursor;

                Company c = DataStore.GetCompanyByAudit(this.SelectedAudit);

                if (c != null)
                {
                    string msg = "If you press 'Yes' the company's address will be copied into the address fields for this building.  This will replace any address " +
                        "data you currently have for this building.\n\nDo you wish to copy the company address to this building?";
                    DialogResult dlg = MessageBox.Show(msg, "Copy Address", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (dlg == DialogResult.Yes)
                    {
                        this.txtBuildingAddress1.Text = String.IsNullOrEmpty(c.AddressLine1) ? String.Empty : c.AddressLine1;
                        this.txtBuildingAddress2.Text = String.IsNullOrEmpty(c.AddressLine2) ? String.Empty : c.AddressLine2;
                        this.txtBuildingCity.Text = String.IsNullOrEmpty(c.City) ? String.Empty : c.City;
                        this.cboBuildingState.Text = String.IsNullOrEmpty(c.State) ? String.Empty : c.State;

                        string zip = "";
                        string ext = "";
                        int pos = c.PostalCode.IndexOf("-");
                        if (pos >= 0)
                        {
                            zip = c.PostalCode.Substring(0, pos);
                            ext = c.PostalCode.Substring(pos + 1);
                        }
                        else
                        {
                            zip = c.PostalCode;
                            ext = c.PostalCodeExtension;
                        }
                        this.txtBuildingZip.Text = String.IsNullOrEmpty(zip) ? String.Empty : zip;
                        this.txtBuildingZipExt.Text = String.IsNullOrEmpty(ext) ? String.Empty : ext;
                    }
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void recRecommendations_IncludeInReportValueChanged(object sender, Controls.RecommendationEventArgs e)
        {
            DataStore.UpdateIncludeInReport(e.Item.RecommendationId, e.Item.IncludeInReport, true);
        }

        private void recRecommendations_RankValueChanged(object sender, Controls.RecommendationEventArgs e)
        {
            DataStore.UpdateRank(e.Item.RecommendationId, e.Item.ReportRank, true);
        }

        private void btnChangeRecommendationRanking_Click(object sender, EventArgs e)
        {
            using (frmRecRank frm = new frmRecRank(this.recRecommendations.Items))
            {
                DialogResult dlg = frm.ShowDialog();

                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    this.recRecommendations.SuspendDrawing();

                    foreach (Recommendation rec in frm.Recommendations)
                    {
                        DataStore.UpdateRankAndInclude(rec.RecommendationId, rec.ReportRank, rec.IncludeInReport, rec.IncludeDetailInReport, true);
                    }

                    //this.SelectedBuilding.Recommendations = frm.Recommendations;

                    this.recRecommendations.RepopulateControlByBuildingId(this.SelectedBuilding);
                    this.tabAuditMain.SelectedTabIndex = 0;
                    this.tabAuditMain.SelectedTabIndex = 1;

                    this.recRecommendations.ResumeDrawing();

                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (frmMainContactHelper.IsContactLockingForm)
            {
                MessageBox.Show("Please save the contact before closing.", "Save Contact", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else if (frmMainCompanyHelper.IsLockedForEdit)
            {
                MessageBox.Show("Please save the company before closing.", "Save Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }

            if (e.CloseReason == CloseReason.UserClosing && ConfigurationManager.AppSettings[AppConstants.AppKeys.ENV] != AppConstants.Enviorment.DEBUG)
            {
                DialogResult dialogResult = MessageBox.Show("Do you really want to exit Efficiency Clipboard?", "Exit", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    DataStore.SaveData();
                }
                else
                {
                    e.Cancel = true;
                }
            }

            if (e.Cancel == false)
            {
                Program.IsOnlyApplicationRunning.Dispose();
            }
        }

        private void btnElectricHistoryCancel_Click_1(object sender, EventArgs e)
        {
            this.ClearElectricHistory();
        }

        private void pnlDirectInstall_Leave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SaveDI();
            Cursor.Current = Cursors.Default;
        }

        #endregion Events

        #region Methods

        private T FindControlByName<T>(Control.ControlCollection controlCollection, string controlName)
            where T : Control
        {
            foreach (Control control in controlCollection)
            {
                if (control.Name == controlName && typeof(T).Equals(control.GetType()))
                {
                    return (T)control;
                }
            }

            return null;
        }

        private void LaunchValidateCompanyForm()
        {
            this.LaunchValidateCompanyForm(-1);
        }

        private void LaunchValidateCompanyForm(int selectedRowIndex)
        {
            bool hasUnvalidatedItems = false;

            if (Constants.Utilities.IsNetworkAvailable())
            {
                Cursor.Current = Cursors.WaitCursor;

                FarPoint.Win.Spread.Column colValidateCheck = grdMyWork_Sheet1.GetColumnFromTag(null, "Validate");
                List<Company> unvalidatedCompanies = new List<Company>();

                if (selectedRowIndex < 0)
                {
                    // Get all unvalidated items

                    for (int i = 0; i < this.grdMyWork_Sheet1.Rows.Count; i++)
                    {
                        object o = this.grdMyWork_Sheet1.Rows[i].Tag;
                        TaskList t = o as TaskList;

                        if (t != null && !t.IsValidated)
                        {
                            hasUnvalidatedItems = true;

                            Company c = DataStore.GetCompany(t.CompanyId);

                            if (c != null)
                            {
                                unvalidatedCompanies.Add(c);
                            }
                        }
                    }

                    if (!hasUnvalidatedItems)
                    {
                        MessageBox.Show("All of your audits are already validated.", "Validate Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Get the selected row's item

                    object o = this.grdMyWork_Sheet1.Rows[selectedRowIndex].Tag;
                    TaskList t = o as TaskList;

                    if (t != null && !t.IsValidated)
                    {
                        hasUnvalidatedItems = true;

                        Company c = DataStore.GetCompany(t.CompanyId);

                        if (c != null)
                        {
                            unvalidatedCompanies.Add(c);
                        }
                    }
                }

                if (hasUnvalidatedItems)
                {
                    if (unvalidatedCompanies.Count == 0)
                    {
                        MessageBox.Show("Please select at least one unvalidated company to validate.", "Validate Company", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        using (frmValidationTerms frm = new frmValidationTerms(unvalidatedCompanies, bsiService))
                        {
                            DialogResult result = frm.ShowDialog(this);

                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                this.LoadMyWorkGrid();
                            }
                        }
                    }
                }

                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("You must have a network connection to validate a company.", "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private string sendAllCps(List<BsiCompositeProject> cps)
        {
            // Mark our progress as we go
            var successes = new List<string>();
            var errored = new List<string>();
            // send CP one at a time
            var errorMsgs = new StringBuilder();

            foreach (var cp in cps)
            {
                string errorMsg = bsiService.AddNewCPRecord(cp);
                string msgSucccess = "";
                if (String.Equals(errorMsg, "true", StringComparison.CurrentCultureIgnoreCase))
                {
                    msgSucccess = "created";
                    successes.Add(cp.ProjectId);
                }
                else
                {
                    msgSucccess = "failed";
                    errored.Add(cp.ProjectId);
                    errorMsgs.AppendLine(errorMsg);
                }
                double totalsofar = (errored.Count + successes.Count);
                int percentComplete = (int)Math.Round((totalsofar / cps.Count) * 100);
                this.lblStatusText.Text = String.Format("{0}% complete... Sending Composite Project Record {1} / {2} : {3}",
                    percentComplete,
                    cp.ProjectId,
                    String.IsNullOrEmpty(cp.RetrofitId) ? "---" : cp.RetrofitId,
                    msgSucccess);
            }
            var resultMessage = String.Format("Sent {0}/{1} successful, {2} errored.", successes.Count, cps.Count, errored.Count);

            this.lblStatusText.Text = resultMessage;

            return errorMsgs.ToString();
        }

        private bool SaveElectricHistory()
        {
            var success = true;
            if (this.SelectedAudit != null)
            {
                List<BLL.ElectricHistoryRecord> histories = new List<BLL.ElectricHistoryRecord>();

                for (int i = 3; i <= 14; i++)
                {
                    ElectricHistoryRecord electricHistoryRecord = new BLL.ElectricHistoryRecord();

                    if (this.grdElectricHist_Sheet1.Cells[i, 2].Value != null)
                    {
                        if (this.grdElectricHist_Sheet1.Cells[i, 2] != null && this.grdElectricHist_Sheet1.Cells[i, 2].Value != null)
                        {
                            electricHistoryRecord.ReadDate = Convert.ToDateTime(this.grdElectricHist_Sheet1.Cells[i, 2].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 3] != null && this.grdElectricHist_Sheet1.Cells[i, 3].Value != null)
                        {
                            electricHistoryRecord.BillDays = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 3].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 4] != null && this.grdElectricHist_Sheet1.Cells[i, 4].Value != null)
                        {
                            electricHistoryRecord.CoolDays = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 4].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 5] != null && this.grdElectricHist_Sheet1.Cells[i, 5].Value != null)
                        {
                            electricHistoryRecord.HeatDays = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 5].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 6] != null && this.grdElectricHist_Sheet1.Cells[i, 6].Value != null)
                        {
                            electricHistoryRecord.TotalKwh = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 6].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 7] != null && this.grdElectricHist_Sheet1.Cells[i, 7].Value != null)
                        {
                            electricHistoryRecord.OnPeakKwh = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 7].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 8] != null && this.grdElectricHist_Sheet1.Cells[i, 8].Value != null)
                        {
                            electricHistoryRecord.OffPeakKwh = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 8].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 9] != null && this.grdElectricHist_Sheet1.Cells[i, 9].Value != null)
                        {
                            electricHistoryRecord.BilledKw = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 9].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 10] != null && this.grdElectricHist_Sheet1.Cells[i, 10].Value != null)
                        {
                            electricHistoryRecord.MaximumCustomerKw = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 10].Value);
                        }

                        if (this.grdElectricHist_Sheet1.Cells[i, 11] != null && this.grdElectricHist_Sheet1.Cells[i, 11].Value != null)
                        {
                            electricHistoryRecord.TotalBill = Convert.ToInt32(this.grdElectricHist_Sheet1.Cells[i, 11].Value);
                        }

                        histories.Add(electricHistoryRecord);
                    }
                }

                var uniqueRecords = histories.Select(x => x.ReadDate.Date).Distinct().Count();
                var historyCount = histories.Count();
                var historyKey = "Electric";
                var dupCount = historyCount - uniqueRecords;
                if (uniqueRecords != historyCount)
                {
                    var msg = String.Format("Cannot save {0} histories because there {1} {2} duplicate meter read {3}",
                        historyKey,
                        (dupCount == 1) ? "is" : "are",
                        dupCount,
                        (dupCount == 1) ? "date" : "dates");
                    var caption = string.Format("{0} Duplicate {1} History Records", historyCount - uniqueRecords, historyKey);
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    success = false;
                }
                else
                {
                    DataStore.DeleteHistory(this.SelectedAudit.Id, historyKey);
                    foreach (var record in histories)
                    {
                        this.SelectedBuilding.AddElectricHistory(record, true);
                    }

                    if (grdElectricHist_Sheet1.Rows.Count > 0)
                    {
                        // Save chart image.
                        SaveChartImage(historyKey);
                    }

                    //EUI gauge- sorry about the grid name 'fpSpread1'.  It wasn't saving the correct chart name in the designer and this was rushed
                    if (fpSpread1_Sheet1.Cells[8, 7].Text.ToString() != "")
                    {
                        SaveChartImage("EUI");
                    }
                }
            }

            return success;
        }

        private void SaveAllSpaces()
        {
            Cursor.Current = Cursors.WaitCursor;

            BuildingSpace.BuildingSpaceCollection spaces = new BuildingSpace.BuildingSpaceCollection();

            foreach (ListViewItem li in this.lstSelectedSpaces.Items)
            {
                spaces.Add(new BuildingSpace(li.Text));
            }

            if (this.SelectedBuilding != null)
            {
                DataStore.AddSpacesToBuilding(this.SelectedBuilding.Id, spaces, true);
            }

            Cursor.Current = Cursors.Default;
        }

        private void SaveAllUnits()
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                List<BuildingUnitType> buildingUnitList = new List<BuildingUnitType>();

                for (int i = 0; i < grdUnits_Sheet1.Rows.Count - 1; i++)
                {   //Don't include last row.  Last row is for Unit total.
                    BuildingUnitType unit = new BLL.BuildingUnitType();

                    if (!String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 0].Value)))
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 0].Value)))
                        {
                            unit.UnitType = "";
                        }
                        else
                        {
                            unit.UnitType = grdUnits_Sheet1.Cells[i, 0].Value.ToString();
                        }

                        if (String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 1].Value)))
                        {
                            unit.UnitTypeName = "";
                        }
                        else
                        {
                            unit.UnitTypeName = grdUnits_Sheet1.Cells[i, 1].Value.ToString();
                        }

                        if (String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 2].Value)))
                        {
                            unit.SquareFeet = 0;
                        }
                        else
                        {
                            unit.SquareFeet = Convert.ToDouble(grdUnits_Sheet1.Cells[i, 2].Value.ToString());
                        }

                        if (String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 4].Value)))
                        {
                            unit.UnitTypeQty = 0;
                        }
                        else
                        {
                            unit.UnitTypeQty = Convert.ToInt32(grdUnits_Sheet1.Cells[i, 4].Value.ToString());
                        }

                        if (String.IsNullOrEmpty(Convert.ToString(grdUnits_Sheet1.Cells[i, 6].Value)))
                        {
                            unit.Id = "";
                        }
                        else
                        {
                            unit.Id = grdUnits_Sheet1.Cells[i, 6].Value.ToString();
                            if (SelectedBuilding.MultiFamily != null)
                            {
                                foreach (BuildingUnitType u in this.SelectedBuilding.MultiFamily.BuildingUnitTypes)
                                {
                                    if (u.Id == unit.Id)
                                    {
                                        if (u.RetrofitEstimates.Count > 0)
                                        {
                                            foreach (RetrofitEstimate r in u.RetrofitEstimates)
                                            {
                                                unit.AddRetrofitEstimate(r, true);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (unit.UnitTypeQty > 0)
                        {
                            buildingUnitList.Add(unit);
                        }
                    }
                }
                //Delete any units in the xml file and replace with what is entered on the grid
                DataStore.DeleteUnits(this.SelectedAudit.Id, this.SelectedBuilding.Id);
                foreach (BuildingUnitType u in buildingUnitList)
                {
                    this.SelectedBuilding.MultiFamily.AddUnit(u, true);
                }
            }
            catch (Exception)
            {
            }

            Cursor.Current = Cursors.Default;
        }

        private void DeleteUnit()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.SelectedBuilding.MultiFamily != null)
            {
                foreach (BuildingUnitType unitType in this.SelectedBuilding.MultiFamily.BuildingUnitTypes)
                {
                    if (unitType.Id.Equals(txtUnitId.Text))
                    {
                        this.SelectedBuilding.MultiFamily.RemoveUnit(unitType, true);
                        break;
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void SaveUnit()
        {
            Cursor.Current = Cursors.WaitCursor;

            bool isNew = false;
            BuildingUnitType unit = null;

            if (String.IsNullOrEmpty(txtUnitId.Text))
            {
                unit = new BLL.BuildingUnitType();
                isNew = true;
            }
            else
            {
                if (this.SelectedBuilding.MultiFamily != null)
                {
                    foreach (BuildingUnitType u in this.SelectedBuilding.MultiFamily.BuildingUnitTypes)
                    {
                        if (u.Id.Equals(txtUnitId.Text))
                        {
                            unit = u;
                            break;
                        }
                    }
                }
            }

            if (!String.IsNullOrEmpty(txtUnitType.Text))
            {
                if (String.IsNullOrEmpty(txtUnitType.Text))
                {
                    unit.UnitType = "";
                }
                else
                {
                    unit.UnitType = txtUnitType.Text;
                }

                if (String.IsNullOrEmpty(txtUnitTypeName.Text))
                {
                    unit.UnitTypeName = "";
                }
                else
                {
                    unit.UnitTypeName = txtUnitTypeName.Text;
                }

                unit.SquareFeet = this.txtUnitSqFt.Value;

                if (String.IsNullOrEmpty(txtUnitQty.Text))
                {
                    unit.UnitTypeQty = 0;
                }
                else
                {
                    unit.UnitTypeQty = Convert.ToInt32(txtUnitQty.Text);
                }

                if (lstUnitBedroom.SelectedItems.Count > 0)
                {
                    unit.NumBedrooms = int.Parse(lstUnitBedroom.SelectedItems[0].Text);
                }

                if (lstUnitBathroom.SelectedItems.Count > 0)
                {
                    unit.NumBathrooms = double.Parse(lstUnitBathroom.SelectedItems[0].Text);
                }

                if (lstUnitHeating.SelectedItems.Count > 0)
                {
                    unit.HeatingType = lstUnitHeating.SelectedItems[0].Text;
                }

                if (lstUnitCooling.SelectedItems.Count > 0)
                {
                    unit.CoolingType = lstUnitCooling.SelectedItems[0].Text;
                }

                if (lstUnitLocation.SelectedItems.Count > 0)
                {
                    unit.Location = lstUnitLocation.SelectedItems[0].Text;
                }

                if (unit.UnitTypeQty > 0 && isNew)
                {
                    this.SelectedBuilding.MultiFamily.AddUnit(unit, true);
                }
            }

            this.SaveUnitDI(unit.Id);

            DataStore.SaveData();

            Cursor.Current = Cursors.Default;
        }

        private void LoadUnits(Building b)
        {
            int rowCount = 0;

            for (int i = 0; i < this.grdUnits_Sheet1.Rows.Count; i++)
            {
                if (i < this.grdUnits_Sheet1.Rows.Count - 1)
                {
                    this.grdUnits_Sheet1.Cells[i, 0].Text = string.Empty;
                    this.grdUnits_Sheet1.Cells[i, 1].Text = string.Empty;
                    this.grdUnits_Sheet1.Cells[i, 2].Text = string.Empty;
                    this.grdUnits_Sheet1.Cells[i, 4].Text = string.Empty;
                }
                this.grdUnits_Sheet1.Cells[i, 6].Text = string.Empty;
            }

            if (b != null && b.MultiFamily != null)
            {
                foreach (BuildingUnitType u in b.MultiFamily.BuildingUnitTypes)
                {
                    this.grdUnits_Sheet1.Cells[rowCount, 0].Text = u.UnitType;
                    this.grdUnits_Sheet1.Cells[rowCount, 1].Text = u.UnitTypeName;
                    this.grdUnits_Sheet1.Cells[rowCount, 2].Text = u.SquareFeet.ToString();
                    this.grdUnits_Sheet1.Cells[rowCount, 4].Text = u.UnitTypeQty.ToString();
                    this.grdUnits_Sheet1.Cells[rowCount, 6].Text = u.Id.ToString();
                    if (rowCount >= this.grdUnits_Sheet1.Rows.Count - 1)
                    {
                        break;
                    }
                    else
                    {
                        rowCount++;
                    }
                }

                this._unitNumberingChangedByUser = false;
                cboUnitNumbering.Text = b.UnitNumbering;
                this._unitNumberingChangedByUser = true;

                this.txtUnitNumberOfUnits.Text = b.NumberOfUnits.ToString();
            }
        }

        private void BuildUnitType(object sender, EventArgs e)
        {
            string unitType = "";

            if (lstUnitBedroom.SelectedItems.Count > 0)
            {
                unitType = lstUnitBedroom.SelectedItems[0].Text + "Bdrm, ";
            }
            if (lstUnitBathroom.SelectedItems.Count > 0)
            {
                unitType += lstUnitBathroom.SelectedItems[0].Text + "Ba, ";
            }
            if (lstUnitHeating.SelectedItems.Count > 0)
            {
                unitType += lstUnitHeating.SelectedItems[0].Text + ", ";
            }
            if (lstUnitCooling.SelectedItems.Count > 0)
            {
                unitType += lstUnitCooling.SelectedItems[0].Text + ", ";
            }
            if (lstUnitLocation.SelectedItems.Count > 0)
            {
                unitType += lstUnitLocation.SelectedItems[0].Text;
            }

            txtUnitType.Text = unitType;
        }

        private void uploadAuditData()
        {
            if (Constants.Utilities.IsNetworkAvailable())
            {
                Cursor.Current = Cursors.WaitCursor;

                this.lblStatusText.Text = "Uploading...";
                var fullCpReport = new StringBuilder();
                // fali safe for mega-error
                try
                {
                    string auditId = "";

                    /**
                     * The Plan
                     * - Foreach row in the grid
                     * - find the AuditProject by auditId
                     * - Use Bsi to lookup the accountId if it is missing on the Company
                     * -- update said Company ExternalId
                     * - SaveToDB on the
                     * */

                    Lg.HeaderStars("uploadAuditData");
                    bool foundAuditsToUpload = false;
                    bool uploadSuccess = true;
                    List<int> rowsToRemove = new List<int>();

                    FarPoint.Win.Spread.Column colCompleteCheck = grdMyWork.ActiveSheet.GetColumnFromTag(null, "Complete");
                    FarPoint.Win.Spread.Column colAuditId = grdMyWork.ActiveSheet.GetColumnFromTag(null, "AuditId");

                    for (int x = 0; x < this.grdMyWork.ActiveSheet.RowCount; x++)
                    {
                        var cpSendReport = new StringBuilder();
                        bool currentAuditSuccess = false;
                        // individual project handling
                        try
                        {
                            this.lblStatusText.Text = String.Format("Checking MyWork row {0}...", x);

                            if (this.grdMyWork.ActiveSheet.Cells[x, colCompleteCheck.Index] != null &&
                                this.grdMyWork.ActiveSheet.Cells[x, colCompleteCheck.Index].Value != null &&
                                this.grdMyWork.ActiveSheet.Cells[x, colCompleteCheck.Index].Value.ToString().Trim().ToUpper() == "TRUE")
                            {
                                foundAuditsToUpload = true;

                                // Save status in XML.
                                auditId = this.grdMyWork.ActiveSheet.Cells[x, colAuditId.Index].Value.ToString();
                                var c = DataStore.GetCompanyByAudit(auditId);

                                Audit audit = c.Audits.FindById(auditId);

                                if (!audit.IsRequiredPRQMissing && !IsRequiredAuditPRQMissing(audit))
                                {
                                    cpSendReport.AppendLine("----------------------------------------");
                                    cpSendReport.AppendFormat("Company {0} :CID({1}):EXT({2}):ID({3})\n", c.Name, c.CompanyId, c.ExternalId, c.Id);
                                    cpSendReport.AppendFormat("AuditId ({0}) : Start", auditId);

                                    if (String.IsNullOrEmpty(c.Id)
                                        || (String.IsNullOrEmpty(c.ElectricRateCode) && !String.IsNullOrEmpty(c.ElectricAccountNumber))
                                        || (String.IsNullOrEmpty(c.GasRateCode) && !String.IsNullOrEmpty(c.GasAccountNumber))
                                        )
                                    {
                                        cpSendReport.AppendFormat("->BsiLookup");
                                        this.lblStatusText.Text = "Backfilling Account info...";
                                        var account = bsiService.FindAccountForBackfill(new BsiBackfillTerms
                                        {
                                            AccountId = c.Id,
                                            AuditId = auditId,
                                            Utility = c.Utility,
                                            ElectricAccountNumber = c.ElectricAccountNumber,
                                            GasAccountNumber = c.GasAccountNumber
                                        });

                                        // this is updated from Bensight directly
                                        c.ExternalId = account.Id;
                                        c.GasRateCode = c.GasRateCode ?? account.GasRateCode;
                                        c.ElectricRateCode = c.ElectricRateCode ?? account.ElectricRateCode;
                                    }
                                    cpSendReport.AppendFormat("->Update");
                                    DataStore.UpdateCompany(c, false);

                                    // Check for existing reports to detect
                                    string reportDirectory = Path.GetDirectoryName(DataStore.XmlReportFile);
                                    string[] reportFiles = Directory.GetFiles(reportDirectory);
                                    string auditReportFilePath = Path.Combine(reportDirectory, ReportHelper.GetReportFileName(ReportHelper.ReportType.AuditReport, auditId));
                                    string signatureReportFilePath = Path.Combine(reportDirectory, ReportHelper.GetReportFileName(ReportHelper.ReportType.SignatureReport, auditId));

                                    // if the reports exist, and the bare minimum for their existence is NOT present, die
                                    if (File.Exists(auditReportFilePath) && c.Audits.Count > 0 && c.Audits[0].Buildings.Count == 0)
                                    {
                                        throw new Exception("Audit has a Audit Report, but has no Building: Submit a bug report IMMEDIATELY.");
                                    }
                                    if (File.Exists(signatureReportFilePath) && c.Audits.Count > 0 && c.Audits[0].InkSecureSignatureData == null)
                                    {
                                        throw new Exception("Audit has a Signature Report, but has no Signature: Submit a bug report IMMEDIATELY.");
                                    }

                                    this.lblStatusText.Text = "Saving to the Cloud...";
                                    cpSendReport.AppendFormat("->SaveDB");

                                    c = DataStore.GetCompanyByAudit(auditId);
                                    // upload all local files
                                    c = frmMainMyWorkHelper.UploadLocalFiles(this._userEmail, lookupService, c, lblStatusText);
                                    DataStore.UpdateCompany(c, false);

                                    string programCode = c.Audits[0].ProgramId;
                                    var listToSave = new List<Company> { c };

                                    // this will explode if there were errors or return a url where the data was stored
                                    UploadReturn saveUrl = DataStore.SaveToDB(listToSave, lookupService, programCode, c.CompanyId, auditId, this._userEmail);
                                    saveUrl.ValidateOrDie();
                                    this.lblStatusText.Text = "Uploaded successfully to " + saveUrl.BackupUri.AbsoluteUri;
                                    cpSendReport.AppendFormat("({0})", saveUrl.BackupUri.AbsoluteUri);

                                    // send registration invite email
                                    if (c.SendRegistrationEmail)
                                    {
                                        cpSendReport.AppendFormat("->SendEmail");
                                        this.lblStatusText.Text = "Sending registration invite email...";
                                        // this throws an exception if there was a problem sending the email
                                        lookupService.SendCompanyRegistrationEmail(c.CompanyId, c.Email, this._userName);
                                    }
                                    else
                                    {
                                        cpSendReport.AppendFormat("->Ignore SendEmail");
                                    }

                                    // upload audit report(s)
                                    this.lblStatusText.Text = "Uploading audit reports...";
                                    string auditReportUploadUrl = string.Empty;

                                    //Audit audit = c.Audits.FindById(auditId);
                                    // in the case of an audit not having Recommendations or DIs, all the project to be submitted without reports
                                    var ignoreReports = audit.CanIgnoreReport();
                                    // audit report is required
                                    this.lblStatusText.Text = "Processing Reports...";
                                    if (!reportFiles.Contains(auditReportFilePath))
                                    {
                                        if (!ignoreReports)
                                        {
                                            throw new Exception("Audit Report not found.  Please generate the audit report before uploading.");
                                        }
                                        else
                                        {
                                            cpSendReport.AppendFormat("->Ignore Audit Report");
                                        }
                                    }
                                    else
                                    {
                                        cpSendReport.AppendFormat("->Upload Audit Report");
                                        UploadReturn auditReportReturn = lookupService.UploadAuditReport(programCode, c.CompanyId, auditId, this._userEmail, auditReportFilePath);
                                        auditReportReturn.ValidateOrDie();
                                        cpSendReport.AppendFormat("({0})", auditReportReturn.BackupUri.AbsoluteUri);
                                        Lg.Info("Uploaded " + auditReportFilePath + " to " + auditReportReturn.BackupUri);
                                        this.lblStatusText.Text += "signature report uploaded...";
                                        audit.PdfReportFileUrl = auditReportReturn.BackupUri.AbsoluteUri;
                                    }

                                    // signature report must exist if the audit has retrofits
                                    if (!reportFiles.Contains(signatureReportFilePath))
                                    {
                                        if (!ignoreReports)
                                        {
                                            throw new Exception("Signature Report not found.  The signature report must be generated for an audit that has retrofits.");
                                        }
                                        else
                                        {
                                            cpSendReport.AppendFormat("->Ignore Sig Report");
                                        }
                                    }
                                    else
                                    {
                                        cpSendReport.AppendFormat("->Upload Sig Report");
                                        UploadReturn signatureReportReturn = lookupService.UploadAuditReport(programCode, c.CompanyId, auditId, this._userEmail, signatureReportFilePath);
                                        signatureReportReturn.ValidateOrDie();
                                        cpSendReport.AppendFormat("({0})", signatureReportReturn.BackupUri.AbsoluteUri);
                                        Lg.Info("Uploaded " + signatureReportFilePath + " to " + signatureReportReturn.BackupUri);
                                        this.lblStatusText.Text += "audit report uploaded...";
                                        audit.PdfSignoffFileUrl = signatureReportReturn.BackupUri.AbsoluteUri;
                                    }
                                    // finally, save the audit progress

                                    this.lblStatusText.Text = "Build Bensight projects...";
                                    cpSendReport.AppendFormat("->BuildCPs");
                                    CPHelper cpHelper = new CPHelper();
                                    List<BsiCompositeProject> cps = cpHelper.CreateCpRecordsFromCompanies(listToSave, _userEmail, saveUrl?.BackupUri?.AbsoluteUri);
                                    cpSendReport.AppendFormat("->Built {0} CPs", cps.Count);
                                    if (cps.Count == 0)
                                    {
                                        cpSendReport.AppendFormat("->ERROR");
                                        throw new Exception("No CPs created from the Audit.");
                                    }
                                    this.lblStatusText.Text = String.Format("Sending {0} composite project records to Bensight...", cps.Count);
                                    cpSendReport.AppendFormat("->SendCPs({0})", cps.Count);
                                    string erroredCpMessages = sendAllCps(cps);
                                    if (String.IsNullOrEmpty(erroredCpMessages))
                                    {
                                        cpSendReport.AppendFormat("->Remove", cps.Count);
                                        this.lblStatusText.Text = "Removing from local store...";
                                        // Delete this audit from the file.
                                        DataStore.DeleteCompany(auditId, false);
                                        cpSendReport.AppendFormat("->Fin!", cps.Count);
                                        this.grdMyWork.ActiveSheet.Cells[x, colCompleteCheck.Index].Value = false;
                                        rowsToRemove.Add(x);
                                        // clean up the report files
                                        if (File.Exists(auditReportFilePath))
                                        {
                                            Lg.Info("Deleting audit report file " + auditReportFilePath);
                                            File.Delete(auditReportFilePath);
                                        }
                                        if (File.Exists(signatureReportFilePath))
                                        {
                                            Lg.Info("Deleting signature report file " + signatureReportFilePath);
                                            File.Delete(signatureReportFilePath);
                                        }
                                        currentAuditSuccess = true;
                                    }
                                    else
                                    {
                                        cpSendReport.AppendFormat("->ERROR");
                                        cpSendReport.AppendLine();
                                        cpSendReport.AppendLine(erroredCpMessages);
                                        this.lblStatusText.Text = erroredCpMessages;
                                    }
                                    this.lblStatusText.Text = String.Format("Finished Sync of AuditProject {0}", auditId);
                                }//Missing PRQ check
                                else
                                {
                                    cpSendReport.AppendFormat("\n\n ** AUDIT SKIPPED ** {0}, {1} -  Can not upload audit : All required questions have not been answered.", auditId, audit.Name);
                                    currentAuditSuccess = false;
                                }
                            }
                            else
                            {
                                // considered successful if the row is skipped
                                currentAuditSuccess = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            var cause = ExceptionHelper.Innermost(ex);
                            cpSendReport.AppendFormat("\n\n{0} error : {1}", auditId, cause.Message);
                            if (!String.IsNullOrEmpty(auditId))
                            {
                                DataStore.GetAudit(auditId).AuditStatus = AuditStatus.INCOMPLETE;
                            }
                            continue;
                        }
                        finally
                        {
                            if (!currentAuditSuccess)
                            {
                                // if one of the audit uploads failed, mark the whole process unsuccessful
                                uploadSuccess = false;
                            }

                            if (!String.IsNullOrWhiteSpace(cpSendReport.ToString()))
                            {
                                Lg.Info(cpSendReport.ToString());
                                fullCpReport.AppendLine(cpSendReport.ToString().Trim());
                            }
                        }
                    }// Loop for each grid row

                    foreach (int rowToRemove in rowsToRemove.OrderByDescending(x => x))
                    {
                        this.grdMyWork.ActiveSheet.RemoveRows(rowToRemove, 1);
                    }

                    // display cp report
                    if (!foundAuditsToUpload)
                    {
                        MessageBox.Show("No CP records to upload.");
                    }
                    else if (uploadSuccess)
                    {
                        MessageBox.Show("Upload completed successfully.");
                    }
                    else
                    {
                        Constants.Utilities.SetClipboardText(fullCpReport.ToString());
                        MessageBox.Show(fullCpReport.ToString().Trim());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    // save any other changes like project unmarked for completion
                    DataStore.SaveData();
                    LoadMyWorkGrid();
                }
                //this.lblStatusText.Text.Replace("Uploading...", "            ");
                this.lblStatusText.Text = "READY";

                Cursor.Current = Cursors.Default;
            }
            else
            {
                MessageBox.Show("You are not connected to the Franklin Energy network.", "No Network Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool IsRequiredAuditPRQMissing(Audit audit)
        {
            List<ApiProgramMetadata> programs = DataStore.GetProgramsByProgramCode(audit.ProgramId);

            List<SurveyDetails> list = new List<SurveyDetails>();

            foreach (Building building in audit.Buildings)
            {
                if (programs.Count > 0)
                {
                    foreach (ApiQuestion item in programs[0].questions.Where(
                        x => x.IsReferral == false
                        && x.IsRecommendation == false
                        && x.ClipboardSectionFlag != ApiQuestionConstants.Neither
                        && x.ClipboardSectionFlag != ApiQuestionConstants.DI
                        )
                        .OrderBy(x => x.Category)
                        .ThenBy(x => x.Order)
                        )
                    {
                        if (item.IsRequired)
                        {
                            bool answerFound = false;
                            foreach (Answer answer in building.Answers)
                            {
                                if (item.Id == answer.Id)
                                {
                                    answerFound = true;
                                }
                            }
                            if (!answerFound)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void SaveDI()
        {
            try
            {
                //Delete any retrofits in the xml file and replace with what is entered on the grid
                DataStore.DeleteBuildingRetrofits(this.SelectedAudit.Id, this.SelectedBuilding.Id);

                for (int i = 0; i < this.grdDirectInstall_Sheet1.Rows.Count; i++)
                {
                    Retrofit di = new BLL.Retrofit();

                    if (!String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_PROGRAM_CODE].Value)))
                    {
                        di.ProgramID = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_PROGRAM_CODE].Value.ToString();
                        di.Description = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_NAME].Value.ToString();

                        di.Quantity = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_QUANTITY].Value)))
                            ? 0 : Convert.ToDouble(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_QUANTITY].Value.ToString());

                        di.EligibleComponentId = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_ID].Value.ToString();

                        di.Space = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SPACE].Value)))
                            ? "" : this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SPACE].Value.ToString();

                        di.KWh = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH].Value)))
                            ? 0 : Convert.ToDouble(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH].Value.ToString());
                        di.KWhUnit = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH_UNIT].Value)))
                            ? 0 : Convert.ToDouble(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH_UNIT].Value.ToString());

                        di.Therms = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS].Value)))
                            ? 0 : Convert.ToDouble(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS].Value.ToString());
                        di.ThermsUnit = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS_UNIT].Value)))
                            ? 0 : Convert.ToDouble(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS_UNIT].Value.ToString());

                        di.Savings = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SAVINGS].Value)))
                            ? 0 : Convert.ToDecimal(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SAVINGS].Value.ToString());

                        if (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_ICON_FILE_NAME].Value)))
                        {
                            di.IconPath = "";
                            di.IconFileName = "";
                        }
                        else
                        {
                            di.IconFileName = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_ICON_FILE_NAME].Value.ToString();
                            di.IconPath = Path.Combine(DataStore.DiIconsDirectoryPath, this.grdDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ICON_FILE_NAME].Value.ToString());
                        }
                        if (this.CurrentAuditReportReportBranding != null)
                        {
                            if (!String.IsNullOrEmpty(this.CurrentAuditReportReportBranding.DIIconFilePath))
                            {
                                if (Directory.Exists(this.CurrentAuditReportReportBranding.DIIconFilePath))
                                {
                                    string iFile = this.CurrentAuditReportReportBranding.DIIconFilePath + "\\" + di.IconFileName;
                                    if (File.Exists(iFile))
                                    {
                                        di.IconPath = iFile;
                                    }
                                }
                            }
                        }

                        di.ComponentType = (String.IsNullOrEmpty(Convert.ToString(this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_COMPONENT_TYPE].Value)))
                            ? "" : this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_COMPONENT_TYPE].Value.ToString();

                        if (di.Quantity > 0)
                        {
                            this.SelectedBuilding.AddDirectInstalls(di, true);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void SaveUnitDI(string unitId)
        {
            try
            {
                //Delete any retrofits in the xml file and replace with what is entered on the grid
                DataStore.DeleteUnitRetrofits(this.SelectedAudit.Id, unitId);

                for (int i = 0; i < this.grdUnitDirectInstall_Sheet1.Rows.Count; i++)
                {
                    RetrofitEstimate di = new BLL.RetrofitEstimate();

                    if (!String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_PROGRAM_CODE].Value)))
                    {
                        di.ProgramID = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_PROGRAM_CODE].Value.ToString();
                        di.Description = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_NAME].Value.ToString();

                        di.Quantity = String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_QUANTITY].Value))
                            ? 0 : Convert.ToDouble(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_QUANTITY].Value.ToString());

                        di.EligibleComponentId = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ID].Value.ToString();

                        di.Space = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SPACE].Value)))
                            ? "" : this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SPACE].Value.ToString();

                        di.KWh = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KWH].Value)))
                            ? 0 : Convert.ToDouble(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KWH].Value.ToString());
                        di.KWhUnit = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KWH_UNIT].Value)))
                            ? 0 : Convert.ToDouble(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KWH_UNIT].Value.ToString());

                        di.Therms = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_THERMS].Value)))
                            ? 0 : Convert.ToDouble(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_THERMS].Value.ToString());
                        di.ThermsUnit = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_THERMS_UNIT].Value)))
                            ? 0 : Convert.ToDouble(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_THERMS_UNIT].Value.ToString());

                        di.Savings = (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SAVINGS].Value)))
                            ? 0 : Convert.ToDecimal(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SAVINGS].Value.ToString());

                        if (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ICON_FILE_NAME].Value)))
                        {
                            di.IconPath = "";
                            di.IconFileName = "";
                        }
                        else
                        {
                            di.IconPath = Path.Combine(DataStore.DiIconsDirectoryPath, this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ICON_FILE_NAME].Value.ToString());
                            di.IconFileName = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ICON_FILE_NAME].Value.ToString();
                        }
                        if (this.CurrentAuditReportReportBranding != null)
                        {
                            if (!String.IsNullOrEmpty(this.CurrentAuditReportReportBranding.DIIconFilePath))
                            {
                                if (Directory.Exists(this.CurrentAuditReportReportBranding.DIIconFilePath))
                                {
                                    string iFile = this.CurrentAuditReportReportBranding.DIIconFilePath + "\\" + di.IconFileName;
                                    if (File.Exists(iFile))
                                    {
                                        di.IconPath = iFile;
                                    }
                                }
                            }
                        }

                        if (String.IsNullOrEmpty(Convert.ToString(this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_COMPONENT_TYPE].Value)))
                        {
                            di.ComponentType = "";
                        }
                        else
                        {
                            di.ComponentType = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_COMPONENT_TYPE].Value.ToString();
                        }

                        if (di.Quantity > 0)
                        {
                            if (this.SelectedBuilding.MultiFamily != null)
                            {
                                foreach (BuildingUnitType u in this.SelectedBuilding.MultiFamily.BuildingUnitTypes)
                                {
                                    if (u.Id == unitId)
                                    {
                                        u.AddRetrofitEstimate(di, true);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void SaveAuditSchedule()
        {
            DataStore.SetAuditSchedules(this.SelectedBuilding.Id,
                this.schAuditOccupancy.GetSchedule(),
                this.schAuditInteriorLights.GetSchedule(),
                this.schAuditExteriorLights.GetSchedule(),
                this.schAuditHvac.GetSchedule(),
                true);
        }

        private bool SaveGasHistory()
        {
            var success = true;
            if (this.SelectedAudit != null)
            {
                //Delete any existing gas history in the xml file
                List<BLL.GasHistoryRecord> histories = new List<BLL.GasHistoryRecord>();

                for (int i = 3; i < this.grdGasHist_Sheet1.Rows.Count; i++)
                {
                    if (this.grdGasHist_Sheet1.Cells[i, 2].Value != null)
                    {
                        GasHistoryRecord gasHistoryRecord = new BLL.GasHistoryRecord();

                        if (this.grdGasHist_Sheet1.Cells[i, 2] != null && this.grdGasHist_Sheet1.Cells[i, 2].Value != null)
                        {
                            gasHistoryRecord.ReadDate = Convert.ToDateTime(this.grdGasHist_Sheet1.Cells[i, 2].Value.ToString());
                        }

                        if (this.grdGasHist_Sheet1.Cells[i, 3] != null && this.grdGasHist_Sheet1.Cells[i, 3].Value != null)
                        {
                            gasHistoryRecord.BillingDays = Convert.ToInt32(this.grdGasHist_Sheet1.Cells[i, 3].Value.ToString());
                        }

                        if (this.grdGasHist_Sheet1.Cells[i, 4] != null && this.grdGasHist_Sheet1.Cells[i, 4].Value != null)
                        {
                            gasHistoryRecord.CoolDegreeDays = Convert.ToInt32(this.grdGasHist_Sheet1.Cells[i, 4].Value.ToString());
                        }

                        if (this.grdGasHist_Sheet1.Cells[i, 5] != null && this.grdGasHist_Sheet1.Cells[i, 5].Value != null)
                        {
                            gasHistoryRecord.HeatDegreeDays = Convert.ToInt32(this.grdGasHist_Sheet1.Cells[i, 5].Value.ToString());
                        }

                        if (this.grdGasHist_Sheet1.Cells[i, 6] != null && this.grdGasHist_Sheet1.Cells[i, 6].Value != null)
                        {
                            gasHistoryRecord.Therms = Convert.ToDouble(this.grdGasHist_Sheet1.Cells[i, 6].Value.ToString());
                        }

                        if (this.grdGasHist_Sheet1.Cells[i, 7].Value != null && this.grdGasHist_Sheet1.Cells[i, 7].Value != null)
                        {
                            gasHistoryRecord.TotalBill = Convert.ToDouble(this.grdGasHist_Sheet1.Cells[i, 7].Value.ToString());
                        }

                        histories.Add(gasHistoryRecord);
                    }
                }

                var uniqueRecords = histories.Select(x => x.ReadDate.Date).Distinct().Count();
                var historyCount = histories.Count();
                var historyKey = "Gas";
                var dupCount = historyCount - uniqueRecords;
                if (uniqueRecords != historyCount)
                {
                    var msg = String.Format("Cannot save {0} histories because there {1} {2} duplicate meter read {3}",
                        historyKey,
                        (dupCount == 1) ? "is" : "are",
                        dupCount,
                        (dupCount == 1) ? "date" : "dates");
                    var caption = string.Format("{0} Duplicate {1} History Records", historyCount - uniqueRecords, historyKey);
                    MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    success = false;
                }
                else
                {
                    DataStore.DeleteHistory(this.SelectedAudit.Id, historyKey);
                    foreach (var record in histories)
                    {
                        this.SelectedBuilding.AddGasHistory(record, true);
                    }

                    if (grdGasHist_Sheet1.Rows.Count > 0)
                    {
                        // Save chart image.
                        SaveChartImage(historyKey);
                    }

                    //EUI gauge- sorry about the grid name 'fpSpread1'.  It wasn't saving the correct chart name in the designer and this was rushed
                    if (fpSpread1_Sheet1.Cells[8, 7].Text.ToString() != "")
                    {
                        SaveChartImage("EUI");
                    }
                }
            }

            return success;
        }

        private void SaveAudit()
        {
            for (int x = 0; x < grdMyWork.ActiveSheet.RowCount; x++)
            {
                FarPoint.Win.Spread.Column colAuditId = grdMyWork.ActiveSheet.GetColumnFromTag(null, "AuditId");
                if (this.grdMyWork.ActiveSheet.Cells[x, colAuditId.Index].Value != null)
                {
                    //if (this.SelectedAudit != null) {
                    object o = this.grdMyWork_Sheet1.Rows[x].Tag;
                    if (o != null)
                    {
                        TaskList t = o as TaskList;
                        if (t != null)
                        {
                            //Audit a = DataStore.GetAudit(t.AuditID);
                            //if (a != null) {
                            FarPoint.Win.Spread.Column colCompleteCheck = grdMyWork.ActiveSheet.GetColumnFromTag(null, "Complete");
                            if (grdMyWork.ActiveSheet.Cells[x, colCompleteCheck.Index].Value.ToString() == "True")
                            {
                                //save status in XML
                                DataStore.UpdateAuditStatus(t.AuditID, AuditStatus.COMPLETE, true);
                            }
                            else
                            {
                                DataStore.UpdateAuditStatus(t.AuditID, AuditStatus.INCOMPLETE, true);
                            }
                            // }
                        }
                    }
                }
            }
        }

        private void NavigateToBuildingPanel()
        {
            this.tabAuditsPanel.Select();
            this.tabAuditMain.SelectedTabIndex = 5;
            this.tabAccountInfo.SelectedTabIndex = 2;
        }

        private void NavigateToDashboardPanel()
        {
            this.tabHomePanel.Select();
            this.pslHomePageSlider.SelectedPageIndex = 0;
            this.tabAuditMain.SelectedTabIndex = 0;
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;

            this.grdElectricHist_Sheet1_SpreadChart1.Refresh();
        }

        private void NavigateToCompanyPanel()
        {
            this.tabAuditMain.SuspendDrawing();

            this.tabAuditsPanel.Select();
            this.tabAuditMain.SelectedTabIndex = 5;
            this.tabAccountInfo.SelectedTabIndex = 0;

            this.tabAuditMain.ResumeDrawing();
        }

        private void NavigateToContactPanel()
        {
            this.tabAuditMain.SuspendDrawing();

            this.tabAuditsPanel.Select();
            this.tabAuditMain.SelectedTabIndex = 5;
            this.tabAccountInfo.SelectedTabIndex = 1;

            this.tabAuditMain.ResumeDrawing();
        }

        private void NavigateToMyWorkPanel()
        {
            this.tabAuditMain.SuspendDrawing();

            this.tabHomePanel.Select();
            this.pslHomePageSlider.SelectedPageIndex = 2;
            this.tabAuditMain.SelectedTabIndex = 0;
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;

            this.tabAuditMain.ResumeDrawing();
        }

        private void ResetSelectedFormTabs()
        {
            this.tabHomePanel.Select();
            this.pslHomePageSlider.SelectedPageIndex = 0;
            this.tabAuditMain.SelectedTabIndex = 0;
            this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;

            this.grdElectricHist_Sheet1_SpreadChart1.Refresh();
        }

        private void LoadUserSettings(bool forceActiveDirectoryUpdate)
        {
            UserSettings settings = null;

            if (UserSettings.SettingsFileExists)
            {
                settings = UserSettings.GetUserSettings();
            }
            else
            {
                settings = new UserSettings();

                // Attempt to fetch from AD.
                settings.UserName = DataStore.UserName;         // returns the logged in user's login name if no network connection.
                settings.Email = DataStore.UserEmail;           // returns the UserName + "@franklinenergy.com" if no network connection
                settings.FullName = DataStore.UserFullName;     // returns "" if no network connection.
                settings.LogTrace = false;

                settings.Save();
            }

            if (forceActiveDirectoryUpdate && Constants.Utilities.IsNetworkAvailable())
            {
                // If it can get a new AD value, use it.  Else use the value from the settings file.
                settings.UserName = (String.IsNullOrWhiteSpace(DataStore.UserName) ? settings.UserName : DataStore.UserName);
                settings.Email = (String.IsNullOrWhiteSpace(DataStore.UserEmail) ? settings.Email : DataStore.UserEmail);
                settings.FullName = (String.IsNullOrWhiteSpace(DataStore.UserFullName) ? settings.FullName : DataStore.UserFullName);

                settings.Save();
            }

            // Either way, update the local variables.
            if (settings != null)
            {
                this._userEmail = settings.Email;
                this._userFullName = settings.FullName;
                this._userName = settings.UserName;
                this._logTrace = settings.LogTrace;
                Lg.DoTrace = settings.LogTrace;
            }
        }

        private void ResetScheduleControls()
        {
            this.schAuditOccupancy.Reset();
            this.schAuditInteriorLights.Reset();
            this.schAuditExteriorLights.Reset();
            this.schAuditHvac.Reset();
        }

        private void ResetItemsGrid()
        {
            this.grdEipItems.ActiveSheet.Rows.Clear();
            this.grdEipItems.ActiveSheet.Rows.Count = 0;
        }

        private void LoadBuilding()
        {
            //this.recRecommendations.Clear();
            this.ResetScheduleControls();
            this.ResetItemsGrid();

            if (this.SelectedAudit != null && this.SelectedBuilding != null)
            {
                this.FillBuildingSpaces(this.SelectedAudit.ProgramType);
                this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces

                this.SuspendLayout();

                this.schAuditOccupancy.SuspendLayout();
                this.schAuditInteriorLights.SuspendLayout();
                this.schAuditExteriorLights.SuspendLayout();
                this.schAuditHvac.SuspendLayout();

                //this.recRecommendations.SuspendLayout();

                foreach (Schedule s in DataStore.GetSchedules(this.SelectedAudit.Id, this.SelectedBuilding.Id))
                {
                    switch (s.ScheduleName.Trim().ToUpper())
                    {
                        case "OCCUPANCY":
                            //this.LoadSchedule(s);
                            this.tabAuditSchedules.SelectedTabIndex = 0;
                            this.schAuditOccupancy.SetSchedule(new Schedule(s));
                            break;

                        case "INTERIORLIGHTS":
                            this.tabAuditSchedules.SelectedTabIndex = 1;
                            this.schAuditInteriorLights.SetSchedule(new Schedule(s));
                            break;

                        case "EXTERIORLIGHTS":
                            this.tabAuditSchedules.SelectedTabIndex = 2;
                            this.schAuditExteriorLights.SetSchedule(new Schedule(s));
                            break;

                        case "HVAC":
                            this.tabAuditSchedules.SelectedTabIndex = 3;
                            this.schAuditHvac.SetSchedule(new Schedule(s));
                            break;
                    }
                }

                this.tabAuditSchedules.SelectedTabIndex = 0;

                foreach (EquipmentMaster equipment in this.SelectedBuilding.Equipments)
                {
                    this.AddEquipmentToGrid(equipment);
                }

                //if (this.SelectedBuilding.Recommendations != null && this.SelectedBuilding.Recommendations.Count > 0) {
                //    this.recRecommendations.Add(this.SelectedBuilding.Recommendations);
                //}

                //this.recRecommendations.ResumeLayout();
                this.schAuditHvac.ResumeLayout();
                this.schAuditExteriorLights.ResumeLayout();
                this.schAuditInteriorLights.ResumeLayout();
                this.schAuditOccupancy.ResumeLayout();

                this.ResumeLayout();

                // Only add this item if the tab that has the Energy Usage Index graph is visible.
                if (this.tabUtilities.Visible)
                {
                    bool found = false;
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        if (li.Text == "Show Energy Usage Chart")
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        this.lstCustomReports.Items.Add(new ListViewItem("Show Energy Usage Chart", this.lstCustomReports.Groups["lvlReportSections"]));
                    }
                }
            }
        }

        private void DownloadXmlData()
        {
            if (!this.DesignMode)
            {
                this.cvApptCalendarView.CalendarModel.Appointments.Clear();

                Audit.AuditCollection audits = DataStore.GetAuditsByDate(this.calApptMonthCalendar.SelectedDate);

                if (audits != null && audits.Count > 0)
                {
                    this.AddAuditsToCalendar(audits);
                }
            }
        }

        public void AddAuditsToCalendar(Audit.AuditCollection audits)
        {
            this.ClearAppointmentControls();

            if (audits != null)
            {
                List<DateTime> auditDays = new List<DateTime>();

                foreach (Audit audit in audits)
                {
                    auditDays.Add(audit.ScheduledStartTimeStamp);

                    string subject = audit.Name + " - " + audit.Description;
                    Appointment appt = new Appointment(audit.ScheduledStartTimeStamp.ToLocalTime(), 30, subject);
                    appt.Locked = true;
                    appt.Tag = audit;
                    this.cvApptCalendarView.CalendarModel.Appointments.Add(appt);
                }

                this.calApptMonthCalendar.MarkedDates = auditDays.ToArray();
                this.calApptMonthCalendar.UpdateMarkedDates();
            }
        }

        private void ClearAppointmentControls()
        {
        }

        private void LoadStateDropdowns()
        {
            List<StateLookup> states = DataStore.GetAllStates();

            foreach (StateLookup state in states)
            {
                this.cboBuildingState.Items.Add(state.Name);
            }
        }

        private void SetGridColumnWidths(bool doDirectInstall, bool doEquipment, bool doUnitDirectInstall)
        {
            if (doDirectInstall)
            {
                // Direct Install grid.
                for (int i = 0; i < this.grdDirectInstall_Sheet1.Columns.Count; i++)
                {
                    this.grdDirectInstall_Sheet1.Columns[i].Width = 0;
                }

                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_PROGRAM_CODE].Width = 102; // Program ID
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_INCREMENT_QTY].Width = 50; // +
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_QUANTITY].Width = 61; // Qty
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_DECREMENT_QTY].Width = 50; // -
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_KWH].Width = 66; // kWh
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_THERMS].Width = 66; // MCF
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_KGALLONS].Width = 123; // Water
                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_SAVINGS].Width = 120; // Savings

                float allColWidth =
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_PROGRAM_CODE].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_INCREMENT_QTY].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_QUANTITY].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_DECREMENT_QTY].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_KWH].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_THERMS].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_KGALLONS].Width +
                    this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_SAVINGS].Width;

                this.grdDirectInstall_Sheet1.Columns[DI_GRID_COLUMN_NAME].Width = this.grdDirectInstall.Width - (allColWidth + 28); // Measure Description
            }

            if (doEquipment)
            {
                // Equipment grid.
                for (int i = 0; i < this.grdEipItems_Sheet1.Columns.Count; i++)
                {
                    this.grdEipItems_Sheet1.Columns[i].Width = 0;
                }

                this.grdEipItems_Sheet1.Columns[EIP_GRID_COLUMN_IMAGE].Width = 70; // Image
                this.grdEipItems_Sheet1.Columns[EIP_GRID_COLUMN_QUANTITY].Width = 87; // Qty

                float allColWidth =
                    this.grdEipItems_Sheet1.Columns[EIP_GRID_COLUMN_IMAGE].Width +
                    this.grdEipItems_Sheet1.Columns[EIP_GRID_COLUMN_QUANTITY].Width;

                this.grdEipItems_Sheet1.Columns[EIP_GRID_COLUMN_NAME].Width = this.grdEipItems.Width - (allColWidth + 6); // Equipment
            }

            if (doUnitDirectInstall)
            {
                // Unit Direct Install grid.
                for (int i = 0; i < this.grdUnitDirectInstall_Sheet1.Columns.Count; i++)
                {
                    this.grdUnitDirectInstall_Sheet1.Columns[i].Width = 0;
                }

                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_NAME].Width = 423; // Measure Description
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_INCREMENT_QTY].Width = 45; // +
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_QUANTITY].Width = 61; // Qty
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_DECREMENT_QTY].Width = 45; // -
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_SPACE].Width = 143; // Space
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_KWH].Width = 66; // kWh
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_THERMS].Width = 66; // MCF
                this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_SAVINGS].Width = 84; // Savings

                float allColWidth =
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_PROGRAM_CODE].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_INCREMENT_QTY].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_QUANTITY].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_DECREMENT_QTY].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_SPACE].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_KWH].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_THERMS].Width +
                    this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_SAVINGS].Width;
            }
        }

        private void InitializeForm()
        {
            if (!this.DesignMode)
            {
                this.EditMode = Enumerations.Mode.None;

                // Home panel.
                this.SelectCalendarDate(DateTime.Now);

                // Audit panel.
                this.InitializeCompanyTab();
                this.InitializeContactTab();
                this.LoadStateDropdowns();
                this.SetScheduleProperties();

                this.lstAvailableSpaces.SuspendLayout();
                this.lstSelectedSpaces.SuspendLayout();

                this.colAvailableSpaces.Width = this.lstAvailableSpaces.Width - 5;
                this.colSelectedSpaces.Width = this.lstSelectedSpaces.Width - 5;

                this.ResetScheduleControls();

                this.lstSelectedSpaces.ResumeLayout();
                this.lstAvailableSpaces.ResumeLayout();

                this.EnableAuditButton();

                string version = "";
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(4);
                }
                else
                {
                    version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
                }
                this.Text = "Efficiency Clipboard - v" + version + envTag();
                this.SetGridColumnWidths(true, true, true);
                // daves testing
                this.lblStatusText.Width = 1040;

                this.lblReportNumRecommendations.Visible = false;
                this.numReportecommendations.Visible = false;

                this.SetupKeyboardToControls();
            }
        }

        private string envTag()
        {
            string env = ConfigurationManager.AppSettings.Get(AppConstants.AppKeys.ENV);

            return (String.IsNullOrEmpty(env) ? String.Empty : " ENV(" + env + ")");
        }

        private void SetHomeNavButtons(bool dashboardChecked, bool myDayChecked, bool myWorkChecked)
        {
            this.btnHomeDashboard.Checked = dashboardChecked;
            this.btnHomeMyDay.Checked = myDayChecked;
            this.btnHomeMyWork.Checked = myWorkChecked;
        }

        private void SizeSpreadColumnsToPreferredWidth(FarPoint.Win.Spread.FpSpread ctl)
        {
            for (int i = 0; i < ctl.ActiveSheet.Columns.Count; i++)
            {
                ctl.ActiveSheet.Columns[i].Width = ctl.ActiveSheet.GetPreferredColumnWidth(i, false, false, false);
            }

            for (int i = 0; i < ctl.ActiveSheet.Rows.Count; i++)
            {
                ctl.ActiveSheet.Rows[i].Height = 50;
            }
        }

        private void ClearButtonChecks()
        {
            if (!this.DesignMode)
            {
                this.btnApptDayView.Checked = false;
                this.btnApptWeekView.Checked = false;
                this.btnApptMonthView.Checked = false;
                this.btnApptYearView.Checked = false;
            }
        }

        private void EnableAuditButton()
        {
            if (this.SelectedAudit == null)
            {
                this.tabAuditsPanel.Enabled = false;
            }
            else
            {
                this.tabAuditsPanel.Enabled = true;

                Company c = DataStore.GetCompanyByAudit(this.SelectedAudit);
                this.NavigateToBuildingPanel(c);
            }
        }

        private void DoLaunch()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                this._isLoadingAudit = true;

                this.tabAuditMain.SuspendDrawing();

                this.EnableAuditButton();
                this.SetAuditNavButtons(false, true, false, false, false);
                this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;

                if (this.SelectedAudit != null)
                {
                    this.InitializeFormByProgramType(this.SelectedAudit.ProgramType);
                    this.LoadReportParameters(this.SelectedAudit.Report);
                    this.SyncBuilding(Enumerations.SyncDirection.EditObjectToControls);
                    if (this.SelectedAudit.ProgramType.Equals(FieldTool.Constants.ProgramType.MultiFamily))
                    {
                        this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                    }
                }

                this.tabAuditMain.ResumeDrawing();

                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this._isLoadingAudit = false;
            }
        }

        private void NavigateToBuildingPanel(Company company)
        {
            if (company.IsValidated)
            {
                this.LoadCompany(company, Enumerations.PanelDisplayMode.ReadOnly);
            }
            else
            {
                this.LoadCompany(company, Enumerations.PanelDisplayMode.Unvalidated);
            }

            this.NavigateToBuildingPanel();
            this.ResetForm();
            this.InitializeBuildingTab("");

            if (this.lstBuildingList.Items.Count == 0)
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.None);
            }
            else
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.Selected);
            }

            this.grdElectricHist_Sheet1_SpreadChart1.Refresh();
        }

        private bool FillAuditDetails(Audit audit)
        {
            var ok = true;
            try
            {
                if (!this.DesignMode)
                {
                    this.SelectedAudit = audit;

                    if (audit == null)
                    {
                        this.txtApptContact.Clear();
                        this.txtApptProject.Clear();
                        this.txtApptAccount.Clear();
                        this.txtApptLocation.Clear();
                        this.txtApptApptType.Clear();
                        this.txtApptApptNotes.Clear();

                        this.dtpApptStartDate.Text = null;
                        this.dtpApptStartTime.Text = null;

                        this.btnApptLaunch.Enabled = false;

                        this.LoadReportParameters(null);
                    }
                    else
                    {
                        this.txtApptContact.Text = this.SelectedAudit.CompanyContact;
                        this.txtApptProject.Text = this.SelectedAudit.Description;
                        this.txtApptAccount.Text = this.SelectedAudit.Name;
                        this.txtApptLocation.Text = this.SelectedAudit.Name;
                        this.txtApptApptType.Text = this.SelectedAudit.Description;
                        this.txtApptApptNotes.Text = this.SelectedAudit.Description;

                        this.dtpApptStartDate.Value = this.SelectedAudit.ScheduledStartTimeStamp.Date;
                        this.dtpApptStartTime.Value = this.SelectedAudit.ScheduledStartTimeStamp.Date;

                        this.btnApptLaunch.Enabled = true;

                        //Apply client branding theme    - Added by Mike Crowell 7/6/15
                        ClientBranding branding = this.ApplyBranding(audit.ClientAccountId, audit.BrandingKey);
                        BrandingHelper brandingHelper = new BrandingHelper();
                        switch (audit.ProgramType)
                        {
                            case FieldTool.Constants.ProgramType.MultiFamily:
                                this.SelectedAuditMultiFamilyReportBranding = brandingHelper.GetClientMultifamilyReportBranding(audit.ClientAccountId, branding);
                                if (this.SelectedAuditMultiFamilyReportBranding != null)
                                {
                                    if (branding != null)
                                    {
                                        if (!String.IsNullOrEmpty(branding.ImageFileName))
                                        {
                                            if (branding.HasValidRGB)
                                            {
                                                this.SelectedAuditMultiFamilyReportBranding.ColorValueRed = branding.ColorValueRed;
                                                this.SelectedAuditMultiFamilyReportBranding.ColorValueGreen = branding.ColorValueGreen;
                                                this.SelectedAuditMultiFamilyReportBranding.ColorValueBlue = branding.ColorValueBlue;
                                                this.SelectedAuditMultiFamilyReportBranding.HasValidRGB = branding.HasValidRGB;
                                            }
                                        }
                                    }
                                }
                                break;

                            case FieldTool.Constants.ProgramType.Commercial:
                                this.SelectedAuditCommercialReportBranding = brandingHelper.GetClientCommercialReportBranding(audit.ClientAccountId, branding);
                                break;
                        }

                        if (this.CurrentAuditReportReportBranding != null)
                        {
                            this.txtSignOffTerms.Text = this.CurrentAuditReportReportBranding.SignOffDisclaimerText;
                        }

                        //this.ClearElectricHistory();
                        //this.ClearGasHistory();
                        //this.ClearAuditDirectInstallGridData();
                        //this.ClearUnitDirectInstallGridData();

                        this.LoadSavedElectricHistory(audit);
                        this.LoadSavedGasHistory(audit);

                        this.LoadReportParameters(this.SelectedAudit.Report);

                        this.ClearAllGridData(this.grdDirectInstall_Sheet1);
                        this.ClearAllGridData(this.grdUnitDirectInstall_Sheet1);

                        this.LoadAuditDirectInstallGrids(audit.ProgramId);

                        if (audit.Buildings.Count > 0)
                        {
                            this.SelectedBuilding = audit.Buildings[0];
                            if (this.SelectedBuilding.Retrofits.Count > 0)
                            {
                                this.LoadSavedRetrofits(audit);
                            }
                            if (this.SelectedBuilding.MultiFamily != null)
                            {
                                if (this.SelectedBuilding.MultiFamily.BuildingUnitTypes.Count > 0)
                                {
                                    this.LoadUnits(this.SelectedBuilding);
                                }
                            }
                        }
                        else
                        {
                            this.SelectedBuilding = null;
                        }

                        this.FillBuildingSpaces(audit.ProgramType);
                        this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces

                        if (audit.ProgramType.Equals(FieldTool.Constants.ProgramType.MultiFamily))
                        {
                            this.btnAuditWalkUnits.Enabled = audit.Buildings.Count > 0;
                            if (this.btnAuditWalkUnits.Enabled)
                            {
                                this.SetAuditNavButtons(true, false, false, false, false);
                                this.pslAuditWalkthruPageSlider.SelectedPageIndex = 1;
                            }
                            else
                            {
                                this.SetAuditNavButtons(false, true, false, false, false);
                                this.pslAuditWalkthruPageSlider.SelectedPageIndex = 2;
                            }
                        }

                        this.FillBuildingDetailDropDowns();
                        var filterPostalCode = String.Empty;
                        var filterData = String.Empty;
                        var c = DataStore.GetCompanyByAudit(audit);
                        if (SelectedBuilding == null)
                        {
                            filterPostalCode = c == null ? String.Empty : c.PostalCode;
                        }
                        else
                        {
                            filterPostalCode = SelectedBuilding.PostalCode;
                            filterData = SelectedBuilding.InitFilterDataForBuilding(c) ?? String.Empty;
                        }

                        this.FillRetrofitFilterDropDowns(audit.ProgramId, filterData, filterPostalCode);
                        this.setBuildingListSelectedBuilding();
                        this.setBuildingComboSelectedBuilding();

                        tabControlPanelAuditSurveyQuestions.Controls.Remove(FindControlByName<ucAuditSurvey>(tabControlPanelAuditSurveyQuestions.Controls, AuditUserControlNames.Survey));
                        tabControlPanelAuditSurveyReferrals.Controls.Remove(FindControlByName<ucAuditReferral>(tabControlPanelAuditSurveyReferrals.Controls, AuditUserControlNames.Referral));
                        tabControlPanelAuditSurveyRecommendations.Controls.Remove(FindControlByName<ucAuditRecommendations>(tabControlPanelAuditSurveyRecommendations.Controls, AuditUserControlNames.Recommendation));
                        if (SelectedBuilding != null)
                        {
                            this.UcAuditSurvey.ResizeGrids();
                            this.UcAuditReferral.ResizeGrids();
                            this.UcAuditRecommendations.ResizeGrids();
                            tabAuditSurvey.Enabled = true;

                            if (this.UcAuditSurvey.surveyTable.RowCount > 0)
                            {
                                tabControlPanelAuditSurveyQuestions.Enabled = true;
                                this.UcAuditSurvey.EnableControls();
                            }
                            else
                            {
                                tabControlPanelAuditSurveyQuestions.Enabled = false;
                                this.UcAuditSurvey.DisableControls();
                            }

                            if (this.UcAuditReferral.referralTable.rowCount > 0)
                            {
                                tabControlPanelAuditSurveyReferrals.Enabled = true;
                                this.UcAuditReferral.referralInit.Table.Enabled = true;
                                this.UcAuditReferral.refResponseInit.Table.Visible = true;
                            }
                            else
                            {
                                tabControlPanelAuditSurveyReferrals.Enabled = false;
                                this.UcAuditReferral.referralInit.Table.Enabled = false;
                                this.UcAuditReferral.refResponseInit.Table.Visible = false;
                            }

                            if (this.UcAuditRecommendations.recommendationTable.rowCount > 0)
                            {
                                tabControlPanelAuditSurveyRecommendations.Enabled = true;
                                this.UcAuditRecommendations.recommendationsInit.Table.Enabled = true;
                                this.UcAuditRecommendations.recResponseInit.Table.Visible = true;
                            }
                            else
                            {
                                tabControlPanelAuditSurveyRecommendations.Enabled = false;
                                this.UcAuditRecommendations.recommendationsInit.Table.Enabled = false;
                                this.UcAuditRecommendations.recResponseInit.Table.Visible = false;
                            }
                        }
                        else
                        {
                            tabAuditSurvey.Enabled = false;
                        }

                        if (audit.CompanyContact != null)
                        {
                            txtReportContactName.Text = audit.CompanyContact;
                        }

                        var company = DataStore.GetCompanyByAudit(this.SelectedAudit.Id);
                        string companyName = company != null ? company.Name : string.Empty;
                        txtReportCompanyName.Text = companyName;

                        if (_userFullName != null)
                        {
                            txtReportEaName.Text = _userFullName;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                var inner = ExceptionHelper.Innermost(e);
                Lg.Error(inner.Message);
                MessageBox.Show(inner.Message, "Cannot fill audit details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ok = false;
            }

            return ok;
        }

        private void LoadAuditDirectInstallGrids(string programId)
        {
            // Load the DI grids with components for this program

            this.ClearAuditDirectInstallGridData();
            this.ClearUnitDirectInstallGridData();

            ApiProgramMetadata programByCode = DataStore.GetProgramsByProgramCode(programId).FirstOrDefault();
            if (programByCode != default(ApiProgramMetadata))
            {
                List<ApiComponent> componentsByProgramCode = programByCode.diComponents ?? new List<ApiComponent>();
                List<ApiComponent> multiFamilyComponentsByProgramCode = programByCode.diComponents ?? new List<ApiComponent>();

                if (!String.IsNullOrEmpty(programByCode.program.AssociatedDIProgram))
                {
                    ApiProgramMetadata associatedDiprogramByCode = DataStore.GetProgramsByProgramCode(programByCode.program.AssociatedDIProgram).FirstOrDefault();
                    if (associatedDiprogramByCode == default(ApiProgramMetadata))
                    {
                        throw new Exception(String.Format("Program {0} has an associated program {1} that is not found in your program file. Try downloading again.",
                            programByCode.program.ProgramCode,
                            programByCode.program.AssociatedDIProgram));
                    }
                    multiFamilyComponentsByProgramCode = associatedDiprogramByCode.diComponents ?? new List<ApiComponent>();
                }

                if (this.SelectedBuilding != null)
                {
                    // only use the first program's filters
                    componentsByProgramCode = ApplyBuildingRetrofitFilters(componentsByProgramCode, this.SelectedBuilding.FilterData, programByCode.filters);
                    var c = DataStore.GetCompanyByAudit(SelectedAudit);
                    multiFamilyComponentsByProgramCode = ApplyBuildingRetrofitFilters(multiFamilyComponentsByProgramCode, this.SelectedBuilding.InitFilterDataForBuilding(c), programByCode.filters);
                }

                foreach (ApiComponent component in componentsByProgramCode)
                {
                    if (string.IsNullOrWhiteSpace(component.ClipboardIcon))
                    {
                        component.ClipboardIcon = DEFAULT_COMPONENT_ICON;
                    }
                }

                foreach (ApiComponent component in multiFamilyComponentsByProgramCode)
                {
                    if (string.IsNullOrWhiteSpace(component.ClipboardIcon))
                    {
                        component.ClipboardIcon = DEFAULT_COMPONENT_ICON;
                    }
                }

                try
                {
                    //--Direct Install Grid--

                    //this.grdDirectInstall_Sheet1.ClearRange(0, 0, this.grdDirectInstall_Sheet1.RowCount, this.grdDirectInstall_Sheet1.ColumnCount, true);
                    this.grdDirectInstall_Sheet1.AutoGenerateColumns = false;

                    this.grdDirectInstall_Sheet1.DataSource = componentsByProgramCode;
                    this.grdDirectInstall_Sheet1.DataMember = "Benlink_Components";
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_PROGRAM_CODE, "ProgramCode");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_NAME, "Name");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_ID, "Id");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_KGALLONS, "KGallons");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_SAVINGS, "Savings");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_ICON_FILE_NAME, "ClipboardIcon");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_KWH_UNIT, "KwhUnit");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_THERMS_UNIT, "Therms");
                    this.grdDirectInstall_Sheet1.BindDataColumn(DI_GRID_COLUMN_COMPONENT_TYPE, "ComponentType");

                    for (int x = 0; x < this.grdDirectInstall_Sheet1.Rows.Count; x++)
                    {
                        this.grdDirectInstall.Sheets[0].SetRowHeight(x, 50);
                        this.grdDirectInstall_Sheet1.Rows[x].Resizable = false;
                    }

                    for (int x = 0; x < this.grdDirectInstall_Sheet1.Columns.Count; x++)
                    {
                        this.grdDirectInstall_Sheet1.Columns[x].CanFocus = (x == DI_GRID_COLUMN_QUANTITY);
                    }

                    //--Unit Direct Install Grid--

                    //this.grdUnitDirectInstall_Sheet1.ClearRange(0, 0, this.grdUnitDirectInstall_Sheet1.RowCount, this.grdUnitDirectInstall_Sheet1.ColumnCount, true);
                    this.grdUnitDirectInstall_Sheet1.AutoGenerateColumns = false;

                    this.grdUnitDirectInstall_Sheet1.DataSource = multiFamilyComponentsByProgramCode;
                    this.grdUnitDirectInstall_Sheet1.DataMember = "Benlink_Components";
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_PROGRAM_CODE, "ProgramCode");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_NAME, "Name");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_ID, "Id");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_SAVINGS, "Savings");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_SPACE, "Space");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_ICON_FILE_NAME, "ClipboardIcon");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_KWH_UNIT, "KwhUnit");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_THERMS_UNIT, "Therms");
                    this.grdUnitDirectInstall_Sheet1.BindDataColumn(UNIT_DI_GRID_COLUMN_COMPONENT_TYPE, "ComponentType");

                    for (int x = 0; x < this.grdUnitDirectInstall_Sheet1.Rows.Count; x++)
                    {
                        this.grdUnitDirectInstall.Sheets[0].SetRowHeight(x, 45);
                        this.grdUnitDirectInstall_Sheet1.Rows[x].Resizable = false;
                    }

                    for (int x = 0; x < this.grdUnitDirectInstall_Sheet1.Columns.Count; x++)
                    {
                        this.grdUnitDirectInstall_Sheet1.Columns[x].CanFocus = (x >= UNIT_DI_GRID_COLUMN_INCREMENT_QTY && x <= UNIT_DI_GRID_COLUMN_SPACE);
                    }

                    this.SetGridColumnWidths(true, false, true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                if (this.grdDirectInstall_Sheet1.Rows.Count > 0)
                {
                    this.grdDirectInstall.Enabled = true;
                }
                else
                {
                    this.grdDirectInstall.Enabled = false;
                }

                if (this.grdUnitDirectInstall_Sheet1.Rows.Count > 0)
                {
                    this.grdUnitDirectInstall.Enabled = true;
                }
                else
                {
                    this.grdUnitDirectInstall.Enabled = false;
                }
            }
            else if (!String.IsNullOrEmpty(programId) && programByCode == default(ApiProgramMetadata))
            {
                throw new Exception(String.Format("No Program found for code {0} in your program file. Try downloading again.", programId));
            }
        }

        private void LoadReportParameters(ReportParameters reportParameters)
        {
            //this.txtReportCompanyName.Text = "";
            //this.txtReportContactName.Text = "";
            //this.txtReportEaName.Text = "";
            this.txtEnergyAdvisorPhone.ResetText();
            this.txtEnergyAdvisorEmail.ResetText();
            this.txtTopRecommendation.ResetText();

            //foreach (ListViewItem li in this.lstCustomReports.Items)
            //{
            //    if (li.Text == "Introduction" || li.Text == "Top 2 Recommendations" || li.Text == "Direct Install Summary")
            //    {
            //        li.Checked = true;
            //    }
            //    else
            //    {
            //        li.Checked = false;
            //    }
            //}

            if (reportParameters != null)
            {
                if (!String.IsNullOrEmpty(reportParameters.CompanyNameOverride)) this.txtReportCompanyName.Text = reportParameters.CompanyNameOverride;
                if (!String.IsNullOrEmpty(reportParameters.ContactNameOverride)) this.txtReportContactName.Text = reportParameters.ContactNameOverride;
                if (!String.IsNullOrEmpty(reportParameters.EnergyAdvisorNameOverride)) this.txtReportEaName.Text = reportParameters.EnergyAdvisorNameOverride;
                this.txtEnergyAdvisorPhone.Text = reportParameters.EnergyAdvisorPhone;
                this.txtEnergyAdvisorEmail.Text = reportParameters.EnergyAdvisorEmail;
                this.txtTopRecommendation.Text = reportParameters.TopRecommendation;

                if (reportParameters.Filters.Count > 0)
                {
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        if (reportParameters.Filters.Contains(li.Text))
                        {
                            li.Checked = true;
                        }
                    }
                }
            }
        }

        private void LoadSavedGasHistory(Audit audit)
        {
            this.ClearGasHistory();

            foreach (Building building in audit.Buildings)
            {
                int row = 3;

                foreach (GasHistoryRecord gasHistoryRecord in building.GasHistory)
                {
                    for (int col = 2; col <= 7; col++)
                    {
                        switch (col)
                        {
                            case 2:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.ReadDate;
                                break;

                            case 3:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.BillingDays;
                                break;

                            case 4:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.CoolDegreeDays;
                                break;

                            case 5:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.HeatDegreeDays;
                                break;

                            case 6:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.Therms;
                                break;

                            case 7:
                                this.grdGasHist_Sheet1.Cells[row, col].Value = gasHistoryRecord.TotalBill;
                                break;
                        }
                    }

                    row++;

                    // Limit the history to 12 rows.
                    if (row > 14)
                    {
                        break;
                    }
                }
            }
        }

        private void LoadSavedElectricHistory(Audit audit)
        {
            this.ClearElectricHistory();

            foreach (Building building in audit.Buildings)
            {
                int row = 3;

                foreach (ElectricHistoryRecord electricHistoryRecord in building.ElectricHistory)
                {
                    for (int col = 2; col <= grdElectricHist_Sheet1.Columns.Count - 2; ++col)
                    {
                        switch (col)
                        {
                            case 2:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.ReadDate;
                                break;

                            case 3:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.BillDays;
                                break;

                            case 4:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.CoolDays;
                                break;

                            case 5:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.HeatDays;
                                break;

                            case 6:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.TotalKwh;
                                break;

                            case 7:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.OnPeakKwh;
                                break;

                            case 8:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.OffPeakKwh;
                                break;

                            case 9:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.BilledKw;
                                break;

                            case 10:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.MaximumCustomerKw;
                                break;

                            case 11:
                                this.grdElectricHist_Sheet1.Cells[row, col].Value = electricHistoryRecord.TotalBill;
                                break;
                        }
                    }

                    row++;

                    // Limit the history to 12 rows.
                    if (row > 14)
                    {
                        break;
                    }
                }
            }
        }

        private void LoadSavedRetrofits(Audit a)
        {
            string componentID = "";
            string quantity = "";

            for (int i = 0; i < this.grdDirectInstall_Sheet1.Rows.Count; i++)
            {
                // Retrofit DI = new BLL.Retrofit();
                if (this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_PROGRAM_CODE].Value != null)
                {
                    if (this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_QUANTITY].Value != null)
                    {
                        quantity = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_QUANTITY].Value.ToString();
                    }

                    //  DI.EligibleComponentId = grdDirectInstall_Sheet1.Cells[i, 4].Value.ToString();
                    if (this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_ID].Value != null)
                    {
                        componentID = this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_ID].Value.ToString();
                    }

                    foreach (Building b in a.Buildings)
                    {
                        if (this.SelectedBuilding != null)
                        {
                            if (b.Id == this.SelectedBuilding.Id)
                            {
                                foreach (Retrofit r in b.Retrofits)
                                {
                                    if (r.EligibleComponentId == componentID)
                                    {
                                        // The kWh Unit and Therms Unit columns are set when loading the retrofits and should not be modified by the specific retrofit
                                        this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_QUANTITY].Value = r.Quantity;
                                        this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH].Value = r.KWh;
                                        this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS].Value = r.Therms;
                                        this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KGALLONS].Value = r.Water;
                                        this.grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SAVINGS].Value = r.Savings;
                                        this.grdDirectInstall_Sheet1.Rows[i].Tag = r;

                                        //var kwh = Convert.ToDouble(grdDirectInstall_Sheet1.Cells[i, 11].Value);
                                        //var kwhSavings = r.Quantity * kwh;
                                        //grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_KWH].Value = kwhSavings;
                                        //var mcf = Convert.ToDouble(grdDirectInstall_Sheet1.Cells[i, 12].Value);
                                        //var mcfSavings = r.Quantity * mcf;
                                        //grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_THERMS].Value = mcfSavings;

                                        //grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SAVINGS].Value = (.1 * kwhSavings) + (8 * mcfSavings);
                                        ////grdDirectInstall_Sheet1.Cells[i, DI_GRID_COLUMN_SAVINGS].Value = r.Savings;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < this.grdDirectInstall_Sheet1.Columns.Count; x++)
            {
                this.grdDirectInstall_Sheet1.Columns[x].CanFocus = (x == DI_GRID_COLUMN_QUANTITY);
            }
        }

        private string GetComponentIconFileName(string componentId)
        {
            string result = "";

            if (File.Exists(DataStore.XmlReportDiIconsFile))
            {
                ReportDiIcons items = new ReportDiIcons();
                using (TextReader reader = new StreamReader(DataStore.XmlReportDiIconsFile))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(ReportDiIcons));
                    object o = ser.Deserialize(reader);
                    items = o as ReportDiIcons;
                    if (items != null)
                    {
                        result = items.GetIconByBenLinkId(componentId);
                    }
                }
            }

            return result;
        }

        private void LoadSavedUnitRetrofits(BuildingUnitType u)
        {
            string componentID = "";
            string quantity = "";

            for (int i = 0; i < this.grdUnitDirectInstall_Sheet1.Rows.Count; i++)
            {
                // Retrofit DI = new BLL.Retrofit();
                if (this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_PROGRAM_CODE].Value != null)
                {
                    if (this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_QUANTITY].Value != null)
                    {
                        quantity = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_QUANTITY].Value.ToString();
                    }

                    //  DI.EligibleComponentId = grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ID ].Value.ToString();
                    if (this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ID].Value != null)
                    {
                        componentID = this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_ID].Value.ToString();
                    }

                    foreach (RetrofitEstimate r in u.RetrofitEstimates)
                    {
                        if (r.EligibleComponentId == componentID)
                        {
                            // The kWh Unit and Therms Unit columns are set when loading the default retrofits and should not be modified by the specific retrofit
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_QUANTITY].Value = r.Quantity;
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SPACE].Value = r.Space;
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KWH].Value = r.KWh;
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_THERMS].Value = r.Therms;
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_KGALLONS].Value = r.Water;
                            this.grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SAVINGS].Value = r.Savings;
                            this.grdUnitDirectInstall_Sheet1.Rows[i].Tag = r;

                            break;
                        }
                    }
                }
            }

            for (int x = 0; x < this.grdUnitDirectInstall_Sheet1.Columns.Count; x++)
            {
                this.grdUnitDirectInstall_Sheet1.Columns[x].CanFocus = (x >= UNIT_DI_GRID_COLUMN_INCREMENT_QTY && x <= UNIT_DI_GRID_COLUMN_SPACE);
            }
        }

        private void SelectCalendarDate(DateTime dt)
        {
            if (!this.DesignMode)
            {
                // Set the calendar control (left panel) to the specified date.
                this.calApptMonthCalendar.DisplayMonth = dt;
                this.calApptMonthCalendar.SelectedDate = dt;

                try
                {
                    this.cvApptCalendarView.ScrollToTime(6, 0);
                }
                catch (Exception)
                {
                    ;
                }
            }
        }

        private void SetNewAudit(bool doLaunch)
        {
            if (!this.DesignMode)
            {
                // Fill the details panel with the selected audit.
                if (this.cvApptCalendarView.SelectedAppointments == null || this.cvApptCalendarView.SelectedAppointments.Count == 0)
                {
                    this.FillAuditDetails(null);
                }
                else
                {
                    Appointment appt = this.cvApptCalendarView.SelectedAppointments[0].Appointment;
                    if (appt != null)
                    {
                        Audit audit = appt.Tag as Audit;
                        if (audit != null)
                        {
                            var ok = this.FillAuditDetails(audit);

                            if (doLaunch && ok)
                            {
                                this.DoLaunch();
                            }
                        }
                    }
                }
            }
        }

        private string Unescape(string value)
        {
            if (value == null)
                return null;

            var length = value.Length;
            var result = new StringBuilder(length);

            for (var i = 0; i < length; i++)
            {
                var c = value[i];

                if (c == '\\' && i++ < length)
                {
                    c = value[i];

                    switch (c)
                    {
                        case 'n':
                            result.Append('\n');
                            break;

                        case 'r':
                            result.Append('\r');
                            break;

                        case 't':
                            result.Append('\t');
                            break;

                        case '\\':
                            result.Append('\\');
                            break;

                        default:
                            result.Append(c);
                            break;
                    }
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        private void ItemEditHelper()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (this.SelectedAudit != null)
            {
                int index = this.grdEipItems.ActiveSheet.ActiveRowIndex;

                if (index >= 0)
                {
                    object tag = this.grdEipItems.ActiveSheet.ActiveRow.Tag;
                    if (tag != null)
                    {
                        EquipmentMaster equipment = tag as EquipmentMaster;

                        if (equipment != null)
                        {
                            Schedule.ScheduleCollection schedules = new Schedule.ScheduleCollection();
                            schedules.Add(this.schAuditOccupancy.GetSchedule());
                            schedules.Add(this.schAuditInteriorLights.GetSchedule());
                            schedules.Add(this.schAuditExteriorLights.GetSchedule());
                            schedules.Add(this.schAuditHvac.GetSchedule());

                            using (frmEquipmentAudit frm = new frmEquipmentAudit(equipment, this.GetSelectedSpaces(), schedules, this.SelectedBuilding.BuildingCategory,
                                this.GetExistingEquipmentNames(), this.SelectedAudit.Id, this.SelectedBuilding))
                            {
                                DialogResult result = frm.ShowDialog();

                                if (result == DialogResult.OK)
                                {
                                    equipment = frm.EquipmentObject;

                                    if (equipment != null)
                                    {
                                        Cursor.Current = Cursors.WaitCursor;

                                        DataStore.UpdateEquipment(equipment, SelectedAudit, SelectedBuilding, true);
                                        List<EquipmentMaster> equipmentCollection = DataStore.GetEquipmentByBuildingId(this.SelectedBuilding.Id);
                                        this.ResetItemsGrid();

                                        foreach (EquipmentMaster equipmentItem in equipmentCollection)
                                        {
                                            this.AddEquipmentToGrid(equipmentItem);
                                        }

                                        // Traverse grid and get recommendations for each one.
                                        this.RefreshRecommendationsByAudit();

                                        Cursor.Current = Cursors.Default;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void AddEquipmentToGrid(EquipmentMaster equipment)
        {
            if (equipment != null)
            {
                int nextAvailableRow = this.grdEipItems.ActiveSheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data) + 1;

                for (int i = 0; i < nextAvailableRow; i++)
                {
                    string cellName = this.grdEipItems.ActiveSheet.GetText(i, EIP_GRID_COLUMN_NAME);
                    if (cellName == equipment.EquipmentName)
                    {
                        nextAvailableRow = i;
                        break;
                    }
                }

                this.grdEipItems.ActiveSheet.AddRows(nextAvailableRow, 1);
                this.grdEipItems.ActiveSheet.Rows[nextAvailableRow].Height = 50;

                string space = "";
                if (equipment.Spaces != null && equipment.Spaces.Count > 0)
                {
                    space = equipment.Spaces[0].Space;
                }
                string name = String.Format("{0} - {1}", equipment.ComponentName, space);

                this.grdEipItems.ActiveSheet.Cells[nextAvailableRow, EIP_GRID_COLUMN_ID].Text = equipment.EquipmentMasterId;
                this.grdEipItems.ActiveSheet.Cells[nextAvailableRow, EIP_GRID_COLUMN_NAME].Text = name;
                this.grdEipItems.ActiveSheet.Cells[nextAvailableRow, EIP_GRID_COLUMN_QUANTITY].Text = equipment.Quantity.ToString();
                this.grdEipItems.ActiveSheet.Rows[nextAvailableRow].Tag = equipment;

                //add the equimpent image
                if (equipment.ImageFilePath != "")
                {
                    if (File.Exists(equipment.ImageFilePath))
                    {
                        FarPoint.Win.Spread.CellType.ImageCellType imgct = new FarPoint.Win.Spread.CellType.ImageCellType();
                        System.Drawing.Image image = System.Drawing.Image.FromFile(equipment.ImageFilePath);

                        imgct.Style = FarPoint.Win.RenderStyle.Stretch;
                        imgct.TransparencyColor = Color.Black;
                        imgct.TransparencyTolerance = 20;

                        this.grdEipItems.ActiveSheet.Rows[nextAvailableRow].Height = 50;
                        this.grdEipItems.ActiveSheet.Columns[EIP_GRID_COLUMN_IMAGE].Width = 70;

                        grdEipItems.Sheets[0].Cells[nextAvailableRow, EIP_GRID_COLUMN_IMAGE].CellType = imgct;
                        this.grdEipItems.Sheets[0].Cells[nextAvailableRow, EIP_GRID_COLUMN_IMAGE].Value = image;
                    }
                    else
                    {
                        BLL.CommonUtilities.DeleteImageFromComponent(equipment, this.SelectedAudit, this.SelectedBuilding, false);
                    }
                }
            }
        }

        private List<string> GetExistingEquipmentNames()
        {
            List<string> result = new List<string>();

            int nextAvailableRow = this.grdEipItems.ActiveSheet.GetLastNonEmptyRow(NonEmptyItemFlag.Data);

            if (nextAvailableRow >= 0)
            {
                for (int i = 0; i <= nextAvailableRow; i++)
                {
                    string txt = this.grdEipItems.ActiveSheet.GetText(i, EIP_GRID_COLUMN_NAME);
                    if (!string.IsNullOrWhiteSpace(txt))
                    {
                        result.Add(txt);
                    }
                }
            }

            return result;
        }

        private void OpenEaForm(Enumerations.EnergyType type, int typeId)
        {
            if (!this.DesignMode)
            {
                if (this.SelectedAudit != null)
                {
                    if (this.SelectedBuilding == null)
                    {
                        MessageBox.Show("Please select a building before adding equipment.", "Select Building", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.cboActiveBuilding.Focus();
                    }
                    else
                    {
                        Schedule.ScheduleCollection schedules = new Schedule.ScheduleCollection();
                        schedules.Add(this.schAuditOccupancy.GetSchedule());
                        schedules.Add(this.schAuditInteriorLights.GetSchedule());
                        schedules.Add(this.schAuditExteriorLights.GetSchedule());
                        schedules.Add(this.schAuditHvac.GetSchedule());

                        using (frmEquipmentAudit frm = new frmEquipmentAudit(type, typeId, this.GetSelectedSpaces(), schedules, this.SelectedBuilding.BuildingCategory,
                            this.GetExistingEquipmentNames(), this.SelectedAudit.Id, this.SelectedBuilding))
                        {
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult dlg = frm.ShowDialog();
                            Cursor.Current = Cursors.Default;

                            if (dlg == DialogResult.OK)
                            {
                                EquipmentMaster equipment = frm.EquipmentObject;

                                if (equipment != null)
                                {
                                    Cursor.Current = Cursors.WaitCursor;

                                    //DataStore.DeleteRecommendationsFromAudit(this.SelectedAudit.Id, true);

                                    // Add equipment to grid.
                                    this.AddEquipmentToGrid(equipment);

                                    DataStore.SaveEquipmentToBuilding(this.SelectedAudit.Id, this.SelectedBuilding.Id, equipment);

                                    // Traverse grid and get recommendations for each one.
                                    this.RefreshRecommendationsByAudit();

                                    Cursor.Current = Cursors.Default;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SetScheduleProperties()
        {
            this.schAuditOccupancy.SetColors();
            this.schAuditInteriorLights.SetColors();
            this.schAuditExteriorLights.SetColors();
            this.schAuditHvac.SetColors();

            this.schAuditOccupancy.SetProperties("Occupancy", "Occupancy");
            this.schAuditInteriorLights.SetProperties("InteriorLights", "Interior Lights");
            this.schAuditExteriorLights.SetProperties("ExteriorLights", "Exterior Lights");
            this.schAuditHvac.SetProperties("HVAC", "HVAC");
        }

        private bool BuildingExists()
        {
            bool result = false;
            foreach (ListViewItem li in this.lstBuildingList.Items)
            {
                object tag = li.Tag;
                if (tag != null)
                {
                    Building b = tag as Building;
                    if (b != null)
                    {
                        if (li.Text == this.txtBuildingName.Text && b.Id == this.txtBuildingId.Text)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private void ClearMultifamilyUnitControls()
        {
            this._unitNumberingChangedByUser = false;
            this.cboUnitNumbering.SelectedIndex = -1;
            this._unitNumberingChangedByUser = true;

            this.txtUnitId.Clear();
            this.txtUnitNumberOfUnits.Clear();

            this.ClearUnits();

            this.lstUnitBedroom.SelectedIndices.Clear();
            this.lstUnitBathroom.SelectedIndices.Clear();
            this.lstUnitHeating.SelectedIndices.Clear();
            this.lstUnitCooling.SelectedIndices.Clear();
            this.lstUnitLocation.SelectedIndices.Clear();

            this.txtUnitType.Clear();
            this.txtUnitTypeName.Clear();
            this.txtUnitSqFt.Value = 0;
            this.txtUnitQty.Clear();

            this.ClearUnitDirectInstallGridData();
        }

        private void ClearBuildingControls()
        {
            this.txtBuildingId.Text = "";
            this.optBuildingCommercial.Checked = true;
            this.cboBuildingCategory.SelectedIndex = -1;
            this.cboBuildingType.SelectedIndex = -1;
            this.txtBuildingName.Text = "";
            this.txtBuildingAddress1.Text = "";
            this.txtBuildingAddress2.Text = "";
            this.txtBuildingCity.Text = "";
            this.cboBuildingState.Text = "";
            this.txtBuildingZip.Text = "";
            this.txtBuildingZipExt.Text = "";
            this.txtBuildingNumUnits.Text = "";
            this.txtBuildingRoofType.Text = "";
            this.cboBuildingWallType.SelectedIndex = -1;
            this.cboBuildingFoundation.SelectedIndex = -1;
            this.txtBuildingSafetyConcerns.Text = "";
            this.txtBuildingSpecialEquipment.Text = "";
            this.txtBuildingComments.Text = "";
            this.txtBuildingYearBuilt.Text = "";
            this.cboBuildingHeatingSystem.SelectedIndex = -1;
            this.cboBuildingHeatFuel.SelectedIndex = -1;
            this.cboBuildingAirConditioning.SelectedIndex = -1;
            this.cboBuildingDHW.SelectedIndex = -1;
            this.cboBuildingDHWFuel.SelectedIndex = -1;
            this.cboBuildingParking.SelectedIndex = -1;
            this.cboBuildingAtticType.SelectedIndex = -1;
            this.cboBuildingWindows.SelectedIndex = -1;
            this.chbAffordableHousing.Checked = false;
            this.txtBuildingElectricAccountNumber.Text = "";
            this.txtBuildingElectricAccountNumberValue.Text = "";
            this.txtBuildingElectricRateCode.Text = "";
            this.txtBuildingGasAccountNumber.Text = "";
            this.txtBuildingGasAccountNumberValue.Text = "";
            this.txtBuildingGasRateCode.Text = "";
            this.numBuildingOccupants.Value = 0;
            this.numBuildingOccupants.ResetText();
            this.numBuildingFloorsAbove.Value = 0;
            this.numBuildingFloorsAbove.ResetText();
            this.numBuildingFloorsBelow.Value = 0;
            this.numBuildingFloorsBelow.ResetText();
            this.numBuildingGrossFloor.Value = 0;
            this.numBuildingGrossFloor.ResetText();
            this.numBuildingOccupiedFloor.Value = 0;
            this.numBuildingOccupiedFloor.ResetText();
            this.SetBuildingPropertyTypeEnabled();
        }

        private Building GetSelectedBuildingFromList()
        {
            Building result = null;

            if (this.lstBuildingList.SelectedItems != null && this.lstBuildingList.SelectedItems.Count > 0)
            {
                Building b = this.lstBuildingList.SelectedItems[0].Tag as Building;

                if (b != null)
                {
                    result = b;
                }
            }

            return result;
        }

        private void InitializeBuildingTab(string selectedBuildingName)
        {
            //frmMainBuildingHelper.RegisterControls(this.lstBuildingList, this.pnlBuildingEditButtons, this.itmBuidlingEditButtons, this.btnBuildingAdd, this.btnBuildingCopy, this.btnBuildingEdit,
            //    this.btnBuildingDelete, this.lblNew, this.lblEdit, this.lblCopy, this.tabBuildingInfo, this.pnlBuildingGeneral, this.lblBuildingCategory, this.cboBuildingCategory, this.lblBuildingType,
            //    this.cboBuildingType, this.lblBuildingName, this.txtBuildingName, this.lblBuildingAddress, this.txtBuildingAddress1, this.txtBuildingAddress2, this.pnlCopyAddressButton,
            //    this.itmCopyAddressButton, this.btnCopyAddress, this.lblBuildingCity, this.txtBuildingCity, this.lblBuildingState, this.cboBuildingState, this.lblBuildingZip, this.txtBuildingZip,
            //    this.txtBuildingZipExt, this.lblBuildingElectricAccountNumber, this.txtBuildingElectricAccountNumber, this.lblBuildingElectricRateCode, this.txtBuildingElectricRateCode,
            //    lblBuildingGasAccountNumber, this.txtBuildingGasAccountNumber, this.lblBuildingGasRateCode, this.txtBuildingGasRateCode, this.pnlBuildingDetails, this.lblBuildingNumUnits, this.txtBuildingNumUnits,
            //    this.lblBuildingYearBuilt, this.txtBuildingYearBuilt, this.lblBuildingRoofType, this.txtBuildingRoofType, this.lblBuildingSafetyConcerns, this.txtBuildingSafetyConcerns, this.lblBuildingWallType,
            //    this.cboBuildingWallType, this.lblBuildingSpecialEquipment, this.txtBuildingSpecialEquipment, this.lblBuildingFoundation, this.cboBuildingFoundation, this.lblBuildingComments,
            //    this.txtBuildingComments, this.lblBuildingOccupants, this.numBuildingOccupants, this.lblBuildingFloorsAbove, this.numBuildingFloorsAbove, this.lblBuildingFloorsBelow, this.numBuildingFloorsBelow,
            //    this.lblBuildingGrossFloor, this.numBuildingGrossFloor, this.lblBuildingOccupiedFloor, this.numBuildingOccupiedFloor, this.pnlBuildingSaveButtons, this.itmBuildingSaveButtons,
            //    this.btnBuildingCancel, this.btnBuildingSave, this.txtBuildingHoursEquivalent, this.txtBuildingId);

            //frmMainBuildingHelper.Initialize();

            this.SetBuildingPropertyTypeEnabled();

            this.txtBuildingId.Text = "";
            this.lstBuildingList.Items.Clear();
            this.cboActiveBuilding.Items.Clear();

            if (this.SelectedAudit == null)
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.None);
            }
            else
            {
                this.LoadSignature();
                this.recRecommendations.RepopulateControl(this.SelectedAudit.Id);

                if (this.SelectedAudit.Buildings == null)
                {
                    this.SetBuildingControlVisibility(Enumerations.EditModes.None);
                }
                else
                {
                    foreach (Building b in this.SelectedAudit.Buildings)
                    {
                        ListViewItem li = this.lstBuildingList.FindItemWithText(b.BuildingName);

                        if (li == null)
                        {
                            li = new ListViewItem(b.BuildingName);
                            li.Tag = b;

                            this.lstBuildingList.Items.Add(li);
                            this.cboActiveBuilding.Items.Add(li);
                        }
                    }

                    if (this.lstBuildingList.Items.Count > 0)
                    {
                        this.lstBuildingList.Items[0].Selected = true;
                        this.SetBuildingControlVisibility(Enumerations.EditModes.Selected);
                    }

                    this.FillBuildingDropDowns(this.SelectedAudit.ProgramType);
                }
            }
        }

        private void SelectAudit()
        {
            this.SelectedBuilding = this.GetSelectedBuildingFromList();

            if (this.SelectedBuilding == null)
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.None);
            }
            else
            {
                this.SetBuildingControlVisibility(Enumerations.EditModes.Edit);
            }
        }

        private void SelectBuildingInList(bool setActiveBuildingDropDown)
        {
            if (this.SelectedBuilding != null)
            {
                foreach (ListViewItem li in this.lstBuildingList.Items)
                {
                    if (String.Compare(li.Text, this.SelectedBuilding.BuildingName, true) == 0)
                    {
                        li.Selected = true;
                        break;
                    }
                }
            }
        }

        private void SetActiveBuilding()
        {
            if (!this._savingBuilding)
            {
                this.lblRecommenationSelectedBuilding.Text = "";

                foreach (ListViewItem li in this.cboActiveBuilding.Items)
                {
                    //if (li.Text == this.SelectedBuilding.BuildingName) {
                    if (this.SelectedBuilding != null)
                    {
                        if (li.Text == this.SelectedBuilding.BuildingName)
                        {
                            this.cboActiveBuilding.SelectedItem = li;
                            this.lblRecommenationSelectedBuilding.Text = "Active Building: " + li.Text;

                            break;
                        }
                    }
                }
            }
        }

        private void SetBuildingButtonsEnabled(bool addEnabled, bool editEnable, bool deleteEnabled, bool cancelEnabled, bool saveEnabled, bool detailPanelEnabled)
        {
            this.btnBuildingAdd.Enabled = addEnabled;
            this.btnBuildingEdit.Enabled = editEnable;
            this.btnBuildingDelete.Enabled = deleteEnabled;
            this.btnBuildingCancel.Enabled = cancelEnabled;
            this.btnBuildingSave.Enabled = saveEnabled;

            this.SetBuildingDetailsControlsEnabled(detailPanelEnabled);

            this.pnlBuildingDetails.Enabled = !editEnable;
            this.pnlBuildingAdditionalDetails.Enabled = !editEnable;
            this.pnlBuildingRetrofitFilters.Enabled = !editEnable;
        }

        private void SetBuildingDetailsControlsEnabled(bool isEnabled)
        {
            this.SetBuildingPropertyTypeEnabled();

            this.cboBuildingCategory.Enabled = isEnabled;
            this.cboBuildingType.Enabled = isEnabled;

            this.txtBuildingName.ReadOnly = !isEnabled;
            this.txtBuildingAddress1.ReadOnly = !isEnabled;
            this.txtBuildingAddress2.ReadOnly = !isEnabled;
            this.btnCopyAddress.Enabled = isEnabled;
            this.txtBuildingCity.ReadOnly = !isEnabled;

            this.cboBuildingState.Enabled = isEnabled;

            this.txtBuildingZip.ReadOnly = !isEnabled;
            this.txtBuildingZipExt.ReadOnly = !isEnabled;

            this.txtBuildingElectricAccountNumber.ReadOnly = !isEnabled;
            this.txtBuildingElectricRateCode.ReadOnly = !isEnabled;
            this.txtBuildingElectricAccountNumberValue.ReadOnly = !isEnabled;

            this.txtBuildingGasAccountNumber.ReadOnly = !isEnabled;
            this.txtBuildingGasRateCode.ReadOnly = !isEnabled;
            this.txtBuildingGasAccountNumberValue.ReadOnly = !isEnabled;
        }

        private void SetBuildingControlVisibility(Enumerations.EditModes mode)
        {
            switch (mode)
            {
                case Enumerations.EditModes.Add:
                    this.lblNew.Visible = true;
                    this.lblCopy.Visible = false;
                    this.lblEdit.Visible = false;
                    this.btnBuildingAdd.Enabled = false;
                    this.btnBuildingCopy.Enabled = false;
                    this.btnBuildingDelete.Enabled = false;
                    this.btnBuildingEdit.Enabled = false;

                    this.SetBuildingDetailsControlsEnabled(true);

                    this.pnlBuildingDetails.Enabled = true;
                    this.pnlBuildingAdditionalDetails.Enabled = true;
                    this.pnlBuildingRetrofitFilters.Enabled = true;
                    this.ClearBuildingControls();
                    this.btnBuildingCancel.Enabled = true;
                    this.btnBuildingSave.Enabled = true;

                    break;

                case Enumerations.EditModes.Cancel:
                    this.lblNew.Visible = false;
                    this.lblCopy.Visible = false;
                    this.lblEdit.Visible = false;
                    this.btnBuildingAdd.Enabled = true;
                    this.btnBuildingCopy.Enabled = true;
                    this.btnBuildingDelete.Enabled = true;
                    this.btnBuildingEdit.Enabled = true;

                    this.SetBuildingDetailsControlsEnabled(false);

                    this.pnlBuildingDetails.Enabled = false;
                    this.pnlBuildingAdditionalDetails.Enabled = false;
                    this.pnlBuildingRetrofitFilters.Enabled = false;
                    this.ClearBuildingControls();
                    this.btnBuildingCancel.Enabled = false;
                    this.btnBuildingSave.Enabled = false;

                    break;

                case Enumerations.EditModes.Copy:
                    this.lblNew.Visible = false;
                    this.lblCopy.Visible = true;
                    this.lblEdit.Visible = false;
                    this.btnBuildingAdd.Enabled = false;
                    this.btnBuildingCopy.Enabled = false;
                    this.btnBuildingDelete.Enabled = false;
                    this.btnBuildingEdit.Enabled = false;

                    this.SetBuildingDetailsControlsEnabled(true);

                    this.pnlBuildingDetails.Enabled = true;
                    this.pnlBuildingAdditionalDetails.Enabled = true;
                    this.pnlBuildingRetrofitFilters.Enabled = true;
                    //this.SyncBuilding(SyncDirection.ObjectToControls);
                    this.btnBuildingCancel.Enabled = true;
                    this.btnBuildingSave.Enabled = true;
                    break;

                case Enumerations.EditModes.Edit:
                    this.lblNew.Visible = false;
                    this.lblCopy.Visible = false;
                    this.lblEdit.Visible = true;
                    this.btnBuildingAdd.Enabled = true;
                    this.btnBuildingCopy.Enabled = true;
                    this.btnBuildingDelete.Enabled = true;
                    this.btnBuildingEdit.Enabled = false;

                    this.SetBuildingDetailsControlsEnabled(true);

                    this.pnlBuildingDetails.Enabled = true;
                    this.pnlBuildingAdditionalDetails.Enabled = true;
                    this.pnlBuildingRetrofitFilters.Enabled = true;
                    this.SyncBuilding(Enumerations.SyncDirection.EditObjectToControls);
                    this.btnBuildingCancel.Enabled = true;
                    this.btnBuildingSave.Enabled = true;

                    break;

                case Enumerations.EditModes.Selected:
                    this.lblNew.Visible = false;
                    this.lblCopy.Visible = false;
                    this.lblEdit.Visible = false;
                    this.btnBuildingAdd.Enabled = true;
                    this.btnBuildingCopy.Enabled = true;
                    this.btnBuildingDelete.Enabled = true;
                    this.btnBuildingEdit.Enabled = true;

                    this.SetBuildingDetailsControlsEnabled(false);

                    this.pnlBuildingDetails.Enabled = false;
                    this.pnlBuildingAdditionalDetails.Enabled = false;
                    this.pnlBuildingRetrofitFilters.Enabled = false;
                    //this.ClearBuildingControls();
                    this.btnBuildingCancel.Enabled = false;
                    this.btnBuildingSave.Enabled = false;

                    break;

                default:
                    //this.SetBuildingControls(null);
                    this.lblNew.Visible = false;
                    this.lblCopy.Visible = false;
                    this.lblEdit.Visible = false;
                    this.btnBuildingAdd.Enabled = true;
                    this.btnBuildingCopy.Enabled = false;
                    this.btnBuildingDelete.Enabled = false;
                    this.btnBuildingEdit.Enabled = false;

                    this.SetBuildingDetailsControlsEnabled(false);

                    this.pnlBuildingDetails.Enabled = false;
                    this.pnlBuildingAdditionalDetails.Enabled = false;
                    this.pnlBuildingRetrofitFilters.Enabled = false;
                    this.ClearBuildingControls();
                    this.btnBuildingCancel.Enabled = false;
                    this.btnBuildingSave.Enabled = false;

                    break;
            }
        }

        public static string SetBrandingFolderPath(string companyFolder)
        {
            string result = String.Empty;

            if (!String.IsNullOrWhiteSpace(companyFolder))
            {
                companyFolder = companyFolder.TrimStart(Path.DirectorySeparatorChar);

                result = Path.Combine(System.Environment.CurrentDirectory, "dat\\branding", companyFolder);

                if (!Directory.Exists(result))
                {
                    throw new DirectoryNotFoundException("Company branding folder not found: " + result);
                }
            }

            return result;
        }

        private void SyncBuilding(Enumerations.SyncDirection direction)
        {
            if (this.SelectedBuilding == null)
            {
                this.ClearBuildingControls();
            }
            else
            {
                if (direction == Enumerations.SyncDirection.ControlsToEditObject)
                {
                    this.SelectedBuilding = new Building();

                    this.SelectedBuilding.IsCommercial = this.optBuildingCommercial.Checked;

                    this.SelectedBuilding.AddressLine1 = this.txtBuildingAddress1.Text;
                    this.SelectedBuilding.AddressLine2 = this.txtBuildingAddress2.Text;
                    this.SelectedBuilding.BuildingName = this.txtBuildingName.Text;

                    if (this.cboBuildingCategory.SelectedIndex >= 0)
                    {
                        this.SelectedBuilding.BuildingCategory = this.cboBuildingCategory.Text;
                    }

                    this.SelectedBuilding.BuildingHoursEquivalent = this.txtBuildingHoursEquivalent.Text;

                    if (this.cboBuildingType.SelectedIndex >= 0)
                    {
                        this.SelectedBuilding.BuildingType = this.cboBuildingType.Text;
                    }

                    this.SelectedBuilding.City = this.txtBuildingCity.Text;
                    this.SelectedBuilding.GasHistory = new GasHistoryRecord.GasHistoryRecordCollection();
                    this.SelectedBuilding.GrossFloorArea = this.numBuildingGrossFloor.Value;
                    this.SelectedBuilding.Id = (String.IsNullOrWhiteSpace(this.txtBuildingId.Text) ? GuidGenerator.Generate() : this.txtBuildingId.Text);
                    this.SelectedBuilding.NumberOfFloorsAboveGround = this.numBuildingFloorsAbove.Value;
                    this.SelectedBuilding.NumberOfFloorsBelowGround = this.numBuildingFloorsBelow.Value;
                    this.SelectedBuilding.NumberOfOccupants = this.numBuildingOccupants.Value;
                    this.SelectedBuilding.OccupiedFloorArea = this.numBuildingOccupiedFloor.Value;
                    this.SelectedBuilding.PostalCode = this.txtBuildingZip.Text;
                    this.SelectedBuilding.PostalCodeExtension = this.txtBuildingZipExt.Text;
                    this.SelectedBuilding.State = this.cboBuildingState.Text;
                    this.SelectedBuilding.NumberOfUnits = Int32.Parse(this.txtBuildingNumUnits.Text);
                    this.SelectedBuilding.UnitNumbering = this.cboUnitNumbering.Text;
                    this.SelectedBuilding.RoofType = this.txtBuildingRoofType.Text;
                    this.SelectedBuilding.BuildingWallType = this.cboBuildingWallType.Text;
                    this.SelectedBuilding.BuildingFoundation = this.cboBuildingFoundation.Text;
                    this.SelectedBuilding.YearBuilt = this.txtBuildingYearBuilt.Text;
                    this.SelectedBuilding.SafetyConcerns = this.txtBuildingSafetyConcerns.Text;
                    this.SelectedBuilding.SpecialEquipment = this.txtBuildingSpecialEquipment.Text;
                    this.SelectedBuilding.Comments = this.txtBuildingComments.Text;
                    this.SelectedBuilding.BuildingHeatingSystem = this.cboBuildingHeatingSystem.Text;
                    this.SelectedBuilding.BuildingHeatFuel = this.cboBuildingHeatFuel.Text;
                    this.SelectedBuilding.BuildingAirConditioning = this.cboBuildingAirConditioning.Text;
                    this.SelectedBuilding.BuildingDHW = this.cboBuildingDHW.Text;
                    this.SelectedBuilding.BuildingDHWFuel = this.cboBuildingDHWFuel.Text;
                    this.SelectedBuilding.BuildingParking = this.cboBuildingParking.Text;
                    this.SelectedBuilding.BuildingAtticType = this.cboBuildingAtticType.Text;
                    this.SelectedBuilding.BuildingWindows = this.cboBuildingWindows.Text;
                    this.SelectedBuilding.IsAffordableHousing = this.chbAffordableHousing.Checked;
                    this.SelectedBuilding.BuildingElectricAccountNumber = this.txtBuildingElectricAccountNumberValue.Text;
                    this.SelectedBuilding.BuildingElectricRateCode = this.txtBuildingElectricRateCode.Text;
                    this.SelectedBuilding.BuildingGasAccountNumber = this.txtBuildingGasAccountNumberValue.Text;
                    this.SelectedBuilding.BuildingGasRateCode = this.txtBuildingGasRateCode.Text;
                    this.SelectedBuilding.FilterData = SetFilterDataFromDynamicControls();
                }
                else
                {
                    this.SetBuildingPropertyTypeEnabled();

                    this.cboBuildingState.Text = this.SelectedBuilding.State;
                    this.cboBuildingCategory.Text = this.SelectedBuilding.BuildingCategory;

                    if (!String.IsNullOrWhiteSpace(this.SelectedBuilding.BuildingType))
                    {
                        this.cboBuildingType.Text = this.SelectedBuilding.BuildingType;
                    }

                    this.numBuildingFloorsAbove.Value = this.SelectedBuilding.NumberOfFloorsAboveGround;
                    this.numBuildingFloorsBelow.Value = this.SelectedBuilding.NumberOfFloorsBelowGround;
                    this.numBuildingGrossFloor.Value = this.SelectedBuilding.GrossFloorArea;
                    this.numBuildingOccupants.Value = this.SelectedBuilding.NumberOfOccupants;
                    this.numBuildingOccupiedFloor.Value = this.SelectedBuilding.OccupiedFloorArea;
                    this.txtBuildingAddress1.Text = this.SelectedBuilding.AddressLine1;
                    this.txtBuildingAddress2.Text = this.SelectedBuilding.AddressLine2;
                    this.txtBuildingCity.Text = this.SelectedBuilding.City;
                    this.txtBuildingId.Text = this.SelectedBuilding.Id;
                    this.txtBuildingName.Text = this.SelectedBuilding.BuildingName;
                    this.txtBuildingZip.Text = this.SelectedBuilding.PostalCode;
                    this.txtBuildingZipExt.Text = this.SelectedBuilding.PostalCodeExtension;
                    this.txtBuildingNumUnits.Text = this.SelectedBuilding.NumberOfUnits.ToString();

                    this.ClearMultifamilyUnitControls();
                    this.LoadUnits(this.SelectedBuilding);
                    this._unitNumberingChangedByUser = false;
                    this.cboUnitNumbering.Text = this.SelectedBuilding.UnitNumbering;
                    this._unitNumberingChangedByUser = true;

                    this.txtBuildingRoofType.Text = this.SelectedBuilding.RoofType;
                    this.txtBuildingYearBuilt.Text = this.SelectedBuilding.YearBuilt;
                    this.txtBuildingSafetyConcerns.Text = this.SelectedBuilding.SafetyConcerns;
                    this.txtBuildingSpecialEquipment.Text = this.SelectedBuilding.SpecialEquipment;
                    this.txtBuildingComments.Text = this.SelectedBuilding.Comments;
                    this.cboBuildingHeatingSystem.Text = this.SelectedBuilding.BuildingHeatingSystem;
                    this.cboBuildingHeatFuel.Text = this.SelectedBuilding.BuildingHeatFuel;
                    this.cboBuildingAirConditioning.Text = this.SelectedBuilding.BuildingAirConditioning;
                    this.cboBuildingDHW.Text = this.SelectedBuilding.BuildingDHW;
                    this.cboBuildingDHWFuel.Text = this.SelectedBuilding.BuildingDHWFuel;
                    this.cboBuildingParking.Text = this.SelectedBuilding.BuildingParking;
                    this.cboBuildingAtticType.Text = this.SelectedBuilding.BuildingAtticType;
                    this.cboBuildingWallType.Text = this.SelectedBuilding.BuildingWallType;
                    this.cboBuildingFoundation.Text = this.SelectedBuilding.BuildingFoundation;
                    this.cboBuildingWindows.Text = this.SelectedBuilding.BuildingWindows;
                    this.chbAffordableHousing.Checked = this.SelectedBuilding.IsAffordableHousing;
                    this.txtBuildingElectricAccountNumberValue.Text = this.SelectedBuilding.BuildingElectricAccountNumber;
                    this.txtBuildingElectricAccountNumber.Text = this.HideAllButLastFourCharacters(this.SelectedBuilding.BuildingElectricAccountNumber);
                    this.txtBuildingElectricRateCode.Text = this.SelectedBuilding.BuildingElectricRateCode;
                    this.txtBuildingGasAccountNumberValue.Text = this.SelectedBuilding.BuildingGasAccountNumber;
                    this.txtBuildingGasAccountNumber.Text = this.HideAllButLastFourCharacters(this.SelectedBuilding.BuildingGasAccountNumber);
                    this.txtBuildingGasRateCode.Text = this.SelectedBuilding.BuildingGasRateCode;

                    this.setBuildingComboSelectedBuilding();
                }
            }
        }

        private string SetFilterDataFromDynamicControls()
        {
            var company = DataStore.GetCompanyByAudit(SelectedAudit);
            var filters = (company != null) ? SelectedBuilding.InitFilterDataForBuilding(company) : SelectedBuilding.FilterData;
            foreach (var item in this.pnlBuildingRetrofitFilters.Controls)
            {
                if (typeof(ComboBoxEx) == item.GetType())
                {
                    var c = item as ComboBoxEx;
                    filters = ApiFilterHelper.SetFilter(filters, c.Name, c.Text);
                }
            }

            return filters;
        }

        private void SetAuditNavButtons(bool unitsChecked, bool spaceChecked, bool scheduleChecked, bool inventoryChecked, bool surveyChecked)
        {
            this.btnAuditWalkUnits.Checked = unitsChecked;
            this.btnAuditWalkSpaces.Checked = spaceChecked;
            this.btnAuditWalkSchedules.Checked = scheduleChecked;
            this.btnAuditWalkInventory.Checked = inventoryChecked;
            this.btnAuditWalkSurvey.Checked = surveyChecked;
        }

        public List<string> GetSelectedSpaces()
        {
            List<string> result = new List<string>();

            for (int i = 0; i < this.lstSelectedSpaces.Items.Count; i++)
            {
                result.Add(this.lstSelectedSpaces.Items[i].Text);
            }

            return result;
        }

        private void LoadSignature()
        {
            if (this.SelectedAudit != null)
            {
                if (this.SelectedAudit.InkSecureSignatureData == default(InkSecureSignatureData))
                {
                    this.sigSignOffSignature.Reset();
                    btnRunSignatureReport.Enabled = false;
                    btnEmailSignatureReport.Enabled = false;
                }
                else
                {
                    this.sigSignOffSignature.Value = this.SelectedAudit.InkSecureSignatureData.ToString();
                    btnRunSignatureReport.Enabled = true;
                    btnEmailSignatureReport.Enabled = true;
                }
            }
        }

        private void SetButtonsEnabled()
        {
            if (this.lstAvailableSpaces.Items.Count == 0)
            {
                this.btnAddAll.Enabled = false;
                this.btnAddOne.Enabled = false;
            }
            else
            {
                this.btnAddAll.Enabled = true;

                if (this.lstAvailableSpaces.SelectedItems.Count > 0)
                {
                    this.btnAddOne.Enabled = true;
                }
                else
                {
                    this.btnAddOne.Enabled = false;
                }
            }

            if (this.lstSelectedSpaces.Items.Count == 0)
            {
                this.btnRemoveAll.Enabled = false;
                this.btnRemoveOne.Enabled = false;
            }
            else
            {
                this.btnRemoveAll.Enabled = true;

                if (this.lstSelectedSpaces.SelectedItems.Count > 0)
                {
                    this.btnRemoveOne.Enabled = true;
                }
                else
                {
                    this.btnRemoveOne.Enabled = false;
                }
            }
        }

        public void AddNewBuilding()
        {
            this.EditMode = Enumerations.Mode.Add;
            this.SetNotificationIconsVisible(true, false);

            this.ClearBuildingControls();
            this.SetBuildingButtonsEnabled(false, false, false, true, true, true);
            this.cboBuildingCategory.Focus();
        }

        private void SelectBuildingInListHelper()
        {
            //if (this._mode != Mode.Canceling || this._mode == Mode.Edit || this._mode == Mode.Add) {
            if (this.EditMode == Enumerations.Mode.None)
            {
                this.SetCurrentBuildingToSelectedItem();
            }
            this.EditMode = Enumerations.Mode.None;

            if (this.SelectedAudit != null && this.SelectedBuilding != null)
            {
                var c = DataStore.GetCompanyByAudit(SelectedAudit);
                this.FillBuildingDropDowns(this.SelectedAudit.ProgramType);
                this.FillBuildingSpaces(this.SelectedAudit.ProgramType);
                this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces
                this.recRecommendations.RepopulateControlByBuildingId(this.SelectedBuilding);
                this.FillRetrofitFilterDropDowns(SelectedAudit.ProgramId, SelectedBuilding.InitFilterDataForBuilding(c), SelectedBuilding.PostalCode ?? DataStore.GetCompanyByAudit(SelectedAudit).PostalCode);
            }

            this.CopyBuildingData(Enumerations.SyncDirection.SelectedObjectToControls);
            this.SetBuildingButtonsEnabled(true, true, true, false, false, false);
        }

        private void EditExistingBuilding()
        {
            this.EditMode = Enumerations.Mode.Edit;
            this.SetNotificationIconsVisible(false, true);
            this.SetBuildingButtonsEnabled(false, false, false, true, true, true);
        }

        public void DeleteExistingBuilding()
        {
            this.EditMode = Enumerations.Mode.Deleting;
            this.SetNotificationIconsVisible(false, false);

            if (this.SelectedBuilding == null)
            {
                MessageBox.Show("Please select a building to delete.", "No Building Selected ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string msg = "Are you sure you want to delete the selected building (" + this.SelectedBuilding.BuildingName +
                    ")?\n\nIf you delete this building, all related equipment, recommendations, and all other related items will also be permanently deleted.\n\n" +
                    "Click the Yes button to permanently remove this building.  Click the No button to cancel this action without removing the building.";

                DialogResult r = MessageBox.Show(msg, "Delete Selected Building", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (r == DialogResult.Yes)
                {
                    DataStore.RemoveBuildingFromAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
                    this.SelectedBuilding = null;
                    this.ClearBuildingControls();
                    this.ResetBuildingListFromDataSource();
                    this.SelectCurrentBuildingInList();
                    this.RefreshActiveBuildingDropdown();
                }
            }

            this.EditMode = Enumerations.Mode.None;
        }

        private void CancelBuilding()
        {
            this.EditMode = Enumerations.Mode.Canceling;
            this.SetNotificationIconsVisible(false, false);
            //this.SelectedBuilding = null;
            this.SelectCurrentBuildingInList();
            if (this.lstBuildingList.SelectedItems.Count > 0)
            {
                this.SelectBuildingInListHelper();
            }
            this.selectedBuildingChanged();
            this.setBuildingComboSelectedBuilding();
            this.EditMode = Enumerations.Mode.None;
        }

        private void SaveBuilding()
        {
            if (this.OkToSave)
            {
                if (this.ValidateBuildingControls())
                {
                    if (this.EditMode == Enumerations.Mode.Add)
                    {
                        this.txtBuildingId.Text = (String.IsNullOrWhiteSpace(this.txtBuildingId.Text) ? GuidGenerator.Generate() : this.txtBuildingId.Text);
                        this.SelectedBuilding = new Building();
                        this.CopyBuildingData(Enumerations.SyncDirection.ControlsToEditObject);
                        DataStore.AddBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
                    }
                    else
                    {
                        //this.SaveUnitsGrid();
                        this.CopyBuildingData(Enumerations.SyncDirection.ControlsToEditObject);
                        DataStore.SaveBuildingToAudit(this.SelectedAudit.Id, this.SelectedBuilding, true);
                    }

                    this.ResetBuildingListFromDataSource();
                    this.SelectCurrentBuildingInList();
                    this.RefreshActiveBuildingDropdown();
                    this.setBuildingListSelectedBuilding();
                    this.setBuildingComboSelectedBuilding();
                    this.LoadAuditDirectInstallGrids(this.SelectedAudit.ProgramId);
                }

                this.SetNotificationIconsVisible(false, false);
            }
        }

        private void SetNotificationIconsVisible(bool newIcon, bool editIcon)
        {
            this.lblNew.Visible = newIcon;
            this.lblEdit.Visible = editIcon;
        }

        private bool ValidateBuildingControls()
        {
            bool result = true;
            string msg = "";

            if (!this.optBuildingCommercial.Checked && !this.optBuildingResidential.Checked)
            {
                msg = "Please select a Building Property Type.";
                this.optBuildingCommercial.Focus();
            }
            else if (this.cboBuildingCategory.SelectedIndex == -1)
            {
                msg = "Please select a Building Category.";
                this.cboBuildingCategory.Focus();
            }
            else if (this.cboBuildingType.SelectedIndex == -1)
            {
                msg = "Please select a Building Type.";
                this.cboBuildingType.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtBuildingName.Text))
            {
                msg = "Please enter a Building Name.";
                this.txtBuildingName.Focus();
            }
            else if (this.BuildingNameExists(this.txtBuildingName.Text, this.txtBuildingId.Text))
            {
                msg = "There is already a building named '" + this.txtBuildingName.Text +
                    "'.\n\nPlease enter a different name.";
                this.txtBuildingName.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtBuildingAddress1.Text))
            {
                msg = "Please enter an Address.";
                this.txtBuildingAddress1.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtBuildingCity.Text))
            {
                msg = "Please enter a City.";
                this.txtBuildingCity.Focus();
            }
            else if (this.cboBuildingState.SelectedIndex == -1 || String.IsNullOrWhiteSpace(this.cboBuildingState.Text))
            {
                msg = "Please select a State";
                this.cboBuildingState.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtBuildingZip.Text))
            {
                msg = "Please enter a Postal Code.";
                this.txtBuildingZip.Focus();
            }

            if (!String.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show(msg, "Invalid Building Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                result = false;
            }

            return result;
        }

        private bool BuildingNameExists(string name, string id)
        {
            bool result = false;
            foreach (ListViewItem li in this.lstBuildingList.Items)
            {
                object tag = li.Tag;
                if (tag != null)
                {
                    Building b = tag as Building;
                    if (b != null)
                    {
                        result = (b.BuildingName == name && b.Id != id);
                        if (result)
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private void ResetBuildingListFromDataSource()
        {
            this.lstBuildingList.Items.Clear();
            if (this.SelectedAudit != null)
            {
                Building.BuildingCollection buildings = DataStore.GetBuildingsByAudit(this.SelectedAudit.Id);
                if (buildings != null)
                {
                    foreach (Building building in buildings)
                    {
                        ListViewItem li = new ListViewItem(building.BuildingName);
                        li.Tag = building;
                        this.lstBuildingList.Items.Add(li);
                    }
                }
            }
        }

        private void SaveChartImage(string sType)
        {
            string chartfilepath = DataStore.ChartImagePath;
            string sChartName = "";
            Bitmap picImage = null;
            switch (sType)
            {
                case "Electric":
                    picImage = grdElectricHist.Sheets[0].Charts["SpreadChart1"].RenderImage();
                    sChartName = "electrichistory" + this.SelectedAudit.Id + ".bmp";
                    break;

                case "Gas":
                    picImage = grdGasHist.Sheets[0].Charts["SpreadChart3"].RenderImage();
                    sChartName = "gashistory" + this.SelectedAudit.Id + ".bmp";
                    break;

                case "EUI":
                    //THIS WAS ALREADY SAVED  WHEN THEY SAVED DATA ON THE EUI GRID
                    //EUIChartGauge
                    //
                    // picImage = fpSpread1.Sheets[0].Charts["EUIChartGauge"].RenderImage();
                    //sChartName = "gashistory" + this.SelectedAudit.Id + ".bmp";
                    break;
            }

            //string filePath = DataStore.ChartImagePath;
            //string directory = ConfigurationManager.AppSettings["xmlMyTaskDir"].ToString();
            //  string directory = + DataStore.ChartImagePath;

            if (!File.Exists(chartfilepath))
            {
                Directory.CreateDirectory(chartfilepath.ToString());
                //  File.Create(filePath).Close();
            }

            if (sType != "EUI")
            {
                Bitmap bmp = new Bitmap(400, 400);
                bmp = picImage;
                bmp.Save(chartfilepath + "\\" + sChartName);
            }
        }

        private void ClearGasHistory()
        {
            SheetView thisInstance = this.grdGasHist_Sheet1;
            thisInstance.ClearRange(3, 2, 12, 6, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
        }

        private void ClearAuditDirectInstallGridData()
        {
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_QUANTITY, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_SPACE, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_KWH, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_THERMS, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_KGALLONS, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdDirectInstall_Sheet1.ClearRange(0, DI_GRID_COLUMN_SAVINGS, grdDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);

            this.SetGridColumnWidths(true, false, false);
        }

        private void ClearAllGridData(SheetView grid)
        {
            grid.DataSource = null;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                grid.Cells[i, 1].Text = "";
            }
        }

        private void ClearUnits()
        {
            for (int i = 0; i < grdUnits_Sheet1.Rows.Count; i++)
            {
                grdUnits_Sheet1.Cells[i, 2].Value = 0;
                grdUnits_Sheet1.Cells[i, 4].Value = 0;
                if (i < grdUnits_Sheet1.Rows.Count - 1)
                {
                    grdUnits_Sheet1.Cells[i, 0].Value = "";
                    grdUnits_Sheet1.Cells[i, 1].Value = "";
                }
            }
        }

        private void ClearUnitDirectInstallGridData()
        {
            this.grdUnitDirectInstall_Sheet1.ClearRange(0, UNIT_DI_GRID_COLUMN_QUANTITY, grdUnitDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdUnitDirectInstall_Sheet1.ClearRange(0, UNIT_DI_GRID_COLUMN_SPACE, grdUnitDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdUnitDirectInstall_Sheet1.ClearRange(0, UNIT_DI_GRID_COLUMN_KWH, grdUnitDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdUnitDirectInstall_Sheet1.ClearRange(0, UNIT_DI_GRID_COLUMN_THERMS, grdUnitDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
            this.grdUnitDirectInstall_Sheet1.ClearRange(0, UNIT_DI_GRID_COLUMN_SAVINGS, grdUnitDirectInstall_Sheet1.Rows.Count, 1, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);

            this.SetGridColumnWidths(false, false, true);
        }

        private void ClearElectricHistory()
        {
            SheetView thisInstance = this.grdElectricHist_Sheet1;
            thisInstance.ClearRange(3, 2, 12, 10, true, FarPoint.Win.Spread.ClipboardCopyOptions.Values);
        }

        private void LoadBuildingSchedule()
        {
            this.SuspendLayout();

            this.schAuditOccupancy.SuspendLayout();
            this.schAuditInteriorLights.SuspendLayout();
            this.schAuditExteriorLights.SuspendLayout();
            this.schAuditHvac.SuspendLayout();

            //this.recRecommendations.SuspendLayout();

            foreach (Schedule s in DataStore.GetSchedules(this.SelectedAudit.Id, this.SelectedBuilding.Id))
            {
                switch (s.ScheduleName.Trim().ToUpper())
                {
                    case "OCCUPANCY":
                        //this.LoadSchedule(s);
                        this.tabAuditSchedules.SelectedTabIndex = 0;
                        this.schAuditOccupancy.SetSchedule(new Schedule(s));
                        break;

                    case "INTERIORLIGHTS":
                        this.tabAuditSchedules.SelectedTabIndex = 1;
                        this.schAuditInteriorLights.SetSchedule(new Schedule(s));
                        break;

                    case "EXTERIORLIGHTS":
                        this.tabAuditSchedules.SelectedTabIndex = 2;
                        this.schAuditExteriorLights.SetSchedule(new Schedule(s));
                        break;

                    case "HVAC":
                        this.tabAuditSchedules.SelectedTabIndex = 3;
                        this.schAuditHvac.SetSchedule(new Schedule(s));
                        break;
                }
            }
        }

        private void SetBuildingPropertyTypeEnabled()
        {
            if (this.SelectedAudit == null)
            {
                this._allowBuildingPropertyTypeEdit = false;

                this.optBuildingCommercial.Checked = false;
                this.optBuildingResidential.Checked = false;
                this.optBuildingCommercial.Enabled = false;
                this.optBuildingResidential.Enabled = false;
            }
            else
            {
                if (String.Compare(this.SelectedAudit.ProgramType, Constants.ProgramType.MultiFamily, true) == 0)
                {
                    if (this.SelectedBuilding == null)
                    {
                        this._allowBuildingPropertyTypeEdit = false;

                        this.optBuildingCommercial.Checked = false;
                        this.optBuildingResidential.Checked = false;
                    }
                    else
                    {
                        this._allowBuildingPropertyTypeEdit = true;

                        this.optBuildingCommercial.Checked = this.SelectedBuilding.IsCommercial;
                        this.optBuildingResidential.Checked = !this.SelectedBuilding.IsCommercial;
                    }

                    switch (this.EditMode)
                    {
                        case Enumerations.Mode.Add:
                        case Enumerations.Mode.Edit:
                            this.optBuildingCommercial.Enabled = true;
                            this.optBuildingResidential.Enabled = true;

                            break;

                        default:
                            this._allowBuildingPropertyTypeEdit = false;

                            this.optBuildingCommercial.Enabled = false;
                            this.optBuildingResidential.Enabled = false;

                            break;
                    }
                }
                else
                {
                    // Program Type is NOT multifamily

                    this._allowBuildingPropertyTypeEdit = false;

                    // Default to the commercial option.
                    this.optBuildingCommercial.Checked = true;
                    this.optBuildingCommercial.Enabled = false;
                    this.optBuildingResidential.Enabled = false;
                }
            }
        }

        private void CopyBuildingData(Enumerations.SyncDirection direction)
        {
            switch (direction)
            {
                case Enumerations.SyncDirection.ControlsToEditObject:
                case Enumerations.SyncDirection.ControlsToSelectedObject:

                    // Controls => Object
                    if (this.SelectedBuilding == null)
                    {
                        this.SelectedBuilding = new Building();
                    }

                    this.SelectedBuilding.IsCommercial = this.optBuildingCommercial.Checked;

                    this.SelectedBuilding.AddressLine1 = this.txtBuildingAddress1.Text;
                    this.SelectedBuilding.AddressLine2 = this.txtBuildingAddress2.Text;
                    this.SelectedBuilding.BuildingName = this.txtBuildingName.Text;

                    if (this.cboBuildingCategory.SelectedIndex >= 0)
                    {
                        this.SelectedBuilding.BuildingCategory = this.cboBuildingCategory.Text;
                    }

                    this.SelectedBuilding.BuildingHoursEquivalent = this.txtBuildingHoursEquivalent.Text;

                    if (this.cboBuildingType.SelectedIndex >= 0)
                    {
                        this.SelectedBuilding.BuildingType = this.cboBuildingType.Text;
                    }

                    this.SelectedBuilding.City = this.txtBuildingCity.Text;
                    this.SelectedBuilding.GasHistory = new GasHistoryRecord.GasHistoryRecordCollection();
                    this.SelectedBuilding.GrossFloorArea = this.numBuildingGrossFloor.Value;
                    this.SelectedBuilding.Id = (String.IsNullOrWhiteSpace(this.txtBuildingId.Text) ? GuidGenerator.Generate() : this.txtBuildingId.Text);
                    this.SelectedBuilding.NumberOfFloorsAboveGround = this.numBuildingFloorsAbove.Value;
                    this.SelectedBuilding.NumberOfFloorsBelowGround = this.numBuildingFloorsBelow.Value;
                    this.SelectedBuilding.NumberOfOccupants = this.numBuildingOccupants.Value;
                    this.SelectedBuilding.OccupiedFloorArea = this.numBuildingOccupiedFloor.Value;
                    this.SelectedBuilding.PostalCode = this.txtBuildingZip.Text;
                    this.SelectedBuilding.PostalCodeExtension = this.txtBuildingZipExt.Text;
                    this.SelectedBuilding.State = this.cboBuildingState.Text;
                    int buildingNumUnits;
                    Int32.TryParse(this.txtBuildingNumUnits.Text, out buildingNumUnits);
                    this.SelectedBuilding.NumberOfUnits = buildingNumUnits;
                    if (this.cboUnitNumbering.SelectedIndex >= 0)
                    {
                        this.SelectedBuilding.UnitNumbering = this.cboUnitNumbering.Items[this.cboUnitNumbering.SelectedIndex].ToString();
                    }
                    else
                    {
                        this.SelectedBuilding.UnitNumbering = "";
                    }
                    this.SelectedBuilding.RoofType = this.txtBuildingRoofType.Text;
                    this.SelectedBuilding.YearBuilt = this.txtBuildingYearBuilt.Text;
                    this.SelectedBuilding.SafetyConcerns = this.txtBuildingSafetyConcerns.Text;
                    this.SelectedBuilding.SpecialEquipment = this.txtBuildingSpecialEquipment.Text;
                    this.SelectedBuilding.Comments = this.txtBuildingComments.Text;
                    this.SelectedBuilding.BuildingHeatingSystem = this.cboBuildingHeatingSystem.Text;
                    this.SelectedBuilding.BuildingHeatFuel = this.cboBuildingHeatFuel.Text;
                    this.SelectedBuilding.BuildingAirConditioning = this.cboBuildingAirConditioning.Text;
                    this.SelectedBuilding.BuildingDHW = this.cboBuildingDHW.Text;
                    this.SelectedBuilding.BuildingDHWFuel = this.cboBuildingDHWFuel.Text;
                    this.SelectedBuilding.BuildingParking = this.cboBuildingParking.Text;
                    this.SelectedBuilding.BuildingAtticType = this.cboBuildingAtticType.Text;
                    this.SelectedBuilding.BuildingWallType = this.cboBuildingWallType.Text;
                    this.SelectedBuilding.BuildingFoundation = this.cboBuildingFoundation.Text;
                    this.SelectedBuilding.BuildingWindows = this.cboBuildingWindows.Text;
                    this.SelectedBuilding.IsAffordableHousing = this.chbAffordableHousing.Checked;
                    this.SelectedBuilding.BuildingElectricAccountNumber = this.txtBuildingElectricAccountNumberValue.Text;
                    this.SelectedBuilding.BuildingElectricRateCode = this.txtBuildingElectricRateCode.Text;
                    this.SelectedBuilding.BuildingGasAccountNumber = this.txtBuildingGasAccountNumberValue.Text;
                    this.SelectedBuilding.BuildingGasRateCode = this.txtBuildingGasRateCode.Text;
                    this.SelectedBuilding.FilterData = SetFilterDataFromDynamicControls();
                    break;

                case Enumerations.SyncDirection.EditObjectToControls:
                case Enumerations.SyncDirection.SelectedObjectToControls:

                    // Object => Controls
                    if (this.SelectedBuilding != null)
                    {
                        this.txtBuildingId.Text = this.SelectedBuilding.Id;
                        this.cboBuildingCategory.Text = this.SelectedBuilding.BuildingCategory;
                        if (!String.IsNullOrWhiteSpace(this.SelectedBuilding.BuildingType))
                        {
                            this.cboBuildingType.Text = this.SelectedBuilding.BuildingType;
                        }

                        this.SetBuildingPropertyTypeEnabled();

                        this.txtBuildingName.Text = this.SelectedBuilding.BuildingName;
                        this.txtBuildingAddress1.Text = this.SelectedBuilding.AddressLine1;
                        this.txtBuildingAddress2.Text = this.SelectedBuilding.AddressLine2;
                        this.txtBuildingCity.Text = this.SelectedBuilding.City;
                        this.cboBuildingState.Text = this.SelectedBuilding.State;
                        this.txtBuildingZip.Text = this.SelectedBuilding.PostalCode;
                        this.txtBuildingZipExt.Text = this.SelectedBuilding.PostalCodeExtension;
                        this.numBuildingOccupants.Value = this.SelectedBuilding.NumberOfOccupants;
                        this.numBuildingFloorsAbove.Value = this.SelectedBuilding.NumberOfFloorsAboveGround;
                        this.numBuildingFloorsBelow.Value = this.SelectedBuilding.NumberOfFloorsBelowGround;
                        this.numBuildingGrossFloor.Value = this.SelectedBuilding.GrossFloorArea;
                        this.numBuildingOccupiedFloor.Value = this.SelectedBuilding.OccupiedFloorArea;
                        this.txtBuildingNumUnits.Text = this.SelectedBuilding.NumberOfUnits.ToString();

                        this._unitNumberingChangedByUser = false;
                        this.cboUnitNumbering.Text = this.SelectedBuilding.UnitNumbering;
                        this._unitNumberingChangedByUser = true;

                        this.txtBuildingRoofType.Text = this.SelectedBuilding.RoofType;
                        this.txtBuildingYearBuilt.Text = this.SelectedBuilding.YearBuilt;
                        this.txtBuildingSafetyConcerns.Text = this.SelectedBuilding.SafetyConcerns;
                        this.txtBuildingSpecialEquipment.Text = this.SelectedBuilding.SpecialEquipment;
                        this.txtBuildingComments.Text = this.SelectedBuilding.Comments;

                        this.cboBuildingHeatingSystem.Text = this.SelectedBuilding.BuildingHeatingSystem;
                        this.cboBuildingHeatFuel.Text = this.SelectedBuilding.BuildingHeatFuel;
                        this.cboBuildingAirConditioning.Text = this.SelectedBuilding.BuildingAirConditioning;
                        this.cboBuildingDHW.Text = this.SelectedBuilding.BuildingDHW;
                        this.cboBuildingDHWFuel.Text = this.SelectedBuilding.BuildingDHWFuel;
                        this.cboBuildingParking.Text = this.SelectedBuilding.BuildingParking;
                        this.cboBuildingAtticType.Text = this.SelectedBuilding.BuildingAtticType;
                        this.cboBuildingWallType.Text = this.SelectedBuilding.BuildingWallType;
                        this.cboBuildingFoundation.Text = this.SelectedBuilding.BuildingFoundation;
                        this.cboBuildingWindows.Text = this.SelectedBuilding.BuildingWindows;
                        this.chbAffordableHousing.Checked = this.SelectedBuilding.IsAffordableHousing;
                        this.txtBuildingElectricAccountNumberValue.Text = this.SelectedBuilding.BuildingElectricAccountNumber;
                        this.txtBuildingElectricAccountNumber.Text = this.HideAllButLastFourCharacters(this.SelectedBuilding.BuildingElectricAccountNumber);
                        this.txtBuildingElectricRateCode.Text = this.SelectedBuilding.BuildingElectricRateCode;
                        this.txtBuildingGasAccountNumberValue.Text = this.SelectedBuilding.BuildingGasAccountNumber;
                        this.txtBuildingGasAccountNumber.Text = this.HideAllButLastFourCharacters(this.SelectedBuilding.BuildingGasAccountNumber);
                        this.txtBuildingGasRateCode.Text = this.SelectedBuilding.BuildingGasRateCode;
                    }

                    break;
            }
        }

        private void SetCurrentBuildingToSelectedItem()
        {
            foreach (ListViewItem li in this.lstBuildingList.SelectedItems)
            {
                object tag = li.Tag;
                if (tag != null)
                {
                    Building b = tag as Building;
                    if (b != null)
                    {
                        this.SelectedBuilding = b;
                        break;
                    }
                }
            }
        }

        private string HideAllButLastFourCharacters(string value)
        {
            string result = value;

            if (value != null && value.Length > 4)
            {
                int numChar = value.Length - 4;
                string hiddenText = new String('*', numChar);
                result = string.Concat(hiddenText, value.Substring(numChar, 4));
            }

            return result;
        }

        private void ResetForm()
        {
            this.ClearBuildingControls();
            this.FillBuildingSpaces(FieldTool.Constants.ProgramType.Commercial);
            this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces
            this.ResetScheduleControls();
            this.lblRecommenationSelectedBuilding.Text = "No building selected.";
            this.recRecommendations.Clear();
            this.ResetItemsGrid();
            this.sigSignOffSignature.Reset();
            btnRunSignatureReport.Enabled = false;
            btnEmailSignatureReport.Enabled = false;
            //this.LoadSignature();
            this.SetBuildingButtonsEnabled(true, false, false, false, false, false);
        }

        private void SelectCurrentBuildingInList()
        {
            if (this.SelectedBuilding == null)
            {
                this.ResetForm();
            }
            else
            {
                foreach (ListViewItem li in this.lstBuildingList.Items)
                {
                    if (li.Text == this.SelectedBuilding.BuildingName)
                    {
                        li.Selected = true;
                        break;
                    }
                }
                this.SetBuildingButtonsEnabled(true, true, true, false, false, true);
            }
        }

        private void RefreshActiveBuildingDropdown()
        {
            this.cboActiveBuilding.Items.Clear();

            this.cboActiveBuilding.Text = "";

            foreach (ListViewItem li in this.lstBuildingList.Items)
            {
                object tag = li.Tag;
                if (tag != null)
                {
                    Building b = tag as Building;
                    if (b != null)
                    {
                        this.cboActiveBuilding.Items.Add(li);
                    }
                }
            }

            if (this.cboActiveBuilding.Items.Count > 0)
            {
                this.cboActiveBuilding.SelectedIndex = 0;
            }
        }

        private void RefreshRecommendationsByAudit()
        {
            if (this.SelectedAudit != null && this.SelectedBuilding != null)
            {
                Cursor.Current = Cursors.WaitCursor;

                // Get any Generic/Custom recommendations and hold them until the end.  This is because this process regenerates all recommendations and there's
                //  no way to regenerate these once they are removed.
                List<Recommendation> generics = this.SelectedBuilding.GetGenericRecommendations();

                DataStore.DeleteRecommendationsFromAudit(this.SelectedAudit.Id, true, true);

                foreach (Building b in this.SelectedAudit.Buildings)
                {
                    foreach (EquipmentMaster equipment in DataStore.GetEquipmentByBuildingId(b.Id))
                    {
                        double heatingCoolingHours = DataStore.GetHeatingCoolingHours(
                            equipment.TypeOfEnergy,
                            b.BuildingCategory,
                            b.ZipZone);

                        if (equipment.OkToGenerateRecommendations)
                        {
                            Recommendation.RecommendationCollection recs =
                                DataStore.GetRecommendationsForEquipment(equipment, heatingCoolingHours, this.SelectedAudit.ProgramId, this.SelectedBuilding.IsCommercial);

                            foreach (Recommendation generic in generics)
                            {
                                recs.Items.Add(generic);
                            }

                            DataStore.AddRecommendationsToBuilding(this.SelectedAudit.Id, b.Id, recs, true, true);
                        }
                    }
                }

                this.recRecommendations.RepopulateControlByBuildingId(this.SelectedBuilding);

                Cursor.Current = Cursors.Default;
            }
        }

        private void AddRecommendationCollectionToGrid(Recommendation.RecommendationCollection recommendations)
        {
            if (recommendations != null &&
                this.SelectedAudit != null &&
                this.SelectedBuilding != null)
            {
                DataStore.AddRecommendationsToBuilding(this.SelectedAudit.Id, this.SelectedBuilding.Id, recommendations, false, true);
                this.recRecommendations.RepopulateControlByBuildingId(this.SelectedBuilding);
            }
        }

        private bool RangeValueChangedHelper(RangeSlider slider, Label startLabel, Label endLabel, Label naLabel, LabelX annualHoursLabel)
        {
            int min = slider.Value.Min;
            int max = slider.Value.Max;
            bool result = false;

            if (min == max)
            {
                this.SetTimeLabelsVisible(startLabel, endLabel, naLabel, false);
                result = false; // HasInterval
            }
            else
            {
                this.SetTimeLabelsVisible(startLabel, endLabel, naLabel, true);
                result = true;

                TimeSpan start = this.ConvertTicksToTimeSpan(slider, min);
                TimeSpan end = this.ConvertTicksToTimeSpan(slider, max);

                startLabel.Text = this.ConvertTimeSpanToText(start);
                endLabel.Text = this.ConvertTimeSpanToText(end);
            }

            return result;
        }

        private TimeSpan ConvertTicksToTimeSpan(RangeSlider slider, int ticks)
        {
            if (ticks == slider.Maximum)
            {
                return new TimeSpan(23, 59, 59);
            }
            else
            {
                double r = 30.0 * (double)ticks;
                double d = r / 60.0;

                DateTime dt = new DateTime();
                //return dt.AddHours(d).ToString("h:mm tt");
                return dt.AddHours(d).TimeOfDay;
            }
        }

        private void SetTimeLabelsVisible(Label startLabel, Label endLabel, Label naLabel, bool visible)
        {
            naLabel.Visible = !visible;
            startLabel.Visible = visible;
            endLabel.Visible = visible;
        }

        public void SetColors(Label startLabel, Label endLabel)
        {
            startLabel.BackColor = Color.PaleGreen;
            endLabel.BackColor = Color.LightCoral;
        }

        private string ConvertTimeSpanToText(TimeSpan ts)
        {
            string result = new DateTime().Add(ts).ToString("h:mm tt");

            return result;
        }

        public int ConvertTimeSpanToTicks(TimeSpan duration)
        {
            int result = 0;

            if (duration != null)
            {
                result = (int)(duration.TotalMinutes / 30);
            }

            return result;
        }

        private string GetAnnualHoursText(Schedule schedule)
        {
            string result = "";

            if (schedule != null)
            {
                string hrs = Math.Round(schedule.GetTotalAnnualHours(), 1).ToString();

                switch (schedule.ScheduleDescription)
                {
                    case "Occupancy":
                        result = hrs + " occ hrs/yr";
                        break;

                    case "Interior Lights":
                    case "Exterior Lights":
                        result = hrs + " hrs/yr";
                        break;

                    case "HVAC":
                        result = hrs + " occ mode hrs/yr";
                        break;

                    default:
                        result = "Annual Hours: " + hrs;
                        break;
                }
            }

            return result;
        }

        /*
          Added by Mike Crowell 6/30/15
          This method applies client branding based of the values from the files in the File System.
          If appropriate branding can not be found in the File System then no changes will be made and the branding defined in the solution will be used.
         */

        private ClientBranding ApplyBranding(string clientAccountId, string brandingKey)
        {
            BrandingHelper brandingHelper = new BrandingHelper();
            ClientBranding branding = brandingHelper.GetBranding(clientAccountId, brandingKey);
            if (branding != null)
            {
                if (!String.IsNullOrEmpty(branding.ImageFileName))
                {
                    picBrandingCompanyLogo.Image = Image.FromFile(branding.SystemPath + "\\" + branding.ImageFileName);
                }
                Color baseColor = Color.FromArgb(branding.ColorValueRed, branding.ColorValueGreen, branding.ColorValueBlue);
                styBrandingStyleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, baseColor);
            }

            return branding;
        }

        #endregion Methods

        #region Contact interface methods and events

        private void btnContactAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.LoadDetails(Enumerations.PanelDisplayMode.Add);
            Cursor.Current = Cursors.Default;
        }

        private void btnContactEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.LoadDetails(Enumerations.PanelDisplayMode.Edit);
            Cursor.Current = Cursors.Default;
        }

        private void btnContactDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.Delete();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactPhoneAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.AddPhone();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactPhoneEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.EditSelectedPhone();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactPhoneDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.DeleteSelectedPhone();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactEmailAdd_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.AddEmail();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactEmailEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.EditSelectedEmail();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactEmailDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.DeleteSelectedEmail();
            Cursor.Current = Cursors.Default;
        }

        private void btnContactCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (frmMainContactHelper.IsRollYourOwn)
            {
                frmMainContactHelper.Cancel();
                this._rollYourOwn = null;
                this.NavigateToMyWorkPanel();
            }
            else
            {
                frmMainContactHelper.Cancel();
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnContactSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool saved = frmMainContactHelper.Save();
            if (saved)
            {
                if (frmMainCompanyHelper.IsRollYourOwn)
                {
                    // Add contact to company & update audit info from contact
                    //_rollYourOwn.AddContacts(frmMainContactHelper.Contacts, true);
                    // Save company to data
                    //DataStore.AddCompany(_rollYourOwn, true);
                    // Load
                    this.LoadMyWorkGrid();

                    if (_rollYourOwn.Audits.Count > 0)
                    {
                        this.AddAuditsToCalendar(_rollYourOwn.Audits);
                        var ok = this.FillAuditDetails(_rollYourOwn.Audits[0]);
                        if (ok)
                        {
                            this.SelectedAudit = _rollYourOwn.Audits[0];
                        }
                    }

                    this.EnableAuditButton();
                    this.NavigateToBuildingPanel(_rollYourOwn);
                }
                else
                {
                    this.DownloadXmlData();
                    this.FillAuditDetails(this.SelectedAudit);
                    _myWorkHelper.LoadMyWorkGrid();
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void lstContactPhone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.EditSelectedPhone();
            Cursor.Current = Cursors.Default;
        }

        private void lstContactPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.SelectPhone();
            Cursor.Current = Cursors.Default;
        }

        private void lstContactEmail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.EditSelectedEmail();
            Cursor.Current = Cursors.Default;
        }

        private void lstContactEmail_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.SelectEmail();
            Cursor.Current = Cursors.Default;
        }

        private void lstContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.LoadDetails(Enumerations.PanelDisplayMode.ReadOnly);
            Cursor.Current = Cursors.Default;
        }

        private void lstContacts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            frmMainContactHelper.LoadDetails(Enumerations.PanelDisplayMode.Edit);
            Cursor.Current = Cursors.Default;
        }

        private void splContacts_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            this.colContacts.Width = -2;
        }

        private void splContacts_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.colContacts.Width = -2;
        }

        private void InitializeContactTab()
        {
            frmMainContactHelper.RegisterControls(this.txtContactId, this.txtContactExternalId, this.btnContactAdd, this.btnContactEdit, this.btnContactDelete,
                this.lblContactAddIcon, this.lblContactEditIcon, this.lblContactName,
                this.lblContactNameValue, this.txtContactFirstName, this.txtContactMiddleName, this.txtContactLastName, this.lblContactJobRole, this.lblContactJobRoleValue,
                this.cboContactJobRole,
                this.lstContactPhone, this.lblContactPhoneValue, this.pnlContactPhoneButtons, this.btnContactPhoneAdd, this.btnContactPhoneEdit, this.btnContactPhoneDelete,
                this.lstContactEmail, this.lblContactEmailValue, this.pnlContactEmailButtons, this.btnContactEmailAdd, this.btnContactEmailEdit, this.btnContactEmailDelete,
                this.lblContactNotes, this.lblContactNoteValue, this.txtContactNote, this.pnlContactSaveButtons, this.btnContactCancel, this.btnContactSave, this.tabHomePanel,
                this.lstContacts, this.pnlContactButtons);

            frmMainContactHelper.Initialize();
        }

        #endregion Contact interface methods and events

        #region Company interface methods and events

        private void btnCompanyCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (frmMainCompanyHelper.IsRollYourOwn)
            {
                frmMainCompanyHelper.Cancel(); // Cancel changes the value of frmMainCompanyHelper.IsRollYourOwn, so it must follow the if clause.
                this._rollYourOwn = null;
                this.NavigateToMyWorkPanel();
            }
            else
            {
                frmMainCompanyHelper.Cancel();
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnCompanyEdit_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.tabCompanyControls.SuspendDrawing();

            frmMainCompanyHelper.Edit();

            this.tabCompanyControls.ResumeDrawing();

            Cursor.Current = Cursors.Default;
        }

        private void btnCompanySave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool saved = frmMainCompanyHelper.Save();

            this.DownloadXmlData();
            this.FillAuditDetails(this.SelectedAudit);
            _myWorkHelper.LoadMyWorkGrid();

            if (saved && frmMainCompanyHelper.IsRollYourOwn)
            {
                this._rollYourOwn = frmMainCompanyHelper.Company;
                frmMainCompanyHelper.AddAudit(this._rollYourOwn);

                this.NavigateToContactPanel();
                frmMainContactHelper.Load(_rollYourOwn);
            }

            Cursor.Current = Cursors.Default;
        }

        private void cboCompanyUtility_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            frmMainCompanyHelper.UpdateProgramsFromUtility(true);

            Cursor.Current = Cursors.Default;
        }

        private string InitializeCompanyTab()
        {
            frmMainCompanyHelper.RegisterControls(
                this.txtCompanyId, this.txtCompanyExternalId,
                this.lblCompanyAddIcon, this.lblCompanyEditIcon,
                this.lblCompanyName, this.lblCompanyNameValue, this.txtCompanyName,
                this.lblPropertyManagement, this.lblPropertyManagementValue, this.txtPropertyManagement,
                this.lblCompanyAddress, this.lblCompanyAddressValue, this.txtCompanyAddress,
                this.lblCompanyAddress2Value, this.txtCompanyAddress2,
                this.lblCompanyCityValue, this.txtCompanyCity, this.lblCompanyStateValue, this.cboCompanyState,
                this.lblCompanyZipValue, this.txtCompanyZip, this.lblCompanyZipExtensionValue, this.txtCompanyZipExtention,
                this.pnlCompanyEditButtons, this.btnCompanyEdit,
                this.lblCompanyUtility, this.lblCompanyUtilityValue, this.cboCompanyUtility,
                this.lblCompanyProgram, this.lblCompanyProgramValue, this.cboCompanyProgram,
                this.lblCompanyAccountType, this.lblCompanyAccountTypeValue, this.cboCompanyAccountType,
                this.lblCompanyElectricAccountNumber, this.lblCompanyElectricAccountNumberValue, this.txtCompanyElectricAccountNumber,
                this.lblCompanyElectricRateCode, this.lblCompanyElectricRateCodeValue, this.txtCompanyElectricRateCode,
                this.lblCompanyGasAccountNumber, this.lblCompanyGasAccountNumberValue, this.txtCompanyGasAccountNumber,
                this.lblCompanyGasRateCode, this.lblCompanyGasRateCodeValue, this.txtCompanyGasRateCode,
                this.lblCompanyStartDateTime, this.lblCompanyStartDateTimeValue, this.calCompanyStartDateTime,
                this.pnlCompanySaveButtons, this.btnCompanyCancel, this.btnCompanySave,
                this.tabHomePanel, this.txtCompanyRecordType, this.lblCompanyInfo,
                this.lblCompanyEmailValue, this.txtCompanyEmail, this.lblCompanyPhoneValue, this.txtCompanyPhone);

            return frmMainCompanyHelper.Initialize();
        }

        private void LoadCompany(Company company, Enumerations.PanelDisplayMode mode)
        {
            frmMainCompanyHelper.Load(company, mode);

            if (company != null)
            {
                // Populate the contact info.
                frmMainContactHelper.Load(company.Id, company.Contacts);
            }
        }

        #endregion Company interface methods and events

        #region My Work panel helper methods and events

        private frmMainMyWorkHelper _myWorkHelper = null;

        private frmMainMyWorkHelper MyWorkHelper
        {
            get
            {
                if (this._myWorkHelper == null)
                {
                    this._myWorkHelper = new frmMainMyWorkHelper(this.lblStatusText, this.grdMyWork_Sheet1);
                    this.MyWorkHelper.ValidateButtonClick += new frmMainMyWorkHelper.ValidateButtonClickHandler(this.MyWorkHelper_ValidateButtonClick);
                    this.MyWorkHelper.AuditDeleted += new frmMainMyWorkHelper.AuditDeletedHandler(this.MyWorkHelper_AuditDeleted);
                }

                return this._myWorkHelper;
            }
        }

        #region Control event handlers

        private void btnHomeMyWork_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.LoadMyWorkGrid();

            this.pslHomePageSlider.SelectedPageIndex = 2;

            Cursor.Current = Cursors.Default;
        }

        private void btnMyWorkUpload_Click(object sender, EventArgs e)
        {
            this.uploadAuditData();
        }

        private void btnMyWorkDelete_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.MyWorkHelper.DeleteSelectedAudit(lookupService, _userEmail);

            Cursor.Current = Cursors.Default;
        }

        private void grdMyWork_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            this.MyWorkHelper.MyWorkGridClickHelper(false, e.Row, e.Column);
        }

        private void grdMyWork_CellClick(object sender, CellClickEventArgs e)
        {
            this.MyWorkHelper.MyWorkGridClickHelper(e.ColumnHeader, e.Row, e.Column);
        }

        private void grdMyWork_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            int sheetIndex = e.Row;

            FarPoint.Win.Spread.Column colAuditId = grdMyWork.ActiveSheet.GetColumnFromTag(null, "AuditId");
            FarPoint.Win.Spread.Column colCompleteCheck = grdMyWork.ActiveSheet.GetColumnFromTag(null, "Complete");
            FarPoint.Win.Spread.Column colValidate = grdMyWork.ActiveSheet.GetColumnFromTag(null, "Validate");

            // don't do this if they checked completed or are clicking on column header
            if (e.Column != colCompleteCheck.Index && e.Column != colValidate.Index && !e.ColumnHeader)
            {
                // get auditId from row clicked on
                var auditId = this.grdMyWork_Sheet1.Cells[e.Row, colAuditId.Index].Text;
                // get audit by id
                Audit audit = DataStore.GetAudit(auditId);
                var ok = this.FillAuditDetails(audit);
                if (ok)
                {
                    this.DoLaunch();
                }
            }

            Cursor.Current = Cursors.Default;
        }

        #endregion Control event handlers

        #region Helper object event handlers

        private void MyWorkHelper_AuditDeleted(object sender, AuditEventArgs e)
        {
            this.MyWorkHelper.LoadMyWorkGrid();

            this.DownloadXmlData();

            if (this.grdMyWork_Sheet1.Rows.Count == 0)
            {
                this.NavigateToDashboardPanel();
                this.EnableAuditButton();
            }
        }

        private void MyWorkHelper_ValidateButtonClick(object sender, GridEventArgs e)
        {
            this.LaunchValidateCompanyForm(e.RowIndex);
        }

        #endregion Helper object event handlers

        private void LoadMyWorkGrid()
        {
            this.MyWorkHelper.LoadMyWorkGrid();
            this.SetHomeNavButtons(false, false, true);
            this.SizeSpreadColumnsToPreferredWidth(this.grdMyWork);
        }

        #endregion My Work panel helper methods and events

        #region DirectInstall

        private void DirectInstallTablItem_Click(object sender, EventArgs e)
        {
            var programs = DataStore.GetPrograms();
            if (programs.Count > 0)
            {
                //frmDirectInstall f = default(frmDirectInstall);
                if (tabPanelDirectInstall.Controls.Count == 0)
                {
                    frmDI = new frmDirectInstall();
                    frmDI.StatusBarLabel = lblStatusText;
                    tabPanelDirectInstall.Controls.Add(frmDI);
                }
                else
                {
                    frmDI = tabPanelDirectInstall.Controls[0] as frmDirectInstall;
                }
                frmDI.Dock = DockStyle.Fill;
                frmDI.prevPageIndex = 0; // use this to prevent the project completion popup.
                frmDI.BeforeShown();
            }
            else
            {
                MessageBox.Show("Looks like you're missing the Programs.xml file. Please go to the Dashboard screen and Download data. If problem continues please contact support.");
                NavigateToDashboardPanel();
            }
        }

        #endregion DirectInstall

        private List<ApiComponent> ApplyBuildingRetrofitFilters(IEnumerable<ApiComponent> componentList, string filterData, List<ApiFilterSet> filters)
        {
            return ApiFilterHelper.FilterComponentByFilters(componentList, filterData ?? String.Empty, filters ?? new List<ApiFilterSet>());
        }

        /// <summary>
        /// Provides a mechanism for initializing the form (layout and data) based on the current audit's Program.
        /// </summary>
        private void InitializeFormByProgramType(String rawProgramType)
        {
            string programType = FieldTool.Constants.ProgramType.Parse(rawProgramType);
            this.txtProgramType.Text = programType.ToString();
            //this.txtProgramType.Visible = this.DoDebug;

            this.FillBuildingDropDowns(programType);
            this.FillBuildingSpaces(programType);
            this.FillBuildingUnitSpaces(FieldTool.Constants.ProgramType.Residential); //Building Units should always use residential spaces

            ListViewItem newItem;

            switch (programType)
            {
                case FieldTool.Constants.ProgramType.MultiFamily:

                    this.btnAuditWalkUnits.Visible = true;
                    this.pagAuditWalkUnits.Visible = true;
                    this.pnlReportCustomFields.Visible = true;
                    this.txtBuildingNumUnits.Visible = true;
                    this.lblBuildingNumUnits.Visible = true;
                    this.btnConsumersEnergyAutoEmail.Visible = false;
                    this.tabBuildingUnits.SelectedTabIndex = 0;
                    this.lblReportNumRecommendations.Visible = false;
                    this.numReportecommendations.Visible = false;

                    //Report options to remove
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        if (li.Text.Equals("Top 2 Recommendations"))
                        {
                            this.lstCustomReports.Items.Remove(li);
                            break;
                        }
                    }
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        if (li.Text.Equals("Top 3 Recommendations"))
                        {
                            this.lstCustomReports.Items.Remove(li);
                            break;
                        }
                    }
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        if (li.Text.Equals("Direct Install Summary"))
                        {
                            this.lstCustomReports.Items.Remove(li);
                            break;
                        }
                    }

                    //Clear report options
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        this.lstCustomReports.Items.Remove(li);
                    }

                    //Add report options
                    if (SelectedAuditMultiFamilyReportBranding != null)
                    {
                        if (SelectedAuditMultiFamilyReportBranding.IncludeIntroPage)
                        {
                            newItem = new ListViewItem("Introduction", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectIntroPage)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeRecommendationsSection)
                        {
                            newItem = new ListViewItem("Recommendations", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectRecommendationsSection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeRecommendationOptionsSection)
                        {
                            newItem = new ListViewItem("Recommendation Options", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectRecommendationOptionsSection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeBuildingDISummarySection)
                        {
                            newItem = new ListViewItem("Building Direct Install Summary", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectBuildingDISummarySection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeUnitDIEstimatorSection)
                        {
                            newItem = new ListViewItem("Unit Direct Install Estimator", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectUnitDIEstimatorSection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeGasHistorySection)
                        {
                            newItem = new ListViewItem("Show Gas History", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectGasHistorySection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeElectricHistorySection)
                        {
                            newItem = new ListViewItem("Show Electric History", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectElectricHistorySection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeNextStepsSection)
                        {
                            newItem = new ListViewItem("Next Steps", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectNextStepsSection)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }

                        if (SelectedAuditMultiFamilyReportBranding.IncludeSignaturePage)
                        {
                            newItem = new ListViewItem("Signature", lstCustomReports.Groups["lvlReportSections"]);
                            if (SelectedAuditMultiFamilyReportBranding.AutoSelectSignaturePage)
                            {
                                newItem.Checked = true;
                            }
                            lstCustomReports.Items.Add(newItem);
                        }
                    }

                    break;

                case FieldTool.Constants.ProgramType.Commercial:

                    if (this.CurrentAuditReportReportBranding != null && this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                    {
                        this.btnAuditWalkUnits.Visible = false;
                        this.pagAuditWalkUnits.Visible = false;
                        this.pnlReportCustomFields.Visible = true;
                        this.txtBuildingNumUnits.Visible = false;
                        this.lblBuildingNumUnits.Visible = false;
                        this.btnConsumersEnergyAutoEmail.Visible = true;
                        this.lblReportNumRecommendations.Visible = true;
                        this.numReportecommendations.Visible = true;
                        txtTopRecommendation.Visible = false;
                        lblTopRecommendation.Visible = false;

                        //Clear report options
                        foreach (ListViewItem li in this.lstCustomReports.Items)
                        {
                            this.lstCustomReports.Items.Remove(li);
                        }

                        //Add report options

                        newItem = new ListViewItem("Introduction", this.lstCustomReports.Groups["lvlReportSections"]);
                        newItem.Checked = true;
                        this.lstCustomReports.Items.Add(newItem);

                        newItem = new ListViewItem("Top 2 Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                        newItem.Checked = true;
                        this.lstCustomReports.Items.Add(newItem);

                        newItem = new ListViewItem("Top 3 Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                        newItem.Checked = true;
                        this.lstCustomReports.Items.Add(newItem);

                        newItem = new ListViewItem("Top X Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                        if (this.SelectedAuditCommercialReportBranding.AutoSelectTopRecommendations)
                            newItem.Checked = true;
                        this.lstCustomReports.Items.Add(newItem);

                        newItem = new ListViewItem("Direct Install Summary", this.lstCustomReports.Groups["lvlReportSections"]);
                        newItem.Checked = true;
                        this.lstCustomReports.Items.Add(newItem);

                        this.lstCustomReports.Items.Add(new ListViewItem("Show Gas History", this.lstCustomReports.Groups["lvlReportSections"]));
                        this.lstCustomReports.Items.Add(new ListViewItem("Show Electric History", this.lstCustomReports.Groups["lvlReportSections"]));
                        this.lstCustomReports.Items.Add(new ListViewItem("Signature", this.lstCustomReports.Groups["lvlReportSections"]));
                    }
                    else
                    {
                        this.btnAuditWalkUnits.Visible = false;
                        this.pagAuditWalkUnits.Visible = false;
                        this.pnlReportCustomFields.Visible = true;
                        this.txtBuildingNumUnits.Visible = false;
                        this.lblBuildingNumUnits.Visible = false;
                        this.btnConsumersEnergyAutoEmail.Visible = false;
                        this.lblReportNumRecommendations.Visible = true;
                        this.numReportecommendations.Visible = true;
                        txtTopRecommendation.Visible = true;
                        lblTopRecommendation.Visible = true;

                        //Clear report options
                        foreach (ListViewItem li in this.lstCustomReports.Items)
                        {
                            this.lstCustomReports.Items.Remove(li);
                        }

                        //Add report options
                        if (this.SelectedAuditCommercialReportBranding.IncludeIntroPage)
                        {
                            newItem = new ListViewItem("Introduction", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectIntroPage)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeRecommendationsSection)
                        {
                            newItem = new ListViewItem("All Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectAllRecommendations)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);

                            newItem = new ListViewItem("Top Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectTopRecommendations)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeRecommendationOptionsSection)
                        {
                            newItem = new ListViewItem("Recommendation Options", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectRecommendationOptionsSection)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeDISummarySection)
                        {
                            newItem = new ListViewItem("Direct Install Summary", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectDISummarySection)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeGasHistorySection)
                        {
                            newItem = new ListViewItem("Show Gas History", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectGasHistorySection)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeElectricHistorySection)
                        {
                            newItem = new ListViewItem("Show Electric History", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectElectricHistorySection)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }

                        if (this.SelectedAuditCommercialReportBranding.IncludeSignaturePage)
                        {
                            newItem = new ListViewItem("Signature", this.lstCustomReports.Groups["lvlReportSections"]);
                            if (this.SelectedAuditCommercialReportBranding.AutoSelectSignaturePage)
                            {
                                newItem.Checked = true;
                            }
                            this.lstCustomReports.Items.Add(newItem);
                        }
                    }

                    break;

                default:

                    this.btnAuditWalkUnits.Visible = false;
                    this.pagAuditWalkUnits.Visible = false;

                    this.pnlReportCustomFields.Visible = true;

                    this.txtBuildingNumUnits.Visible = false;
                    this.lblBuildingNumUnits.Visible = false;
                    this.btnConsumersEnergyAutoEmail.Visible = true;

                    this.lblReportNumRecommendations.Visible = false;
                    this.numReportecommendations.Visible = false;

                    //Clear report options
                    foreach (ListViewItem li in this.lstCustomReports.Items)
                    {
                        this.lstCustomReports.Items.Remove(li);
                    }

                    //Add report options
                    newItem = new ListViewItem("Introduction", this.lstCustomReports.Groups["lvlReportSections"]);
                    newItem.Checked = true;
                    this.lstCustomReports.Items.Add(newItem);

                    newItem = new ListViewItem("Top 2 Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                    newItem.Checked = true;
                    this.lstCustomReports.Items.Add(newItem);

                    newItem = new ListViewItem("Top 3 Recommendations", this.lstCustomReports.Groups["lvlReportSections"]);
                    newItem.Checked = true;
                    this.lstCustomReports.Items.Add(newItem);

                    newItem = new ListViewItem("Direct Install Summary", this.lstCustomReports.Groups["lvlReportSections"]);
                    newItem.Checked = true;
                    this.lstCustomReports.Items.Add(newItem);

                    this.lstCustomReports.Items.Add(new ListViewItem("Show Gas History", this.lstCustomReports.Groups["lvlReportSections"]));
                    this.lstCustomReports.Items.Add(new ListViewItem("Show Electric History", this.lstCustomReports.Groups["lvlReportSections"]));
                    this.lstCustomReports.Items.Add(new ListViewItem("Signature", this.lstCustomReports.Groups["lvlReportSections"]));

                    break;
            }

            if (String.IsNullOrEmpty(txtSignOffTerms.Text))
            {
                tabSignOff.Visible = false;
                foreach (ListViewItem li in this.lstCustomReports.Items)
                {
                    if (li.Text.Equals("Signature"))
                    {
                        this.lstCustomReports.Items.Remove(li);
                    }
                }
            }
            else
            {
                tabSignOff.Visible = true;
            }
        }

        private void FillRetrofitFilterDropDowns(string programCode, string filterData, string zipCode)
        {
            var p = DataStore.GetProgramsByProgramCode(programCode).FirstOrDefault();
            if (p != default(ApiProgramMetadata))
            {
                var map = ApiFilterHelper.FilterDataToMap(filterData);
                // WeatherZone. Load options, and if the option that is found exists, select it.
                // Others, load a control and select it's option from the matching building filter string options
                this.pnlBuildingRetrofitFilters.Controls.Clear();
                var associatedProgram = DataStore.GetProgramsByProgramCode(p.program.AssociatedDIProgram).FirstOrDefault();
                foreach (var filter in p.filters)
                {
                    var currentSettings = map.ContainsKey(filter.Label) ? map[filter.Label] : String.Empty;
                    List<string> optionValues;
                    var parentOptionValues = filter.AllOptions(true);
                    if (associatedProgram != null && associatedProgram.filters != null)
                    {
                        var associatedFilter = associatedProgram.filters.Where(x => x.PropertyName == filter.PropertyName).FirstOrDefault();
                        var associatedOptionValues = associatedFilter == null ? new List<string>() : associatedFilter.AllOptions(true);
                        optionValues = parentOptionValues.Union(associatedOptionValues).ToList();
                    }
                    else
                    {
                        optionValues = parentOptionValues;
                    }

                    if (filter.PropertyName == ApiFilterHelper.CommonKeys.KEY_WEATHER_ZONE)
                    {
                        var z = DataStore.ZipZones.FirstOrDefault(x => !String.IsNullOrEmpty(x.ZipCode) && x.ZipCode == zipCode);
                        if (z != default(Zip) && String.IsNullOrEmpty(currentSettings))
                        {
                            currentSettings = z.WeatherCityName;
                        }
                        optionValues = new List<string> { String.Empty };
                        optionValues.AddRange(DataStore.ZipZones.Select(x => x.WeatherCityName).Distinct().ToList());
                    }

                    if (filter.PropertyName == ApiFilterHelper.CommonKeys.KEY_FUEL_TYPE)
                    {
                        // completely skip
                        continue;
                    }

                    // Label
                    var label = new LabelX();
                    label.Text = filter.Label + ":";
                    label.TextAlignment = System.Drawing.StringAlignment.Far;
                    label.TextLineAlignment = StringAlignment.Center;
                    label.Name = filter.Label;
                    label.BackColor = Color.White;
                    label.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                    label.Height = 29;
                    label.Width = 200;

                    this.pnlBuildingRetrofitFilters.Controls.Add(label);

                    // Select
                    var combo = new ComboBoxEx();
                    combo.Name = filter.Label;

                    var bs = new BindingSource();
                    bs.DataSource = optionValues;
                    combo.DataSource = bs;

                    Font f = new System.Drawing.Font("Segoa UI", 12);
                    combo.Font = f;

                    combo.Text = currentSettings;
                    combo.DisplayMember = "Text";
                    combo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
                    combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    combo.ForeColor = System.Drawing.Color.Black;
                    combo.FormattingEnabled = true;
                    combo.ItemHeight = 23;
                    combo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                    combo.TabIndex = 13 + this.pnlBuildingRetrofitFilters.Controls.Count;
                    combo.Height = 29;
                    combo.Width = 300;
                    this.pnlBuildingRetrofitFilters.Controls.Add(combo);
                    this.pnlBuildingRetrofitFilters.Controls.Cast<Control>().Where(x => x.Name == filter.Label && x.GetType() == typeof(ComboBoxEx)).First().Text = currentSettings;
                }
            }

            IOrderedEnumerable<Control> controls = this.pnlBuildingRetrofitFilters.Controls.Cast<Control>().OrderBy(x => x.Name).ThenByDescending(x => x.GetType().Name);
            int currTop = 70;
            int cboOffset = 20;
            foreach (Control ctl in controls)
            {
                LabelX lbl = ctl as LabelX;
                if (lbl != null)
                {
                    lbl.Location = new Point(10, currTop);
                    cboOffset = lbl.Width + 20;
                }
                else
                {
                    ComboBoxEx cbo = ctl as ComboBoxEx;
                    if (cbo != null)
                    {
                        cbo.Location = new Point(cboOffset, currTop);

                        currTop += 70;
                    }
                }
            }
        }

        private void FillBuildingDropDowns(String programType)
        {
            this.cboBuildingCategory.Items.Clear();

            List<BuildingCategoryDropDownItem> categoryItems = DataStore.GetBuildingCategories(programType);

            foreach (BuildingCategoryDropDownItem item in categoryItems)
            {
                ListViewItem li = new ListViewItem(item.Category);
                li.Tag = item;
                this.cboBuildingCategory.Items.Add(li);
            }
        }

        private void FillBuildingDetailDropDowns()
        {
            string buildingDetailsFile = DataStore.XmlBuildingDetailsFile;

            if (File.Exists(buildingDetailsFile))
            {
                try
                {
                    BuildingDetails xmlData = null;
                    using (TextReader reader = new StreamReader(buildingDetailsFile))
                    {
                        XmlSerializer deserializer = new XmlSerializer(typeof(BuildingDetails));
                        object obj = deserializer.Deserialize(reader);
                        xmlData = (BuildingDetails)obj;
                    }
                    if (xmlData != null)
                    {
                        this.cboBuildingHeatingSystem.DataSource = xmlData.HeatingSystem;
                        this.cboBuildingHeatFuel.DataSource = xmlData.HeatFuel;
                        this.cboBuildingAirConditioning.DataSource = xmlData.AirConditioning;
                        this.cboBuildingDHW.DataSource = xmlData.DHW;
                        this.cboBuildingDHWFuel.DataSource = xmlData.DHWFuel;
                        this.cboBuildingParking.DataSource = xmlData.Parking;
                        this.cboBuildingAtticType.DataSource = xmlData.AtticType;
                        this.cboBuildingWallType.DataSource = xmlData.WallType;
                        this.cboBuildingFoundation.DataSource = xmlData.Foundation;
                        this.cboBuildingWindows.DataSource = xmlData.Windows;
                    }
                }
                catch
                {
                    this.cboBuildingHeatingSystem.Text = "";
                    this.cboBuildingHeatFuel.Text = "";
                    this.cboBuildingAirConditioning.Text = "";
                    this.cboBuildingDHW.Text = "";
                    this.cboBuildingDHWFuel.Text = "";
                    this.cboBuildingParking.Text = "";
                    this.cboBuildingAtticType.Text = "";
                    this.cboBuildingWallType.Text = "";
                    this.cboBuildingFoundation.Text = "";
                    this.cboBuildingWindows.Text = "";
                }
            }
        }

        private void FillBuildingSpaces(String programType)
        {
            this.lstAvailableSpaces.Items.Clear();
            this.lstSelectedSpaces.Items.Clear();

            if (this.SelectedBuilding != null)
            {
                if (this.SelectedBuilding.BuildingSpaces != null && this.SelectedBuilding.BuildingSpaces.Count > 0)
                {
                    foreach (BuildingSpace space in this.SelectedBuilding.BuildingSpaces)
                    {
                        this.lstSelectedSpaces.Items.Add(space.Space);
                    }

                    List<string> allSpaces = DataStore.GetAllAvailableSpaces(programType, this.SelectedBuilding.BuildingCategory, this.SelectedBuilding.BuildingType);

                    for (int i = 0; i < allSpaces.Count; i++)
                    {
                        if (!this.lstSelectedSpaces.Items.Contains(new ListViewItem(allSpaces[i])))
                        {
                            this.lstAvailableSpaces.Items.Add(allSpaces[i]);
                        }
                    }
                }
                else
                {
                    List<string> availableList = DataStore.GetAvailableSpacesWithoutDefaults(programType, this.SelectedBuilding.BuildingCategory, this.SelectedBuilding.BuildingType);
                    List<string> selectedList = DataStore.GetDefaultSpaces(programType, this.SelectedBuilding.BuildingCategory, this.SelectedBuilding.BuildingType);

                    for (int i = 0; i < availableList.Count; i++)
                    {
                        this.lstAvailableSpaces.Items.Add(availableList[i]);
                    }

                    for (int i = 0; i < selectedList.Count; i++)
                    {
                        this.lstSelectedSpaces.Items.Add(selectedList[i]);
                    }
                }
            }
        }

        private void FillBuildingUnitSpaces(String programType)
        {
            if (this.SelectedBuilding != null)
            {
                if (this.SelectedBuilding.BuildingSpaces != null && this.SelectedBuilding.BuildingSpaces.Count > 0)
                {
                    //The following line is being replaced below - Mike C
                    //List<string> allSpaces = DataStore.GetAllAvailableSpaces(programType, this.SelectedBuilding.BuildingCategory, this.SelectedBuilding.BuildingType);

                    //Per Eric, spaces in the unit walk through should always use residential
                    //Since Building Category and Building Type are multifamily we will need to pass in a default residential value for those values to pull residential spaces
                    //A default category="Residential" and buildingType="Apartment in 5+ unit building" is being hard coded at this point.

                    //Hard coded defaults for the unit space lookup
                    string unitBuildingCategory = "Residential";
                    string unitBuildingType = "Apartment in 5+ unit building";

                    List<string> allSpaces = DataStore.GetAllAvailableSpaces(programType, unitBuildingCategory, unitBuildingType);

                    ComboBoxCellType cmbocell = new ComboBoxCellType();
                    cmbocell.Items = allSpaces.ToArray<string>();
                    cmbocell.AutoSearch = FarPoint.Win.AutoSearch.SingleCharacter;
                    cmbocell.Editable = false;
                    cmbocell.MaxDrop = 8;

                    //for (int i = 0; i < grdUnitDirectInstall_Sheet1.Rows.Count - 1; i++) {   //Don't inlude last row.  Last row is for Unit total.
                    //    grdUnitDirectInstall_Sheet1.Cells[i, UNIT_DI_GRID_COLUMN_SPACE].CellType = cmbocell;
                    ComboBoxCellType c = this.grdUnitDirectInstall_Sheet1.Columns[UNIT_DI_GRID_COLUMN_SPACE].CellType as ComboBoxCellType;
                    c.Items = cmbocell.Items;

                    //}
                }
            }
        }

        private EfficiencyNavigatorEmailDialogResult ShowEfficiencyNavigatorRegistrationEmailDialog(string defaultEmailAddress, bool sendRegistrationEmail)
        {
            Form form = new Form();
            form.Width = 400;
            form.Height = 180;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            form.Text = "Account Email";
            form.StartPosition = FormStartPosition.CenterParent;

            CheckBox sendEmailCheckBox = new CheckBox() { Left = 10, Top = 10, Width = 360, Text = "Send Efficiency Navigator registration email" };
            Label emailAddressPromptLabel = new Label() { Left = 10, Top = 40, Width = 360, Height = 35, Text = "Verify or enter the email address where the Efficiency Navigator registration email should be sent." };
            TextBox emailAddressTextBox = new TextBox() { Left = 10, Top = 75, Width = 360, Text = defaultEmailAddress };
            Button okButton = new Button() { Text = "OK", Left = 160, Width = 100, Top = 100, DialogResult = System.Windows.Forms.DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 280, Width = 100, Top = 100, DialogResult = System.Windows.Forms.DialogResult.Cancel };

            // Attach control events
            sendEmailCheckBox.CheckedChanged += (sender, e) =>
            {
                emailAddressPromptLabel.Enabled = emailAddressTextBox.Enabled = sendEmailCheckBox.Checked;
                okButton.Enabled = (!sendEmailCheckBox.Checked || !string.IsNullOrWhiteSpace(emailAddressTextBox.Text));
            };

            emailAddressTextBox.TextChanged += (sender, e) =>
            {
                okButton.Enabled = (!sendEmailCheckBox.Checked || !string.IsNullOrWhiteSpace(emailAddressTextBox.Text));
            };

            okButton.Click += (sender, e) => { form.Close(); };
            cancelButton.Click += (sender, e) => { form.Close(); };

            // Set starting control properties
            sendEmailCheckBox.Checked = emailAddressPromptLabel.Enabled = emailAddressTextBox.Enabled = sendRegistrationEmail;
            okButton.Enabled = (!sendEmailCheckBox.Checked || !string.IsNullOrWhiteSpace(emailAddressTextBox.Text));

            // Add controls to form
            form.Controls.Add(sendEmailCheckBox);
            form.Controls.Add(emailAddressPromptLabel);
            form.Controls.Add(emailAddressTextBox);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);
            form.AcceptButton = okButton;

            DialogResult dialogResult = form.ShowDialog();

            EfficiencyNavigatorEmailDialogResult result = new EfficiencyNavigatorEmailDialogResult()
            {
                DialogResult = dialogResult,
                EmailAddress = emailAddressTextBox.Text,
                SendRegistrationEmail = sendEmailCheckBox.Checked
            };

            return result;
        }

        private class EfficiencyNavigatorEmailDialogResult
        {
            public string EmailAddress { get; set; }
            public bool SendRegistrationEmail { get; set; }
            public DialogResult DialogResult { get; set; }
        }

        private void grdEipItems_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            EquipmentMaster equipment = grdEipItems.ActiveSheet.ActiveRow.Tag as EquipmentMaster;
            if (equipment != null)
            {
                int quantity;
                if (e.Column == EIP_GRID_COLUMN_QUANTITY && int.TryParse(e.EditingControl.Text, out quantity))
                {
                    equipment.Quantity = quantity;
                    DataStore.UpdateEquipment(equipment, SelectedAudit, SelectedBuilding, true);
                    this.recRecommendations.MergeEquipment(equipment);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private string GetEquipmentIdFromActiveRow()
        {
            return grdEipItems.ActiveSheet.Cells[grdEipItems.ActiveSheet.ActiveRowIndex, EIP_GRID_COLUMN_ID].Text;
        }

        private EquipmentMaster GetSelectedEquipment()
        {
            return GetEquipmentById(GetEquipmentIdFromActiveRow());
        }

        private EquipmentMaster GetEquipmentById(string equipmentId)
        {
            return SelectedBuilding.Equipments.Where(x => x.EquipmentMasterId == equipmentId).FirstOrDefault();
        }

        private void tabStatisticsMain_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (!this._isLoadingForm && !this._isLoadingAudit)
            {
                Cursor.Current = Cursors.WaitCursor;

                switch (e.OldTab.Name)
                {
                    case "tabHistoricalElectricUse":

                        if (!this.SaveElectricHistory())
                        {
                            e.Cancel = true;
                        }

                        break;

                    case "tabHistoricalGasUse":

                        if (!this.SaveGasHistory())
                        {
                            e.Cancel = true;
                        }

                        break;
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void tabCompanyInfoMain_LostFocus(object sender, EventArgs e)
        {
            //if (this.SelectedAudit != null) {
            //    Cursor.Current = Cursors.WaitCursor;
            //    this.SaveBuilding();
            //    this.SaveElectricHistory();
            //    this.SaveGasHistory();
            //    Cursor.Current = Cursors.Default;
            //}
        }

        private void tabCompanyInfoMain_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (!this._isLoadingForm && !this._isLoadingAudit)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (frmMainContactHelper.IsContactLockingForm)
                {
                    MessageBox.Show("Please save the contact before switching tabs.", "Save Contact", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else if (frmMainCompanyHelper.IsLockedForEdit)
                {
                    MessageBox.Show("Please save the company before switching tabs.", "Save Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else
                {
                    switch (e.OldTab.Name)
                    {
                        case "tabBuildings":

                            this.SaveBuilding();

                            break;

                        case "tabStatistics":

                            if (!this.SaveElectricHistory() || !this.SaveGasHistory())
                            {
                                e.Cancel = true;
                            }

                            break;
                    }
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void tabAuditMain_LostFocus(object sender, EventArgs e)
        {
            if (this.SelectedAudit != null)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (this.SelectedBuilding != null)
                {
                    this.SaveAuditSchedule();
                }
                this.SaveAllSpaces();

                Cursor.Current = Cursors.Default;
            }
        }

        private void tabAuditMain_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (!this._isLoadingForm && !this._isLoadingAudit)
            {
                Cursor.Current = Cursors.WaitCursor;

                if (frmMainContactHelper.IsContactLockingForm)
                {
                    MessageBox.Show("Please save the contact before switching tabs.", "Save Contact", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else if (frmMainCompanyHelper.IsLockedForEdit)
                {
                    MessageBox.Show("Please save the company before switching tabs.", "Save Company", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                }
                else
                {
                    switch (e.OldTab.Name)
                    {
                        case "tabWalkThru":

                            if (this.SelectedBuilding != null)
                            {
                                this.SaveAuditSchedule();
                            }

                            this.SaveAllSpaces();

                            break;

                        case "tabCompanyInfo":

                            if (!this.SaveElectricHistory() || !this.SaveGasHistory())
                            {
                                e.Cancel = true;
                            }

                            break;
                    }
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void lstCustomReports_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
            {
                ListViewEx ctl = sender as ListViewEx;

                if (ctl != null && ctl.Items.Count > 0)
                {
                    if (this.CurrentAuditReportReportBranding.IsConsumersEnergy)
                    {
                        ListViewEx ctl2 = sender as ListViewEx;
                        ListViewItem li;
                        ListViewItem li2;
                        switch (e.Item.Text)
                        {
                            case "Top 2 Recommendations":
                                try
                                {
                                    li = ctl.FindItemWithText("Top 3 Recommendations");
                                }
                                catch (Exception ex)
                                {
                                    li = null;
                                }
                                if (li != null)
                                {
                                    li.Checked = false;
                                }
                                if (ctl2 != null && ctl2.Items.Count > 0)
                                {
                                    try
                                    {
                                        li2 = ctl2.FindItemWithText("Top X Recommendations");
                                    }
                                    catch (Exception ex)
                                    {
                                        li2 = null;
                                    }
                                    if (li2 != null)
                                    {
                                        li2.Checked = false;
                                    }
                                }
                                numReportecommendations.Enabled = false;
                                break;

                            case "Top 3 Recommendations":
                                try
                                {
                                    li = ctl.FindItemWithText("Top 2 Recommendations");
                                }
                                catch (Exception ex)
                                {
                                    li = null;
                                }
                                if (li != null)
                                {
                                    li.Checked = false;
                                }
                                if (ctl2 != null && ctl2.Items.Count > 0)
                                {
                                    try
                                    {
                                        li2 = ctl2.FindItemWithText("Top X Recommendations");
                                    }
                                    catch (Exception ex)
                                    {
                                        li2 = null;
                                    }
                                    if (li2 != null)
                                    {
                                        li2.Checked = false;
                                    }
                                }
                                numReportecommendations.Enabled = false;
                                break;

                            case "Top X Recommendations":
                                try
                                {
                                    li = ctl.FindItemWithText("Top 2 Recommendations");
                                }
                                catch (Exception ex)
                                {
                                    li = null;
                                }
                                if (li != null)
                                {
                                    li.Checked = false;
                                }
                                if (ctl2 != null && ctl2.Items.Count > 0)
                                {
                                    try
                                    {
                                        li2 = ctl2.FindItemWithText("Top 3 Recommendations");
                                    }
                                    catch (Exception ex)
                                    {
                                        li2 = null;
                                    }
                                    if (li2 != null)
                                    {
                                        li2.Checked = false;
                                    }
                                }
                                numReportecommendations.Enabled = true;
                                break;
                        }
                    }
                    else
                    {
                        try
                        {
                            ListViewItem li;
                            switch (e.Item.Text)
                            {
                                case "Top 2 Recommendations":
                                    li = ctl.FindItemWithText("Top 3 Recommendations");
                                    if (li != null)
                                    {
                                        li.Checked = false;
                                    }
                                    break;

                                case "Top 3 Recommendations":
                                    li = ctl.FindItemWithText("Top 2 Recommendations");
                                    if (li != null)
                                    {
                                        li.Checked = false;
                                    }
                                    break;

                                case "Top Recommendations":
                                    this.lblReportNumRecommendations.Visible = true;
                                    this.numReportecommendations.Visible = true;
                                    li = ctl.FindItemWithText("All Recommendations");
                                    if (li != null)
                                    {
                                        li.Checked = false;
                                    }
                                    break;

                                case "All Recommendations":
                                    li = ctl.FindItemWithText("Top Recommendations");
                                    if (li != null)
                                    {
                                        li.Checked = false;
                                        this.lblReportNumRecommendations.Visible = false;
                                        this.numReportecommendations.Visible = false;
                                    }
                                    break;
                            }
                        }
                        catch (NullReferenceException)
                        {
                            ;
                        }
                    }
                }
            }
            else
            {
                try
                {
                    switch (e.Item.Text)
                    {
                        case "Top Recommendations":
                            this.lblReportNumRecommendations.Visible = false;
                            this.numReportecommendations.Visible = false;
                            break;
                    }
                }
                catch (NullReferenceException)
                {
                    ;
                }
            }
        }

        private void btnImportElectricUsage_Click(object sender, EventArgs e)
        {
            string utilityName = ""; // TODO: get this value from audit
            ImportUtilityUsageHistory_Click(grdElectricHist_Sheet1, DataRepositoryService.EnergyType.Electric, x => x.ElectricAccountNumber, utilityName);
        }

        private void btnImportGasUsage_Click(object sender, EventArgs e)
        {
            string utilityName = ""; // TODO: get this value from audit
            ImportUtilityUsageHistory_Click(grdGasHist_Sheet1, DataRepositoryService.EnergyType.Gas, x => x.GasAccountNumber, utilityName);
        }

        private void ImportUtilityUsageHistory_Click(FarPoint.Win.Spread.SheetView sheet, DataRepositoryService.EnergyType energyType, Func<Company, string> utilityAccountNumberSelector, string utilityName)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                int numberOfImportedRecords = FillUtilityUsageHistory(sheet, energyType, utilityAccountNumberSelector, utilityName);
                MessageBox.Show(string.Format("{0} {1} usage history records imported.", numberOfImportedRecords, energyType), "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Lg.Error(string.Format("An error occurred importing {0} usage history. {1}", energyType, ex.ToString()));
                MessageBox.Show(string.Format("An error occurred importing {0} usage history.", energyType), "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;
        }

        private void txtUnitQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int FillUtilityUsageHistory(FarPoint.Win.Spread.SheetView sheet, DataRepositoryService.EnergyType energyType, Func<Company, string> utilityAccountNumberSelector, string utilityName)
        {
            List<DataRepositoryUsageHistory> data = new List<DataRepositoryUsageHistory>();

            DataRepositoryService service = new DataRepositoryService(ConfigurationManager.AppSettings);

            if (SelectedAudit != null)
            {
                Company company = DataStore.GetCompanyByAudit(SelectedAudit.Id);
                if (company != null)
                {
                    data = service.GetUsageHistory(utilityAccountNumberSelector(company), energyType, utilityName);
                    data.Sort();

                    int initialRowIndex = 3;
                    for (int i = 0; i < 12; i++)
                    {
                        int row = i + initialRowIndex;
                        if (i < data.Count)
                        {
                            sheet.Cells[row, 2].Value = data[i].MeterReadDate;
                            sheet.Cells[row, 3].Value = data[i].BillDays;
                            sheet.Cells[row, 4].Value = data[i].CoolingDegreeDays;
                            sheet.Cells[row, 5].Value = data[i].HeatingDegreeDays;
                            sheet.Cells[row, 6].Value = data[i].TotalEnergyUsage;

                            switch (energyType)
                            {
                                case DataRepositoryService.EnergyType.Gas:
                                    sheet.Cells[row, 7].Value = data[i].BillAmount;
                                    break;

                                case DataRepositoryService.EnergyType.Electric:
                                    sheet.Cells[row, 7].Value = data[i].OnPeakUsage;
                                    sheet.Cells[row, 8].Value = data[i].OffPeakUsage;
                                    sheet.Cells[row, 9].Value = data[i].BilledUsage;
                                    sheet.Cells[row, 10].Value = data[i].MaxUsage;
                                    sheet.Cells[row, 11].Value = data[i].BillAmount;
                                    break;
                            }
                        }
                        else
                        {
                            // clear rows that were not returned in the import
                            for (int j = 2; j < sheet.Columns.Count; j++)
                            {
                                sheet.Cells[row, j].Value = null;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception(string.Format("Cannot import {0} history: company for audit [{1}] not found.", energyType, SelectedAudit.Id));
                }
            }
            else
            {
                throw new Exception(string.Format("Cannot import {0} history: selected audit is null.", energyType));
            }

            return data.Count;
        }

        private DialogResult ShowEmailDialog(string instructionsText, string defaultEmailAddress, out string emailAddress, out string emailCc, out string emailBcc)
        {
            Form form = new Form();
            form.Width = 400;
            form.Height = 190;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            form.Text = "Account Email";
            form.StartPosition = FormStartPosition.CenterParent;

            int promptLabelLeft = 10;
            int promptLabelWidth = 80;
            int textBoxLeft = promptLabelLeft + promptLabelWidth;
            int textBoxWidth = form.Width - textBoxLeft - promptLabelLeft - 20;
            int inputSectionTop = 45;
            int inputSectionSpacing = 25;

            Label instructionsLabel = new Label() { Left = 10, Top = 10, Width = 380, Height = 35, Text = instructionsText };
            Label emailLabel = new Label() { Left = promptLabelLeft, Top = inputSectionTop, Width = promptLabelWidth, Text = "Email Address" };
            Label ccLabel = new Label() { Left = promptLabelLeft, Top = inputSectionTop + inputSectionSpacing, Width = promptLabelWidth, Text = "Cc" };
            Label bccLabel = new Label() { Left = promptLabelLeft, Top = inputSectionTop + (inputSectionSpacing * 2), Width = promptLabelWidth, Text = "Bcc" };
            TextBox emailAddressTextBox = new TextBox() { Left = textBoxLeft, Top = inputSectionTop, Width = textBoxWidth, Text = defaultEmailAddress };
            TextBox ccTextBox = new TextBox() { Left = textBoxLeft, Top = inputSectionTop + inputSectionSpacing, Width = textBoxWidth, Text = "" };
            TextBox bccTextBox = new TextBox() { Left = textBoxLeft, Top = inputSectionTop + (inputSectionSpacing * 2), Width = textBoxWidth, Text = "" };
            Button okButton = new Button() { Text = "OK", Left = 160, Width = 100, Top = inputSectionTop + (inputSectionSpacing * 3), DialogResult = System.Windows.Forms.DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 270, Width = 100, Top = inputSectionTop + (inputSectionSpacing * 3), DialogResult = System.Windows.Forms.DialogResult.Cancel };

            // Attach control events
            emailAddressTextBox.TextChanged += (sender, e) =>
            {
                okButton.Enabled = !string.IsNullOrWhiteSpace(emailAddressTextBox.Text);
            };

            okButton.Click += (sender, e) => { form.Close(); };
            cancelButton.Click += (sender, e) => { form.Close(); };

            // Set starting control properties
            okButton.Enabled = !string.IsNullOrWhiteSpace(emailAddressTextBox.Text);

            // Add controls to form
            form.Controls.Add(instructionsLabel);
            form.Controls.Add(emailLabel);
            form.Controls.Add(ccLabel);
            form.Controls.Add(bccLabel);
            form.Controls.Add(emailAddressTextBox);
            form.Controls.Add(ccTextBox);
            form.Controls.Add(bccTextBox);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);
            form.AcceptButton = okButton;

            DialogResult result = form.ShowDialog();
            emailAddress = emailAddressTextBox.Text;
            emailCc = ccTextBox.Text;
            emailBcc = bccTextBox.Text;

            return result;
        }

        private void CopyUserDocs()
        {
            CopyUserDoc(PathConstant.docClipboardAuditUserGuideFileName);
            CopyUserDoc(PathConstant.docClipboardAuditFAQFileName);
            CopyUserDoc(PathConstant.docClipboardDIUserGuideFileName);
            CopyUserDoc(PathConstant.docClipboardDIFAQFileName);
        }

        private void CopyUserDoc(string fileName)
        {
            string installDocsPath = DataStore.UserDocsInstallPath;
            string docAppFilePath = Path.Combine(installDocsPath, fileName);

            if (File.Exists(docAppFilePath))
            {
                string userDocsPath = DataStore.UserDocsDirectory;
                string docUserFilePath = Path.Combine(userDocsPath, fileName);

                DateTime appFileDT = File.GetLastWriteTime(docAppFilePath);
                DateTime userFileDT = File.GetLastWriteTime(docUserFilePath);

                if (appFileDT > userFileDT)
                {
                    try
                    {
                        File.Copy(docAppFilePath, docUserFilePath);
                        Lg.Info("New file copied locally.  " + docUserFilePath);
                    }
                    catch (Exception e)
                    {
                        var inner = ExceptionHelper.Innermost(e);
                        Lg.Info("No need to copy file.  Most current file is already loaded locally.");
                        Lg.Info(inner.Message);
                    }
                }
            }
        }

        private void lblBuildingPropertyTypeCom_Click(object sender, EventArgs e)
        {
            if (this._allowBuildingPropertyTypeEdit)
            {
                this.optBuildingCommercial.Checked = true;
            }
        }

        private void lblBuildingPropertyTypeRes_Click(object sender, EventArgs e)
        {
            if (this._allowBuildingPropertyTypeEdit)
            {
                this.optBuildingResidential.Checked = true;
            }
        }

        private void lblBuildingAffordableHousing_Click(object sender, EventArgs e)
        {
            this.chbAffordableHousing.Checked = !this.chbAffordableHousing.Checked;
        }

        private void SetupKeyboardToControls()
        {
            DevComponents.DotNetBar.Keyboard.KeyboardControl numKeys = new DevComponents.DotNetBar.Keyboard.KeyboardControl();
            numKeys.Keyboard = FieldTool.UI.Helpers.ControlHelper.GetKeyboard("Numeric");
            numKeys.AttachTo(this.numBuildingOccupants);
        }

        private void grdEipItems_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == 0 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // Image column & right mouse button.
                this.grdEipItems.ContextMenuStrip = this.mnuEipGrid;
            }
            else
            {
                this.grdEipItems.ContextMenuStrip = null;
            }
        }

        private void mnuEipGridDeleteImage_Click(object sender, EventArgs e)
        {
            if (this.grdEipItems_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("Please select a component with an image to remove.", "Delete Image", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                EquipmentMaster equipment = this.grdEipItems_Sheet1.ActiveRow.Tag as EquipmentMaster;

                BLL.CommonUtilities.DeleteImageFromComponent(equipment, this.SelectedAudit, this.SelectedBuilding, true);
                this.grdEipItems_Sheet1.Cells[this.grdEipItems_Sheet1.ActiveRowIndex, 0].Value = null;
            }
        }

        private void InitMemoryCheckTimer(int interval)
        {
            memoryCheckTimer.Tick += new EventHandler(memoryCheckTimer_Tick);
            memoryCheckTimer.Interval = interval;
            memoryCheckTimer.Start();
        }

        private void memoryCheckTimer_Tick(object sender, EventArgs e)
        {
            double availableMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;

            if (totalMemory > 0 && totalMemory > availableMemory)
            {
                int percentAvailable = (int)((availableMemory / totalMemory) * 100.0);
                if (percentAvailable < Constants.MemoryMonitor.AVAILABLE_MEMORY_THRESHOLD)
                {
                    txtMemoryWarning.BackColor = Color.Red;
                    txtMemoryWarning.ForeColor = Color.White;
                    txtMemoryWarning.Text = "WARNING: Available memory is below " + Constants.MemoryMonitor.AVAILABLE_MEMORY_THRESHOLD + "%. This may cause problems with some functionality.  You should consider closing and restarting Clipboard.  AVAILABLE MEMORY = " + percentAvailable + "%";
                    txtMemoryWarning.Width = sbarMain.Width;
                    txtMemoryWarning.Height = sbarMain.Height + 10;
                    txtMemoryWarning.Left = sbarMain.Left;
                    txtMemoryWarning.Visible = true;
                    lblStatusText.Visible = false;
                }
                else
                {
                    txtMemoryWarning.Visible = false;
                    lblStatusText.Visible = true;
                }
            }
        }
    }
}