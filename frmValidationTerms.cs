using FieldTool.BLL;
using FieldTool.Bsi.Helpers;
using FieldTool.Bsi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmValidationTerms : DevComponents.DotNetBar.Metro.MetroForm
    {
        #region Private member variables

        private bool _isColumnWidthsSet = false;
        private bool _fireAccountSearchCheck = true;

        private List<Company> _companyCollection = null;
        private IBsiService _bsiService = null;

        #endregion Private member variables

        #region Constructors

        public frmValidationTerms()
        {
            InitializeComponent();

            this._bsiService = new BsiService(ConfigurationManager.AppSettings);
            this._companyCollection = new List<Company>();
        }

        public frmValidationTerms(List<Company> companyCollection, IBsiService bsiService)
        {
            InitializeComponent();

            if (bsiService == null)
            {
                this._bsiService = new BsiService(ConfigurationManager.AppSettings);
            }
            else
            {
                this._bsiService = bsiService;
            }

            if (companyCollection == null)
            {
                this._companyCollection = new List<Company>();
            }
            else
            {
                this._companyCollection = companyCollection;
            }
        }

        #endregion Constructors

        #region Properties

        private Company CurrentCompany { get; set; }

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ValidateTerms())
            {
                Cursor.Current = Cursors.WaitCursor;

                BsiSearchTerms searchTerms = this.GetSearchTermsFromControls();
                List<BsiSearchResult> results = new List<BsiSearchResult>();
                try
                {
                    results = this._bsiService.SearchAccounts(searchTerms);
                }
                catch (Exception)
                {
                    // allow the bsi exception to be ignored
                }

                if (results == null || results.Count == 0)
                {
                    MessageBox.Show("Unable to find a company with those search terms.", "Validate Company",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (frmValidationResults frm = new frmValidationResults(results))
                    {
                        DialogResult result = frm.ShowDialog(this);

                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            BsiSearchResult selectedCompany = frm.SelectedResult;

                            if (selectedCompany != null)
                            {
                                this._companyCollection.Remove(this.CurrentCompany);
                                this.UpdateCurrentCompany(selectedCompany);
                                DataStore.UpdateCompany(this.CurrentCompany, true);

                                this.LoadAllData();

                                this.CheckForCompletion();
                            }
                        }
                    }
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void chbElectricAccountNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (this._fireAccountSearchCheck)
            {
                this._fireAccountSearchCheck = false;

                if (this.chbElectricAccountNumber.Checked)
                {
                    this.chbGasAccountNumber.BeginUpdate();
                    this.chbGasAccountNumber.Checked = false;
                    this.chbGasAccountNumber.EndUpdate();
                }

                this._fireAccountSearchCheck = true;
            }
        }

        private void chbGasAccountNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (this._fireAccountSearchCheck)
            {
                this._fireAccountSearchCheck = false;

                if (this.chbGasAccountNumber.Checked)
                {
                    this.chbElectricAccountNumber.BeginUpdate();
                    this.chbElectricAccountNumber.Checked = false;
                    this.chbElectricAccountNumber.EndUpdate();
                }

                this._fireAccountSearchCheck = true;
            }
        }

        private void chbUtility_CheckedChanging(object sender, DevComponents.DotNetBar.Controls.CheckBoxXChangeEventArgs e)
        {
            // The Utility field is required and cannot be unselected.
            e.Cancel = true;
        }

        private void frmValidationTerms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.CancelAndClose();
            }
        }

        private void frmValidationTerms_Load(object sender, EventArgs e)
        {
            this.InitializeForm();
            this.LoadAllData();
        }

        private void lstItemsToValidate_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LoadItemToControls(this.lstItemsToValidate.SelectedIndices[0]);
        }

        #endregion Events

        #region Private helper methods

        private void CancelAndClose()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void CheckForCompletion()
        {
            if (this._companyCollection.Count == 0)
            {
                this.SaveAndClose();
            }
        }

        private BsiSearchTerms GetSearchTermsFromControls()
        {
            BsiSearchTerms result = new BsiSearchTerms();

            if (this.chbElectricAccountNumber.Checked)
            {
                result.accountNumber = this.txtElectricAccountNumber.Text;
            }
            else if (chbGasAccountNumber.Checked)
            {
                result.accountNumber = this.txtGasAccountNumber.Text;
            }

            if (this.chbCompanyName.Checked)
            {
                result.customerName = this.txtCompany.Text;
            }

            if (this.chbAddress.Checked)
            {
                result.streetName = this.txtAddress.Text;
            }

            if (this.chbCity.Checked)
            {
                result.cityName = this.txtCity.Text;
            }

            if (this.chbState.Checked)
            {
                ;
            }

            if (this.chbZip.Checked)
            {
                ;
            }

            if (this.chbUtility.Checked)
            {
                result.clientName = this.txtUtility.Text;
            }

            if (this.chbProgram.Checked)
            {
                ;
            }

            return result;
        }

        private void InitializeForm()
        {
            this.SuspendLayout();

            this.LoadDropDowns();

            this.ResumeLayout();
        }

        private void LoadAllData()
        {
            this.LoadToDoList();

            if (this.lstItemsToValidate.Items.Count > 0)
            {
                this.LoadItemToControls(0);
            }
        }

        private void LoadDropDowns()
        {
            this.cboState.SuspendLayout();

            this.cboState.Items.Clear();

            this.cboState.Items.Add("MI");
            this.cboState.Items.Add("WI");

            this.cboState.ResumeLayout();
        }

        private void LoadItemToControls(int index)
        {
            if (index < 0 || index >= this.lstItemsToValidate.Items.Count)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                ListViewItem li = this.lstItemsToValidate.Items[index];

                if (li != null)
                {
                    //li.Selected = true;
                    object o = li.Tag;
                    Company company = o as Company;

                    if (company != null)
                    {
                        this.CurrentCompany = company;
                        this.SetControlDataFromObject(company);
                    }
                }
            }
        }

        private void LoadToDoList()
        {
            this.lstItemsToValidate.Items.Clear();

            foreach (Company company in this._companyCollection)
            {
                ListViewItem li = new ListViewItem();

                li.Text = company.ElectricAccountNumber;
                li.SubItems.Add(company.GasAccountNumber);
                li.SubItems.Add(company.Name);
                li.SubItems.Add(company.AddressLine1);
                li.SubItems.Add(company.City);
                li.SubItems.Add(company.State);
                li.SubItems.Add(company.PostalCode);
                li.SubItems.Add(company.Utility);
                li.SubItems.Add(company.Program);

                li.Tag = company;
                this.lstItemsToValidate.Items.Add(li);
            }

            this.SetColumnWidths();
            this.lblTermCount.Text = "Items: " + this._companyCollection.Count.ToString();
        }

        private void SaveAndClose()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void SetAccountNumberSearchOptions(string electricAccountNumber, string gasAccountNumber)
        {
            this._fireAccountSearchCheck = false;

            this.chbElectricAccountNumber.BeginUpdate();
            this.chbGasAccountNumber.BeginUpdate();

            if (String.IsNullOrWhiteSpace(electricAccountNumber))
            {
                this.chbElectricAccountNumber.Checked = false;

                if (String.IsNullOrWhiteSpace(gasAccountNumber))
                {
                    this.chbGasAccountNumber.Checked = false;
                }
                else
                {
                    this.chbGasAccountNumber.Checked = true;
                }
            }
            else
            {
                this.chbElectricAccountNumber.Checked = true;
                this.chbGasAccountNumber.Checked = false;
            }

            this.chbCompanyName.Checked = (String.IsNullOrWhiteSpace(electricAccountNumber) && String.IsNullOrWhiteSpace(gasAccountNumber));

            this.chbGasAccountNumber.EndUpdate();
            this.chbElectricAccountNumber.EndUpdate();

            this._fireAccountSearchCheck = true;
        }

        private void SetColumnWidths()
        {
            if (!this._isColumnWidthsSet)
            {
                // http://stackoverflow.com/questions/14133225/listview-autoresizecolumns-based-on-both-column-content-and-header
                //  Answer dated 11/29/13 (matsolof) (modifed variable names)

                this.lstItemsToValidate.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

                ListView.ColumnHeaderCollection cols = lstItemsToValidate.Columns;

                for (int i = 0; i < cols.Count; i++)
                {
                    int colWidth = TextRenderer.MeasureText(cols[i].Text, lstItemsToValidate.Font).Width + 10;

                    if (colWidth > cols[i].Width)
                    {
                        cols[i].Width = colWidth;
                    }
                }

                this._isColumnWidthsSet = true;
            }
        }

        private void SetControlDataFromObject()
        {
            this.SetControlDataFromObject(null);
        }

        private void SetControlDataFromObject(Company company)
        {
            if (company == null)
            {
                this.txtElectricAccountNumber.Text = "";
                this.txtGasAccountNumber.Text = "";
                this.txtCompany.Text = "";
                this.txtAddress.Text = "";
                this.txtCity.Text = "";
                this.cboState.SelectedIndex = -1;
                this.txtZip.Text = "";
                this.txtUtility.Text = "";
                this.txtProgram.Text = "";

                this.txtCompanyExternalId.Text = "";
                this.txtCompanyId.Text = "";
                this.txtId.Text = "";
            }
            else
            {
                this.txtElectricAccountNumber.Text = company.ElectricAccountNumber;
                this.txtGasAccountNumber.Text = company.GasAccountNumber;
                this.txtCompany.Text = company.Name;
                this.txtAddress.Text = company.AddressLine1;
                this.txtCity.Text = company.City;
                this.cboState.Text = company.State;
                this.txtZip.Text = company.PostalCode;
                this.txtUtility.Text = company.Utility;
                this.txtProgram.Text = company.Program;

                this.txtCompanyExternalId.Text = company.ExternalId;
                this.txtCompanyId.Text = company.CompanyId;
                this.txtId.Text = company.Id;
            }

            this.SetAccountNumberSearchOptions(company.ElectricAccountNumber, company.GasAccountNumber);
        }

        private void UpdateCurrentCompany(BsiSearchResult selectedCompany)
        {
            if (selectedCompany != null)
            {
                if (this.CurrentCompany == null)
                {
                    this.CurrentCompany = new Company();
                }

                this.CurrentCompany.Id = selectedCompany.Id;
                this.CurrentCompany.CompanyId = selectedCompany.Id;
                this.CurrentCompany.ExternalId = selectedCompany.Id;

                this.CurrentCompany.City = selectedCompany.BillingCity;
                this.CurrentCompany.PostalCode = selectedCompany.BillingPostalCode;
                this.CurrentCompany.State = selectedCompany.BillingState;
                this.CurrentCompany.AddressLine1 = selectedCompany.BillingStreet;

                this.CurrentCompany.AccountType = selectedCompany.BusinessType;  //*
                this.CurrentCompany.RecordType = selectedCompany.BusinessType;  //*

                this.CurrentCompany.ElectricAccountNumber = selectedCompany.ElectricAccountNumber;
                this.CurrentCompany.ElectricRateCode = selectedCompany.ElectricRateCode;
                this.CurrentCompany.GasAccountNumber = selectedCompany.GasAccountNumber;
                this.CurrentCompany.GasRateCode = selectedCompany.GasRateCode;
                this.CurrentCompany.Utility = (!String.IsNullOrEmpty(this.CurrentCompany.GasAccountNumber) && !String.IsNullOrEmpty(selectedCompany.GasAccountNumber)) ? selectedCompany.GasUtilityName : selectedCompany.ElectricUtilityName;

                this.CurrentCompany.Name = selectedCompany.Name;
                this.CurrentCompany.Program = DataStore.GetProgramCodeForProgramName(this.CurrentCompany.Program);

                // now update all audits in the company
                for (int i = 0; i < this.CurrentCompany.Audits.Count; i++)
                {
                    this.CurrentCompany.Audits[i].ProgramId = this.CurrentCompany.Program;
                }
            }
        }

        private bool ValidateData()
        {
            bool result = false;
            string msg = "";

            // -------------------------------------------------------------------------------------------------------
            bool noValidatedTerms = true;
            foreach (Company company in this._companyCollection)
            {
                if (company.IsValidated)
                {
                    noValidatedTerms = false;
                    break;
                }
            }

            if (noValidatedTerms)
            {
                msg = "All of the items are still unvalidated.  Please validate at least one item and try again.";
            }
            // -------------------------------------------------------------------------------------------------------

            if (msg == "")
            {
                result = true;
            }
            else
            {
                MessageBox.Show(msg, "Validate Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        private bool ValidateTerms()
        {
            bool result = false;
            string msg = "";

            if (!this.chbElectricAccountNumber.Checked && !this.chbGasAccountNumber.Checked && !this.chbCompanyName.Checked && !this.chbAddress.Checked && !
                this.chbCity.Checked && !this.chbState.Checked && !this.chbZip.Checked && !this.chbProgram.Checked)
            {
                msg = "For God's sake, please select the Utility and SOMETHING else to search on!\n\nPreferrably an account number or the company name, man.";
                this.chbElectricAccountNumber.Focus();
            }
            else if (this.chbElectricAccountNumber.Checked && String.IsNullOrWhiteSpace(this.txtElectricAccountNumber.Text))
            {
                msg = "Please enter an Electric Account Number when searching for electric accounts.";
                this.txtElectricAccountNumber.Focus();
            }
            else if (this.chbGasAccountNumber.Checked && String.IsNullOrWhiteSpace(this.txtGasAccountNumber.Text))
            {
                msg = "Please enter a Gas Account Number when searching for gas accounts.";
                this.txtGasAccountNumber.Focus();
            }
            else if (this.chbCompanyName.Checked && String.IsNullOrWhiteSpace(this.txtCompany.Text))
            {
                msg = "Please enter a Company Name when including it in the search.";
                this.txtCompany.Focus();
            }
            else if (this.chbAddress.Checked && String.IsNullOrWhiteSpace(this.txtAddress.Text))
            {
                msg = "Please enter a Address when including it in the search.";
                this.txtAddress.Focus();
            }
            else if (this.chbCity.Checked && String.IsNullOrWhiteSpace(this.txtCity.Text))
            {
                msg = "Please enter a City when including it in the search.";
                this.txtCity.Focus();
            }
            else if (this.chbState.Checked && this.cboState.SelectedIndex == -1)
            {
                msg = "Please select a State when including it in the search.";
                this.cboState.Focus();
            }
            else if (this.chbZip.Checked && String.IsNullOrWhiteSpace(this.txtZip.Text))
            {
                msg = "Please enter a ZIP Code when including it in the search.";
                this.txtZip.Focus();
            }
            else if (this.chbProgram.Checked && String.IsNullOrWhiteSpace(this.txtProgram.Text))
            {
                msg = "Please enter a Program when including it in the search.";
                this.txtProgram.Focus();
            }
            else if (String.IsNullOrWhiteSpace(this.txtUtility.Text))
            {
                msg = "Please enter a Utility.";
                this.txtUtility.Focus();
            }

            if (msg == "")
            {
                result = true;
            }
            else
            {
                MessageBox.Show(msg, "Invalid Search Terms", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return result;
        }

        #endregion Private helper methods
    }
}