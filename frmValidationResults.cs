using FieldTool.Bsi.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmValidationResults : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private List<BsiSearchResult> _searchResults = null;

        #endregion Private member variables

        #region Constructors

        public frmValidationResults()
        {
            InitializeComponent();

            this._searchResults = new List<BsiSearchResult>();
        }

        public frmValidationResults(List<BsiSearchResult> searchResults)
        {
            InitializeComponent();

            if (searchResults == null)
            {
                this._searchResults = new List<BsiSearchResult>();
            }
            else
            {
                this._searchResults = searchResults;
            }
        }

        #endregion Constructors

        #region Properties

        public BsiSearchResult SelectedResult { get; private set; }

        #endregion Properties

        #region Events

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.CancelAndClose();

            Cursor.Current = Cursors.Default;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SaveAndClose();

            Cursor.Current = Cursors.Default;
        }

        private void frmValidationResults_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.InitializeForm();
            this.LoadListFromGlobal();

            Cursor.Current = Cursors.Default;
        }

        private void lstResults_DoubleClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            this.SaveAndClose();

            Cursor.Current = Cursors.Default;
        }

        #endregion Events

        #region Private helper methods

        private void CancelAndClose()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void InitializeForm()
        {
            this.SetColumnWidths();
        }

        private void LoadListFromGlobal()
        {
            this.lstResults.Items.Clear();

            if (this._searchResults != null)
            {
                foreach (BsiSearchResult item in this._searchResults)
                {
                    ListViewItem li = new ListViewItem(item.ToListViewItemsArray());

                    li.Tag = item;

                    this.lstResults.Items.Add(li);
                }

                this.SetColumnWidths();
                this.UpdateCount();
            }
        }

        private void SaveAndClose()
        {
            if (this.ValidateData())
            {
                object o = this.lstResults.SelectedItems[0].Tag;
                BsiSearchResult r = o as BsiSearchResult;

                if (r != null)
                {
                    this.SelectedResult = r;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void SetColumnWidths()
        {
            // http://stackoverflow.com/questions/14133225/listview-autoresizecolumns-based-on-both-column-content-and-header
            //  Answer dated 11/29/13 (matsolof) (modifed variable names)

            this.lstResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            ListView.ColumnHeaderCollection cols = lstResults.Columns;

            for (int i = 0; i < cols.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cols[i].Text, lstResults.Font).Width + 10;

                if (colWidth > cols[i].Width)
                {
                    cols[i].Width = colWidth;
                }
            }

            this.colId.Width = 0;
            this.colExternalId.Width = 0;
        }

        private void UpdateCount()
        {
            if (this.lstResults.Items.Count == 0)
            {
                this.lblCount.Text = "No items found.";
            }
            else
            {
                this.lblCount.Text = "Items: " + this.lstResults.Items.Count.ToString();
            }
        }

        private bool ValidateData()
        {
            bool result = false;
            string msg = "";

            if (this.lstResults.SelectedItems.Count == 0)
            {
                msg = "Please select a company to validate against.";
            }

            if (msg == "")
            {
                result = true;
            }
            else
            {
                result = false;
                MessageBox.Show(msg, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        #endregion Private helper methods
    }
}