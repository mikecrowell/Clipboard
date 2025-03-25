using FieldTool.BLL;
using FieldTool.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class frmCompanyValidationSearch : DevComponents.DotNetBar.Metro.MetroForm
    {
        private Company _searchCompany = null;

        public Company SelectedCompany
        {
            get
            {
                if (this.lstResults.CheckedItems != null && this.lstResults.CheckedItems.Count > 0)
                {
                    ListViewItem li = this.lstResults.CheckedItems[0];
                    object o = li.Tag;
                    if (o != null)
                    {
                        Company c = o as Company;
                        if (c != null)
                        {
                            return c;
                        }
                    }
                }
                return null;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public frmCompanyValidationSearch()
        {
            InitializeComponent();
        }

        public frmCompanyValidationSearch(BLL.Company searchCompany)
        {
            InitializeComponent();

            this._searchCompany = searchCompany;
        }

        private void frmCompanyValidationSearch_Load(object sender, EventArgs e)
        {
            List<Company> results = this.LookupCompany(true);
            this.LoadList(results);
        }

        private void InitializeControls(List<Company> companies)
        {
            if (companies == null || companies.Count == 0)
            {
                this.lblMessage.Text = "There are no companies found in Bensight that match your search criteria.";
                this.lstResults.Items.Clear();
                this.lstResults.Visible = false;
                this.btnSave.Visible = false;
                this.Height = 113;
            }
            else
            {
                this.lblMessage.Text = "The following matching companies were found in Bensight.  Please select one and press okay to validate your originally selected company and update from Bensight.";
                this.lstResults.Visible = true;
                this.btnSave.Visible = true;
                this.Height = 469;
            }
        }

        private void LoadList(List<Company> companies)
        {
            this.InitializeControls(companies);

            if (companies != null)
            {
                this.lstResults.BeginUpdate();

                foreach (Company company in companies)
                {
                    ListViewItem li = new ListViewItem(company.Name);
                    li.Tag = company;
                    li.Checked = false;

                    li.SubItems.Add(company.ToString("A"));
                    li.SubItems.Add(company.City);
                    li.SubItems.Add(company.State);
                    li.SubItems.Add(company.ToString("Z"));
                    li.SubItems.Add(company.ElectricAccountNumber);
                    li.SubItems.Add(company.GasAccountNumber);
                    li.SubItems.Add(company.Utility);
                    li.SubItems.Add(company.Program);
                    li.SubItems.Add(company.Id);
                    li.SubItems.Add(company.CompanyId);

                    this.lstResults.Items.Add(li);
                }

                this.lstResults.AutoSizeColumnsToFit();
                this.lstResults.Columns[colId.Index].Width = 0;
                this.lstResults.Columns[colCompanyId.Index].Width = 0;

                this.lstResults.EndUpdate();
            }
        }

        private List<Company> LookupCompany(bool useSample)
        {
            List<Company> result = new List<Company>();

            // Using dummy data for now.  Dave said he'll pound out this actual lookup later.

            if (!useSample)
            {
            }
            else
            {
                #region 5 records

                Company c = new Company();
                c.Id = "123456789";
                c.ExternalId = "EX123456789";
                c.CompanyId = "123456789";
                c.Name = "Test Company DEF";
                c.AddressLine1 = "456 Elm Street";
                c.AddressLine2 = "Unit 23";
                c.AddressLine3 = "";
                c.City = "Kenosha";
                c.State = "WI";
                c.PostalCode = "53447";
                c.PostalCodeExtension = "4560";
                c.ElectricAccountNumber = "E456456456";
                c.GasAccountNumber = "";
                c.Utility = "WE Energies";
                c.Program = "Program WE";
                result.Add(c);

                c = new Company();
                c.Id = "345678912";
                c.ExternalId = "EX345678912";
                c.CompanyId = "345678912";
                c.Name = "Test Company ZYX";
                c.AddressLine1 = "788 Maple Street";
                c.AddressLine2 = "";
                c.AddressLine3 = "";
                c.City = "Racine";
                c.State = "WI";
                c.PostalCode = "53404";
                c.PostalCodeExtension = "";
                c.ElectricAccountNumber = "";
                c.GasAccountNumber = "G45454545";
                c.Utility = "WE Energies";
                c.Program = "Prog A";
                result.Add(c);

                c = new Company();
                c.Id = "789789789";
                c.ExternalId = "EX789789789";
                c.CompanyId = "789789789";
                c.Name = "Test Company DDD";
                c.AddressLine1 = "888 Lewis Ave";
                c.AddressLine2 = "#22";
                c.AddressLine3 = "";
                c.City = "Kenosha";
                c.State = "WI";
                c.PostalCode = "53446";
                c.PostalCodeExtension = "";
                c.ElectricAccountNumber = "E11122222";
                c.GasAccountNumber = "G44559444";
                c.Utility = "WE Energies";
                c.Program = "Program WE";
                result.Add(c);

                c = new Company();
                c.Id = "4147441014";
                c.ExternalId = "EX4147441014";
                c.CompanyId = "4147441014";
                c.Name = "Test Company ABC";
                c.AddressLine1 = "7878 Hwy 1";
                c.AddressLine2 = "";
                c.AddressLine3 = "";
                c.City = "Somers";
                c.State = "WI";
                c.PostalCode = "53177";
                c.PostalCodeExtension = "";
                c.ElectricAccountNumber = "E191191919";
                c.GasAccountNumber = "G14564646";
                c.Utility = "WE Energies";
                c.Program = "Save Energy 101";
                result.Add(c);

                c = new Company();
                c.Id = "866114444";
                c.ExternalId = "EX866114444";
                c.CompanyId = "866114444";
                c.Name = "Test Company EFG";
                c.AddressLine1 = "555 E Street";
                c.AddressLine2 = "Apt #5";
                c.AddressLine3 = "";
                c.City = "Fifth";
                c.State = "WI";
                c.PostalCode = "55555";
                c.PostalCodeExtension = "5555";
                c.ElectricAccountNumber = "E55555555";
                c.GasAccountNumber = "G55555555";
                c.Utility = "WE Energies";
                c.Program = "Save Energy 555";
                result.Add(c);

                #endregion 5 records
            }

            return result;
        }
    }
}