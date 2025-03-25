using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using FieldTool.BLL;
using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Cursor = System.Windows.Forms.Cursor;

namespace FieldTool.UI
{
    public partial class frmEquipmentAudit : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private Dictionary<string, string> _filters = new Dictionary<string, string>();

        //private bool _isLoaded = false;
        private List<string> _mainFormEquipmentList = new List<string>();

        private bool _isNew = true;
        private bool fireDurationDropDown = true;

        #endregion Private member variables

        #region Constructors

        public frmEquipmentAudit(EquipmentMaster equipment, List<string> selectedSpaces, Schedule.ScheduleCollection defaultSchedules, string buildingType,
            List<string> mainFormEquipmentList, string auditId, Building selectedBuilding)
        {
            InitializeComponent();

            if (equipment != null)
            {
                this.EquipmentObject = equipment;
                this.Type = equipment.TypeOfEnergy;
                this.TypeId = 1;
                this.SelectedSpaces = selectedSpaces;
                this.DefaultSchedules = defaultSchedules;
                this.BuildingType = buildingType;
                this._mainFormEquipmentList = mainFormEquipmentList;
                this.AuditId = auditId;
                this.SelectedBuilding = selectedBuilding;

                this._isNew = false;
            }
        }

        public frmEquipmentAudit(Enumerations.EnergyType type, int typeId, List<string> selectedSpaces, Schedule.ScheduleCollection defaultSchedules, string buildingType,
            List<string> mainFormEquipmentList, string auditId, Building selectedBuilding)
        {
            InitializeComponent();

            this.Type = type;
            this.TypeId = typeId;
            this._mainFormEquipmentList = mainFormEquipmentList;

            this.SelectedSpaces = selectedSpaces;
            this.DefaultSchedules = defaultSchedules;
            this.BuildingType = buildingType;
            this.AuditId = auditId;
            this.SelectedBuilding = selectedBuilding;
            //this.SetFormControlsEnabled(FormState.SelectingFilter);

            this._isNew = true;
        }

        #endregion Constructors

        #region Properties

        public string AuditId { get; set; }

        public string BuildingType { get; set; }

        public Building SelectedBuilding { get; set; }

        public Schedule.ScheduleCollection DefaultSchedules { get; set; }

        public EquipmentMaster EquipmentObject { get; set; }

