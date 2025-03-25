using DevComponents.DotNetBar.Controls;
using FieldTool.BLL;
using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmRecommendationLibraryDetails : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private bool _hasComponentIds = false;
        private bool _hasExistingComponent = false;
        private bool _hasProposedComponent = false;
        private bool _hasQuantityUnit = false;
        private bool _hasSizeUnit = false;
        private bool _hasRebateAmount = false;
        private bool _hasKwh = false;
        private bool _hasMcf = false;
        private int _nextSortIndex = 0;

        #endregion Private member variables

        #region Constructors

        public frmRecommendationLibraryDetails()
        {
            InitializeComponent();

            this.OriginalRecommendation = new RecommendationLibrary();
            this.NewRecommendation = new RecommendationLibrary();
            this.SpaceCollection = new List<string>();
        }

        public frmRecommendationLibraryDetails(RecommendationLibrary originalRecommendation, List<string> spaceCollection, int lastSortIndex)
        {
            this.InitializeComponent();

            this.OriginalRecommendation = (originalRecommendation ?? new RecommendationLibrary());
            this.NewRecommendation = new RecommendationLibrary(this.OriginalRecommendation);
            this.SpaceCollection = (spaceCollection ?? new List<string>());
            this._nextSortIndex = (lastSortIndex + 1);
        }

        #endregion Constructors

        #region Properties

        private RecommendationLibrary NewRecommendation { get; set; }

        public RecommendationLibrary OriginalRecommendation { get; set; }

        private List<string> SpaceCollection { get; set; }

        #endregion Properties

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.CancelAndClose();

            Cursor.Current = Cursors.Default;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.pslMain.SelectedPageIndex = 1;

            Cursor.Current = Cursors.Default;
        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.pslMain.SelectedPageIndex = 0;

            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SaveAndClose();

            Cursor.Current = Cursors.Default;
        }

        private void cboDetailRebateFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SetRebateFormula();
            this.SetSizeControlsVisibility();
            this.UpdateTotals(true, false, false, false);

            Cursor.Current = Cursors.Default;
        }

        private void frmRecommendationLibraryDetails_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.InitializeForm();

            if (this.NewRecommendation.IsCustomRecommendation)
            {
                this.txtGeneralRecommendationName.Focus();
            }

            Cursor.Current = Cursors.Default;
        }

        private void pslMain_SelectedPageChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                PageSlider ctl = sender as PageSlider;
                if (ctl != null)
                {
                    switch (ctl.SelectedPageIndex)
                    {
                        case 0:
                            this.SetNavButtons(true, false);
                            break;

                        case 1:
                            this.SetNavButtons(false, true);
                            break;
                    }
                }
            }
        }

        private void txtDetailKwh_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.NewRecommendation.Kwh = this.txtDetailKwh.Value;
            this.UpdateTotals(false, true, true, false);

            Cursor.Current = Cursors.Default;
        }

        private void txtDetailMcf_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.NewRecommendation.Mcf = this.txtDetailMcf.Value;
            this.UpdateTotals(false, true, false, true);

            Cursor.Current = Cursors.Default;
        }

        private void txtDetailQuantity_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.NewRecommendation.Quantity = this.txtDetailQuantity.Value;
            this.UpdateTotals(true, true, true, true);

            Cursor.Current = Cursors.Default;
        }

        private void txtDetailRebateAmount_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.NewRecommendation.RebateAmount = this.txtDetailRebateAmount.Value;
            this.UpdateTotals(true, false, false, false);

            Cursor.Current = Cursors.Default;
        }

        private void txtDetailSize_ValueChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.NewRecommendation.Size = this.txtDetailSize.Value;
            this.UpdateTotals(true, false, false, false);

            Cursor.Current = Cursors.Default;
        }

        #endregion Events

        #region Private methods

        private void CancelAndClose()
        {
            this.NewRecommendation = null;

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ClearDropDownItems()
        {
            this.cboGeneralTechnologyType.Items.Clear();
            this.cboDetailRebateFormula.Items.Clear();
            this.cboDetailSpace.Items.Clear();
        }

        private void FillDropdowns()
        {
            this.ClearDropDownItems();

            // General -> Technology Type
            foreach (string technologyType in DataStore.GetAllRecommendationTechnologyTypes())
            {
                if (technologyType != "Standard")
                {
                    this.cboGeneralTechnologyType.Items.Add(technologyType);
                }
            }

            // Detail -> Space
            foreach (string space in this.SpaceCollection)
            {
                if (!String.IsNullOrWhiteSpace(space))
                {
                    ListViewItem li = new ListViewItem();
                    li.Text = space;
                    li.Tag = space;
                    this.cboDetailSpace.Items.Add(li);
                }
            }

            // Detail -> Rebate Formula
            ListViewItem rebateLi = new ListViewItem();
            rebateLi.Text = "Size Based Rebate";
            rebateLi.Tag = Enumerations.RebateFormulaType.Size_Based_Rebate;
            this.cboDetailRebateFormula.Items.Add(rebateLi);

            rebateLi = new ListViewItem();
            rebateLi.Text = "Standard Rebate";
            rebateLi.Tag = Enumerations.RebateFormulaType.Standard_Rebate;
            this.cboDetailRebateFormula.Items.Add(rebateLi);
        }

        private void InitializeForm()
        {
            this.FillDropdowns();
            this.SyncData(Enumerations.SyncDirection.EditObjectToControls);
            this.SetFormControlsState();

            this.SetDebugVisibility();

            this.pslMain.SelectedPageIndex = 0;
        }

        private void SaveAndClose()
        {
            if (this.ValidateData())
            {
                this.SyncData(Enumerations.SyncDirection.ControlsToEditObject);
                this.NewRecommendation.DetailIsDirty = true;
                this.OriginalRecommendation = new RecommendationLibrary(this.NewRecommendation);

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void SetDebugVisibility()
        {
            bool b = false;
            bool.TryParse(ConfigurationManager.AppSettings["DoDebug"], out b);

            this.txtGeneralKey.Visible = b;
            this.txtGeneralSortIndex.Visible = b;

            this.txtDetailExistingComponentId.Visible = b;
            this.txtDetailProposedComponentId.Visible = b;
        }

        private void SetFormControlsState()
        {
            this.txtGeneralKey.Visible = false;
            this.txtGeneralSortIndex.Visible = false;

            if (this.NewRecommendation.IsCustomRecommendation)
            {
                // General panel

                this.lblGeneralRecommendationName.Visible = false;
                this.txtGeneralRecommendationName.Visible = true;

                this.lblGeneralTechnologyType.Visible = false;
                this.cboGeneralTechnologyType.Visible = true;

                this.lblGeneralRecommendationText.Visible = false;
                this.txtGeneralRecommendationText.Visible = true;

                // Detail panel

                this.lblDetailExistingComponent.Visible = false;
                this.txtDetailExistingComponent.Visible = true;

                this.lblDetailProposedComponent.Visible = false;
                this.txtDetailProposedComponent.Visible = true;

                // Space: Always visible

                // Quantity/Quantity Unit: Always visible

                this.lblDetailQuantityUnit.Visible = false;
                this.txtDetailQuantityUnit.Visible = true;

                this.SetSizeControlsVisibility();

                this.lblDetailRebateFormula.Visible = false;
                this.cboDetailRebateFormula.Visible = true;

                this.lblDetailRebateAmount.Visible = this._hasRebateFormula;
                this.txtDetailRebateAmount.Visible = !this._hasRebateFormula;

                this.lblDetailKwh.Visible = false;
                this.txtDetailKwh.Visible = true;

                this.lblDetailMcf.Visible = false;
                this.txtDetailMcf.Visible = true;
            }
            else
            {
                // General panel

                this.lblGeneralRecommendationName.Visible = true;
                this.txtGeneralRecommendationName.Visible = false;

                this.lblGeneralTechnologyType.Visible = true;
                this.cboGeneralTechnologyType.Visible = false;

                this.lblGeneralRecommendationText.Visible = true;
                this.txtGeneralRecommendationText.Visible = false;

                // Detail panel

                this.lblDetailExistingComponent.Visible = this._hasExistingComponent;
                this.txtDetailExistingComponent.Visible = !this._hasExistingComponent;

                this.lblDetailProposedComponent.Visible = this._hasProposedComponent;
                this.txtDetailProposedComponent.Visible = !this._hasProposedComponent;

                // Space: Always visible

                // Quantity: Always visible

                this.lblDetailQuantityUnit.Visible = this._hasQuantityUnit;
                this.txtDetailQuantityUnit.Visible = !this._hasQuantityUnit;

                this.SetSizeControlsVisibility();

                this.lblDetailRebateFormula.Visible = true;
                this.cboDetailRebateFormula.Visible = false;

                this.lblDetailRebateAmount.Visible = this._hasRebateFormula;
                this.txtDetailRebateAmount.Visible = !this._hasRebateFormula;

                this.lblDetailKwh.Visible = this._hasKwh;
                this.txtDetailKwh.Visible = !this._hasKwh;

                this.lblDetailMcf.Visible = this._hasMcf;
                this.txtDetailMcf.Visible = !this._hasMcf;
            }
        }

        private void SetSizeControlsVisibility()
        {
            if (this.NewRecommendation != null)
            {
                if (this.NewRecommendation.RebateFormula == Enumerations.RebateFormulaType.Size_Based_Rebate)
                {
                    this.lblDetailSizeCaption.Visible = true;
                    this.txtDetailSize.Visible = true;
                    this.btnSizeInc.Visible = true;
                    this.btnSizeDec.Visible = true;

                    this.lblDetailSizeUnitCaption.Visible = true;

                    if (this.NewRecommendation.IsCustomRecommendation)
                    {
                        this.lblDetailSizeUnit.Visible = false;
                        this.txtDetailSizeUnit.Visible = true;
                    }
                    else
                    {
                        this.lblDetailSizeUnit.Visible = this._hasSizeUnit;
                        this.txtDetailSizeUnit.Visible = !this._hasSizeUnit;
                    }
                }
                else
                {
                    // Any Rebate Formula other than Size-based:

                    this.lblDetailSizeCaption.Visible = false;
                    this.txtDetailSize.Visible = false;
                    this.btnSizeInc.Visible = false;
                    this.btnSizeDec.Visible = false;

                    this.lblDetailSizeUnitCaption.Visible = false;
                    this.lblDetailSizeUnit.Visible = false;
                    this.txtDetailSizeUnit.Visible = false;
                }
            }
        }

        private void SetNavButtons(bool generalChecked, bool detailChecked)
        {
            this.btnGeneral.Checked = generalChecked;
            this.btnDetail.Checked = detailChecked;
        }

        private void SetRebateFormula()
        {
            if (this.NewRecommendation != null)
            {
                string formulaTypeString = "";

                if (!String.IsNullOrWhiteSpace(this.cboDetailRebateFormula.Text))
                {
                    ListViewItem li = (ListViewItem)this.cboDetailRebateFormula.SelectedItem;
                    formulaTypeString = (li.Tag ?? "").ToString();

                    if (formulaTypeString == "None" || String.IsNullOrWhiteSpace(formulaTypeString))
                    {
                        this._hasRebateFormula = false;
                    }
                    else
                    {
                        this._hasRebateFormula = true;
                    }
                }

                this.NewRecommendation.RebateFormula = RebateFormulaTypeHelper.ParseStringToFormulaType(formulaTypeString);
            }
        }

        private bool _hasRebateFormula = true;

        private void SyncData(Enumerations.SyncDirection direction)
        {
            if (this.NewRecommendation == null)
            {
                this.NewRecommendation = new RecommendationLibrary();
            }

            switch (direction)
            {
                case Enumerations.SyncDirection.ControlsToEditObject:
                case Enumerations.SyncDirection.ControlsToSelectedObject:

                    this.NewRecommendation.IncludeInRecommendations = true;
                    this.NewRecommendation.Key = this.txtGeneralKey.Text;
                    this.NewRecommendation.SortIndex = int.Parse(this.txtGeneralSortIndex.Text);

                    // General panel

                    this.NewRecommendation.RecommendationId = (String.IsNullOrWhiteSpace(this.lblGeneralRecommendationId.Text)) ? GuidGenerator.Generate() : this.lblGeneralRecommendationId.Text;

                    this.NewRecommendation.RecommendationName = this.txtGeneralRecommendationName.Text;
                    this.NewRecommendation.LibraryType = this.lblGeneralRecommendationType.Text;//"Generic";
                    this.NewRecommendation.TechnologyType = this.cboGeneralTechnologyType.Text;
                    this.NewRecommendation.RecommendationDescription = this.txtGeneralRecommendationText.Text;

                    // Detail panel

                    this.NewRecommendation.ExistingComponentId = this.txtDetailExistingComponentId.Text;
                    this.NewRecommendation.ExistingComponent = this.txtDetailExistingComponent.Text;

                    this.NewRecommendation.ProposedComponentId = this.txtDetailProposedComponentId.Text;
                    this.NewRecommendation.ProposedComponent = this.txtDetailProposedComponent.Text;

                    this.NewRecommendation.Space = this.cboDetailSpace.Text;

                    this.SetRebateFormula();

                    this.NewRecommendation.Quantity = this.txtDetailQuantity.Value;
                    this.NewRecommendation.QuantityUnit = this.txtDetailQuantityUnit.Text;

                    this.NewRecommendation.Size = this.txtDetailSize.Value;
                    this.NewRecommendation.SizeUnit = this.txtDetailSizeUnit.Text;

                    this.NewRecommendation.RebateAmount = this.txtDetailRebateAmount.Value;

                    // No Total Rebate field in RecommendationLibrary object...calculated field

                    this.NewRecommendation.Kwh = this.txtDetailKwh.Value;

                    this.NewRecommendation.Mcf = this.txtDetailMcf.Value;

                    //double d = 0;
                    //Double.TryParse(this.lblDetailAnnualSavings.Text, out d);
                    //this.NewRecommendation.AnnualSavings = d;

                    break;

                case Enumerations.SyncDirection.EditObjectToControls:
                case Enumerations.SyncDirection.SelectedObjectToControls:

                    if (this.NewRecommendation.EditAsCustomRecommendation)
                    {
                        // General panel

                        this.txtGeneralKey.Text = "";
                        this.txtGeneralSortIndex.Text = this._nextSortIndex.ToString();

                        this.lblGeneralRecommendationId.Text = "";

                        this.txtGeneralRecommendationName.Text = "";
                        this.lblGeneralRecommendationName.Text = "";

                        this.lblGeneralRecommendationType.Text = "Custom";

                        this.cboGeneralTechnologyType.Text = "";
                        this.lblGeneralTechnologyType.Text = "";

                        this.txtGeneralRecommendationText.Text = "";
                        this.lblGeneralRecommendationText.Text = "";

                        // Detail panel

                        this.txtDetailExistingComponentId.Text = "";

                        this.txtDetailExistingComponent.Text = "";
                        this.lblDetailExistingComponent.Text = "";

                        this.txtDetailProposedComponentId.Text = "";

                        this.txtDetailProposedComponent.Text = "";
                        this.lblDetailProposedComponent.Text = "";

                        this.cboDetailSpace.Text = "Whole Building";

                        this.cboDetailRebateFormula.Text = "";
                        this._hasRebateFormula = false;

                        this.lblDetailRebateFormula.Text = "";

                        this.txtDetailQuantity.Value = 0;

                        this.txtDetailQuantityUnit.Text = "";
                        this.lblDetailQuantityUnit.Text = "";

                        this.txtDetailSize.Value = 0;

                        this.txtDetailSizeUnit.Text = "";
                        this.lblDetailSizeUnit.Text = "";

                        this.txtDetailRebateAmount.Value = 0;
                        this.lblDetailRebateAmount.Text = 0.ToString("C");

                        this.txtDetailTotalRebate.Value = 0;

                        this.txtDetailKwh.Value = 0;
                        this.lblDetailKwh.Text = "0";

                        this.txtDetailMcf.Value = 0;
                        this.txtDetailMcf.Text = "0";

                        this.txtDetailTotalAnnualSavings.Value = 0;
                    }
                    else
                    {
                        // General panel

                        this.txtGeneralKey.Text = this.NewRecommendation.Key;
                        this.txtGeneralSortIndex.Text = this.NewRecommendation.SortIndex.ToString();

                        this.lblGeneralRecommendationId.Text = this.NewRecommendation.RecommendationId;

                        this.txtGeneralRecommendationName.Text = this.NewRecommendation.RecommendationName;
                        this.lblGeneralRecommendationName.Text = this.NewRecommendation.RecommendationName;

                        this.lblGeneralRecommendationType.Text = this.NewRecommendation.LibraryType;

                        this.cboGeneralTechnologyType.Text = this.NewRecommendation.TechnologyType;
                        this.lblGeneralTechnologyType.Text = this.NewRecommendation.TechnologyType;

                        this.txtGeneralRecommendationText.Text = this.NewRecommendation.RecommendationDescription;
                        this.lblGeneralRecommendationText.Text = this.NewRecommendation.RecommendationDescription;

                        // Detail panel

                        this.txtDetailExistingComponentId.Text = this.NewRecommendation.ExistingComponentId;

                        this._hasExistingComponent = !String.IsNullOrWhiteSpace(this.NewRecommendation.ExistingComponent);
                        this.txtDetailExistingComponent.Text = this.NewRecommendation.ExistingComponent;
                        this.lblDetailExistingComponent.Text = this.NewRecommendation.ExistingComponent;

                        this.txtDetailProposedComponentId.Text = this.NewRecommendation.ProposedComponentId;

                        this._hasProposedComponent = !String.IsNullOrWhiteSpace(this.NewRecommendation.ProposedComponent);
                        this.txtDetailProposedComponent.Text = this.NewRecommendation.ProposedComponent;
                        this.lblDetailProposedComponent.Text = this.NewRecommendation.ProposedComponent;

                        this._hasComponentIds = this.NewRecommendation.HasComponentIds;
                        // As soon as we have both component IDs we can fetch the rebate amount.
                        this.NewRecommendation.TrySetRebateProperties();

                        if (String.IsNullOrWhiteSpace(this.NewRecommendation.Space))
                        {
                            this.cboDetailSpace.Text = "Whole Building";
                        }
                        else
                        {
                            this.cboDetailSpace.Text = this.NewRecommendation.Space;
                        }

                        string tmp = this.NewRecommendation.RebateFormula.ToString().Replace("_", " ");
                        this.cboDetailRebateFormula.Text = tmp;
                        if (tmp == "None" || String.IsNullOrWhiteSpace(tmp))
                        {
                            this._hasRebateFormula = false;
                        }
                        else
                        {
                            this._hasRebateFormula = true;
                        }

                        this.lblDetailRebateFormula.Text = tmp;

                        this.txtDetailQuantity.Value = this.NewRecommendation.Quantity;

                        this._hasQuantityUnit = !String.IsNullOrWhiteSpace(this.NewRecommendation.QuantityUnit);
                        this.txtDetailQuantityUnit.Text = this.NewRecommendation.QuantityUnit;
                        this.lblDetailQuantityUnit.Text = this.NewRecommendation.QuantityUnit;

                        this.txtDetailSize.Value = this.NewRecommendation.Size;

                        this._hasSizeUnit = !String.IsNullOrWhiteSpace(this.NewRecommendation.SizeUnit);
                        this.txtDetailSizeUnit.Text = this.NewRecommendation.SizeUnit;
                        this.lblDetailSizeUnit.Text = this.NewRecommendation.SizeUnit;

                        this._hasRebateAmount = this.NewRecommendation.RebateAmount > 0;
                        this.txtDetailRebateAmount.Value = this.NewRecommendation.RebateAmount;
                        this.lblDetailRebateAmount.Text = this.NewRecommendation.RebateAmount.ToString("C");

                        this._hasKwh = this.NewRecommendation.Kwh > -1;
                        this.txtDetailKwh.Value = this.NewRecommendation.Kwh;
                        this.lblDetailKwh.Text = this.NewRecommendation.Kwh.ToString("#,###");
                        this.lblDetailTotalKwh.Text = this.NewRecommendation.TotalRebate.ToString("#,###");

                        this._hasMcf = this.NewRecommendation.Mcf > -1;
                        this.txtDetailMcf.Value = this.NewRecommendation.Mcf;
                        this.lblDetailMcf.Text = this.NewRecommendation.Mcf.ToString("#,###");
                        this.lblDetailTotalMcf.Text = this.NewRecommendation.TotalMcf.ToString("#,###");

                        this.txtDetailTotalRebate.Value = this.NewRecommendation.TotalRebate;

                        this.txtDetailTotalAnnualSavings.Value = this.NewRecommendation.TotalSavings;
                    }

                    break;
            }
        }

        private void UpdateTotals(bool updateTotalRebate, bool updateTotalSavings, bool updateTotalKwh, bool updateTotalMcf)
        {
            if (this.NewRecommendation != null)
            {
                if (updateTotalKwh)
                {
                    this.lblDetailTotalKwh.Text = this.NewRecommendation.TotalKwh.ToString("#,###");
                }

                if (updateTotalMcf)
                {
                    this.lblDetailTotalMcf.Text = this.NewRecommendation.TotalMcf.ToString("#,###");
                }

                if (updateTotalRebate)
                {
                    this.txtDetailTotalRebate.Value = this.NewRecommendation.TotalRebate;
                }

                if (updateTotalSavings)
                {
                    this.txtDetailTotalAnnualSavings.Value = this.NewRecommendation.TotalSavings;
                }
            }
        }

        private bool ValidateData()
        {
            bool result = true;
            string msg = "";

            if (String.IsNullOrWhiteSpace(this.txtGeneralRecommendationName.Text))
            {
                msg = "Please enter the Recommendation Name.";
                this.txtGeneralRecommendationName.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.cboGeneralTechnologyType.Text))
            {
                msg = "Please select a Technology Type.";
                this.cboGeneralTechnologyType.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtGeneralRecommendationText.Text))
            {
                msg = "Please enter the Recommendation Text.";
                this.txtGeneralRecommendationText.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtDetailExistingComponent.Text) && !this.NewRecommendation.IsCustomRecommendation)
            {
                msg = "Please enter an Existing Component.";
                this.txtDetailExistingComponent.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtDetailProposedComponent.Text))
            {
                msg = "Please enter a Proposed Component.";
                this.txtDetailProposedComponent.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.cboDetailRebateFormula.Text))
            {
                msg = "Please select a Rebate Formula.";
                this.cboDetailRebateFormula.Focus();
            }
            else if (this.cboDetailRebateFormula.Text.ToUpper() == "SIZE BASED" && this.txtDetailSize.Value < 1)
            {
                msg = "You must provide a size for size-based rebate formulas.";
                this.txtDetailSize.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.cboDetailSpace.Text))
            {
                msg = "Please select a Space.";
                this.cboDetailSpace.Focus();
            }

            if (msg != "")
            {
                result = false;
                MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        #endregion Private methods

        private void btnQtyInc_Click(object sender, EventArgs e)
        {
            this.txtDetailQuantity.Value++;
        }

        private void btnQtyDec_Click(object sender, EventArgs e)
        {
            int qty = this.txtDetailQuantity.Value;

            qty--;

            if (qty < 0)
            {
                qty = 0;
            }

            this.txtDetailQuantity.Value = qty;
        }

        private void btnSizeInc_Click(object sender, EventArgs e)
        {
            this.txtDetailSize.Value++;
        }

        private void btnSizeDec_Click(object sender, EventArgs e)
        {
            double qty = this.txtDetailSize.Value;

            qty--;

            if (qty < 0)
            {
                qty = 0;
            }

            this.txtDetailSize.Value = qty;
        }
    }
}