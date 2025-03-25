using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FieldTool.BLL;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmAdditionalRecommendations : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private List<RecommendationLibrary> _items = new List<RecommendationLibrary>();
        private int _nextRank = 0;

        #endregion Private member variables

        #region Constructors

        public frmAdditionalRecommendations()
        {
            InitializeComponent();

            this.SpaceCollection = new List<string>();
        }

        public frmAdditionalRecommendations(string companyId, Building building, List<string> spaceCollection, int lastRank, string programCode)
        {
            InitializeComponent();

            this.CompanyId = (companyId ?? "").Trim();
            this.SelectedBuilding = building;
            this.SpaceCollection = (spaceCollection ?? new List<string>());
            this._nextRank = (lastRank + 1);
            this.ProgramCode = programCode;
        }

        #endregion Constructors

        #region Properties

        private string CompanyId { get; set; }
        private Building SelectedBuilding { get; set; }
        private List<string> SpaceCollection { get; set; }
        private string ProgramCode { get; set; }

        private bool IsGridGrouped
        {
            get
            {
                return (this.grdRecommendations_Sheet1.Models.Data != null && this.grdRecommendations_Sheet1.Models.Data is GroupDataModel);
            }
        }

        public List<RecommendationLibrary> Items
        {
            get
            {
                return this._items;
            }
            set
            {
                this._items = value;
            }
        }

        #endregion Properties

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CancelAndClose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveAndClose();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (RecommendationLibrary recommendation in this.Items)
            {
                recommendation.IncludeInRecommendations = true;
            }

            this.FillGridFromGlobalCollection();
            this.SizeSpreadColumnsToPreferredWidth();
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (RecommendationLibrary recommendation in this.Items)
            {
                recommendation.IncludeInRecommendations = false;
            }

            this.FillGridFromGlobalCollection();
            this.SizeSpreadColumnsToPreferredWidth();
        }

        private void frmAdditionalRecommendations_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.InitializeForm(this.SelectedBuilding, this.ProgramCode);

            Cursor.Current = Cursors.Default;
        }

        private void grdRecommendations_CellClick(object sender, CellClickEventArgs e)
        {
            RecommendationLibrary recommendation = null;

            switch (e.Column)
            {
                case 0:

                    // Include in Recommendation column

                    Cursor.Current = Cursors.WaitCursor;

                    recommendation = this.GetRecommendationLibraryFromSpecifiedRow(e.Row);

                    if (recommendation != null)
                    {
                        recommendation.IncludeInRecommendations = !recommendation.IncludeInRecommendations;
                        this.UpdateRecommendationInGlobalCollection(recommendation);
                        //this.FillGridFromGlobalCollection();
                        //this.SizeSpreadColumnsToPreferredWidth(this.grdRecommendations);
                        this.SetItemsIncludedCountLabel();
                    }

                    Cursor.Current = Cursors.Default;

                    break;

                case 10:

                    // Edit column

                    Cursor.Current = Cursors.WaitCursor;

                    recommendation = this.GetRecommendationLibraryFromSpecifiedRow(e.Row);

                    if (recommendation != null)
                    {
                        if (this.LaunchDetailForm(ref recommendation) == System.Windows.Forms.DialogResult.OK)
                        {
                            this.UpdateRecommendationInGlobalCollection(recommendation);
                            this.FillGridFromGlobalCollection();
                            this.SizeSpreadColumnsToPreferredWidth();
                            //this.ExpandGroupWithRecommendation(recommendation);
                            ((GroupDataModel)this.grdRecommendations_Sheet1.Models.Data).GetGroup(e.Row).Expanded = true;
                        }
                    }

                    Cursor.Current = Cursors.Default;

                    break;

                default:

                    // All other columns.

                    break;
            }
        }

        private void grdRecommendations_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            ((GroupDataModel)this.grdRecommendations_Sheet1.Models.Data).GetGroup(e.Row).Expanded = !((GroupDataModel)this.grdRecommendations_Sheet1.Models.Data).GetGroup(e.Row).Expanded;
        }

        #endregion Events

        #region Private methods

        private void AddCustomRecommendationsToGlobalCollection()
        {
            List<RecommendationLibrary> customRecommendations = DataStore.GetCustomRecommendations();
            this.Items.AddRange(customRecommendations);
        }

        private void AddGenericRecommendationsToGlobalCollection(Building building)
        {
            if (building != null)
            {
                //List<RecommendationLibrary> recLibs = new List<RecommendationLibrary>();
                List<Recommendation> genericRecommendations = building.GetGenericRecommendations();
                foreach (Recommendation rec in genericRecommendations)
                {
                    foreach (RecommendationLibrary existing in this.Items)
                    {
                        if (rec.RecommendationId == existing.RecommendationId)
                        {
                            existing.Update(rec, true);
                        }
                    }
                }

                //foreach (Recommendation r in genericRecommendations) {
                //    recLibs.Add(r.ConvertToRecommendationLibrary(programCode));
                //}

                //foreach (RecommendationLibrary lib in recLibs) {
                //    foreach (RecommendationLibrary existing in this.Items) {
                //        if (existing.RecommendationId == lib.RecommendationId) {
                //            existing.Update(lib);
                //            break;
                //        }
                //    }
                //}
            }
        }

        private void CancelAndClose()
        {
            // this.NewRecommendationCollection = null;

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FillGridFromGlobalCollection()
        {
            this.SuspendLayout();
            this.grdRecommendations.SuspendLayout();

            if (this.IsGridGrouped)
            {
                this.UngroupGrid();
            }

            this.grdRecommendations_Sheet1.Rows.Remove(0, this.grdRecommendations_Sheet1.Rows.Count);

            if (this.Items != null && this.Items.Count > 0)
            {
                this.grdRecommendations_Sheet1.AutoGenerateColumns = false;
                this.grdRecommendations_Sheet1.Rows.Add(0, this.Items.Count);

                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].RecommendationId == "CUS1")
                    {
                        this.grdRecommendations_Sheet1.Cells[i, 0].CellType = new TextCellType();               // Include, not available on the Custom row.
                        this.grdRecommendations_Sheet1.Cells[i, 1].Value = this.Items[i].TechnologyType;        // TechnologyType
                        this.grdRecommendations_Sheet1.Cells[i, 2].Value = this.Items[i].RecommendationName;    // RecommendationName
                        this.grdRecommendations_Sheet1.AddSpanCell(i, 2, 1, 7);
                        this.grdRecommendations_Sheet1.Cells[i, 2].ColumnSpan = 7;

                        FarPoint.Win.Spread.CellType.ButtonCellType editButton = new ButtonCellType();
                        editButton.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;

                        editButton.Text = "Add";                                                                // Edit
                        this.grdRecommendations_Sheet1.Cells[i, 10].CellType = editButton;
                        this.grdRecommendations_Sheet1.Cells[i, 10].Tag = this.Items[i];

                        this.grdRecommendations_Sheet1.Cells[i, 11].Value = this.Items[i].SortIndex;        // SortIndex (hidden)
                        this.grdRecommendations_Sheet1.Cells[i, 12].Value = this.Items[i].RecommendationId; // RecommendationId (hidden)
                    }
                    else
                    {
                        this.grdRecommendations_Sheet1.Cells[i, 0].Value = this.Items[i].IncludeInRecommendations;  // Include
                        this.grdRecommendations_Sheet1.Cells[i, 1].Value = this.Items[i].TechnologyType;            // TechnologyType
                        this.grdRecommendations_Sheet1.Cells[i, 2].Value = this.Items[i].RecommendationName;        // RecommendationName
                        this.grdRecommendations_Sheet1.Cells[i, 3].Value = this.Items[i].ProgramID;
                        this.grdRecommendations_Sheet1.Cells[i, 4].Value = this.Items[i].Quantity;      // Qty
                        this.grdRecommendations_Sheet1.Cells[i, 5].Value = this.Items[i].Size;          // Size
                        this.grdRecommendations_Sheet1.Cells[i, 6].Value = this.Items[i].TotalKwh;           // kWh
                        this.grdRecommendations_Sheet1.Cells[i, 7].Value = this.Items[i].TotalMcf;           // MCF
                        this.grdRecommendations_Sheet1.Cells[i, 8].Value = this.Items[i].TotalSavings;  // $ Annual
                        this.grdRecommendations_Sheet1.Cells[i, 9].Value = this.Items[i].TotalRebate;   // $ Rebate

                        FarPoint.Win.Spread.CellType.ButtonCellType editButton = new ButtonCellType();
                        editButton.ButtonColor2 = System.Drawing.SystemColors.ButtonFace;

                        editButton.Text = "Edit";                                                       // Edit
                        this.grdRecommendations_Sheet1.Cells[i, 10].CellType = editButton;
                        this.grdRecommendations_Sheet1.Cells[i, 10].Tag = this.Items[i];

                        this.grdRecommendations_Sheet1.Cells[i, 11].Value = this.Items[i].SortIndex;        // SortIndex (hidden)
                        this.grdRecommendations_Sheet1.Cells[i, 12].Value = this.Items[i].RecommendationId; // RecommendationId (hidden)
                    }
                }

                if (this.grdRecommendations_Sheet1.Models.Data != null)
                {
                    GroupDataModel gm = new GroupDataModel(this.grdRecommendations_Sheet1.Models.Data);

                    if (this.grdRecommendations_Sheet1.Models.Data.GetType() == typeof(FarPoint.Win.Spread.Model.DefaultSheetDataModel))
                    {
                        SortInfo[] si = new SortInfo[] { new SortInfo(1, true) };
                        gm.Group(si);

                        try
                        {
                            this.grdRecommendations_Sheet1.Models.Data = gm;
                            this.grdRecommendations_Sheet1.GroupingPolicy = GroupingPolicy.CollapseAll;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    // Parse the group name so only the name part is displayed.
                    for (int i = 0; i < gm.RowCount; i++)
                    {
                        if (gm.IsGroup(i))
                        {
                            Group g = gm.GetGroup(i);

                            string s = gm.GetValue(i, g.Column).ToString();
                            string[] oldText = s.Split(":".ToCharArray());

                            g.Text = oldText[1].ToString();
                        }
                        else
                        {
                            if (gm.GetValue(i, 11).ToString() == "CUS1")
                            {
                                this.grdRecommendations_Sheet1.AddSpanCell(i, 2, 1, 7);
                            }
                        }
                    }
                }
            }

            this.SetItemsIncludedCountLabel();

            this.grdRecommendations.ResumeLayout();
            this.ResumeLayout();
        }

        private RecommendationLibrary GetRecommendationLibraryFromSpecifiedRow(int rowIndex)
        {
            RecommendationLibrary result = null;

            object o = this.grdRecommendations_Sheet1.Cells[rowIndex, 10].Tag;

            if (o != null)
            {
                RecommendationLibrary r = o as RecommendationLibrary;

                if (r != null)
                {
                    result = r;
                }
            }

            return result;
        }

        private void InitializeForm(Building building, string programCode)
        {
            List<RecommendationLibraryParameter> parameters = DataStore.GetAllRecommendationLibraryParameters();

            //Add custom recommendation option to the recommendation library
            var customParam = new RecommendationLibraryParameter();
            customParam.RecommendationId = "CUS1";
            customParam.RecommendationType = "Custom";
            customParam.SortIndex = 1000;
            customParam.TechnologyType = "Custom";
            customParam.ProgramID = programCode;
            //customParam.RecommendationName = "*Add new custom recommendation*";
            customParam.TechnologyType = "Custom";

            parameters.Add(customParam);
            this.Items = RecommendationLibrary.ConvertFromParameterCollection(parameters, programCode);

            this.AddCustomRecommendationsToGlobalCollection();
            this.AddGenericRecommendationsToGlobalCollection(building);
            this.FillGridFromGlobalCollection();
            this.SizeSpreadColumnsToPreferredWidth();
        }

        private DialogResult LaunchDetailForm(ref RecommendationLibrary recommendation)
        {
            DialogResult result = System.Windows.Forms.DialogResult.Cancel;

            if (recommendation != null)
            {
                using (frmRecommendationLibraryDetails frm = new frmRecommendationLibraryDetails(recommendation, this.SpaceCollection, this.Items.Count))
                {
                    result = frm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        recommendation = new RecommendationLibrary(frm.OriginalRecommendation);
                    }
                }
            }

            return result;
        }

        private void SaveAndClose()
        {
            if (this.ValidateData())
            {
                this.SaveCustomRecommendations();

                foreach (RecommendationLibrary recommendation in this.Items)
                {
                    if (recommendation.IncludeInRecommendations)
                    {
                        this.SelectedBuilding.AddRecommendation(new Recommendation(recommendation, this._nextRank));
                        this._nextRank++;
                    }
                    else
                    {
                        this.SelectedBuilding.RemoveRecommendationByRecommendationId(recommendation.RecommendationId);
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void SaveCustomRecommendations()
        {
            List<RecommendationLibrary> customRecommendations = new List<RecommendationLibrary>();

            foreach (RecommendationLibrary recommendation in this.Items)
            {
                if (recommendation.IsCustomRecommendation && recommendation.RecommendationId != "CUS1")
                {
                    var newRec = new RecommendationLibrary(recommendation);
                    newRec.IncludeInRecommendations = false;  //Set to false when saving to the library
                    customRecommendations.Add(newRec);
                }
            }

            DataStore.SaveCustomRecommendations(customRecommendations);
        }

        private void SetItemsIncludedCountLabel()
        {
            if (this.Items == null)
            {
                this.lblItemCount.Text = "Included items: 0";
            }
            else
            {
                this.lblItemCount.Text = "Included items: " + this.Items.FindAll(x => x.IncludeInRecommendations).Count.ToString();
            }
        }

        private void SizeSpreadColumnsToPreferredWidth()
        {
            this.SuspendLayout();
            this.grdRecommendations.SuspendLayout();

            for (int i = 0; i < this.grdRecommendations_Sheet1.Columns.Count; i++)
            {
                this.grdRecommendations_Sheet1.Columns[i].Width = this.grdRecommendations_Sheet1.GetPreferredColumnWidth(i, false, false, false);
            }

            for (int i = 0; i < this.grdRecommendations_Sheet1.Rows.Count; i++)
            {
                this.grdRecommendations_Sheet1.Rows[i].Height = 50;
            }

            this.grdRecommendations.ResumeLayout();
            this.ResumeLayout();
        }

        private void UngroupGrid()
        {
            try
            {
                if (this.IsGridGrouped)
                {
                    GroupDataModel gm = this.grdRecommendations_Sheet1.Models.Data as GroupDataModel;

                    int n = gm.SortInfo.Length;
                    SortInfo[] tmp = new SortInfo[n - 1];
                    int i = 0;
                    while (i < n - 1)
                    {
                        tmp[i] = gm.SortInfo[i];
                        i = i + 1;
                    }
                    n = n - 1;
                    if (n >= 1)
                    {
                        GroupingEventArgs fe = new GroupingEventArgs(tmp);
                        if (!fe.Cancel)
                        {
                            gm.Group(tmp, fe.GroupComparer);
                        }
                        return;
                    }
                    else
                    {
                        this.grdRecommendations_Sheet1.Models.Data = ((GroupDataModel)this.grdRecommendations_Sheet1.Models.Data).TargetModel;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void UpdateRecommendationInGlobalCollection(RecommendationLibrary recommendation)
        {
            bool found = false;

            if (recommendation != null && this.Items != null)
            {
                foreach (RecommendationLibrary rec in this.Items)
                {
                    if (rec.RecommendationId == recommendation.RecommendationId)
                    {
                        found = true;
                        rec.Update(recommendation);

                        break;
                    }
                }

                if (!found)
                {
                    // New recommendation.
                    this.Items.Add(recommendation);
                    this.grdRecommendations_Sheet1.Rows.Add(this.grdRecommendations_Sheet1.Rows.Count, 1);
                }
            }
        }

        private bool ValidateData()
        {
            bool result = true;
            string msg = "";

            if (msg != "")
            {
                result = false;
                MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        #endregion Private methods
    }
}