        public Dictionary<string, string> Filter
        {
            get
            {
                this._filters = new Dictionary<string, string>();

                SystemTypeLookup item = this.GetSelectedItemFromList(this.lstFilter1);
                if (item != null)
                {
                    this._filters.Add("Filter1", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter2);
                if (item != null)
                {
                    this._filters.Add("Filter2", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter3);
                if (item != null)
                {
                    this._filters.Add("Filter3", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter4);
                if (item != null)
                {
                    this._filters.Add("Filter4", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter5);
                if (item != null)
                {
                    this._filters.Add("Filter5", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter6);
                if (item != null)
                {
                    this._filters.Add("Filter6", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter7);
                if (item != null)
                {
                    this._filters.Add("Filter7", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter8);
                if (item != null)
                {
                    this._filters.Add("Filter8", item.Name);
                }

                item = this.GetSelectedItemFromList(this.lstFilter9);
                if (item != null)
                {
                    this._filters.Add("Filter9", item.Name);
                }

                return this._filters;
            }
            set
            {
                this._filters = value;

                if (value != null)
                {
                    //this._isLoaded = true;

                    if (value.ContainsKey("Filter1"))
                    {
                        string name = value["Filter1"];
                        //ListViewItem li = this.lstFilter1.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter1.FindItemWithText(name) != null)
                        {
                            this.lstFilter1.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter2"))
                    {
                        string name = value["Filter2"];
                        //ListViewItem li = this.lstFilter2.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter2.FindItemWithText(name) != null)
                        {
                            this.lstFilter2.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter3"))
                    {
                        string name = value["Filter3"];
                        //ListViewItem li = this.lstFilter3.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter3.FindItemWithText(name) != null)
                        {
                            this.lstFilter3.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter4"))
                    {
                        string name = value["Filter4"];
                        //ListViewItem li = this.lstFilter4.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter4.FindItemWithText(name) != null)
                        {
                            this.lstFilter4.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter5"))
                    {
                        string name = value["Filter5"];
                        //ListViewItem li = this.lstFilter5.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter5.FindItemWithText(name) != null)
                        {
                            this.lstFilter5.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter6"))
                    {
                        string name = value["Filter6"];
                        //ListViewItem li = this.lstFilter6.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter6.FindItemWithText(name) != null)
                        {
                            this.lstFilter6.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter7"))
                    {
                        string name = value["Filter7"];
                        //ListViewItem li = this.lstFilter7.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter7.FindItemWithText(name) != null)
                        {
                            this.lstFilter7.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter8"))
                    {
                        string name = value["Filter8"];
                        //ListViewItem li = this.lstFilter8.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter8.FindItemWithText(name) != null)
                        {
                            this.lstFilter8.FindItemWithText(name).Selected = true;
                        }
                    }

                    if (value.ContainsKey("Filter9"))
                    {
                        string name = value["Filter9"];
                        //ListViewItem li = this.lstFilter9.FindItemWithText(name);
                        //if (li != null) {
                        //    li.Selected = true;
                        //}
                        if (this.lstFilter9.FindItemWithText(name) != null)
                        {
                            this.lstFilter9.FindItemWithText(name).Selected = true;
                        }
                    }

                    this.FillComponentIdField(this.Type);
                    //this._isLoaded = false;
                }
            }
        }

        public List<string> SelectedSpaces { get; set; }

        public Enumerations.EnergyType Type { get; set; }

        private int TypeId { get; set; }

        #endregion Properties

        #region Internal events

        private void frmEquipmentAudit_Load(object sender, EventArgs e)
        {
            this.InitializeForm(this.TypeId);

            //this._isLoaded = true;
        }

        #region Filter events

        private bool ValidateFilter()
        {
            string msg = "";

            if (String.IsNullOrWhiteSpace(this.txtComponentId.Text))
            {
                msg = "Please select a filter with a valid Component ID.";
            }

            if (msg == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(msg, "Invalid Component Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        #region Filter select events

        private void SetFilter1Helper(object sender)
        {
            //if (this._isLoaded) {
            Cursor.Current = Cursors.WaitCursor;

            ListViewEx list = sender as ListViewEx;

            if (list != null)
            {
                // The type changed.  Reset the form.
                if (list.SelectedItems.Count > 0)
                {
                    SystemTypeLookup s = list.SelectedItems[0].Tag as SystemTypeLookup;

                    if (s != null)
                    {
                        if (Enum.IsDefined(typeof(Enumerations.EnergyType), s.Name))
                        {
                            Enumerations.EnergyType newType = (Enumerations.EnergyType)Enum.Parse(typeof(Enumerations.EnergyType), s.Name);
                            if (newType != this.Type)
                            {
                                this.Type = newType;
                                this.ResetForm();
                            }
                        }
                    }
                }

                this.RunSelect(sender, this.lstFilter2);
            }

            Cursor.Current = Cursors.Default;

            //}
        }

        private void lstFilter1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.SetFilter1Helper(sender);
        }

        private void lstFilter1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SetFilter1Helper(sender);
        }

        private void lstFilter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter3);
        }

        private void lstFilter3_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter4);
        }

        private void lstFilter4_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter5);
        }

        private void lstFilter5_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter6);
        }

        private void lstFilter6_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter7);
        }

        private void lstFilter7_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter8);
        }

        private void lstFilter8_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, this.lstFilter9);
        }

        private void lstFilter9_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RunSelect(sender, null);
        }

        #endregion Filter select events

        #endregion Filter events

        #region Equipment panel events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.EquipmentObject = null;

            this.picEquipment.Image = null;
            this.txtImageFile.ResetText();

            this.Close();
        }

        private void btnMediaCapture_Click(object sender, EventArgs e)
        {
            string filePath = DataStore.XmlEquipmentPicturePath;
            string componentid = "";

            if (this.EquipmentObject != null)
            {
                componentid = this.EquipmentObject.ComponentId;
            }
            else
            {
                componentid = txtComponentId.Text;
            }

            if (this.SyncObject(Enumerations.SyncDirection.ControlsToSelectedObject))
            {
                btnMediaCapture.Enabled = false;

                try
                {
                    using (MultiMedia.frmImageCapture frm = new MultiMedia.frmImageCapture())
                    {
                        string imageFile = "";

                        var result = frm.ShowDialog(filePath, this.AuditId, componentid);

                        if (result == DialogResult.OK)
                        {
                            imageFile = frm.PictureFilePath;
                        }

                        if (File.Exists(imageFile))
                        {
                            try
                            {
                                this.picEquipment.SizeMode = PictureBoxSizeMode.Zoom;
                                this.picEquipment.Image = new Bitmap(imageFile);
                                txtImageFile.Text = imageFile;
                                this.btnDeleteImage.Enabled = true;
                                this.btnSave.Enabled = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                        else
                        {
                            this.btnDeleteImage.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var cause = ExceptionHelper.Innermost(ex);
                    Lg.FatalError(ex, "ImageCapture");
                    Lg.FatalError(cause, "Cause");
                    Lg.FailedFinish("ClipBoard");

                    MessageBox.Show(cause.Message, "Fatal Error: Error Loading Image Capture form.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btnMediaCapture.Enabled = true;
            }
            else
            {
                throw new ApplicationException("Unable to generate object for recommendation.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool okToGenerateRecommendations = true;

            if (this.ValidateData())
            {
                bool doSave = true;

                if (this.Type == Enumerations.EnergyType.Lighting)
                {
                    string msg = "";

                    if (this.GetSchedule().GetTotalAnnualHours() == 0)
                    {
                        msg += "You have not set a schedule for this lighting item.  This will result in zero savings for all recommendations.\n\n";
                    }

                    if (this.cboTechControlType.SelectedIndex == -1)
                    {
                        msg += "You have not selected a Control Type for this lighting item.  This will result in no recommendations generated for this item.\n\n";
                    }

                    if (msg != "")
                    {
                        okToGenerateRecommendations = false;

                        DialogResult result = MessageBox.Show(msg + "Do you wish to continue?", "Incomplete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (result == System.Windows.Forms.DialogResult.No)
                        {
                            doSave = false;
                        }
                    }
                }

                if (doSave)
                {
                    string componentId = (this.EquipmentObject == null ? this.txtComponentId.Text : this.EquipmentObject.ComponentId);
                    Audit audit = DataStore.GetAudit(this.AuditId);
                    //audit.UpdateComponentImage(componentId, this.txtImageFile.Text, true);
                    audit.UpdateComponentImageForBuilding(componentId, this.txtImageFile.Text, true, this.SelectedBuilding);

                    if (this._deleteImageOnExit)
                    {
                        BLL.CommonUtilities.DeleteImageFromComponent(this.EquipmentObject, audit, this.SelectedBuilding, false);
                    }

                    if (this.SyncObject(Enumerations.SyncDirection.ControlsToSelectedObject))
                    {
                        if (this.EquipmentObject != null)
                        {
                            this.EquipmentObject.OkToGenerateRecommendations = okToGenerateRecommendations;
                        }

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        throw new ApplicationException("Unable to generate object for recommendation.");
                    }
                }
            }
        }

        private bool ValidateData()
        {
            string msg = "";

            if (this.numQuantity.Value <= 0)
            {
                msg = "Quantity must be greater than zero.";
                this.numQuantity.Focus();
            }
            else if (this._isNew && this.DoesMainFormContainEquipment(this.txtName.Text))
            {
                msg = "There is already a piece of equipment with the name\n'" + this.txtName.Text + "'.\nPlease enter a unique equipment name.";
                this.txtName.Focus();
                this.txtName.SelectAll();
            }

            //if (this.cboTechEfficiency.Text == "AFUE" || this.cboTechEfficiency.Text == "COP") {
            //    double d = 0.0;
            //    if (Double.TryParse(this.txtTechEfficiency.Text, out d)) {
            //        if (d > 1) {
            //            msg = "The Efficiency value must be less than 1 when using Efficiency Types 'AFUE' or 'COP'.";
            //            this.txtTechEfficiency.Focus();
            //        }
            //    }

            //}

            if (msg == "")
            {
                return true;
            }
            else
            {
                MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void cboTechControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.SuspendLayout();
            this.cboTechControlSubType.SuspendLayout();

            if (this.cboTechControlSubType.Items != null)
            {
                this.cboTechControlSubType.Items.Clear();
            }

            if (this.cboTechControlType.SelectedItem != null)
            {
                ListViewItem li = this.cboTechControlType.SelectedItem as ListViewItem;

                if (li != null)
                {
                    ControlTypeLookup c = li.Tag as ControlTypeLookup;

                    if (c != null)
                    {
                        switch (c.Name)
                        {
                            case "Switch":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Light Switch") { Tag = c.Factor });
                                break;

                            case "Occupancy":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Occupancy Sensor") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Time Clock") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Energy Management System") { Tag = c.Factor });
                                break;

                            case "Daylighting":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Photosensors") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Time Clock") { Tag = c.Factor });
                                break;

                            case "Personal Tuning":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Dimmers") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Wireless on-off switches") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Bi-level Switches") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Computer based controls") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Pre-set scene selection") { Tag = c.Factor });
                                break;

                            case "Institutional Tuning":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Dimmable Ballasts") { Tag = c.Factor });
                                this.cboTechControlSubType.Items.Add(new ListViewItem("On-off or dimmer switches for non personal tuning") { Tag = c.Factor });
                                break;

                            case "Multiple Types":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Occupancy and personal tuning/daylighting and occupancy") { Tag = c.Factor });
                                break;

                            case "Unknown":
                                this.cboTechControlSubType.Items.Add(new ListViewItem("Unknown") { Tag = c.Factor });
                                break;
                        }

                        if (this.cboTechControlSubType.Items.Count > 0)
                        {
                            this.cboTechControlSubType.SelectedIndex = 0;
                        }
                    }
                }
            }

            this.cboTechControlSubType.ResumeLayout();
            this.ResumeLayout();

            Cursor.Current = Cursors.Default;
        }

        #region Increment/Decrement button events

        private void btnDecrement_Click(object sender, EventArgs e)
        {
            this.numQuantity.DecreaseValue();
        }

        private void btnIncrement_Click(object sender, EventArgs e)
        {
            this.numQuantity.IncreaseValue();
        }

        #endregion Increment/Decrement button events

        private void txtEquipmentPanelName_TextChanged(object sender, EventArgs e)
        {
            this.grpTech.Text = this.txtName.Text;
            this.grpSpace.Text = this.txtName.Text;
            this.grpSchedule.Text = this.txtName.Text;
        }

        #endregion Equipment panel events

        #endregion Internal events

        #region Equipment panel helper methods

        private void AdjustFilterColumnWidths()
        {
            //this.colFilter1.Width = -1;
            //this.colFilter2.Width = -1;
            //this.colFilter3.Width = -1;
            //this.colFilter4.Width = -1;
            //this.colFilter5.Width = -1;
            //this.colFilter6.Width = -1;
            //this.colFilter7.Width = -1;
            //this.colFilter8.Width = -1;
            //this.colFilter9.Width = -1;

            this.colFilter1.Width = this.lstFilter1.Width - 1;
            this.colFilter2.Width = this.lstFilter2.Width - 1;
            this.colFilter3.Width = this.lstFilter3.Width - 1;
            this.colFilter4.Width = this.lstFilter4.Width - 1;
            this.colFilter5.Width = this.lstFilter5.Width - 1;
            this.colFilter6.Width = this.lstFilter6.Width - 1;
            this.colFilter7.Width = this.lstFilter7.Width - 1;
            this.colFilter8.Width = this.lstFilter8.Width - 1;
            this.colFilter9.Width = this.lstFilter9.Width - 1;
        }

        private void ClearDetailFields()
        {
            this.txtName.Clear();
            this.txtMfgName.Clear();
            this.txtMfgModel.Clear();
            this.txtBurnerMfgName.Clear();
            this.txtBurnerMfgModel.Clear();
            this.txtBallastMfgName.Clear();
            this.txtBallastMfgModel.Clear();

            this.txtTechSize.Clear();
            this.cboTechSize.SelectedIndex = -1;
            this.txtTechEfficiency.Clear();
            this.cboTechEfficiency.SelectedIndex = -1;
            this.txtTechPartLoadEfficiency.Clear();
            this.cboTechPartLoadEfficiency.SelectedIndex = -1;
            this.cboTechSystemControlType.SelectedIndex = -1;
            this.cboTechSupplementalControlType.SelectedIndex = -1;
            this.cboTechBurnerType.SelectedIndex = -1;
            this.cboTechBurnerControlType.SelectedIndex = -1;
            this.cboTechCompressorType.SelectedIndex = -1;
            this.cboTechEconomizer.SelectedIndex = -1;
            this.cboTechControlType.SelectedIndex = -1;
            this.cboTechControlSubType.SelectedIndex = -1;
            this.txtTechCapacity.Clear();
            this.numTechControlQuantity.ResetText();

            for (int i = 0; i < this.lstSpaceSpecific.Items.Count; i++)
            {
                this.lstSpaceSpecific.Items[i].Selected = false;
            }

            //this.txtSpaceLabel.Clear();
            //this.cboSpaceLHC.SelectedIndex = -1;

            this.cboSchedulePresets.SelectedIndex = -1;
            this.ResetSchedule();
        }

        private void ClearThisAndHigherLists(int selectedListIndex)
        {
            for (int i = 9; i >= selectedListIndex; i--)
            {
                string key = String.Format("lstFilter{0}", i);

                ((ListViewEx)this.pnlMain.Controls[key]).Items.Clear();
            }
        }

        private void FillListControl(ListViewEx listControl, IEnumerable<SystemTypeLookup> data)
        {
            if (listControl != null && data != null)
            {
                string name = listControl.Name;
                int len = name.Length;
                int i = int.Parse(name.Substring(len - 1, 1));

                this.ClearThisAndHigherLists(i);

                foreach (SystemTypeLookup d in data)
                {
                    ListViewItem li = new ListViewItem(d.Name) { Tag = d, Name = d.Name };
                    listControl.Items.Add(li);
                }
            }
        }

        private void FillSystemTypeFilter()
        {
            List<SystemTypeLookup> items = DataStore.GetSystemTypesByParentId(0, "");

            if (this.lstFilter1.Items != null)
            {
                this.lstFilter1.Items.Clear();
            }

            foreach (SystemTypeLookup item in items)
            {
                ListViewItem li = new ListViewItem(item.Name);
                li.Tag = item;
                this.lstFilter1.Items.Add(li);
            }
        }

        private string GetDefaultName(string componentId)
        {
            string newName = "";

            if (!String.IsNullOrWhiteSpace(componentId))
            {
                string componentName = DataStore.GetComponentName(componentId, this.Type);
                string spaceName = "";

                if (this.lstSpaceSpecific.SelectedItems.Count > 0)
                {
                    spaceName = " - " + this.lstSpaceSpecific.SelectedItems[0].Text;
                }

                newName = componentName + spaceName;

                string newNameWithIndex = newName;

                int i = 1;

                while (this.DoesMainFormContainEquipment(newNameWithIndex))
                {
                    newNameWithIndex = newName + i.ToString().PadLeft(2, '0');
                    i++;
                }
            }

            return newName;
        }

        private bool DoesMainFormContainEquipment(string newName)
        {
            bool result = false;

            if (!String.IsNullOrWhiteSpace(newName) && this._mainFormEquipmentList != null)
            {
                foreach (string s in this._mainFormEquipmentList)
                {
                    if (s.Replace(" ", "").ToUpper() == newName.Replace(" ", "").ToUpper())
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        private SystemTypeLookup GetSelectedItemFromList(object listControl)
        {
            SystemTypeLookup result = null;

            if (listControl != null)
            {
                ListViewEx list = listControl as ListViewEx;

                if (list != null)
                {
                    if (list.SelectedItems != null && list.SelectedItems.Count > 0)
                    {
                        ListViewItem li = list.SelectedItems[0];

                        if (li != null)
                        {
                            SystemTypeLookup s = li.Tag as SystemTypeLookup;

                            if (s != null)
                            {
                                result = s;
                            }
                        }
                    }
                }
            }

            return result;
        }

        private List<string> GetTypesAsStringBreadcrumb()
        {
            List<string> result = new List<string>();

            if (this.lstFilter1.SelectedIndices.Count > 0)
            {
                result.Add(this.lstFilter1.SelectedItems[0].Text);

                if (this.lstFilter2.SelectedIndices.Count > 0)
                {
                    result.Add(this.lstFilter2.SelectedItems[0].Text);

                    if (this.lstFilter3.SelectedIndices.Count > 0)
                    {
                        result.Add(this.lstFilter3.SelectedItems[0].Text);

                        if (this.lstFilter4.SelectedIndices.Count > 0)
                        {
                            result.Add(this.lstFilter4.SelectedItems[0].Text);

                            if (this.lstFilter5.SelectedIndices.Count > 0)
                            {
                                result.Add(this.lstFilter5.SelectedItems[0].Text);

                                if (this.lstFilter6.SelectedIndices.Count > 0)
                                {
                                    result.Add(this.lstFilter6.SelectedItems[0].Text);

                                    if (this.lstFilter7.SelectedIndices.Count > 0)
                                    {
                                        result.Add(this.lstFilter7.SelectedItems[0].Text);

                                        if (this.lstFilter8.SelectedIndices.Count > 0)
                                        {
                                            result.Add(this.lstFilter8.SelectedItems[0].Text);

                                            if (this.lstFilter9.SelectedIndices.Count > 0)
                                            {
                                                result.Add(this.lstFilter9.SelectedItems[0].Text);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private LinkedList<SystemTypeLookup> GetSystemTypeBreadcrumb()
        {
            LinkedList<SystemTypeLookup> result = new LinkedList<SystemTypeLookup>();

            SystemTypeLookup s1 = this.GetSelectedItemFromList(this.lstFilter1);

            if (s1 != null)
            {
                result.AddFirst(
                    new LinkedListNode<SystemTypeLookup>(s1));

                SystemTypeLookup s2 = this.GetSelectedItemFromList(this.lstFilter2);

                if (s2 != null)
                {
                    result.AddLast(new LinkedListNode<SystemTypeLookup>(s2));

                    SystemTypeLookup s3 = this.GetSelectedItemFromList(this.lstFilter3);

                    if (s3 != null)
                    {
                        result.AddLast(
                            new LinkedListNode<SystemTypeLookup>(s3));

                        SystemTypeLookup s4 = this.GetSelectedItemFromList(this.lstFilter4);

                        if (s4 != null)
                        {
                            result.AddLast(
                                new LinkedListNode<SystemTypeLookup>(s4));

                            SystemTypeLookup s5 = this.GetSelectedItemFromList(this.lstFilter5);

                            if (s5 != null)
                            {
                                result.AddLast(
                                    new LinkedListNode<SystemTypeLookup>(s5));

                                SystemTypeLookup s6 = this.GetSelectedItemFromList(this.lstFilter6);

                                if (s6 != null)
                                {
                                    result.AddLast(
                                        new LinkedListNode<SystemTypeLookup>(s6));

                                    SystemTypeLookup s7 = this.GetSelectedItemFromList(this.lstFilter7);

                                    if (s7 != null)
                                    {
                                        result.AddLast(
                                            new LinkedListNode<SystemTypeLookup>(s7));

                                        SystemTypeLookup s8 = this.GetSelectedItemFromList(this.lstFilter8);

                                        if (s8 != null)
                                        {
                                            result.AddLast(
                                                new LinkedListNode<SystemTypeLookup>(s8));

                                            SystemTypeLookup s9 = this.GetSelectedItemFromList(this.lstFilter9);
                                            if (s9 != null)
                                            {
                                                result.AddLast(
                                                    new LinkedListNode<SystemTypeLookup>(s9));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private void InitializeEquipmentPanel()
        {
            #region Set control visibility on all tabs

            switch (this.Type)
            {
                case Enumerations.EnergyType.Heating:
                    this.SetEquipmentPanelNameplateControlsVisibility(true, true, true, true, true, false, false);
                    this.SetEquipmentPanelTechnicalSpecsControlsVisibility(true, true, false, true, true, true, true, false, false, false, false, false, false);
                    this.SetEquipmentPanelSpaceControlsVisibility(true, false);
                    break;

                case Enumerations.EnergyType.Cooling:
                    this.SetEquipmentPanelNameplateControlsVisibility(true, true, true, false, false, false, false);
                    this.SetEquipmentPanelTechnicalSpecsControlsVisibility(true, true, true, true, true, false, false, true, true, false, false, false, false);
                    this.SetEquipmentPanelSpaceControlsVisibility(true, false);
                    break;

                case Enumerations.EnergyType.Lighting:
                    this.SetEquipmentPanelNameplateControlsVisibility(true, true, true, false, false, true, true);
                    this.SetEquipmentPanelTechnicalSpecsControlsVisibility(false, false, false, false, false, false, false, false, false, true, true, true, false);
                    this.SetEquipmentPanelSpaceControlsVisibility(true, true);
                    break;

                case Enumerations.EnergyType.DomesticWaterHeating:
                    this.SetEquipmentPanelNameplateControlsVisibility(true, true, true, false, false, false, false);
                    this.SetEquipmentPanelTechnicalSpecsControlsVisibility(true, true, false, true, false, false, false, false, false, false, false, false, true);
                    this.SetEquipmentPanelSpaceControlsVisibility(true, false);
                    break;

                default:
                    this.SetEquipmentPanelNameplateControlsVisibility(true, true, true, true, true, true, true);
                    this.SetEquipmentPanelTechnicalSpecsControlsVisibility(true, true, true, true, true, true, true, true, true, true, true, true, true);
                    break;
            }

            #endregion Set control visibility on all tabs

            //TODO: This section needs to be generated dynamically.
            //this.lstSpaceGeneral.Items.Clear();
            //this.lstSpaceGeneral.Items.Add("% of Cond Space");
            //this.lstSpaceGeneral.Items.Add("Break Room");
            //this.lstSpaceGeneral.Items.Add("Whole Room");

            this.lstSpaceSpecific.Items.Clear();

            ListViewGroup grp = this.lstSpaceSpecific.Groups["lvgGeneral"];
            if (grp == null)
            {
                grp = new ListViewGroup("lvgGeneral", "General");
            }

            this.lstSpaceSpecific.Items.Add(new ListViewItem("Exterior", grp));
            this.lstSpaceSpecific.Items.Add(new ListViewItem("Whole Building", grp));

            grp = this.lstSpaceSpecific.Groups["lvgSpecific"];
            if (grp == null)
            {
                grp = new ListViewGroup("lvgSpecific", "Specific");
            }

            if (this.SelectedSpaces != null)
            {
                this.SelectedSpaces.Sort();

                foreach (string space in this.SelectedSpaces)
                {
                    this.lstSpaceSpecific.Items.Add(new ListViewItem(space, grp));
                }

                if (this.lstSpaceSpecific.Items != null)
                {
                    if (this.EquipmentObject != null && this.EquipmentObject.Spaces != null && this.EquipmentObject.Spaces.Count > 0)
                    {
                        if (this.lstSpaceSpecific.FindItemWithText(this.EquipmentObject.Spaces[0].Space) != null)
                        {
                            this.lstSpaceSpecific.FindItemWithText(this.EquipmentObject.Spaces[0].Space).Selected = true;
                        }
                        else
                        {
                            this.lstSpaceSpecific.FindItemWithText("Whole Building").Selected = true;
                        }
                    }
                    else
                    {
                        this.lstSpaceSpecific.FindItemWithText("Whole Building").Selected = true;
                    }
                }
            }

            this.LoadEquipmentPanelDropDowns();

            this.colSpaceSpecific.Width = this.lstSpaceSpecific.Width - 1;

            this.tabEquipmentDetails.SelectedTabIndex = 0;
        }

        private void InitializeFilters()
        {
            this.FillSystemTypeFilter();
            this.SetFilterListsVisibility();
            this.AdjustFilterColumnWidths();

            //List<SystemTypeLookup> systemTypes = DataStore.GetSystemTypesByParentId(this.TypeId, this.Type.ToString());
            // this.FillListControl(this.lstFilter2, systemTypes);
            this.SelectSystemType();
        }

        private void InitializeForm(int typeId)
        {
            this.Text = this.Type.ToString();

            try
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    this.lblVersion.Text = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                else
                {
                    this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
            }
            catch
            {
                this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

            //this.FillSystemTypeFilter();
            //this.SetFilterListsVisibility();
            //this.AdjustFilterColumnWidths();
            //this.SelectSystemType();
            //this.InitializeGlobalDropdowns();
            //this.InitializeEquipmentPanel();
            //this.schSchedule.SetColors();
            //this.SetScheduleColors();
            //this.InitializeScheduleTab();

            this.InitializeFilters();
            this.InitializeGlobalDropdowns();
            this.InitializeEquipmentPanel();
            //this.schSchedule.SetColors();
            this.SetScheduleColors();

            //this.InitializeSpaceList();
            this.InitializeScheduleTab();

            //this.SetFormValues();
            this.SyncObject(Enumerations.SyncDirection.SelectedObjectToControls);

            if (!string.IsNullOrEmpty(txtImageFile.Text))
            {
                this.picEquipment.SizeMode = PictureBoxSizeMode.Zoom;
                this.picEquipment.Image = new Bitmap(txtImageFile.Text);
            }

            this.btnSave.Enabled = !String.IsNullOrWhiteSpace(this.txtComponentId.Text);
            this.btnDeleteImage.Enabled = File.Exists(this.txtImageFile.Text);
        }

        private void SetFormValues()
        {
            if (this.EquipmentObject != null)
            {
                this.cboTechSize.Text = this.EquipmentObject.SizeUnit;
                this.cboTechEfficiency.Text = this.EquipmentObject.EfficiencyUnit;
                this.cboTechPartLoadEfficiency.Text = this.EquipmentObject.PartLoadEfficiencyUnit;
                this.cboTechSupplementalControlType.Text = this.EquipmentObject.SupplementalControlType;
            }
        }

        private void InitializeScheduleTab()
        {
            if (this.DefaultSchedules != null)
            {
                this.cboSchedulePresets.Items.Clear();

                foreach (Schedule s in this.DefaultSchedules)
                {
                    ListViewItem li = new ListViewItem(s.ScheduleDescription);
                    li.Tag = s;
                    this.cboSchedulePresets.Items.Add(li);
                }
            }

            //if (this.EquipmentObject != null && this.EquipmentObject.EquipmentSchedule != null) {
            //    this.SetScheduleControl(this.EquipmentObject.EquipmentSchedule);
            //}
            //else {
            //    this.ResetSchedule();
            //}
        }

        private void FillSystemControlTypeDropdown()
        {
            this.cboTechSystemControlType.SuspendLayout();
            this.cboTechSystemControlType.Items.Clear();

            this.cboTechSystemControlType.Items.Add("Programmable Thermostat");
            this.cboTechSystemControlType.Items.Add("Manual Analog Thermostat");
            this.cboTechSystemControlType.Items.Add("Manual Digital Thermostat");
            this.cboTechSystemControlType.Items.Add("Guest Room Energy Management");
            this.cboTechSystemControlType.Items.Add("On/Off");
            this.cboTechSystemControlType.Items.Add("EMCS");
            this.cboTechSystemControlType.Items.Add("Always on");
            this.cboTechSystemControlType.Items.Add("Other / Combination");
            this.cboTechSystemControlType.Items.Add("Unknown");

            this.cboTechSystemControlType.ResumeLayout();
        }

        private void InitializeGlobalDropdowns()
        {
            this.SuspendLayout();
            this.cboTechSupplementalControlType.SuspendLayout();
            this.cboTechBurnerType.SuspendLayout();
            this.cboTechBurnerControlType.SuspendLayout();
            this.cboTechCompressorType.SuspendLayout();
            this.cboTechEconomizer.SuspendLayout();
            this.cboTechControlType.SuspendLayout();
            this.cboTechPartLoadEfficiency.SuspendLayout();

            this.cboTechSupplementalControlType.Items.Clear();
            this.cboTechBurnerType.Items.Clear();
            this.cboTechBurnerControlType.Items.Clear();
            this.cboTechCompressorType.Items.Clear();
            this.cboTechEconomizer.Items.Clear();
            this.cboTechControlType.Items.Clear();
            this.cboTechPartLoadEfficiency.Items.Clear();

            this.FillSystemControlTypeDropdown();

            this.cboTechSupplementalControlType.Items.Add("Hot Water Reset");
            this.cboTechSupplementalControlType.Items.Add("Hot Water Reset with Outside Air Cutout");
            this.cboTechSupplementalControlType.Items.Add("Chilled Water Reset");
            this.cboTechSupplementalControlType.Items.Add("Chiller Optimization");
            this.cboTechSupplementalControlType.Items.Add("Not Applicable");

            this.cboTechBurnerType.Items.Add("Atmospheric");
            this.cboTechBurnerType.Items.Add("Power");
            this.cboTechBurnerType.Items.Add("Sealed combustion");
            this.cboTechBurnerType.Items.Add("Rotary cup");
            this.cboTechBurnerType.Items.Add("Not Applicable");
            this.cboTechBurnerType.Items.Add("Unknown");

            this.cboTechBurnerControlType.Items.Add("Linkage");
            this.cboTechBurnerControlType.Items.Add("Linkageless");
            this.cboTechBurnerControlType.Items.Add("O2 Trim");
            this.cboTechBurnerControlType.Items.Add("Not Applicable");
            this.cboTechBurnerControlType.Items.Add("Unknown");

            this.cboTechCompressorType.Items.Add("Reciprocating");
            this.cboTechCompressorType.Items.Add("Screw");
            this.cboTechCompressorType.Items.Add("Scroll");
            this.cboTechCompressorType.Items.Add("Centrifugal");
            this.cboTechCompressorType.Items.Add("Not Applicable");
            this.cboTechCompressorType.Items.Add("Unknown");

            this.cboTechEconomizer.Items.Add("Dry bulb temperature");
            this.cboTechEconomizer.Items.Add("Enthalpy");
            this.cboTechEconomizer.Items.Add("Demand controlled ventilation");
            this.cboTechEconomizer.Items.Add("Not Applicable");
            this.cboTechEconomizer.Items.Add("Unknown");

            this.cboTechControlType.Items.Add(new ListViewItem("Switch") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.Switch) });
            this.cboTechControlType.Items.Add(new ListViewItem("Occupancy") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.Occupancy) });
            this.cboTechControlType.Items.Add(new ListViewItem("Daylighting") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.Daylighting) });
            this.cboTechControlType.Items.Add(new ListViewItem("Personal Tuning") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.PersonalTuning) });
            this.cboTechControlType.Items.Add(new ListViewItem("Institutional Tuning") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.InstitutionalTuning) });
            this.cboTechControlType.Items.Add(new ListViewItem("Multiple Types") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.MultipleTypes) });
            this.cboTechControlType.Items.Add(new ListViewItem("Unknown") { Tag = ControlTypeLookupFactory.Create(Enumerations.ControlTypeName.Unknown) });

            this.cboTechPartLoadEfficiency.Items.Add("COP");
            this.cboTechPartLoadEfficiency.Items.Add("EER");
            this.cboTechPartLoadEfficiency.Items.Add("IPLV");
            this.cboTechPartLoadEfficiency.Items.Add("kW/Ton");
            this.cboTechPartLoadEfficiency.Items.Add("SEER");
            this.cboTechPartLoadEfficiency.Items.Add("HSPF");

            this.cboTechPartLoadEfficiency.ResumeLayout();
            this.cboTechControlType.ResumeLayout();
            this.cboTechEconomizer.ResumeLayout();
            this.cboTechCompressorType.ResumeLayout();
            this.cboTechBurnerControlType.ResumeLayout();
            this.cboTechBurnerType.ResumeLayout();
            this.cboTechSupplementalControlType.ResumeLayout();
            this.ResumeLayout();
        }

        private void LoadEquipmentPanelDropDowns()
        {
            // Nameplate tab.
            // None

            // Technical Specs tab.
            this.cboTechSize.Items.Clear();
            this.cboTechEfficiency.Items.Clear();
            //this.cboTechPartLoadEfficiency.Items.Clear();
            //this.cboTechControlType.Items.Clear();
            //this.cboTechControlSubType.Items.Clear();

            switch (this.Type)
            {
                case Enumerations.EnergyType.Heating:

                    this.cboTechSize.Items.Add("BoilerHP");
                    this.cboTechSize.Items.Add("kBtu");
                    this.cboTechSize.Items.Add("kW");
                    this.cboTechSize.Items.Add("Watts");

                    this.cboTechEfficiency.Items.Add("AFUE");
                    this.cboTechEfficiency.Items.Add("Combustion Efficiency");
                    this.cboTechEfficiency.Items.Add("COP");
                    this.cboTechEfficiency.Items.Add("HSPF");
                    this.cboTechEfficiency.Items.Add("Thermal Efficiency");

                    break;

                case Enumerations.EnergyType.Cooling:

                    this.cboTechSize.Items.Add("kBtu");
                    this.cboTechSize.Items.Add("Tons");

                    this.cboTechEfficiency.Items.Add("COP");
                    this.cboTechEfficiency.Items.Add("EER");
                    this.cboTechEfficiency.Items.Add("HSPF");
                    this.cboTechEfficiency.Items.Add("SEER");
                    this.cboTechEfficiency.Items.Add("kW/Ton");

                    break;

                case Enumerations.EnergyType.Lighting:

                    //this.cboTechControlType.Items.Add("Switch");
                    //this.cboTechControlType.Items.Add("Occupancy");
                    //this.cboTechControlType.Items.Add("Daylighting");
                    //this.cboTechControlType.Items.Add("Personal Tuning");
                    //this.cboTechControlType.Items.Add("Institutional Tuning");
                    //this.cboTechControlType.Items.Add("Multiple Types");
                    //this.cboTechControlType.Items.Add("Unknown");

                    break;

                case Enumerations.EnergyType.DomesticWaterHeating:

                    this.cboTechSize.Items.Add("kBtu");
                    this.cboTechSize.Items.Add("Watts");

                    this.cboTechEfficiency.Items.Add("Energy Factor");
                    this.cboTechEfficiency.Items.Add("Thermal Efficiency");

                    break;

                    //default:

                    //    this.cboTechSize.Items.Add("BoilerHP");
                    //    this.cboTechSize.Items.Add("kBtu");
                    //    this.cboTechSize.Items.Add("Watts");
                    //    this.cboTechSize.Items.Add("kW");

                    //    this.cboTechEfficiency.Items.Add("AFUE");
                    //    this.cboTechEfficiency.Items.Add("Thermal Efficiency");
                    //    this.cboTechEfficiency.Items.Add("Combustion Efficiency");
                    //    this.cboTechEfficiency.Items.Add("COP");
                    //    this.cboTechEfficiency.Items.Add("HSPF");

                    //    this.cboTechPartLoadEfficiency.Items.Add("AFUE");
                    //    this.cboTechPartLoadEfficiency.Items.Add("Thermal Efficiency");
                    //    this.cboTechPartLoadEfficiency.Items.Add("Combustion Efficiency");
                    //    this.cboTechPartLoadEfficiency.Items.Add("COP");
                    //    this.cboTechPartLoadEfficiency.Items.Add("HSPF");

                    //    break;
            }

            // Spaces tab.

            // Schedule tab.
            // None
        }

        private void ResetForm()
        {
            this.SuspendLayout();
            this.tabEquipmentDetails.SuspendLayout();

            this.Text = this.Type.ToString();
            this.SetFilterListsVisibility();
            this.ClearDetailFields();
            this.InitializeEquipmentPanel();
            this.InitializeGlobalDropdowns();

            this.numQuantity.ResetText();

            this.tabEquipmentDetails.ResumeLayout();
            this.ResumeLayout();
        }

        private bool OkToGetComponentId()
        {
            bool result = true;

            if (this.Type == Enumerations.EnergyType.Lighting)
            {
                if (this.GetSystemTypeBreadcrumb().Count <= 1)
                {
                    this.highlighter1.SetHighlightColor(this.txtComponentId, eHighlightColor.None);
                    this.txtComponentId.FocusHighlightEnabled = false;
                    this.tipMain.RemoveAll();
                    //this.txtComponentId.Text = componentId;
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.txtTechSize.Text) && String.IsNullOrWhiteSpace(this.txtTechEfficiency.Text))
                {
                    result = false;
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(this.txtTechSize.Text))
                    {
                        result = this.cboTechSize.SelectedIndex >= 0;
                    }

                    if (result && !String.IsNullOrWhiteSpace(this.txtTechEfficiency.Text))
                    {
                        result = this.cboTechEfficiency.SelectedIndex >= 0;
                    }
                }
            }

            return result;
        }

        private void FillComponentIdField(Enumerations.EnergyType type)
        {
            this.txtComponentId.Text = "";

            // If Efficiency has a value, then only fire this if the Efficiency Unit is also selected.  Ditto with the Size.
            if (this.OkToGetComponentId())
            {
                List<string> types = this.GetTypesAsStringBreadcrumb();

                double efficiency = 0.0;
                string efficiencyCop = "";

                if (!String.IsNullOrWhiteSpace(this.txtTechEfficiency.Text))
                {
                    if (double.TryParse(this.txtTechEfficiency.Text, out efficiency))
                    {
                        double d = BLL.CommonUtilities.ConvertByUnit(efficiency, this.cboTechEfficiency.Text, "COP");
                        efficiencyCop = d.ToString();
                    }
                }

                double size = 0.0;
                string sizeBtu = "";

                if (!String.IsNullOrWhiteSpace(this.txtTechSize.Text))
                {
                    if (double.TryParse(this.txtTechSize.Text, out size))
                    {
                        double d = BLL.CommonUtilities.ConvertByUnit(size, this.cboTechSize.Text, "BTU");
                        sizeBtu = d.ToString();
                    }
                }

                string componentId = "";
                string msg = "";
                bool showMsg = true;

                switch (type)
                {
                    case Enumerations.EnergyType.Lighting:
                        if (types.Count == 9)
                        {
                            componentId = DataStore.GetLightingComponentId(types[0], types[1], types[2], types[3], types[4], types[5], types[6], types[7], types[8]);
                        }
                        break;

                    case Enumerations.EnergyType.DomesticWaterHeating:
                        if (types.Count == 3)
                        {
                            componentId = DataStore.GetDomesticWaterHeatingComponentId(types[0], types[1], types[2], efficiencyCop, sizeBtu, out msg);
                        }
                        break;

                    case Enumerations.EnergyType.Heating:
                        if (types.Count == 3)
                        {
                            componentId = DataStore.GetHeatingComponentId(types[0], types[1], types[2], efficiencyCop, sizeBtu, out msg);
                        }

                        break;

                    case Enumerations.EnergyType.Cooling:
                        if (types.Count == 2)
                        {
                            componentId = DataStore.GetCoolingComponentId(types[0], types[1], efficiencyCop, sizeBtu, out msg);
                        }

                        break;

                    default:
                        showMsg = false;
                        break;
                }

                if (String.IsNullOrWhiteSpace(componentId) && String.IsNullOrWhiteSpace(msg))
                {
                    msg = "Component ID not found.  You may need to provide a more specific filter.";
                }

                if (showMsg && String.IsNullOrWhiteSpace(componentId))
                {
                    this.highlighter1.SetHighlightColor(this.txtComponentId, eHighlightColor.Red);
                    this.txtComponentId.FocusHighlightEnabled = true;
                    this.tipMain.SetToolTip(this.txtComponentId, msg);
                    this.txtComponentName.Text = "";
                }
                else
                {
                    this.highlighter1.SetHighlightColor(this.txtComponentId, eHighlightColor.None);
                    this.txtComponentId.FocusHighlightEnabled = false;
                    this.tipMain.RemoveAll();
                    this.txtComponentId.Text = componentId;
                    this.txtComponentName.Text = DataStore.GetComponentName(componentId, type);
                }
            }
        }

        private void RunSelect(object listControl, ListViewEx nextList)
        {
            Cursor.Current = Cursors.WaitCursor;

            SystemTypeLookup s = this.GetSelectedItemFromList(listControl);
            if (s != null)
            {
                this.txtActualWattage.Text = s.ActualWattage;
                if (this.Type == Enumerations.EnergyType.Lighting)
                {
                    this.txtComponentId.Text = s.ComponentId;
                }
                else
                {
                    this.txtComponentId.Text = "";
                }
                //this.FillComponentIdField(Enumerations.EnergyType.Lighting);

                List<SystemTypeLookup> children = DataStore.GetSystemTypesByParentId(s.Id, this.Type.ToString());

                if (children.Count == 0)
                {
                    this.SetEquipmentNameFields();
                }
                else
                {
                    this.FillListControl(nextList, children);

                    this.ScrollToActiveFilter(listControl);
                }

                this.FillComponentIdField(this.Type);
            }

            Cursor.Current = Cursors.Default;
        }

        private void ScrollToActiveFilter(object listControl)
        {
            if (listControl != null)
            {
                ListViewEx list = listControl as ListViewEx;

                if (list != null)
                {
                    int index = int.Parse(list.Name.Substring(list.Name.Length - 1, 1));
                    int pos = (index * list.Width) - (list.Width * 2);

                    if (pos < 0)
                    {
                        pos = 0;
                    }

                    this.pnlMain.AutoScrollPosition = new Point(pos, 3);
                }
            }
        }

        private void SelectSystemType()
        {
            int typeIndex = 0;
            foreach (ListViewItem li in this.lstFilter1.Items)
            {
                if (li.Text.Replace(" ", "") == this.Type.ToString())
                {
                    typeIndex = li.Index;
                    break;
                }
            }

            if (this.lstFilter1.Items.Count > 0)
            {
                this.lstFilter1.Items[typeIndex].Selected = true;
            }
        }

        #region Methods to set Equipment panel controls visibility

        private void SetEquipmentPanelNameplateControlsVisibility(bool nameVisible, bool mfgNameVisible, bool mfgModelVisible,
            bool burnerMfgNameVisible, bool burnerMfgModelVisible, bool ballastMfgNameVisible, bool ballastMfgModelVisible)
        {
            this.lblName.Visible = nameVisible;
            this.txtName.Visible = nameVisible;

            this.lblMfgName.Visible = mfgNameVisible;
            this.txtMfgName.Visible = mfgNameVisible;

            this.lblMfgModel.Visible = mfgModelVisible;
            this.txtMfgModel.Visible = mfgModelVisible;

            this.lblBurnerMfgName.Visible = burnerMfgNameVisible;
            this.txtBurnerMfgName.Visible = burnerMfgNameVisible;

            this.lblBurnerMfgModel.Visible = burnerMfgModelVisible;
            this.txtBurnerMfgModel.Visible = burnerMfgModelVisible;

            this.lblBallastMfgName.Visible = ballastMfgNameVisible;
            this.txtBallastMfgName.Visible = ballastMfgNameVisible;

            this.lblBallastMfgModel.Visible = ballastMfgModelVisible;
            this.txtBallastMfgModel.Visible = ballastMfgModelVisible;
        }

        private void SetEquipmentPanelTechnicalSpecsControlsVisibility(bool sizeVisibility, bool efficiencyVisibility, bool partLoadEfficiencyVisibility,
            bool systemControlTypeVisibility, bool supplementalControlVisibility, bool burnerTypeVisibility,
            bool burnerControlTypeVisibility, bool compressorTypeVisibility, bool waterCoolingControlTypeVisibility, bool controlTypeVisibility,
            bool controlSubTypeVisibility, bool controlQuantityVisible, bool capacityVisibility)
        {
            this.lblTechSize.Visible = sizeVisibility;
            this.txtTechSize.Visible = sizeVisibility;
            this.cboTechSize.Visible = sizeVisibility;

            this.lblTechEfficiency.Visible = efficiencyVisibility;
            this.txtTechEfficiency.Visible = efficiencyVisibility;
            this.cboTechEfficiency.Visible = efficiencyVisibility;

            this.lblTechPartLoadEfficiency.Visible = partLoadEfficiencyVisibility;
            this.txtTechPartLoadEfficiency.Visible = partLoadEfficiencyVisibility;
            this.cboTechPartLoadEfficiency.Visible = partLoadEfficiencyVisibility;

            this.lblTechSystemControlType.Visible = systemControlTypeVisibility;
            this.cboTechSystemControlType.Visible = systemControlTypeVisibility;

            this.lblTechSupplementalControlType.Visible = supplementalControlVisibility;
            this.cboTechSupplementalControlType.Visible = supplementalControlVisibility;

            this.lblTechBurnerType.Visible = burnerTypeVisibility;
            this.cboTechBurnerType.Visible = burnerTypeVisibility;

            this.lblTechBurnerControlType.Visible = burnerControlTypeVisibility;
            this.cboTechBurnerControlType.Visible = burnerControlTypeVisibility;

            this.lblTechCompressorType.Visible = compressorTypeVisibility;
            this.cboTechCompressorType.Visible = compressorTypeVisibility;

            this.lblTechEconomizer.Visible = waterCoolingControlTypeVisibility;
            this.cboTechEconomizer.Visible = waterCoolingControlTypeVisibility;

            this.lblTechControlType.Visible = controlTypeVisibility;
            this.cboTechControlType.Visible = controlTypeVisibility;

            this.lblTechControlSubType.Visible = controlSubTypeVisibility;
            this.cboTechControlSubType.Visible = controlSubTypeVisibility;

            this.lblTechControlQuantity.Visible = controlQuantityVisible;
            this.numTechControlQuantity.Visible = controlQuantityVisible;
            this.pnlTechControlQuantity.Visible = controlQuantityVisible;

            this.lblTechCapacity.Visible = capacityVisibility;
            this.txtTechCapacity.Visible = capacityVisibility;
            this.lblTechCapacityGallons.Visible = capacityVisibility;
        }

        private void SetEquipmentPanelSpaceControlsVisibility(bool spaceLabelVisibility, bool lightingHeatingCoolingTypeVisibility)
        {
            this.lblSpaceLabel.Visible = spaceLabelVisibility;
            this.txtSpaceLabel.Visible = spaceLabelVisibility;

            //this.lblSpaceLHC.Visible = lightingHeatingCoolingTypeVisibility;
            //this.cboSpaceLHC.Visible = lightingHeatingCoolingTypeVisibility;
        }

        #endregion Methods to set Equipment panel controls visibility

        private void SetFilterListsVisibility()
        {
            switch (this.Type)
            {
                case Enumerations.EnergyType.Heating:
                    this.lstFilter2.Visible = true;
                    this.lstFilter3.Visible = true;
                    this.lstFilter4.Visible = false;
                    this.lstFilter5.Visible = false;
                    this.lstFilter6.Visible = false;
                    this.lstFilter7.Visible = false;
                    this.lstFilter8.Visible = false;
                    this.lstFilter9.Visible = false;

                    this.colFilter2.Text = "Type";
                    this.colFilter3.Text = "Fuel Type";

                    break;

                case Enumerations.EnergyType.Cooling:
                    this.lstFilter2.Visible = true;
                    this.lstFilter3.Visible = false;
                    this.lstFilter4.Visible = false;
                    this.lstFilter5.Visible = false;
                    this.lstFilter6.Visible = false;
                    this.lstFilter7.Visible = false;
                    this.lstFilter8.Visible = false;
                    this.lstFilter9.Visible = false;

                    this.colFilter2.Text = "Type";
                    //this.colFilter2.Text = "Water Cooling Source";

                    break;

                case Enumerations.EnergyType.Lighting:
                    this.lstFilter2.Visible = true;
                    this.lstFilter3.Visible = true;
                    this.lstFilter4.Visible = true;
                    this.lstFilter5.Visible = true;
                    this.lstFilter6.Visible = true;
                    this.lstFilter7.Visible = true;
                    this.lstFilter8.Visible = true;
                    this.lstFilter9.Visible = true;

                    this.colFilter2.Text = "Light Category";
                    this.colFilter3.Text = "Fixture Type";
                    /*
                    this.colFilter4.Text = "Fixture Type 2";
                    this.colFilter5.Text = "Lamp Wattage;
                    this.colFilter6.Text = "Lamp Quantity";
                    this.colFilter7.Text = "Lamp Length";
                    this.colFilter8.Text = "Ballast Type";
                    this.colFilter9.Text = "Ballast Quantity";
                    */
                    break;

                case Enumerations.EnergyType.DomesticWaterHeating:
                    this.lstFilter2.Visible = true;
                    this.lstFilter3.Visible = true;
                    this.lstFilter4.Visible = false;
                    this.lstFilter5.Visible = false;
                    this.lstFilter6.Visible = false;
                    this.lstFilter7.Visible = false;
                    this.lstFilter8.Visible = false;
                    this.lstFilter9.Visible = false;

                    this.colFilter2.Text = "System Equipment Type";
                    this.colFilter3.Text = "Fuel Type";

                    break;

                default:
                    break;
            }
        }

        private Dictionary<string, string> ParseFilterString()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (this.EquipmentObject != null)
            {
                string[] items = this.EquipmentObject.FilterString.Split("|".ToCharArray());

                if (items.Length > 0)
                {
                    switch (this.Type)
                    {
                        case Enumerations.EnergyType.Heating:
                            result.Add("Filter1", items[0]);
                            result.Add("Filter2", items[1]);
                            result.Add("Filter3", items[2]);
                            break;

                        case Enumerations.EnergyType.Cooling:
                            result.Add("Filter1", items[0]);
                            result.Add("Filter2", items[1]);
                            break;

                        case Enumerations.EnergyType.Lighting:
                            result.Add("Filter1", items[0]);
                            result.Add("Filter2", items[1]);
                            result.Add("Filter3", items[2]);
                            result.Add("Filter4", items[3]);
                            result.Add("Filter5", items[4]);
                            result.Add("Filter6", items[5]);
                            result.Add("Filter7", items[6]);
                            result.Add("Filter8", items[7]);
                            result.Add("Filter9", items[8]);
                            break;

                        case Enumerations.EnergyType.DomesticWaterHeating:
                            result.Add("Filter1", items[0]);
                            result.Add("Filter2", items[1]);
                            result.Add("Filter3", items[2]);
                            break;
                    }
                }
            }

            return result;
        }

        private string SetFilterString()
        {
            string result = "";

            if (this.lstFilter1.Visible && this.lstFilter1.SelectedItems.Count > 0)
            {
                result += this.lstFilter1.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter2.Visible && this.lstFilter2.SelectedItems.Count > 0)
            {
                result += this.lstFilter2.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter3.Visible && this.lstFilter3.SelectedItems.Count > 0)
            {
                result += this.lstFilter3.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter4.Visible && this.lstFilter4.SelectedItems.Count > 0)
            {
                result += this.lstFilter4.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter5.Visible && this.lstFilter5.SelectedItems.Count > 0)
            {
                result += this.lstFilter5.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter6.Visible && this.lstFilter6.SelectedItems.Count > 0)
            {
                result += this.lstFilter6.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter7.Visible && this.lstFilter7.SelectedItems.Count > 0)
            {
                result += this.lstFilter7.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter8.Visible && this.lstFilter8.SelectedItems.Count > 0)
            {
                result += this.lstFilter8.SelectedItems[0].Text + "|";
            }

            if (this.lstFilter9.Visible && this.lstFilter9.SelectedItems.Count > 0)
            {
                result += this.lstFilter9.SelectedItems[0].Text + "|";
            }

            //result = result.Trim("|".ToCharArray());

            return result;
        }

        private bool SyncObject(Enumerations.SyncDirection direction)
        {
            switch (direction)
            {
                case Enumerations.SyncDirection.EditObjectToControls:
                case Enumerations.SyncDirection.SelectedObjectToControls:

                    if (this.EquipmentObject != null)
                    {
                        this.txtEquipmentMasterId.Text = this.EquipmentObject.EquipmentMasterId;
                        this.txtActualWattage.Text = this.EquipmentObject.ActualWattage;

                        if (this._isNew)
                        {
                            this.Filter = this.EquipmentObject.Filters;
                        }
                        else
                        {
                            this.Filter = this.ParseFilterString();
                        }

                        this.txtComponentId.Text = this.EquipmentObject.ComponentId;
                        this.txtComponentName.Text = this.EquipmentObject.ComponentName;

                        this.txtName.Text = this.EquipmentObject.EquipmentName;
                        this.txtMfgName.Text = this.EquipmentObject.ManufacturerName;
                        this.txtMfgModel.Text = this.EquipmentObject.ManufacturerModel;
                        this.txtBurnerMfgName.Text = this.EquipmentObject.BurnerManufacturerName;
                        this.txtBurnerMfgModel.Text = this.EquipmentObject.BurnerManufacturerModel;
                        this.txtBallastMfgName.Text = this.EquipmentObject.BallastManufacturerName;
                        this.txtBallastMfgModel.Text = this.EquipmentObject.BallastManufacturerModel;

                        this.txtTechSize.Text = this.EquipmentObject.Size.ToString();
                        this.cboTechSize.Text = this.EquipmentObject.SizeUnit;
                        this.txtTechEfficiency.Text = this.EquipmentObject.Efficiency.ToString();
                        this.cboTechEfficiency.Text = this.EquipmentObject.EfficiencyUnit;
                        this.txtTechPartLoadEfficiency.Text = this.EquipmentObject.PartLoadEfficiency.ToString();
                        this.cboTechPartLoadEfficiency.Text = this.EquipmentObject.PartLoadEfficiencyUnit;
                        this.cboTechSystemControlType.Text = this.EquipmentObject.SystemControlType;
                        this.cboTechSupplementalControlType.Text = this.EquipmentObject.SupplementalControlType;
                        this.cboTechBurnerType.Text = this.EquipmentObject.BurnerType;
                        this.cboTechBurnerControlType.Text = this.EquipmentObject.BurnerControlType;
                        this.cboTechCompressorType.Text = this.EquipmentObject.CompressorType;
                        this.cboTechEconomizer.Text = this.EquipmentObject.WaterCoolingControlType;
                        this.cboTechControlType.Text = this.EquipmentObject.ControlType;
                        this.cboTechControlSubType.Text = this.EquipmentObject.ControlSubType;
                        this.numTechControlQuantity.Value = this.EquipmentObject.ControlQuantity;
                        this.txtTechCapacity.Text = this.EquipmentObject.Capacity.ToString();
                        this.txtImageFile.Text = this.EquipmentObject.ImageFilePath.ToString();

                        bool found = false;
                        foreach (BuildingSpace space in this.EquipmentObject.Spaces)
                        {
                            if (this.lstSpaceSpecific.FindItemWithText(space.Space) != null)
                            {
                                this.lstSpaceSpecific.FindItemWithText(space.Space).Tag = space;
                                this.lstSpaceSpecific.FindItemWithText(space.Space).Selected = true;
                                this.txtSpaceLabel.Text = space.SpaceLabel;

                                found = true;
                            }

                            if (found)
                            {
                                break;
                            }
                        }

                        //if (this.EquipmentObject.LightingHeatingCoolingType != null && !String.IsNullOrWhiteSpace(this.EquipmentObject.LightingHeatingCoolingType)) {
                        //    this.cboSpaceLHC.SelectedIndex = this.cboSpaceLHC.FindStringExact(this.EquipmentObject.LightingHeatingCoolingType);
                        //}

                        string presetScheduleName = this.GetSchedulePresetName(this.EquipmentObject.EquipmentSchedule);

                        if (String.IsNullOrWhiteSpace(presetScheduleName))
                        {
                            this.fireDurationDropDown = false;
                            this.cboSchedulePresets.SelectedIndex = -1;
                            this.SetScheduleControl(this.EquipmentObject.EquipmentSchedule);
                            this.fireDurationDropDown = true;
                        }
                        else
                        {
                            this.cboSchedulePresets.SelectedIndex = this.cboSchedulePresets.FindStringExact(this.EquipmentObject.PresetSchedule);
                        }

                        this.numQuantity.Value = this.EquipmentObject.Quantity;
                    }

                    break;

                case Enumerations.SyncDirection.ControlsToEditObject:
                case Enumerations.SyncDirection.ControlsToSelectedObject:

                    this.EquipmentObject = new EquipmentMaster();

                    if (this.txtEquipmentMasterId.Text == "")
                    {
                        this.EquipmentObject.EquipmentMasterId = GuidGenerator.Generate();
                    }
                    else
                    {
                        this.EquipmentObject.EquipmentMasterId = this.txtEquipmentMasterId.Text;
                    }

                    this.EquipmentObject.SystemType = this.GetSystemTypeBreadcrumb().Last.Value;

                    this.EquipmentObject.ComponentId = this.txtComponentId.Text;
                    this.EquipmentObject.ComponentName = this.txtComponentName.Text;

                    //this.txtName.Text = this.GetDefaultName(this.txtComponentId.Text);

                    this.EquipmentObject.EquipmentName = this.txtName.Text;
                    this.EquipmentObject.ManufacturerName = this.txtMfgName.Text;
                    this.EquipmentObject.ManufacturerModel = this.txtMfgModel.Text;
                    this.EquipmentObject.BurnerManufacturerName = this.txtBurnerMfgName.Text;
                    this.EquipmentObject.BurnerManufacturerModel = this.txtBurnerMfgModel.Text;
                    this.EquipmentObject.BallastManufacturerName = this.txtBallastMfgName.Text;
                    this.EquipmentObject.BallastManufacturerModel = this.txtBallastMfgModel.Text;

                    this.EquipmentObject.Size = (String.IsNullOrWhiteSpace(this.txtTechSize.Text) ? 0 : double.Parse(this.txtTechSize.Text));
                    this.EquipmentObject.SizeUnit = this.cboTechSize.Text;
                    this.EquipmentObject.Efficiency = (String.IsNullOrWhiteSpace(this.txtTechEfficiency.Text) ? 0 : double.Parse(this.txtTechEfficiency.Text));
                    this.EquipmentObject.EfficiencyUnit = this.cboTechEfficiency.Text;
                    this.EquipmentObject.PartLoadEfficiency = (String.IsNullOrWhiteSpace(this.txtTechPartLoadEfficiency.Text) ? 0 : double.Parse(this.txtTechPartLoadEfficiency.Text));
                    this.EquipmentObject.PartLoadEfficiencyUnit = this.cboTechPartLoadEfficiency.Text;
                    this.EquipmentObject.SystemControlType = this.cboTechSystemControlType.Text;
                    this.EquipmentObject.SupplementalControlType = this.cboTechSupplementalControlType.Text;
                    this.EquipmentObject.BurnerType = this.cboTechBurnerType.Text;
                    this.EquipmentObject.BurnerControlType = this.cboTechBurnerControlType.Text;
                    this.EquipmentObject.CompressorType = this.cboTechCompressorType.Text;
                    this.EquipmentObject.WaterCoolingControlType = this.cboTechEconomizer.Text;
                    this.EquipmentObject.ControlType = this.cboTechControlType.Text;
                    this.EquipmentObject.ControlSubType = this.cboTechControlSubType.Text;
                    this.EquipmentObject.ControlQuantity = this.numTechControlQuantity.Value;
                    this.EquipmentObject.ImageFilePath = this.txtImageFile.Text;

                    double controlFactor = 0;
                    if (this.cboTechControlSubType.SelectedItem != null)
                    {
                        ListViewItem li = this.cboTechControlSubType.SelectedItem as ListViewItem;

                        if (li != null)
                        {
                            controlFactor = (double)(li.Tag ?? 0);
                        }
                    }
                    this.EquipmentObject.ControlFactor = controlFactor;

                    this.EquipmentObject.Capacity = (String.IsNullOrWhiteSpace(this.txtTechCapacity.Text) ? 0 : double.Parse(this.txtTechCapacity.Text));

                    foreach (ListViewItem li in this.lstSpaceSpecific.SelectedItems)
                    {
                        BuildingSpace bs = new BuildingSpace();
                        bs.Id = GuidGenerator.Generate();
                        bs.Space = li.Text;
                        bs.SpaceLabel = this.txtSpaceLabel.Text;
                        this.EquipmentObject.AddSpace(bs);
                    }

                    this.EquipmentObject.LightingHeatingCoolingType = this.cboSpaceLHC.Text;

                    this.EquipmentObject.PresetSchedule = this.cboSchedulePresets.Text;
                    this.EquipmentObject.EquipmentSchedule = this.GetSchedule();

                    this.EquipmentObject.Quantity = this.numQuantity.Value;
                    this.EquipmentObject.TypeOfEnergy = this.Type;
                    this.EquipmentObject.ActualWattage = this.txtActualWattage.Text;
                    this.EquipmentObject.FilterString = this.SetFilterString();

                    break;
            }

            return true;
        }

        private string GetSchedulePresetName(Schedule s)
        {
            string result = "";

            if (s != null)
            {
                foreach (Schedule p in this.DefaultSchedules)
                {
                    if (p.Equals(s))
                    {
                        result = p.ScheduleDescription;
                        break;
                    }
                }
            }

            return result;
        }

        private void SetPresetFromScheduleControls()
        {
            Schedule s = new Schedule();
            s.NumberOfDaysPerWeek = this.numDaysWeek.Value;
            s.NumberOfWeeksPerYear = this.numWeeksYear.Value;
            s.NumberOfHolidays = this.numHolidays.Value;

            Duration d = new Duration();
            s.AddDuration(d);

            d = new Duration();
            s.AddDuration(d);

            d = new Duration();
            s.AddDuration(d);

            string presetScheduleName = this.GetSchedulePresetName(s);

            if (String.IsNullOrWhiteSpace(presetScheduleName))
            {
                this.fireDurationDropDown = false;
                this.cboSchedulePresets.SelectedIndex = -1;
                this.fireDurationDropDown = true;
            }
            else
            {
                this.cboSchedulePresets.SelectedIndex = this.cboSchedulePresets.FindStringExact(presetScheduleName);
            }
        }

        #endregion Equipment panel helper methods

        #region Testing methods to delete

        public Recommendation.RecommendationCollection Recommendations = new Recommendation.RecommendationCollection();

        private void GetSampleEquipment(int index)
        {
            try
            {
                switch (index)
                {
                    case 1:
                        // Lighting
                        this.lstFilter1.FindItemWithText("Lighting").Selected = true;
                        this.lstFilter2.FindItemWithText("Interior").Selected = true;
                        this.lstFilter3.FindItemWithText("Linear Fluorescent").Selected = true;
                        this.lstFilter4.FindItemWithText("T12").Selected = true;
                        this.lstFilter5.FindItemWithText("34").Selected = true;
                        this.lstFilter6.FindItemWithText("2").Selected = true;
                        this.lstFilter7.FindItemWithText("4").Selected = true;
                        this.lstFilter8.FindItemWithText("Instant start Ballast (Default)").Selected = true;
                        this.lstFilter9.FindItemWithText("1").Selected = true;

                        //this.txtName.Text = "Lighting01";
                        this.txtMfgName.Text = "GE";
                        this.txtMfgModel.Text = "AD-132";
                        this.txtBurnerMfgName.Text = "Honeywell";
                        this.txtBurnerMfgModel.Text = "GFD9-02";
                        this.txtBallastMfgName.Text = "The Ballast Company";
                        this.txtBallastMfgModel.Text = "883-A";

                        this.txtTechSize.Text = "4.3";
                        if (this.cboTechSize.Items.Count > 0)
                        {
                            this.cboTechSize.SelectedIndex = 1;
                        }

                        this.txtTechEfficiency.Text = ".4";
                        if (this.cboTechEfficiency.Items.Count > 0)
                        {
                            this.cboTechEfficiency.SelectedIndex = this.cboTechPartLoadEfficiency.Items.Count - 1;
                        }

                        this.txtTechPartLoadEfficiency.Text = "944.4";
                        if (this.cboTechPartLoadEfficiency.Items.Count > 0)
                        {
                            this.cboTechPartLoadEfficiency.SelectedIndex = 1;
                        }

                        this.cboTechSystemControlType.SelectedItem = "Automatic";
                        this.cboTechSupplementalControlType.SelectedItem = "None";
                        this.cboTechBurnerControlType.SelectedItem = "Manual";
                        this.cboTechBurnerType.SelectedItem = "Gas";
                        this.cboTechCompressorType.SelectedItem = "Electric";
                        this.cboTechEconomizer.SelectedItem = "Gas";

                        this.cboTechControlType.Text = "Occupancy";
                        //this.cboTechControlType.SelectedItem = "Occupancy";
                        this.cboTechControlSubType.SelectedItem = "Time Clock";
                        this.numTechControlQuantity.Value = 3;
                        this.txtTechCapacity.Text = "45887";

                        this.txtSpaceLabel.Text = "West Building";
                        this.cboSpaceLHC.SelectedIndex = 2;

                        this.tabEquipmentDetails.SelectedTabIndex = 3;
                        this.cboSchedulePresets.SelectedIndex = 0;
                        this.numQuantity.Value = 5;

                        break;

                    case 2:
                        // Heating
                        this.lstFilter1.FindItemWithText("Heating").Selected = true;
                        this.lstFilter2.FindItemWithText("Boiler, Hot Water").Selected = true;
                        this.lstFilter3.FindItemWithText("Natural Gas").Selected = true;

                        this.txtName.Text = "Radiator";
                        this.txtMfgName.Text = "Radiator Mfg. Worldwide";
                        this.txtMfgModel.Text = "AD-132";
                        this.txtBurnerMfgName.Text = "Honeywell";
                        this.txtBurnerMfgModel.Text = "GFD9-02";
                        this.txtBallastMfgName.Text = "The Ballast Company";
                        this.txtBallastMfgModel.Text = "883-A";

                        this.txtTechSize.Text = "299";
                        if (this.cboTechSize.Items.Count > 0)
                        {
                            this.cboTechSize.SelectedIndex = 1;
                        }

                        this.txtTechEfficiency.Text = "84";
                        if (this.cboTechEfficiency.Items.Count > 0)
                        {
                            this.cboTechEfficiency.SelectedIndex = 0;
                        }

                        this.txtTechPartLoadEfficiency.Text = ".5654";
                        if (this.cboTechPartLoadEfficiency.Items.Count > 0)
                        {
                            this.cboTechPartLoadEfficiency.SelectedIndex = 1;
                        }

                        this.cboTechSystemControlType.SelectedItem = "Automatic";
                        this.cboTechSupplementalControlType.SelectedItem = "None";
                        this.cboTechBurnerControlType.SelectedItem = "Manual";
                        this.cboTechBurnerType.SelectedItem = "Gas";
                        this.cboTechCompressorType.SelectedItem = "Electric";
                        this.cboTechEconomizer.SelectedItem = "Gas";
                        this.cboTechControlType.Text = "Institutional Tuning";
                        this.cboTechControlSubType.SelectedItem = "Dimmable Ballasts";
                        this.numTechControlQuantity.Value = 4;
                        this.txtTechCapacity.Text = "45887";

                        this.txtSpaceLabel.Text = "Lobby";
                        this.cboSpaceLHC.SelectedIndex = 1;

                        this.tabEquipmentDetails.SelectedTabIndex = 3;
                        this.cboSchedulePresets.SelectedIndex = 1;
                        this.numQuantity.Value = 5;

                        break;

                    case 3:
                        // Cooling
                        this.lstFilter1.FindItemWithText("Cooling").Selected = true;
                        this.lstFilter2.FindItemWithText("Packaged Direct Expansion").Selected = true;

                        this.txtName.Text = "Rooftop AC";
                        this.txtMfgName.Text = "Whirlpool";
                        this.txtMfgModel.Text = "WP-1000";
                        this.txtBurnerMfgName.Text = "Honeywell";
                        this.txtBurnerMfgModel.Text = "GFD9-02";
                        this.txtBallastMfgName.Text = "The Ballast Company";
                        this.txtBallastMfgModel.Text = "883-A";

                        this.txtTechSize.Text = "10";
                        if (this.cboTechSize.Items.Count > 0)
                        {
                            this.cboTechSize.SelectedIndex = 1;
                        }

                        this.txtTechEfficiency.Text = "9";
                        if (this.cboTechEfficiency.Items.Count > 0)
                        {
                            this.cboTechEfficiency.Text = "EER";
                        }

                        this.txtTechPartLoadEfficiency.Text = "9";
                        if (this.cboTechPartLoadEfficiency.Items.Count > 0)
                        {
                            this.cboTechPartLoadEfficiency.Text = "IPLV";
                        }

                        this.cboTechSystemControlType.SelectedItem = "Automatic";
                        this.cboTechSupplementalControlType.SelectedItem = "None";
                        this.cboTechBurnerControlType.SelectedItem = "Manual";
                        this.cboTechBurnerType.SelectedItem = "Gas";
                        this.cboTechCompressorType.SelectedItem = "Electric";
                        this.cboTechEconomizer.Text = "Central Air Conditioning";
                        this.cboTechControlType.Text = "Personal Tuning";
                        this.cboTechControlSubType.SelectedItem = "Dimmers";
                        this.numTechControlQuantity.Value = 87;
                        this.txtTechCapacity.Text = "45887";

                        this.txtSpaceLabel.Text = "Basement";
                        this.cboSpaceLHC.SelectedIndex = 2;

                        this.tabEquipmentDetails.SelectedTabIndex = 3;
                        this.cboSchedulePresets.SelectedIndex = 2;
                        this.numQuantity.Value = 5;

                        break;

                    default:
                        // Domestic Water Heating

                        this.lstFilter1.FindItemWithText("DomesticWaterHeating").Selected = true;
                        this.lstFilter2.FindItemWithText("Commercial Water Heater - Storage").Selected = true;
                        this.lstFilter3.FindItemWithText("Natural Gas").Selected = true;

                        this.txtName.Text = "Test";
                        this.txtMfgName.Text = "GE";
                        this.txtMfgModel.Text = "AD-132";
                        this.txtBurnerMfgName.Text = "Honeywell";
                        this.txtBurnerMfgModel.Text = "GFD9-02";
                        this.txtBallastMfgName.Text = "The Ballast Company";
                        this.txtBallastMfgModel.Text = "883-A";

                        this.txtTechSize.Text = "300";
                        if (this.cboTechSize.Items.Count > 0)
                        {
                            this.cboTechSize.SelectedIndex = 0;
                        }

                        this.txtTechEfficiency.Text = "80";
                        if (this.cboTechEfficiency.Items.Count > 0)
                        {
                            this.cboTechEfficiency.SelectedIndex = 1;
                        }

                        this.txtTechPartLoadEfficiency.Text = "987.415";
                        if (this.cboTechPartLoadEfficiency.Items.Count > 0)
                        {
                            this.cboTechPartLoadEfficiency.SelectedIndex = 1;
                        }

                        this.cboTechSystemControlType.SelectedItem = "Always On";
                        this.cboTechSupplementalControlType.SelectedItem = "None";
                        this.cboTechBurnerControlType.SelectedItem = "Manual";
                        this.cboTechBurnerType.SelectedItem = "Gas";
                        this.cboTechCompressorType.SelectedItem = "Electric";
                        this.cboTechEconomizer.SelectedItem = "Gas";
                        this.cboTechControlType.Text = "Occupancy";
                        this.cboTechControlSubType.SelectedItem = "Occupancy Sensor";
                        this.numTechControlQuantity.Value = 43;
                        this.txtTechCapacity.Text = "45887";

                        this.txtSpaceLabel.Text = "First Floor";
                        this.cboSpaceLHC.SelectedIndex = 1;

                        this.tabEquipmentDetails.SelectedTabIndex = 3;
                        this.cboSchedulePresets.SelectedIndex = 3;
                        this.numQuantity.Value = 5;

                        break;
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        private void mnuLight_Click(object sender, EventArgs e)
        {
            this.GetSampleEquipment(1);
        }

        private void mnuCool_Click(object sender, EventArgs e)
        {
            this.GetSampleEquipment(3);
        }

        private void mnuHeat_Click(object sender, EventArgs e)
        {
            this.GetSampleEquipment(2);
        }

        private void mnuDWH_Click(object sender, EventArgs e)
        {
            this.GetSampleEquipment(4);
        }

        #endregion Testing methods to delete

        #region Schedule methods

        private void cboSchedulePresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.fireDurationDropDown)
            {
                ListViewItem li = this.cboSchedulePresets.SelectedItem as ListViewItem;
                if (li != null)
                {
                    Schedule s = li.Tag as Schedule;
                    if (s != null)
                    {
                        this.SetScheduleControl(s);
                    }
                }
            }
        }

        private void SetScheduleControl(Schedule schedule)
        {
            if (schedule != null)
            {
                this.txtScheduleId.Text = schedule.ScheduleId;
                this.txtScheduleExternalId.Text = schedule.ExternalId;
                this.txtScheduleDescription.Text = schedule.ScheduleDescription;

                this.numDaysWeek.Value = schedule.NumberOfDaysPerWeek;
                this.numWeeksYear.Value = schedule.NumberOfWeeksPerYear;
                this.numHolidays.Value = schedule.NumberOfHolidays;

                int min = this.ConvertTimeSpanToTicks(schedule.Durations[0].StartTime);
                int max = this.ConvertTimeSpanToTicks(schedule.Durations[0].EndTime);
                this.rngWeekdays.Value = new RangeValue(min, max);

                min = this.ConvertTimeSpanToTicks(schedule.Durations[1].StartTime);
                max = this.ConvertTimeSpanToTicks(schedule.Durations[1].EndTime);
                this.rngWeekends.Value = new RangeValue(min, max);

                min = this.ConvertTimeSpanToTicks(schedule.Durations[2].StartTime);
                max = this.ConvertTimeSpanToTicks(schedule.Durations[2].EndTime);
                this.rngHolidays.Value = new RangeValue(min, max);

                this.lblExteriorNote.Visible = false;
                this.lblHvacNote.Visible = false;

                switch (schedule.ScheduleDescription)
                {
                    case "Exterior Lights":
                        this.lblExteriorNote.Visible = true;
                        //this.lblHvacNote.Visible = false;
                        break;

                    case "HVAC":
                        //this.lblExteriorNote.Visible = false;
                        this.lblHvacNote.Visible = true;
                        break;
                }
            }
        }

        private Schedule GetSchedule()
        {
            Schedule result = new Schedule();

            result.ExternalId = this.txtScheduleExternalId.Text;
            result.ScheduleDescription = this.txtScheduleDescription.Text;
            result.ScheduleId = this.txtScheduleId.Text;
            result.ScheduleName = this.Type.ToString();
            result.ScheduleType = this.cboSchedulePresets.Text;

            result.NumberOfDaysPerWeek = this.numDaysWeek.Value;
            result.NumberOfWeeksPerYear = this.numWeeksYear.Value;
            result.NumberOfHolidays = this.numHolidays.Value;

            Duration d = new Duration();
            d.Type = "Weekdays";
            d.StartTime = this.ConvertTicksToTimeSpan(this.rngWeekdays.Value.Min);
            d.EndTime = this.ConvertTicksToTimeSpan(this.rngWeekdays.Value.Max);
            result.AddDuration(d);

            d = new Duration();
            d.Type = "Weekends";
            d.StartTime = this.ConvertTicksToTimeSpan(this.rngWeekends.Value.Min);
            d.EndTime = this.ConvertTicksToTimeSpan(this.rngWeekends.Value.Max);
            result.AddDuration(d);

            d = new Duration();
            d.Type = "Holidays";
            d.StartTime = this.ConvertTicksToTimeSpan(this.rngHolidays.Value.Min);
            d.EndTime = this.ConvertTicksToTimeSpan(this.rngHolidays.Value.Max);
            result.AddDuration(d);

            return result;
        }

        private TimeSpan ConvertTicksToTimeSpan(int ticks)
        {
            if (ticks == 48)
            {
                return new TimeSpan(23, 59, 59);
            }
            else
            {
                double r = 30.0 * (double)ticks;
                double d = r / 60.0;

                DateTime dt = new DateTime();
                return dt.AddHours(d).TimeOfDay;
            }
        }

        private const int SLIDER_MAX_TICKS = 48;

        public int ConvertTimeSpanToTicks(TimeSpan duration)
        {
            int result = 0;

            if (duration != null)
            {
                if (duration == new TimeSpan(23, 59, 59))
                {
                    result = SLIDER_MAX_TICKS;
                }
                else
                {
                    result = (int)(duration.TotalMinutes / 30);
                }
            }

            return result;
        }

        private string ConvertTimeSpanToText(TimeSpan ts)
        {
            return new DateTime().Add(ts).ToString("h:mm tt");
        }

        private void SetScheduleColors()
        {
            this.lblWeekdayStart.BackColor = Color.PaleGreen;
            this.lblWeekdayEnd.BackColor = Color.LightCoral;

            this.lblWeekendStart.BackColor = Color.PaleGreen;
            this.lblWeekendEnd.BackColor = Color.LightCoral;

            this.lblHolidayStart.BackColor = Color.PaleGreen;
            this.lblHolidayEnd.BackColor = Color.LightCoral;
        }

        private void SetTimeLabelsVisible(bool visible, Enumerations.DurationType type)
        {
            switch (type)
            {
                case Enumerations.DurationType.Weekdays:
                    this.lblWeekdayNA.Visible = !visible;
                    this.lblWeekdayStart.Visible = visible;
                    this.lblWeekdayEnd.Visible = visible;

                    break;

                case Enumerations.DurationType.Weekends:
                    this.lblWeekendNA.Visible = !visible;
                    this.lblWeekendStart.Visible = visible;
                    this.lblWeekendEnd.Visible = visible;
                    break;

                case Enumerations.DurationType.Holidays:
                    this.lblHolidayNA.Visible = !visible;
                    this.lblHolidayStart.Visible = visible;
                    this.lblHolidayEnd.Visible = visible;
                    break;
            }
        }

        //private void LoadData(RangeSlider ctl) {
        //    if (this.Item != null) {
        //        int startTicks = this.ConvertTimeSpanToTicks(this.Item.StartTime);
        //        int endTicks = this.ConvertTimeSpanToTicks(this.Item.EndTime);

        //        ctl.Value = new RangeValue(startTicks, endTicks);

        //    }
        //}

        private void SetDefaultSpan(RangeSlider ctl, Enumerations.DurationType type)
        {
            switch (type)
            {
                case Enumerations.DurationType.Weekdays:
                    // Set 7AM - 5PM.
                    ctl.Value = new RangeValue(
                        this.ConvertTimeSpanToTicks(new TimeSpan(0, 7, 0, 0)),
                        this.ConvertTimeSpanToTicks(new TimeSpan(0, 17, 0, 0)));
                    break;

                case Enumerations.DurationType.Weekends:
                    // Set 8AM - 12PM.
                    ctl.Value = new RangeValue(
                            this.ConvertTimeSpanToTicks(new TimeSpan(0, 8, 0, 0)),
                            this.ConvertTimeSpanToTicks(new TimeSpan(0, 12, 0, 0)));
                    break;

                default:
                    // Do not set.
                    break;
            }
        }

        private void ResetSchedule()
        {
            this.rngWeekdays.Value = new RangeValue(0, 0);
            this.rngWeekends.Value = new RangeValue(0, 0);
            this.rngHolidays.Value = new RangeValue(0, 0);
            this.SetTimeLabelsVisible(false, Enumerations.DurationType.Weekdays);
            this.SetTimeLabelsVisible(false, Enumerations.DurationType.Weekends);
            this.SetTimeLabelsVisible(false, Enumerations.DurationType.Holidays);
        }

        #endregion Schedule methods

        private void lstSpaceSpecific_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewEx ctl = sender as ListViewEx;

            //this.lblSpaceLHC.Enabled = true;
            //this.cboSpaceLHC.Enabled = true;

            if (ctl != null)
            {
                if (ctl.SelectedItems.Count > 0)
                {
                    if (ctl.SelectedItems[0].Text.TrueCompare("Exterior"))
                    {
                        //this.lblSpaceLHC.Enabled = false;
                        //this.cboSpaceLHC.Enabled = false;
                    }
                }
            }

            if (!String.IsNullOrWhiteSpace(this.txtComponentId.Text))
            {
                this.SetEquipmentNameFields();
            }
        }

        private void txtTechEfficiency_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtTechEfficiency.Text))
            {
                this.txtComponentId.Text = "";
                this.cboTechEfficiency.SelectedIndex = -1;
            }
            else
            {
                this.FillComponentIdField(this.Type);
            }

            this.SetEquipmentNameFields();
        }

        private void txtTechSize_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtTechSize.Text))
            {
                this.txtComponentId.Text = "";
                this.cboTechSize.SelectedIndex = -1;
            }
            else
            {
                this.FillComponentIdField(this.Type);
            }

            this.SetEquipmentNameFields();
        }

        private void cboTechEfficiency_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillComponentIdField(this.Type);
            this.SetEquipmentNameFields();
        }

        private void cboTechSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillComponentIdField(this.Type);
            this.SetEquipmentNameFields();
        }

        private void btnUpdateStats_Click(object sender, EventArgs e)
        {
            if (this.EquipmentObject != null)
            {
                this.txtStats.Text = this.EquipmentObject.ToString();
            }
        }

        private void txtComponentId_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = !String.IsNullOrWhiteSpace(this.txtComponentId.Text);
        }

        private void btnNumDaysWeekInc_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numDaysWeek, "I");
        }

        private void btnNumDaysWeekDec_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numDaysWeek, "D");
        }

        private void btnNumWeeksYrInc_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numWeeksYear, "I");
        }

        private void btnNumWeeksYrDec_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numWeeksYear, "D");
        }

        private void btnNumHolidaysInc_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numHolidays, "I");
        }

        private void btnNumHolidaysDec_Click(object sender, EventArgs e)
        {
            this.NumberControlCounterHelper(this.numHolidays, "D");
        }

        private void NumberControlCounterHelper(DevComponents.Editors.IntegerInput ctl, string direction)
        {
            if (ctl != null)
            {
                switch (direction)
                {
                    case "I":
                        if (ctl.Value == ctl.MaxValue)
                        {
                            ctl.Value = ctl.MinValue;
                        }
                        else
                        {
                            ctl.IncreaseValue();
                        }

                        break;

                    case "D":
                        if (ctl.Value == ctl.MinValue)
                        {
                            ctl.Value = ctl.MaxValue;
                        }
                        else
                        {
                            ctl.DecreaseValue();
                        }
                        break;
                }
            }
        }

        private void numDaysWeek_ValueChanged(object sender, EventArgs e)
        {
            //this.SetPresetFromScheduleControlHelper();
        }

        private void numWeeksYear_ValueChanged(object sender, EventArgs e)
        {
            //this.SetPresetFromScheduleControlHelper();
        }

        private void numHolidays_ValueChanged(object sender, EventArgs e)
        {
            //this.SetPresetFromScheduleControlHelper();
        }

        private void rngWeekdays_ValueChanged(object sender, EventArgs e)
        {
            this.ValueChangedHelper(this.rngWeekdays, this.lblWeekdayStart, this.lblWeekdayEnd, Enumerations.DurationType.Weekdays);
        }

        private void rngWeekends_ValueChanged(object sender, EventArgs e)
        {
            this.ValueChangedHelper(this.rngWeekends, this.lblWeekendStart, this.lblWeekendEnd, Enumerations.DurationType.Weekends);
        }

        private void rngHolidays_ValueChanged(object sender, EventArgs e)
        {
            this.ValueChangedHelper(this.rngHolidays, this.lblHolidayStart, this.lblHolidayEnd, Enumerations.DurationType.Holidays);
        }

        //private void SetPresetFromScheduleControlHelper() {
        //if (this.EquipmentObject != null && this.EquipmentObject.EquipmentSchedule != null) {
        //    this.EquipmentObject.EquipmentSchedule.ScheduleDescription = "Custom";
        //        this.SetPresetFromScheduleControls();
        //}
        // }

        private void ValueChangedHelper(RangeSlider ctl, Label startLabel, Label endLabel, Enumerations.DurationType type)
        {
            int min = ctl.Value.Min;
            int max = ctl.Value.Max;

            TimeSpan startTS = this.ConvertTicksToTimeSpan(min);
            TimeSpan endTS = this.ConvertTicksToTimeSpan(max);
            startLabel.Text = this.ConvertTimeSpanToText(startTS);
            endLabel.Text = this.ConvertTimeSpanToText(endTS);

            if (min == max)
            {
                this.SetTimeLabelsVisible(false, type);
            }
            else
            {
                this.SetTimeLabelsVisible(true, type);
            }
        }

        private void mnuOff_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 0, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 0, 0, 0))));
        }

        private void mnu63_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 6, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 15, 0, 0))));
        }

        private void mnu74_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 7, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 16, 0, 0))));
        }

        private void mnu85_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 8, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 17, 0, 0))));
        }

        private void mnu96_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 9, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 18, 0, 0))));
        }

        private void mnu24Hr_Click(object sender, EventArgs e)
        {
            this.SchedulePopupMenuHelper(
                sender,
                new RangeValue(
                    this.ConvertTimeSpanToTicks(new TimeSpan(0, 0, 0)),
                    this.ConvertTimeSpanToTicks(new TimeSpan(23, 59, 59))));
        }

        private void SchedulePopupMenuHelper(object sender, RangeValue range)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item != null)
            {
                ContextMenuStrip parent = item.GetCurrentParent() as ContextMenuStrip;

                if (parent != null && parent.SourceControl != null)
                {
                    string source = parent.SourceControl.Name;

                    if (!String.IsNullOrWhiteSpace(source))
                    {
                        switch (source.Trim().ToUpper())
                        {
                            case "LBLWEEKDAYS":
                                this.rngWeekdays.Value = range;
                                //this.ValueChangedHelper(this.rngWeekdays, this.lblWeekdayStart, this.lblWeekdayEnd, Enumerations.DurationType.Weekdays);
                                break;

                            case "LBLWEEKENDS":
                                this.rngWeekends.Value = range;
                                //this.ValueChangedHelper(this.rngWeekends, this.lblWeekendStart, this.lblWeekendEnd, Enumerations.DurationType.Weekends);
                                break;

                            case "LBLHOLIDAYS":
                                this.rngHolidays.Value = range;
                                //this.ValueChangedHelper(this.rngHolidays, this.lblHolidayStart, this.lblHolidayEnd, Enumerations.DurationType.Holidays);
                                break;
                        }
                    }
                }
            }
        }

        private void btnTechControlQuantityDecrement_Click(object sender, EventArgs e)
        {
            this.numTechControlQuantity.DecreaseValue();
        }

        private void btnTechControlQuantityIncrement_Click(object sender, EventArgs e)
        {
            this.numTechControlQuantity.IncreaseValue();
        }

        private void SetEquipmentNameFields()
        {
            this.txtName.Text = this.GetDefaultName(this.txtComponentId.Text);
        }

        //private bool _saveImageOnExit = true;

        private void txtImageFile_ButtonCustomClick(object sender, EventArgs e)
        {
            Audit audit = DataStore.GetAudit(this.AuditId);

            if (audit != null && audit.Buildings != null)
            {
                string path = Path.Combine(DataStore.XmlPicturePath, "Equipment");
                if (Directory.Exists(path))
                {
                    this.dlgSaveFile.InitialDirectory = path;
                }
                else
                {
                    this.dlgSaveFile.InitialDirectory = DataStore.XmlPicturePath;
                }

                this.dlgSaveFile.OverwritePrompt = false;
                this.dlgSaveFile.Title = "Select Image File";
                this.dlgSaveFile.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.gif)|*.bmp;*.jpg;*.jpeg;*.gif|All files (*.*)|*.*";
                this.dlgSaveFile.CheckFileExists = true;

                DialogResult result = this.dlgSaveFile.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && File.Exists(this.dlgSaveFile.FileName))
                {
                    try
                    {
                        this.txtImageFile.Text = this.dlgSaveFile.FileName;
                        this.btnDeleteImage.Enabled = true;

                        this.picEquipment.SizeMode = PictureBoxSizeMode.Zoom;
                        this.picEquipment.Image = new Bitmap(this.dlgSaveFile.FileName);

                        // Doesn't save the image until the user clicks the form's Save button.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    this.txtImageFile.ResetText();
                    this.btnDeleteImage.Enabled = false;
                    this.picEquipment.Image = null;
                    //this._saveImageOnExit = false;
                }
            }
            else
            {
                this.txtImageFile.ResetText();
                this.btnDeleteImage.Enabled = false;
                this.picEquipment.Image = null;
            }
        }

        private bool _deleteImageOnExit = false;

        private void btnDeleteImage_Click(object sender, EventArgs e)
        {
            string msg = "This will remove this image from this component.  If you wish to re-add it, you will need to update the picture through " +
                "the Equipment Inventory Detail's Picture tab.\n\nDo you really want to remove this image?";

            DialogResult result = MessageBox.Show(msg, "Delete Image", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this._deleteImageOnExit = true;
                this.btnDeleteImage.Enabled = false;
                this.txtImageFile.ResetText();
                this.picEquipment.Image = null;
            }
            else
            {
                this._deleteImageOnExit = false;
            }
        }
    }
}