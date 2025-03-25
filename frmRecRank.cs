using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FieldTool.BLL;
using FieldTool.Constants.Helpers;
using FieldTool.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmRecRank : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private bool _cancelClose = false;

        #endregion Private member variables

        #region Constructors

        public frmRecRank(Recommendation.RecommendationCollection recommendations)
        {
            InitializeComponent();

            if (recommendations == null)
            {
                this.Recommendations = new Recommendation.RecommendationCollection();
            }
            else
            {
                this.Recommendations = new Recommendation.RecommendationCollection(recommendations);
            }
        }

        #endregion Constructors

        #region Properties

        public Recommendation.RecommendationCollection Recommendations { get; set; }

        #endregion Properties

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRecommendationRankings();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            MoveItemUp();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            MoveItemDown();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetIncludeChecked(true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            SetIncludeChecked(false);
        }

        private void frmRecRank_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._cancelClose)
            {
                this._cancelClose = false;
                e.Cancel = true;
            }
        }

        private void frmRecRank_Load(object sender, EventArgs e)
        {

            this.LoadList(false);
            this.SetUpDownButtonsEnabled();
            this.SetListColumnWidths();

        }

        private void fpSpreadRecommendations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetUpDownButtonsEnabled();
            if (e.Range.RowCount > 1)
            {
                fpSpreadRecommendations_Sheet1.ClearSelection();
            }
            SetRowSelection(e.Range.Row, false);
        }

        private void fpSpreadRecommendations_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            using (DrawingContext context = new DrawingContext(this))
            {
                if (e.Column == RecommendationRankColumns.IncludeInReport)
                {
                    if (!GetGridValue<bool>(e.Row, RecommendationRankColumns.IncludeInReport))
                    {
                        fpSpreadRecommendations_Sheet1.Cells[e.Row, RecommendationRankColumns.IncludeDetailInReport].Value = false;
                        //fpSpreadRecommendations_Sheet1.Cells[e.Row, RecommendationRankColumns.IncludeDetailInReport].Locked = true;
                        Recommendation recommendation = GetRecommendationById(GetGridValue<string>(e.Row, RecommendationRankColumns.RecommendationId));
                        recommendation.IncludeDetailInReport = false;
                    }
                    else
                    {
                        fpSpreadRecommendations_Sheet1.Cells[e.Row, RecommendationRankColumns.IncludeDetailInReport].Locked = false;
                    }

                    RerankItems(GetRecommendationById(GetGridValue<string>(e.Row, RecommendationRankColumns.RecommendationId)), true);
                }
                else if (e.Column == RecommendationRankColumns.IncludeDetailInReport)
                {
                    if (!GetGridValue<bool>(e.Row, RecommendationRankColumns.IncludeInReport))
                    {
                        fpSpreadRecommendations_Sheet1.Cells[e.Row, RecommendationRankColumns.IncludeDetailInReport].Value = false;
                    }
                }

                if (e.Column == RecommendationRankColumns.IncludeInReport
                    || e.Column == RecommendationRankColumns.IncludeDetailInReport)
                {
                    btnSave.Enabled = true;
                }
            }
        }

        private void fpSpreadRecommendations_CellClick(object sender, CellClickEventArgs e)
        {
            using (DrawingContext context = new DrawingContext(this))
            {
                CellRange selectedRange = fpSpreadRecommendations_Sheet1.GetSelection(0);
                if (selectedRange != null && selectedRange.RowCount > 0 && selectedRange.Row != e.Row)
                {
                    fpSpreadRecommendations_Sheet1.ClearSelection();
                }
                SetRowSelection(e.Row, false);
                if (e.Column == RecommendationRankColumns.RecommendationName) SetUpDownButtonsEnabled();
            }
        }

        private void moveSelectedRecommendationDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveItemDown();
        }

        private void moveSelectedRecommendationUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveItemUp();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetIncludeChecked(true);
        }

        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetIncludeChecked(false);
        }

        #endregion Events

        #region Private helper methods

        private int GetIndexByRecommendation(Recommendation recommendation)
        {
            int result = -1;
            if (recommendation != null)
            {
                Row row = GetRowsList().FirstOrDefault(x => GetGridValue<string>(x.Index, RecommendationRankColumns.RecommendationId) == recommendation.RecommendationId);
                result = row?.Index ?? -1;
            }
            return result;
        }

        private int GetIndexOfFirstNonIncludedRecommendation()
        {
            Recommendation rec = Recommendations.Where(x => !x.IncludeInReport).OrderBy(x => x.ReportRank).FirstOrDefault();
            return GetIndexByRecommendation(rec);
        }

        private int GetIndexOfLastIncludedRecommendation()
        {
            Recommendation rec = Recommendations.Where(x => x.IncludeInReport).OrderByDescending(x => x.ReportRank).FirstOrDefault();
            return GetIndexByRecommendation(rec);
        }

        private Recommendation GetRecommendationByRank(int rank)
        {
            Recommendation result = null;

            if (rank > 0 && rank <= fpSpreadRecommendations_Sheet1.RowCount)
            {
                foreach (Recommendation r in this.Recommendations)
                {
                    if (r.ReportRank == rank)
                    {
                        result = r;
                        break;
                    }
                }
            }

            return result;
        }

        private Recommendation GetRecommendationById(string recommendationId)
        {
            return Recommendations.FirstOrDefault(x => x.RecommendationId == recommendationId);
        }

        private Recommendation GetRecommendationByIndex(int index)
        {
            return Recommendations.FirstOrDefault(x => x.RecommendationId == GetGridValue<string>(index, RecommendationRankColumns.RecommendationId));
        }

        private Recommendation GetSelectedRecommendation()
        {
            Recommendation result = null;

            //int? selectedRow = fpSpreadRecommendations_Sheet1.GetSelection(0)?.Row;

            int? selectedRow = fpSpreadRecommendations_Sheet1.ActiveRow.Index;

            if (selectedRow.HasValue && selectedRow.Value < Recommendations.Items.Count)
            {
                result = Recommendations.Items.FirstOrDefault(x => x.RecommendationId == GetGridValue<string>(selectedRow.Value, RecommendationRankColumns.RecommendationId));
            }

            return result;
        }

        private void LoadList()
        {
            this.LoadList(null, true);
        }

        private void LoadList(bool saveEnabled)
        {
            this.LoadList(null, saveEnabled);
        }

        private void LoadList(Recommendation selectedRecommendation, bool saveEnabled)
        {

            using (DrawingContext context = new DrawingContext(this))
            {
                LoadSpreadGrid(selectedRecommendation);

                this.btnSave.Enabled = saveEnabled;
            }

        }

        private void LoadSpreadGrid(Recommendation selectedRecommendation = null)
        {
            fpSpreadRecommendations_Sheet1.AutoGenerateColumns = false;
            fpSpreadRecommendations.DataSource = Recommendations.Items;
            fpSpreadRecommendations_Sheet1.RowCount = Recommendations.Count;
            fpSpreadRecommendations_Sheet1.DataAutoCellTypes = false;
            fpSpreadRecommendations_Sheet1.DataAutoSizeColumns = false;

            fpSpreadRecommendations_Sheet1.BindDataColumn(RecommendationRankColumns.IncludeInReport, "IncludeInReport");
            fpSpreadRecommendations_Sheet1.BindDataColumn(RecommendationRankColumns.IncludeDetailInReport, "IncludeDetailInReport");
            fpSpreadRecommendations_Sheet1.BindDataColumn(RecommendationRankColumns.RecommendationName, "RecommendationName");
            fpSpreadRecommendations_Sheet1.BindDataColumn(RecommendationRankColumns.ReportRank, "ReportRank");
            fpSpreadRecommendations_Sheet1.BindDataColumn(RecommendationRankColumns.RecommendationId, "RecommendationId");

            SortGrid();
            if (selectedRecommendation != null)
            {
                SetSelectedRecommendation(selectedRecommendation.RecommendationId);
            }

            SetSelectButtonsEnabled();
            SetUpDownButtonsEnabled();
            SetIncludeDetailCheckboxesEnabled();
            SetCheckboxCellTypes();

            if (fpSpreadRecommendations_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < fpSpreadRecommendations_Sheet1.Rows.Count; i++)
                {
                    if (fpSpreadRecommendations_Sheet1.Columns != null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType textcell = new FarPoint.Win.Spread.CellType.TextCellType();
                        textcell.WordWrap = true;
                        textcell.Multiline = true;
                        textcell.ScrollBars = ScrollBars.Vertical;

                        fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.RecommendationName].CellType = textcell;
                    }
                }
            }
        }

        private void SetCheckboxCellTypes()
        {
            using (DrawingContext context = new DrawingContext(this))
            {
                ICellType cellType = fpSpreadRecommendations_Sheet1.Cells.Get(0, 0).CellType;
                for (int i = 0; i < fpSpreadRecommendations_Sheet1.RowCount; i++)
                {
                    fpSpreadRecommendations_Sheet1.Cells.Get(i, RecommendationRankColumns.IncludeInReport).CellType = cellType;
                    fpSpreadRecommendations_Sheet1.Cells.Get(i, RecommendationRankColumns.IncludeDetailInReport).CellType = cellType;
                }
            }
        }

        private void MoveItemDown()
        {
            Recommendation selectedRec = this.GetSelectedRecommendation();

            if (selectedRec != null && selectedRec.ReportRank < this.Recommendations.Count)
            {
                Recommendation nextRecommendation = this.GetRecommendationByRank(selectedRec.ReportRank + 1);

                selectedRec.ReportRank++;

                if (nextRecommendation != null)
                {
                    nextRecommendation.ReportRank--;
                }
                fpSpreadRecommendations_Sheet1.SetActiveCell(nextRecommendation.ReportRank, RecommendationRankColumns.RecommendationName);
                LoadList(selectedRec, true);
            }
        }

        private void MoveItemUp()
        {
            Recommendation selectedRec = this.GetSelectedRecommendation();

            if (selectedRec != null && selectedRec.ReportRank > 0)
            {
                Recommendation nextRecommendation = this.GetRecommendationByRank(selectedRec.ReportRank - 1);

                selectedRec.ReportRank--;

                if (nextRecommendation != null)
                {
                    nextRecommendation.ReportRank++;
                }

                fpSpreadRecommendations_Sheet1.SetActiveCell(selectedRec.ReportRank - 1, RecommendationRankColumns.RecommendationName);
                LoadList(selectedRec, true);
            }
        }

        private void RerankItems(Recommendation targetRecommendation, bool refreshList)
        {
            if (targetRecommendation != null)
            {
                int newRank = -1;

                int indexOfLastIncluded = GetIndexOfLastIncludedRecommendation();
                int indexOfFirstNonIncluded = GetIndexOfFirstNonIncludedRecommendation();

                Recommendation lastIncludedRec = GetRecommendationByIndex(indexOfLastIncluded);
                Recommendation firstNonIncludedRec = GetRecommendationByIndex(indexOfFirstNonIncluded);

                // Set variable to hold the action to perform in this method.  If the recommendation was already included, exclude it, and vice versa.
                bool excludeTarget = targetRecommendation.IncludeInReport;

                // Flip include status of target
                targetRecommendation.IncludeInReport = !targetRecommendation.IncludeInReport;

                if (excludeTarget)
                {
                    // Get rank of the last item
                    newRank = GetRecommendationByIndex(fpSpreadRecommendations_Sheet1.RowCount - 1).ReportRank;

                    // Decrement rank of items that were 'after' target
                    Recommendations.Where(x => x.ReportRank > targetRecommendation.ReportRank).ForEach(x => x.ReportRank--);

                    // Set rank of target to the rank of the last included item
                    targetRecommendation.ReportRank = newRank;
                }
                else
                {
                    // Set target rank to (rank of last included item) + 1.  If there aren't any included items, set to rank of first excluded item.
                    newRank = DataHelper.GetPropertyIfNotNull<Recommendation, int>(lastIncludedRec, x => x.ReportRank + 1,
                        DataHelper.GetPropertyIfNotNull<Recommendation, int>(firstNonIncludedRec, x => x.ReportRank, 1));

                    // Increment rank of excluded items that were 'before' target
                    Recommendations.Where(x => !x.IncludeInReport && x.ReportRank < targetRecommendation.ReportRank).ForEach(x => x.ReportRank++);

                    // Set target rank
                    targetRecommendation.ReportRank = newRank;
                }

                if (refreshList)
                {
                    LoadList();
                }
            }
        }

        private void SaveRecommendationRankings()
        {
            DialogResult prompt = MessageBox.Show("Do you wish to save these rankings?", "Save Recommendation Rankings",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (prompt == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else if (prompt == System.Windows.Forms.DialogResult.No)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                // Do nothing
                this._cancelClose = true;
            }
        }

        private void SetIncludeChecked(bool value)
        {

            using (DrawingContext context = new DrawingContext(this))
            {
                foreach (Recommendation rec in this.Recommendations)
                {
                    rec.IncludeInReport = value;
                    if (!value)
                    {
                        rec.IncludeDetailInReport = false;
                    }
                }
                SetIncludeDetailCheckboxesEnabled();
            }

        }

        private void SetListColumnWidths()
        {
            int includeWidth = 100;
            int includeDetailWidth = 100;
            int scrollbarWidth = 100;

            fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.IncludeInReport].Width = includeWidth;
            fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.IncludeDetailInReport].Width = includeDetailWidth;
            fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.ReportRank].Width = 0;
            fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.RecommendationId].Width = 0;
            fpSpreadRecommendations_Sheet1.Columns[RecommendationRankColumns.RecommendationName].Width = fpSpreadRecommendations.Width - includeWidth - includeDetailWidth - scrollbarWidth;
        }

        private void SetSelectButtonsEnabled()
        {
            var includedRows = GetIncludedRowsList();
            if (includedRows.Count == 0)
            {
                // No items are checked.
                btnDeselectAll.Enabled = false;
                btnSelectAll.Enabled = true;
                unselectAllToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = true;
            }
            else if (includedRows.Count == fpSpreadRecommendations_Sheet1.RowCount)
            {
                // All items are checked.
                btnDeselectAll.Enabled = true;
                btnSelectAll.Enabled = false;
                unselectAllToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = false;
            }
            else
            {
                // Some items are checked; others are not.
                btnDeselectAll.Enabled = true;
                btnSelectAll.Enabled = true;
                unselectAllToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
        }

        private void SetUpDownButtonsEnabled()
        {
            int itemCount = fpSpreadRecommendations_Sheet1.RowCount;
            int selectedItemCount = 0;
            int selectedItemIndex = 0;
            int indexOfFirstIncluded = 0;
            int indexOfLastIncluded = this.GetIndexOfLastIncludedRecommendation();
            int indexOfFirstNonIncluded = this.GetIndexOfFirstNonIncludedRecommendation();
            int indexOfLastNonIncluded = itemCount - 1;

            if (fpSpreadRecommendations_Sheet1.SelectionCount > 0)
            {
                selectedItemCount = fpSpreadRecommendations_Sheet1.SelectionCount;

                if (selectedItemCount > 0)
                {
                    selectedItemIndex = fpSpreadRecommendations_Sheet1.GetSelection(0).Row;
                }
            }

            if (itemCount == 0 || itemCount == 1)
            {
                // There is either 0 or 1 item in the list.
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                moveSelectedRecommendationUpToolStripMenuItem.Enabled = false;
                moveSelectedRecommendationDownToolStripMenuItem.Enabled = false;
            }
            else
            {
                if (selectedItemCount == 0)
                {
                    // There are items in the list, but nothing selected.
                    btnUp.Enabled = false;
                    btnDown.Enabled = false;
                    moveSelectedRecommendationUpToolStripMenuItem.Enabled = false;
                    moveSelectedRecommendationDownToolStripMenuItem.Enabled = false;
                }
                else
                {
                    // There are at least two items in the list and one is selected (this is a not a MultiSelect listbox).

                    btnUp.Enabled = (selectedItemIndex > indexOfFirstIncluded && selectedItemIndex != indexOfFirstNonIncluded);
                    btnDown.Enabled = (selectedItemIndex != indexOfLastIncluded && selectedItemIndex != indexOfLastNonIncluded);

                    moveSelectedRecommendationUpToolStripMenuItem.Enabled = this.btnUp.Enabled;
                    moveSelectedRecommendationDownToolStripMenuItem.Enabled = this.btnDown.Enabled;
                }
            }
        }

        private void SetIncludeDetailCheckboxesEnabled()
        {
            for (int i = 0; i < fpSpreadRecommendations_Sheet1.RowCount; i++)
            {
                bool includeInReport = GetGridValue<bool>(i, RecommendationRankColumns.IncludeInReport);
                //fpSpreadRecommendations_Sheet1.Cells[i, RecommendationRankColumns.IncludeDetailInReport].Locked = !includeInReport;
            }
        }

        private List<Row> GetRowsList()
        {
            List<Row> rows = new List<Row>();
            foreach (Row row in fpSpreadRecommendations_Sheet1.Rows)
            {
                rows.Add(row);
            }

            return rows;
        }

        private List<Row> GetIncludedRowsList()
        {
            return GetRowsList().Where(x => RecommendationRowIsIncludedInReport(x)).ToList();
        }

        private bool RecommendationRowIsIncludedInReport(Row row)
        {
            return RecommendationRowIsIncludedInReport(row.Index);
        }

        private bool RecommendationRowIsIncludedInReport(int rowIndex)
        {
            return GetGridValue<bool>(rowIndex, RecommendationRankColumns.IncludeInReport);
        }

        private T GetGridValue<T>(int rowIndex, int colIndex)
        {
            if (rowIndex < 0 || rowIndex >= fpSpreadRecommendations_Sheet1.RowCount
                || colIndex < 0 || colIndex >= fpSpreadRecommendations_Sheet1.ColumnCount)
            {
                return default(T);
            }

            return (T)fpSpreadRecommendations_Sheet1.Cells[rowIndex, colIndex].Value;
        }

        private void SortGrid()
        {
            fpSpreadRecommendations_Sheet1.SortRows(RecommendationRankColumns.ReportRank, true, true);
        }

        private void SetSelectedRecommendation(string recommendationId)
        {
            int? rowIndex = GetRowsList().FirstOrDefault(x => GetGridValue<string>(x.Index, RecommendationRankColumns.RecommendationId) == recommendationId)?.Index;

            if (rowIndex.HasValue)
            {
                SetRowSelection(rowIndex.Value);
            }
        }

        private void SetRowSelection(int rowIndex, bool clearPreviousSelection = true)
        {
            if (clearPreviousSelection)
            {
                fpSpreadRecommendations_Sheet1.ClearSelection();
            }
            fpSpreadRecommendations_Sheet1.AddSelection(rowIndex, 0, 1, fpSpreadRecommendations_Sheet1.ColumnCount);
        }

        #endregion Private helper methods

        public static class RecommendationRankColumns
        {
            public static int IncludeInReport = 0;
            public static int IncludeDetailInReport = 1;
            public static int RecommendationName = 2;
            public static int RecommendationId = 3;
            public static int ReportRank = 4;
        }
    }
}