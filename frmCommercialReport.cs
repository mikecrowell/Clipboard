using FieldTool.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

//using System.recordsourceCollections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    public partial class frmCommercialReport : Form
    {
        #region Private member variables

        private string _activeUserFullName;
        private string _auditId;
        private string _companyNameOverride;
        private string _contactNameOverride;
        private string _energyAdvisorNameOverride;
        private string _energyAdvisorPhone;
        private string _energyAdvisorEmail;
        private string _topRecommendation;
        private List<string> _filters = new List<string>();
        private bool fromSignOffTab = false;
        private Company.CompanyCollection _coList;
        private string _filePath;
        private string _primaryContact;
        private ClientCommercialReportBranding _reportBranding;
        private bool _isTableAdjusted;
        private int _numRecommendations;

        #endregion Private member variables

        #region Constructors

        public frmCommercialReport(string activeUserFullName, string auditID, string companyNameOverride, string contactNameOverride, string energyAdvisorNameOverride, string energyAdvisorPhone, string energyAdvisorEmail, string topRecommendation, List<string> filters, bool fromSignOffTab, Company.CompanyCollection coList, string filePath, string primaryContact, ClientCommercialReportBranding reportBranding)
        {
            InitializeComponent();

            this.ActiveUserFullName = activeUserFullName;
            this.AuditID = auditID;
            this.CompanyNameOverride = companyNameOverride;
            this.ContactNameOverride = contactNameOverride;
            this.EnergyAdvisorNameOverride = energyAdvisorNameOverride;
            this.FilterOptions = filters;
            this.fromSignOffTab = fromSignOffTab;
            this.CoList = coList;
            this.FilePath = filePath;
            this.PrimaryContact = primaryContact;
            this.ReportBranding = reportBranding;
            this.EnergyAdvisorPhone = energyAdvisorPhone;
            this.EnergyAdvisorEmail = energyAdvisorEmail;
            this.TopRecommendation = topRecommendation;
            this.IsTableAdjusted = false;
            this.NumRecommendations = 0;
        }

        public frmCommercialReport(string activeUserFullName, string auditID, string companyNameOverride, string contactNameOverride, string energyAdvisorNameOverride, string energyAdvisorPhone, string energyAdvisorEmail, string topRecommendation, List<string> filters, bool fromSignOffTab, Company.CompanyCollection coList, string filePath, string primaryContact, ClientCommercialReportBranding reportBranding, int numRecommendations)
        {
            InitializeComponent();

            this.ActiveUserFullName = activeUserFullName;
            this.AuditID = auditID;
            this.CompanyNameOverride = companyNameOverride;
            this.ContactNameOverride = contactNameOverride;
            this.EnergyAdvisorNameOverride = energyAdvisorNameOverride;
            this.FilterOptions = filters;
            this.fromSignOffTab = fromSignOffTab;
            this.CoList = coList;
            this.FilePath = filePath;
            this.PrimaryContact = primaryContact;
            this.ReportBranding = reportBranding;
            this.EnergyAdvisorPhone = energyAdvisorPhone;
            this.EnergyAdvisorEmail = energyAdvisorEmail;
            this.TopRecommendation = topRecommendation;
            this.IsTableAdjusted = false;
            this.NumRecommendations = numRecommendations;
        }

        #endregion Constructors

        #region Properties

        private string ActiveUserFullName
        {
            get
            {
                return this._activeUserFullName;
            }
            set
            {
                this._activeUserFullName = (value ?? "").Trim();
            }
        }

        private string AuditID
        {
            get
            {
                return this._auditId;
            }
            set
            {
                this._auditId = (value ?? "").Trim();
            }
        }

        private string CompanyNameOverride
        {
            get
            {
                return this._companyNameOverride;
            }
            set
            {
                this._companyNameOverride = (value ?? "").Trim();
            }
        }

        private string ContactNameOverride
        {
            get
            {
                return this._contactNameOverride;
            }
            set
            {
                this._contactNameOverride = (value ?? "").Trim();
            }
        }

        private string EnergyAdvisorNameOverride
        {
            get
            {
                return this._energyAdvisorNameOverride;
            }
            set
            {
                this._energyAdvisorNameOverride = (value ?? "").Trim();
            }
        }

        private List<string> FilterOptions
        {
            get
            {
                return this._filters;
            }
            set
            {
                this._filters = (value ?? new List<string>());
            }
        }

        private Company.CompanyCollection CoList
        {
            get
            {
                return this._coList;
            }
            set
            {
                this._coList = (value ?? new Company.CompanyCollection());
            }
        }

        private string FilePath
        {
            get
            {
                return this._filePath;
            }
            set
            {
                this._filePath = (value ?? "").Trim();
            }
        }

        private string PrimaryContact
        {
            get
            {
                return this._primaryContact;
            }
            set
            {
                this._primaryContact = (value ?? "").Trim();
            }
        }

        private ClientCommercialReportBranding ReportBranding
        {
            get
            {
                return this._reportBranding;
            }
            set
            {
                this._reportBranding = (value ?? new ClientCommercialReportBranding());
            }
        }

        private string EnergyAdvisorPhone
        {
            get
            {
                return this._energyAdvisorPhone;
            }
            set
            {
                this._energyAdvisorPhone = (value ?? "").Trim();
            }
        }

        private string EnergyAdvisorEmail
        {
            get
            {
                return this._energyAdvisorEmail;
            }
            set
            {
                this._energyAdvisorEmail = (value ?? "").Trim();
            }
        }

        private string TopRecommendation
        {
            get
            {
                return this._topRecommendation;
            }
            set
            {
                this._topRecommendation = (value ?? "").Trim();
            }
        }

        private bool IsTableAdjusted
        {
            get
            {
                return this._isTableAdjusted;
            }
            set
            {
                this._isTableAdjusted = value;
            }
        }

        private int NumRecommendations
        {
            get
            {
                return this._numRecommendations;
            }
            set
            {
                this._numRecommendations = value;
            }
        }

        #endregion Properties

        #region Events

        private void frmCommercialReport_Load(object sender, EventArgs e)
        {
            this.CreateReportXml();
        }

        #endregion Events

        #region Public methods

        public void SaveReportAsPDF(string saveAsFileName)
        {
            this.CreateReportXml();
            try
            {
                c1Report1.RenderToFile(saveAsFileName, C1.C1Report.FileFormatEnum.PDF);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Public methods

        #region Private helper methods

        private void CreateReportXml()
        {
            // create an xml file for reporting

            Report auditReport = new Report();

            C1.C1Report.Field rpt = this.c1Report1.Fields["subGasElectric"];
            C1.C1Report.Field rptEUI = this.c1Report1.Fields["subEUI"];
            C1.C1Report.Field rptSignature = this.c1Report1.Fields["subSig"]; //Added 8/13/15 by Mike Crowell
            C1.C1Report.Field rptCover = this.c1Report1.Fields["subCover"];
            C1.C1Report.Field rptCompanyInfo = this.c1Report1.Fields["subCompanyInfo"];
            C1.C1Report.Field rptIntro = this.c1Report1.Fields["subIntro"];
            C1.C1Report.Field rptDI = this.c1Report1.Fields["subDI"];
            C1.C1Report.Field rptReportRec = this.c1Report1.Fields["subReportRec"];
            C1.C1Report.Field rptMeasureSummary = this.c1Report1.Fields["subMeasureSummary"];
            C1.C1Report.Field rptRec = this.c1Report1.Fields["subRec"];
            C1.C1Report.Field rptRecOptions = this.c1Report1.Fields["subRecOptions"];
            C1.C1Report.Field rptRecSavings = this.c1Report1.Fields["subrecSavings"];
            C1.C1Report.Field rptReplacementEquip = this.c1Report1.Fields["fldReplacementEquipmentSubReport"];

            Report.ReportCollection NewFormat = new Report.ReportCollection();
            bool showTopRecommendations = false;
            bool showAllRecommendations = false;
            bool showRecommendationOptions = false;
            int iRecCount = 0;
            int iCtr = 1;
            string sField = "";
            string recommendationid = "";
            int iDegreeDays = 0;
            string sChartDescription = "";

            //DF 7/8/15
            //include totals for occupancy sensor recommenation options (excluding MCF (therms))
            double dTotalkWhSaved = 0;
            double dTotalRebates = 0;
            double dTotalAnnualCostSavings = 0;
            int iControlQuantity = 0;
            int iPageCount = 1;

            bool showEnergyUsageChart = false;
            bool showSignatureReport = false; //Added 8/13/15 by Mike Crowell
            bool showElectricHistoryGraph = false;
            bool showGasHistoryGraph = false;
            bool showDirectInstallSummary = false;
            bool showIntro = false;

            foreach (Company c in this.CoList)
            {
                iRecCount = 0;
                iCtr = 1;
                sField = "";

                showTopRecommendations = false;
                showAllRecommendations = false;
                showRecommendationOptions = false;
                showSignatureReport = false; //Added 8/13/15 by Mike Crowell
                showElectricHistoryGraph = false;
                showGasHistoryGraph = false;
                showDirectInstallSummary = false;

                this.c1Report1.Fields["subEUI"].Visible = false;
                //this.c1Report1.MaxPages = iPageCount;

                //there will only be one company (our test data has more)
                foreach (Audit audit in c.Audits)
                {
                    if (audit.Id == this.AuditID || audit.ExternalId == this.AuditID)
                    {
                        for (int i = 0; i < this.FilterOptions.Count; ++i)
                        {
                            sField = this.c1Report1.Fields[0].Name;

                            switch (this.FilterOptions[i])
                            {
                                case "All Recommendations":
                                    showAllRecommendations = true;
                                    showTopRecommendations = false;
                                    break;

                                case "Top Recommendations":
                                    showTopRecommendations = true;
                                    showAllRecommendations = false;
                                    break;

                                case "Direct Install Summary":
                                    showDirectInstallSummary = true;
                                    break;

                                case "Recommendation Options":
                                    showRecommendationOptions = true;
                                    break;

                                case "Show Electric History":
                                    showElectricHistoryGraph = true;
                                    break;

                                case "Show Gas History":
                                    showGasHistoryGraph = true;
                                    break;

                                case "Signature": //Added 8/13/15 by Mike Crowell
                                    showSignatureReport = true;
                                    break;

                                case "Introduction":
                                    showIntro = true;
                                    iPageCount++;
                                    break;
                            }
                        }

                        //Iterate through the audit

                        if (!String.IsNullOrWhiteSpace(this.EnergyAdvisorNameOverride))
                        {
                            auditReport.EnergyAdvisorFullName = this.EnergyAdvisorNameOverride;
                        }
                        else
                        {
                            auditReport.EnergyAdvisorFullName = audit.EnergyAdvisorName;
                        }

                        if (!String.IsNullOrWhiteSpace(this.CompanyNameOverride))
                        {
                            auditReport.CompanyName = this.CompanyNameOverride;
                        }
                        else
                        {
                            auditReport.CompanyName = c.Name;
                        }

                        if (!String.IsNullOrWhiteSpace(this.ContactNameOverride))
                        {
                            auditReport.ContactName = this.ContactNameOverride;
                        }
                        else
                        {
                            if (String.IsNullOrWhiteSpace(audit.CompanyContact))
                            {
                                foreach (Contact contact in c.Contacts)
                                {
                                    if (contact.FirstName != "")
                                    {
                                        audit.CompanyContact = contact.FirstName + " " + contact.LastName;
                                    }
                                }
                            }

                            auditReport.ContactName = audit.CompanyContact;
                        }

                        if (!String.IsNullOrWhiteSpace(this.EnergyAdvisorPhone))
                        {
                            auditReport.EnergyAdvisorPhone = this.EnergyAdvisorPhone;
                        }
                        else
                        {
                            auditReport.EnergyAdvisorPhone = "";
                        }
                        if (!String.IsNullOrWhiteSpace(this.EnergyAdvisorEmail))
                        {
                            auditReport.EnergyAdvisorEmail = this.EnergyAdvisorEmail;
                        }
                        else
                        {
                            auditReport.EnergyAdvisorEmail = "";
                        }
                        if (!String.IsNullOrWhiteSpace(this.TopRecommendation))
                        {
                            auditReport.TopRecommendation = this.TopRecommendation;
                        }
                        else
                        {
                            auditReport.TopRecommendation = "";
                        }

                        auditReport.AddressLine1 = c.AddressLine1;
                        auditReport.AddressLine2 = c.AddressLine2;
                        auditReport.AddressLine3 = c.AddressLine3;
                        auditReport.City = c.City;
                        auditReport.State = c.State;

                        string tmp = "";
                        if (c.PostalCode.Length == 9)
                        {
                            tmp = c.PostalCode.Insert(5, "-");
                        }
                        else
                        {
                            if (!String.IsNullOrWhiteSpace(c.PostalCode))
                            {
                                tmp = c.PostalCode.ToString() + (String.IsNullOrWhiteSpace(c.PostalCodeExtension) ? String.Empty : "-" + c.PostalCodeExtension);
                            }
                        }
                        auditReport.Zip = tmp;

                        //auditReport.Zip = (String.IsNullOrEmpty(c.PostalCode) ? String.Empty : c.PostalCode.ToString() + (String.IsNullOrWhiteSpace(c.PostalCodeExtension) ? String.Empty : "-" + c.PostalCodeExtension));
                        auditReport.AssessmentDate = audit.ScheduledStartTimeStamp.ToShortDateString();

                        auditReport.ElectricAccountNumber = c.ElectricAccountNumber;
                        auditReport.GasAccountNumber = c.GasAccountNumber;
                        auditReport.HideElectricChart = true;
                        auditReport.HideGasChart = true;

                        #region Apply Client ReportBranding

                        if (this.ReportBranding != null)
                        {
                            if (!String.IsNullOrEmpty(ReportBranding.BrandingFilePath) && !String.IsNullOrEmpty(ReportBranding.HeaderImageFileName))
                            {
                                rptCover.Subreport.Fields["CoverBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptIntro.Subreport.Fields["IntroBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptDI.Subreport.Fields["DIBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rpt.Subreport.Fields["GasElectBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptSignature.Subreport.Fields["SigBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptRec.Subreport.Fields["RecBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptEUI.Subreport.Fields["EUIBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptRecOptions.Subreport.Fields["RecOptionsBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["RecSavingsBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.HeaderImageFileName;
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.BrandingFilePath) && !String.IsNullOrEmpty(ReportBranding.CoverImageFileName))
                            {
                                rptCover.Subreport.Fields["CoverPageImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.CoverImageFileName;
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.BrandingFilePath) && !String.IsNullOrEmpty(ReportBranding.NextStepsImageFileName))
                            {
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["NextStepsImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.NextStepsImageFileName;
                            }

                            rptCover.Subreport.Fields["ReportTitle"].Text = !String.IsNullOrEmpty(ReportBranding.ReportTitleText) ? ReportBranding.ReportTitleText : "";
                            if (!String.IsNullOrEmpty(ReportBranding.ReportTitleFontType))
                            {
                                rptCover.Subreport.Fields["ReportTitle"].Font.Name = ReportBranding.ReportTitleFontType;
                                rptCover.Subreport.Fields["CoverCompanyName"].Font.Name = ReportBranding.ReportTitleFontType;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.ReportTitleFontSize))
                            {
                                rptCover.Subreport.Fields["ReportTitle"].Font.Size = Single.Parse(ReportBranding.ReportTitleFontSize);
                            }
                            rptCover.Subreport.Fields["ReportTitle"].ForeColor = ReportBranding.ReportTitleFontColor;
                            rptCover.Subreport.Fields["CoverCompanyName"].ForeColor = ReportBranding.ReportTitleFontColor;
                            if (ReportBranding.IsReportTitleAllCaps)
                            {
                                rptCover.Subreport.Fields["ReportTitle"].Text.ToUpper();
                            }

                            rptCover.Subreport.Fields["PreparedBy"].Text = !String.IsNullOrEmpty(ReportBranding.ReportPreparedByText) ? ReportBranding.ReportPreparedByText : "";
                            if (!String.IsNullOrEmpty(ReportBranding.ReportPreparedByFontType))
                            {
                                rptCover.Subreport.Fields["PreparedBy"].Font.Name = ReportBranding.ReportPreparedByFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorFullName"].Font.Name = ReportBranding.ReportPreparedByFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].Font.Name = ReportBranding.ReportPreparedByFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].Font.Name = ReportBranding.ReportPreparedByFontType;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.ReportPreparedByFontSize))
                            {
                                rptCover.Subreport.Fields["PreparedBy"].Font.Size = Single.Parse(ReportBranding.ReportPreparedByFontSize);
                            }
                            rptCover.Subreport.Fields["PreparedBy"].ForeColor = ReportBranding.ReportPreparedByFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvisorFullName"].ForeColor = ReportBranding.ReportPreparedByFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].ForeColor = ReportBranding.ReportPreparedByFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].ForeColor = ReportBranding.ReportPreparedByFontColor;
                            if (ReportBranding.IsReportPreparedByAllCaps)
                            {
                                rptCover.Subreport.Fields["PreparedBy"].Text.ToUpper();
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.PrimaryFontType))
                            {
                                rptIntro.Subreport.Fields["lblIntroPageDate"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldContactName"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldCompanyName"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldAccountInfo"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar1"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar2"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar3"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldTopRecommendation"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["IntroContactText"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["lblClosingText"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Font.Name = ReportBranding.PrimaryFontType;
                                rptIntro.Subreport.Fields["IntroSignatureText"].Font.Name = ReportBranding.PrimaryFontType;
                                rptDI.Subreport.Fields["lblDIPageDescription"].Font.Name = ReportBranding.PrimaryFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorFullName"].Font.Name = ReportBranding.PrimaryFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].Font.Name = ReportBranding.PrimaryFontType;
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].Font.Name = ReportBranding.PrimaryFontType;
                                rptRec.Subreport.Fields["lblRecommendationPageDescription"].Font.Name = ReportBranding.PrimaryFontType;
                                rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Font.Name = ReportBranding.PrimaryFontType;
                                rpt.Subreport.Fields["txtDescription"].Font.Name = ReportBranding.PrimaryFontType;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.PrimaryFontSize))
                            {
                                rptIntro.Subreport.Fields["lblIntroPageDate"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldContactName"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldCompanyName"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldAccountInfo"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar1"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar2"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar3"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldTopRecommendation"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["IntroContactText"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["lblClosingText"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptIntro.Subreport.Fields["IntroSignatureText"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptDI.Subreport.Fields["lblDIPageDescription"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorFullName"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptRec.Subreport.Fields["lblRecommendationPageDescription"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                                rpt.Subreport.Fields["txtDescription"].Font.Size = Single.Parse(ReportBranding.PrimaryFontSize);
                            }
                            rptIntro.Subreport.Fields["lblIntroPageDate"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldContactName"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldCompanyName"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldAccountInfo"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar1"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar2"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar3"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldTopRecommendation"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["IntroContactText"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["lblClosingText"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptIntro.Subreport.Fields["IntroSignatureText"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptDI.Subreport.Fields["lblDIPageDescription"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvisorFullName"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptRec.Subreport.Fields["lblRecommendationPageDescription"].ForeColor = ReportBranding.PrimaryFontColor;
                            rptSignature.Subreport.Fields["txtSignatureDisclaimer"].ForeColor = ReportBranding.PrimaryFontColor;
                            rpt.Subreport.Fields["txtDescription"].ForeColor = ReportBranding.PrimaryFontColor;

                            if (!String.IsNullOrEmpty(ReportBranding.IntroPageFontType))
                            {
                                rptIntro.Subreport.Fields["lblIntroPageDate"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldContactName"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldCompanyName"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldAccountInfo"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar1"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar2"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["IntroBodyPar3"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldTopRecommendation"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["IntroContactText"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["lblClosingText"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Font.Name = ReportBranding.IntroPageFontType;
                                rptIntro.Subreport.Fields["IntroSignatureText"].Font.Name = ReportBranding.IntroPageFontType;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.IntroPageFontSize))
                            {
                                rptIntro.Subreport.Fields["lblIntroPageDate"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldContactName"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldCompanyName"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldAccountInfo"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar1"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar2"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["IntroBodyPar3"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldTopRecommendation"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["IntroContactText"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["lblClosingText"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                                rptIntro.Subreport.Fields["IntroSignatureText"].Font.Size = Single.Parse(ReportBranding.IntroPageFontSize);
                            }
                            rptIntro.Subreport.Fields["lblIntroPageDate"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldContactName"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldCompanyName"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldIntroAddressCityStateZip"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldAccountInfo"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar1"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar2"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["IntroBodyPar3"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldTopRecommendation"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["IntroContactText"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["lblClosingText"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvisorName"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].ForeColor = ReportBranding.IntroPageFontColor;
                            rptIntro.Subreport.Fields["IntroSignatureText"].ForeColor = ReportBranding.IntroPageFontColor;

                            rptIntro.Subreport.Fields["IntroBodyPar1"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar1) ? ReportBranding.IntroBodyPar1 : "";
                            rptIntro.Subreport.Fields["IntroBodyPar2"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar2) ? ReportBranding.IntroBodyPar2 : "";
                            rptIntro.Subreport.Fields["IntroBodyPar3"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar3) ? ReportBranding.IntroBodyPar3 : "";
                            rptIntro.Subreport.Fields["IntroContactText"].Text = !String.IsNullOrEmpty(ReportBranding.IntroContactText) ? ReportBranding.IntroContactText : "";
                            rptIntro.Subreport.Fields["IntroSignatureText"].Text = !String.IsNullOrEmpty(ReportBranding.IntroSignatureText) ? ReportBranding.IntroSignatureText : "";

                            rptDI.Subreport.Fields["lblDITitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section1DITitleText) ? ReportBranding.Section1DITitleText : "";
                            rptDI.Subreport.Fields["lblDIPageDescription"].Text = !String.IsNullOrEmpty(ReportBranding.Section1DIIntroParagraphText) ? ReportBranding.Section1DIIntroParagraphText : "";
                            rptRec.Subreport.Fields["lblRecTitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section2RecommendationTitleText) ? ReportBranding.Section2RecommendationTitleText : "";
                            rptRec.Subreport.Fields["lblRecommendationPageDescription"].Text = !String.IsNullOrEmpty(ReportBranding.Section2RecommendationIntroParagraphText) ? ReportBranding.Section2RecommendationIntroParagraphText : "";
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblRecSavingsTitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section2RecommendationTitleText) ? ReportBranding.Section2RecommendationTitleText : "";
                            rptRecOptions.Subreport.Fields["lblRecOptionsTitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section3RecOptionsTitleText) ? ReportBranding.Section3RecOptionsTitleText : "";
                            rpt.Subreport.Fields["lblEnergyHistoryTitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section4HistoryTitleText) ? ReportBranding.Section4HistoryTitleText : "";
                            rptSignature.Subreport.Fields["lblSignoffTitle"].Text = !String.IsNullOrEmpty(ReportBranding.Section5SignoffTitleText) ? ReportBranding.Section5SignoffTitleText : "";

                            if (!String.IsNullOrEmpty(ReportBranding.SectionTitleFontStyle))
                            {
                                rptDI.Subreport.Fields["lblDITitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                                rptRec.Subreport.Fields["lblRecTitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblRecSavingsTitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                                rptRecOptions.Subreport.Fields["lblRecOptionsTitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                                rpt.Subreport.Fields["lblEnergyHistoryTitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                                rptSignature.Subreport.Fields["lblSignoffTitle"].Font.Name = ReportBranding.SectionTitleFontStyle;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.SectionTitleFontSize))
                            {
                                rptDI.Subreport.Fields["lblDITitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                                rptRec.Subreport.Fields["lblRecTitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblRecSavingsTitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                                rptRecOptions.Subreport.Fields["lblRecOptionsTitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                                rpt.Subreport.Fields["lblEnergyHistoryTitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                                rptSignature.Subreport.Fields["lblSignoffTitle"].Font.Size = Single.Parse(ReportBranding.SectionTitleFontSize);
                            }
                            rptDI.Subreport.Fields["lblDITitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            rptRec.Subreport.Fields["lblRecTitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblRecSavingsTitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            rptRecOptions.Subreport.Fields["lblRecOptionsTitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            rpt.Subreport.Fields["lblEnergyHistoryTitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            rptSignature.Subreport.Fields["lblSignoffTitle"].ForeColor = ReportBranding.SectionTitleFontColor;
                            if (ReportBranding.IsReportTitleAllCaps)
                            {
                                rptDI.Subreport.Fields["lblDITitle"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblRecTitle"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblRecSavingsTitle"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["lblRecOptionsTitle"].Text.ToUpper();
                                rpt.Subreport.Fields["lblEnergyHistoryTitle"].Text.ToUpper();
                                rptSignature.Subreport.Fields["lblSignoffTitle"].Text.ToUpper();
                            }

                            rptReportRec.Subreport.Fields["lblReportRank"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptMeasureSummary.Subreport.Fields["lblMeasureSummaryGroup"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptDI.Subreport.Fields["DescriptionLbl"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptDI.Subreport.Fields["QuantityLbl"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptDI.Subreport.Fields["KwHLbl"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptDI.Subreport.Fields["ThermsLbl"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptDI.Subreport.Fields["CostSavingsLbl"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["fldRecName"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblExisting"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblProposed"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblSpace"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblQuantity"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblAnnualSavings"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblKwh"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblMcf"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblCost"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["lblEstimatedIncentive"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRecOptions.Subreport.Fields["fldReportRank"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].BackColor = ReportBranding.TableHeaderBackgroundColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].BackColor = ReportBranding.TableHeaderBackgroundColor;

                            rptReportRec.Subreport.Fields["lblReportRank"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptMeasureSummary.Subreport.Fields["lblMeasureSummaryGroup"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptDI.Subreport.Fields["DescriptionLbl"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptDI.Subreport.Fields["QuantityLbl"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptDI.Subreport.Fields["KwHLbl"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptDI.Subreport.Fields["ThermsLbl"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptDI.Subreport.Fields["CostSavingsLbl"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["fldRecName"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblExisting"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblProposed"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblSpace"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblQuantity"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblAnnualSavings"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblKwh"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblMcf"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblCost"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["lblEstimatedIncentive"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRecOptions.Subreport.Fields["fldReportRank"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].ForeColor = ReportBranding.TableHeaderFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].ForeColor = ReportBranding.TableHeaderFontColor;

                            if (ReportBranding.IsTableHeaderAllCaps)
                            {
                                rptReportRec.Subreport.Fields["lblReportRank"].Text.ToUpper();
                                rptMeasureSummary.Subreport.Fields["lblMeasureSummaryGroup"].Text.ToUpper();
                                rptDI.Subreport.Fields["DescriptionLbl"].Text.ToUpper();
                                rptDI.Subreport.Fields["QuantityLbl"].Text.ToUpper();
                                rptDI.Subreport.Fields["KwHLbl"].Text.ToUpper();
                                rptDI.Subreport.Fields["ThermsLbl"].Text.ToUpper();
                                rptDI.Subreport.Fields["CostSavingsLbl"].Text.ToUpper();
                                rptRec.Subreport.Fields["fldRecName"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblExisting"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblProposed"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblSpace"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblQuantity"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblAnnualSavings"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblKwh"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblMcf"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblCost"].Text.ToUpper();
                                rptRec.Subreport.Fields["lblEstimatedIncentive"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["fldReportRank"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].Text.ToUpper();
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].Text.ToUpper();
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].Text.ToUpper();
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.TableHeaderFontSize))
                            {
                                rptReportRec.Subreport.Fields["lblReportRank"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptMeasureSummary.Subreport.Fields["lblMeasureSummaryGroup"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptDI.Subreport.Fields["DescriptionLbl"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptDI.Subreport.Fields["QuantityLbl"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptDI.Subreport.Fields["KwHLbl"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptDI.Subreport.Fields["ThermsLbl"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptDI.Subreport.Fields["CostSavingsLbl"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["fldRecName"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblExisting"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblProposed"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblSpace"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblQuantity"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblAnnualSavings"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblKwh"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblMcf"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblCost"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["lblEstimatedIncentive"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRecOptions.Subreport.Fields["fldReportRank"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].Font.Size = Single.Parse(ReportBranding.TableHeaderFontSize);
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.TableHeaderFontStyle))
                            {
                                rptReportRec.Subreport.Fields["lblReportRank"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptMeasureSummary.Subreport.Fields["lblMeasureSummaryGroup"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptDI.Subreport.Fields["DescriptionLbl"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptDI.Subreport.Fields["QuantityLbl"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptDI.Subreport.Fields["KwHLbl"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptDI.Subreport.Fields["ThermsLbl"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptDI.Subreport.Fields["CostSavingsLbl"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["fldRecName"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblExisting"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblProposed"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblSpace"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblQuantity"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblAnnualSavings"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblKwh"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblMcf"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblCost"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["lblEstimatedIncentive"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRecOptions.Subreport.Fields["fldReportRank"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].Font.Name = ReportBranding.TableHeaderFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].Font.Name = ReportBranding.TableHeaderFontStyle;
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.TableCellFontSize))
                            {
                                rptDI.Subreport.Fields["DescriptionCtl"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["QuantityCtl"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["KwHCtl"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["ThermsCtl"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["CostSavingsCtl"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["lblTotalSavings"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["fldKwhSum"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["fldMcfSum"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptDI.Subreport.Fields["fldCostSavingsSum"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldRecommendationDescription"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldExisting"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldProposed"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldSpace"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldQuantity"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldKwh"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldMcf"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldCost"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["fldEstimatedIncentive"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementDescription"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementSavingsTherms"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementAnnualSavings"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementIncentive"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtRec"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtCostSavings"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtIncentive"].Font.Size = Single.Parse(ReportBranding.TableCellFontSize);
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.TableCellFontStyle))
                            {
                                rptDI.Subreport.Fields["DescriptionCtl"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["QuantityCtl"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["KwHCtl"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["ThermsCtl"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["CostSavingsCtl"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["lblTotalSavings"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["fldKwhSum"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["fldMcfSum"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptDI.Subreport.Fields["fldCostSavingsSum"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldRecommendationDescription"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldExisting"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldProposed"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldSpace"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldQuantity"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldKwh"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldMcf"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldCost"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["fldEstimatedIncentive"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementDescription"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementSavingsTherms"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementAnnualSavings"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementIncentive"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtRec"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtCostSavings"].Font.Name = ReportBranding.TableCellFontStyle;
                                rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtIncentive"].Font.Name = ReportBranding.TableCellFontStyle;
                            }

                            rptDI.Subreport.Fields["picType"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["DescriptionCtl"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["QuantityCtl"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["KwHCtl"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["ThermsCtl"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["CostSavingsCtl"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["lblTotalSavings"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["lblEmptyCell"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["fldKwhSum"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["fldMcfSum"].ForeColor = ReportBranding.TableCellFontColor;
                            rptDI.Subreport.Fields["fldCostSavingsSum"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldRecommendationDescription"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldExisting"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldProposed"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldSpace"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldQuantity"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldKwh"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldMcf"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldCost"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["fldEstimatedIncentive"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementDescription"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementSavingsTherms"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementAnnualSavings"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementIncentive"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtRec"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtCostSavings"].ForeColor = ReportBranding.TableCellFontColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtIncentive"].ForeColor = ReportBranding.TableCellFontColor;

                            rptDI.Subreport.Fields["DescriptionLbl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["QuantityLbl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["KwHLbl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["ThermsLbl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["CostSavingsLbl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["picType"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["DescriptionCtl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["QuantityCtl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["KwHCtl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["ThermsCtl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["CostSavingsCtl"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["lblTotalSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["lblEmptyCell"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["fldKwhSum"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["fldMcfSum"].BorderColor = ReportBranding.TableBorderColor;
                            rptDI.Subreport.Fields["fldCostSavingsSum"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblReportRank"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldRecName"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldRecommendationDescription"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblExisting"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblProposed"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblSpace"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblQuantity"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblAnnualSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblKwh"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblMcf"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblCost"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["lblEstimatedIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldExisting"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldProposed"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldSpace"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldQuantity"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldKwh"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldMcf"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldCost"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["fldEstimatedIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecOptions"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecKwh"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["lblRecIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementDescription"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementSavingsTherms"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementAnnualSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRecOptions.Subreport.Fields["subReplaceEquip"].Subreport.Fields["fldReplacementIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSubRecSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblSavingsOpportunity"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblAnnualCostSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblEstimatedIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtRec"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtCostSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["txtIncentive"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["lblTotalSavingsAndIncentives"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumAnnualSavings"].BorderColor = ReportBranding.TableBorderColor;
                            rptRec.Subreport.Fields["subRecommendSavings"].Subreport.Fields["fldSumEstimatedIncentive"].BorderColor = ReportBranding.TableBorderColor;

                            rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Text = !String.IsNullOrEmpty(ReportBranding.SignOffDisclaimerText) ? ReportBranding.SignOffDisclaimerText : "";
                            if (!String.IsNullOrEmpty(ReportBranding.SignOffDisclaimerFontSize))
                            {
                                rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Font.Size = Single.Parse(ReportBranding.SignOffDisclaimerFontSize);
                            }
                        }
                        else
                        {
                            //rptIntro.Subreport.Fields["IntroBody"].Text = "";
                        }

                        if (String.IsNullOrEmpty(auditReport.TopRecommendation))
                        {
                            rptIntro.Subreport.Fields["fldTopRecommendation"].Visible = false;
                        }

                        if (String.IsNullOrEmpty(auditReport.EnergyAdvisorEmail))
                        {
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Visible = false;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvisorEmail"].Visible = false;
                        }

                        if (String.IsNullOrEmpty(auditReport.EnergyAdvisorPhone))
                        {
                            rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Visible = false;
                            rptCover.Subreport.Fields["fldCoverEnergyAdvsiorPhone"].Visible = false;
                        }

                        #endregion Apply Client ReportBranding

                        #region Direct Installs

                        if (showDirectInstallSummary && audit.Buildings != null)
                        {
                            bool hasDI = false;
                            int retroPageCount = 1;
                            int retrosPerPageCount = 0;

                            foreach (Building currentBuilding in audit.Buildings)
                            {
                                if (currentBuilding.Retrofits != null && currentBuilding.Retrofits.Count > 0)
                                {
                                    foreach (Retrofit retrofit in currentBuilding.Retrofits)
                                    {
                                        if (String.Compare((retrofit.ComponentType ?? "").Trim(), "Process/Misc", true) != 0)
                                        {
                                            auditReport.DirectInstall.Add(new DirectInstalls(retrofit, audit.ScheduledStartTimeStamp.ToString()));

                                            retrosPerPageCount++;

                                            if (retroPageCount == 1)
                                            {
                                                if (retrosPerPageCount >= 12)
                                                {
                                                    retroPageCount++;
                                                    retrosPerPageCount = 0;
                                                }
                                            }
                                            else
                                            {
                                                if (retrosPerPageCount >= 26)
                                                {
                                                    retroPageCount++;
                                                    retrosPerPageCount = 0;
                                                }
                                            }

                                            hasDI = true;
                                        }
                                    }
                                }
                            }

                            iPageCount += retroPageCount;

                            if (ReportBranding.HideDIIcons)
                            {
                                foreach (DirectInstalls di in auditReport.DirectInstall)
                                {
                                    di.IconPath = "\\blank.jpg";
                                }
                            }

                            if (hasDI)
                            {
                                this.c1Report1.Fields["subDI"].Visible = true;
                                this.c1Report1.Fields["subDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
                            }
                            else
                            {
                                auditReport.DirectInstall.Add(new DirectInstalls("None Selected", 0, 0, 0, ""));
                                this.c1Report1.Fields["subDI"].Visible = false;
                                this.c1Report1.Fields["subDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.None;
                            }
                        }
                        else
                        {
                            this.c1Report1.Fields["subDI"].Visible = false;
                            this.c1Report1.Fields["subDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.None;
                        }

                        #endregion Direct Installs

                        #region Recommendations

                        if (showAllRecommendations || showTopRecommendations)
                        {
                            this.c1Report1.Fields["subRec"].Visible = true;
                        }
                        else
                        {
                            this.c1Report1.Fields["subRec"].Visible = false;
                        }

                        int recPageCount = 2;
                        int recOptionPageCount = 1;
                        int recsPerPageCount = 0;
                        int recRank = 1;

                        foreach (Building building in audit.Buildings)
                        {
                            // If Recommendations is not checked, don't show any recommendations.
                            if (building.Recommendations.Count == 0)
                            {
                                // There were no recommendations
                                this.c1Report1.Fields["subRec"].Visible = false;
                                ReportRecommendation Recommend = new ReportRecommendation();
                                Recommend.RecommendationID = "0";
                                Recommend.RecommendationName = "None";
                                Recommend.RecommendationDescription = "None";
                                auditReport.Recommendations.Add(Recommend);

                                break;
                            }

                            var sortedrecs = building.Recommendations.ToList();
                            sortedrecs.Sort(new RecommendationRankSort_ASC());

                            if (showTopRecommendations)
                            {
                                if (NumRecommendations == 0)
                                {
                                    iRecCount = building.Recommendations.Count();
                                }
                                else
                                {
                                    iRecCount = NumRecommendations;
                                }
                            }
                            else
                            {
                                iRecCount = building.Recommendations.Count();
                            }

                            foreach (Recommendation rec in sortedrecs)
                            {
                                if (rec.IncludeInReport == true)
                                {
                                    if (iCtr > iRecCount)
                                    {
                                        recPageCount++;
                                        recOptionPageCount++;
                                        break;
                                    }
                                    recommendationid = rec.RecommendationId.ToString();
                                    ReportRecommendation Recommend = new ReportRecommendation();
                                    Recommend.RecommendationID = rec.RecommendationId.ToString();
                                    Recommend.RecommendationName = rec.RecommendationNameOnly;
                                    Recommend.RecommendationDescription = rec.RecommendationDescription;
                                    Recommend.BuildingID = building.Id;

                                    // SMM - 6/23/2015
                                    if (rec.OriginalEquipments != null && rec.OriginalEquipments.Count > 0)
                                    {
                                        if (rec.IsOccupancySensor == true)
                                        {
                                            Recommend.ExistingComponentName = "Non-occupancy lighting";
                                        }
                                        else
                                        {
                                            Recommend.ExistingComponentName = rec.OriginalEquipments[0].ComponentName;
                                        }
                                    }

                                    // Iterate through each building and grab the recommendation info
                                    // Add to the collection
                                    foreach (RecommendationOption option in rec.RecommendationOptions)
                                    {
                                        RecommendationOpt recs = new RecommendationOpt();

                                        recs.RecommendationName = rec.RecommendationName;

                                        recs.RecommendationOptionID = option.RecommendationOptionId;
                                        recs.ReportRank = rec.ReportRank.ToString();
                                        recs.Savings = Convert.ToDecimal(option.Saving);
                                        recs.Rebate = Convert.ToDecimal(option.Rebate);
                                        recs.kWHSaved = option.KwhSaved;
                                        recs.ThermsSaved = option.ThermsSaved;

                                        Recommend.ReplacementIncentive = Convert.ToDecimal(option.Rebate);
                                        Recommend.ReplacementAnnualSavings = Convert.ToDecimal(option.Saving);
                                        Recommend.ReplacementAnnualSavingsFormatted = "$" + Math.Round(Recommend.ReplacementAnnualSavings, 0);
                                        Recommend.RecommendationID = recommendationid.ToString();
                                        Recommend.reportRank = Convert.ToInt32(rec.ReportRank);
                                        Recommend.KwhSaved = Convert.ToDecimal(option.KwhSaved);
                                        Recommend.ThermsSaved = Convert.ToDecimal(option.ThermsSaved);

                                        // SMM - 6/23/2015
                                        if (rec.RecommendationOptions != null && rec.RecommendationOptions.Count > 0)
                                        {
                                            if (rec.IsOccupancySensor == true)
                                            {
                                                Recommend.TopOptionComponentName = "Add occupancy sensor to all";
                                            }
                                            else
                                            {
                                                Recommend.TopOptionComponentName = option.ReplacementEquipment.Name;// rec.RecommendationOptions[0].RecommendationOptionName;
                                            }
                                        }

                                        //new for occupancy sensors
                                        if (rec.IsOccupancySensor == true)
                                        {
                                            dTotalRebates += option.Rebate;
                                            dTotalkWhSaved += option.KwhSaved;
                                            dTotalAnnualCostSavings += option.Saving;
                                            iControlQuantity += option.OriginalEquipment.ControlQuantity;

                                            Recommend.KwhSaved = Convert.ToDecimal(dTotalkWhSaved);
                                            Recommend.ReplacementAnnualSavings = Convert.ToDecimal(dTotalAnnualCostSavings);
                                            Recommend.ReplacementAnnualSavingsFormatted = "$" + Math.Round(Recommend.ReplacementAnnualSavings, 0);
                                            Recommend.ReplacementIncentive = Convert.ToDecimal(dTotalRebates);
                                        }

                                        //                                        auditReport.RecommendationOptions.Add(recs);
                                        recsPerPageCount++;
                                        if (recPageCount == 1)
                                        {
                                            if (recsPerPageCount >= 4)
                                            {
                                                recPageCount++;
                                                recsPerPageCount = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (recsPerPageCount >= 5)
                                            {
                                                recPageCount++;
                                                recsPerPageCount = 0;
                                            }
                                        }
                                    }

                                    foreach (EquipmentMaster ExistEquip in rec.OriginalEquipments)
                                    {
                                        // Add existing equip to list
                                        ExistingEquipment ExistE = new ExistingEquipment();

                                        ExistE.ExistingEquipmentDesc = ExistEquip.EquipmentName;
                                        ExistE.ExistingEquipmentQuantity = ExistEquip.Quantity;
                                        ExistE.ExistingEquipmentDesc = ExistEquip.EquipmentDescription;
                                        ExistE.RecommendationID = rec.RecommendationId.ToString();
                                        ExistE.ImagePath = ExistEquip.ImageFilePath;

                                        if (ExistEquip.Spaces._items.Count == 0)
                                        {
                                            throw new Exception("Existing equipment Spaces has no items");
                                        }

                                        Recommend.EquipmentSpace = ExistEquip.Spaces._items[0].Space;
                                        Recommend.ExistingEquipDescription = ExistEquip.EquipmentName;

                                        if (rec.IsOccupancySensor == true)
                                        {
                                            Recommend.Quantity = iControlQuantity;
                                        }
                                        else
                                        {
                                            Recommend.Quantity += ExistEquip.Quantity;
                                        }

                                        Recommend.ExistingEquipment.Add(ExistE);
                                    }

                                    // Recommendation Details
                                    foreach (RecommendationOption recoptions in rec.RecommendationOptions)
                                    {
                                        // Add replacement equip to list
                                        ReplacementEquipment ReplacementEquip = new ReplacementEquipment();

                                        if (rec.IsOccupancySensor == true)
                                        {
                                            ReplacementEquip.ReplacementDescription = "Non-occupancy lighting";
                                        }
                                        else
                                        {
                                            ReplacementEquip.ReplacementDescription = recoptions.ReplacementEquipment.Name; //recoptions.RecommendationEquipment.EquipmentName;
                                        }

                                        ReplacementEquip.ReplacementSavingsTherms = recoptions.ReplacementEquipment.ThermalEfficiency;// RecExisting.RecommendationEquipment.ThermalEfficiency;

                                        if (rec.IsOccupancySensor == true)
                                        {
                                            ReplacementEquip.ReplacementIncentive = dTotalRebates;
                                            ReplacementEquip.ReplacementAnnualSavings = dTotalAnnualCostSavings;
                                        }
                                        else
                                        {
                                            ReplacementEquip.ReplacementIncentive = recoptions.ReplacementEquipment.Efficiency;//.Rebate;
                                            ReplacementEquip.ReplacementAnnualSavings = recoptions.Saving;//   RecReplace.sa   RecExisting.Saving;
                                        }
                                        ReplacementEquip.RecommendationID = rec.RecommendationId.ToString();// rec.RecommendationId;

                                        Recommend.ReplacementEquipment.Add(ReplacementEquip);

                                        dTotalAnnualCostSavings = 0;
                                        dTotalRebates = 0;
                                        dTotalkWhSaved = 0;
                                        iControlQuantity = 0;
                                    }

                                    foreach (ExistingEquipment ex in Recommend.ExistingEquipment)
                                    {
                                        if (ex.RecommendationID == rec.RecommendationId.ToString())
                                        {
                                            Recommend.ImagePath = ex.ImagePath;
                                            break;
                                        }
                                    }

                                    if (!String.IsNullOrEmpty(Recommend.ImagePath))
                                    {
                                        rptRecOptions.Subreport.Fields["RecImage"].Picture = Recommend.ImagePath;
                                    }
                                    else
                                    {
                                        rptRecOptions.Subreport.Fields["RecImage"].Picture = null;
                                    }

                                    Recommend.reportRank = recRank++;
                                    auditReport.Recommendations.Add(Recommend);
                                    if (recPageCount == 1)
                                    {
                                        if (recsPerPageCount >= 4)
                                        {
                                            recPageCount++;
                                            recsPerPageCount = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (recsPerPageCount >= 5)
                                        {
                                            recPageCount++;
                                            recsPerPageCount = 0;
                                        }
                                    }
                                    iCtr++;
                                    recOptionPageCount++;
                                }
                            }
                        }

                        if (this.c1Report1.Fields["subRec"].Visible)
                        {
                            iPageCount += recPageCount;
                        }

                        #endregion Recommendations

                        #region Recommendation Options

                        if (showRecommendationOptions)
                        {
                            this.c1Report1.Fields["subRecOptions"].Visible = true;
                            iPageCount += recOptionPageCount;
                            //this.c1Report1.Fields["subNext"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.Before;
                            this.c1Report1.Fields["subGasElectric"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.Before;
                        }
                        else
                        {
                            this.c1Report1.Fields["subRecOptions"].Visible = false;
                        }

                        #endregion Recommendation Options

                        #region Gas and Electric History

                        foreach (Building building in audit.Buildings)
                        {
                            // If only gas history shown then
                            //iDegreeDays = 0;

                            if ((building.GasHistory.Count > 0 && showGasHistoryGraph) || (building.ElectricHistory.Count > 0 && showElectricHistoryGraph))
                            {
                                auditReport.hasGasElectricHistory = true;
                                if (showGasHistoryGraph)
                                {
                                    auditReport.GasChartImagePath = DataStore.ChartImagePath + "gashistory" + this.AuditID + ".bmp";
                                    rpt.Subreport.Fields["chtGas"].Picture = auditReport.GasChartImagePath.ToString();
                                    foreach (GasHistoryRecord gasHistoryRecord in building.GasHistory)
                                    {
                                        auditReport.GasHistConsumption.Add(new GasHistory(gasHistoryRecord, ref iDegreeDays));
                                    }
                                    auditReport.HideGasChart = false;
                                    sChartDescription = "The line graph below illustrates previous 12 months of annual gas usage for " + auditReport.CompanyName + " - ";
                                }
                                if (showElectricHistoryGraph)
                                {
                                    auditReport.ElectricChartImagePath = DataStore.ChartImagePath + "electrichistory" + this.AuditID + ".bmp";
                                    rpt.Subreport.Fields["chtElectric"].Picture = auditReport.ElectricChartImagePath.ToString();
                                    foreach (ElectricHistoryRecord electricHistoryRecord in building.ElectricHistory)
                                    {
                                        auditReport.ElectricHistConsumption.Add(new ElectricHistory(electricHistoryRecord, ref iDegreeDays));
                                    }
                                    auditReport.HideElectricChart = false;
                                    sChartDescription = "The line graph below illustrates previous 12 months of annual electric usage for " + auditReport.CompanyName + " - ";
                                }
                                if (showGasHistoryGraph && showElectricHistoryGraph)
                                {
                                    sChartDescription = "The line graphs below illustrate previous 12 months of annual gas and electric usage for " + auditReport.CompanyName + " - ";
                                }

                                auditReport.EUIChartImagePath = DataStore.ChartImagePath + "EUI" + this.AuditID + ".bmp";

                                foreach (EnergyUsageIndex EUI in building.EnergyUsage)
                                {
                                    EUIData UsageHistory = new EUIData();
                                    UsageHistory.EUIFacilityDescription = EUI.EUIDescription;
                                    UsageHistory.RankingStatement = EUI.EUIRankText;
                                    UsageHistory.AverageUsage = EUI.AverageUsage.ToString();
                                    UsageHistory.YourUsage = EUI.YourUsage.ToString();
                                    UsageHistory.YourCost = EUI.YourCost.ToString();
                                    UsageHistory.TargetCost = EUI.TargetCost;
                                    UsageHistory.TargetSavings = EUI.TargetSavings.ToString();
                                    UsageHistory.TargetUsage = EUI.TargetUsage.ToString();
                                    UsageHistory.TotalElectricUsage = EUI.TotalElectricUsage.ToString();
                                    UsageHistory.TotalGasUsage = EUI.TotalGasUsage.ToString();
                                    UsageHistory.TotalFloorArea = EUI.TotalFloorArea.ToString();
                                    UsageHistory.TotalEnergyCosts = EUI.TotalEnergyCosts.ToString();
                                    UsageHistory.facilityType = EUI.FacilityType.ToString();
                                    UsageHistory.EUIChartImagePath = DataStore.ChartImagePath + "EUI" + this.AuditID + ".bmp";

                                    rptEUI.Subreport.Fields["EUIGauge"].Picture = auditReport.EUIChartImagePath.ToString();

                                    auditReport.EnergyUsage.Add(UsageHistory);
                                }

                                sChartDescription += auditReport.AddressLine1;

                                if (!String.IsNullOrEmpty(auditReport.AddressLine2))
                                {
                                    sChartDescription += " " + auditReport.AddressLine2;
                                }
                                else
                                {
                                    sChartDescription += " " + auditReport.City + ", " + auditReport.State + " " + auditReport.Zip;
                                }

                                rpt.Subreport.Fields["txtDescription"].Value = sChartDescription;

                                this.c1Report1.Fields["subGasElectric"].Visible = true;
                                //this.c1Report1.Fields["brkRec"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
                                iPageCount++;
                            }
                            else
                            {
                                rpt.Subreport.Fields["chtElectric"].Picture = null;
                                rptEUI.Subreport.Fields["EUIGauge"].Picture = null;
                                rpt.Subreport.Fields["chtGas"].Picture = null;
                                this.c1Report1.Fields["subGasElectric"].Visible = false;
                            }
                        }

                        #endregion Gas and Electric History

                        #region Next Steps

                        auditReport.NextSteps.sDescription = "Next Steps";

                        #endregion Next Steps

                        #region Signature report

                        //Added 8/12/15 by Mike Crowell for Signature report
                        if (showSignatureReport)
                        {
                            if (audit.InkSecureSignatureData != null)
                            {
                                Microsoft.TabletPC.Samples.InkSecureSignature.InkSecureSignature sig = new Microsoft.TabletPC.Samples.InkSecureSignature.InkSecureSignature();
                                XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document,
                                // Initializes a new instance of the XmlDocument class.
                                XmlSerializer xmlSerializer = new XmlSerializer(audit.InkSecureSignatureData.GetType());
                                // Creates a stream whose backing store is memory.
                                using (MemoryStream xmlStream = new MemoryStream())
                                {
                                    xmlSerializer.Serialize(xmlStream, audit.InkSecureSignatureData);
                                    xmlStream.Position = 0;
                                    //Loads the XML document from the specified string.
                                    xmlDoc.Load(xmlStream);
                                    sig.Value = xmlDoc.InnerXml;
                                }

                                int strWidth;
                                int strHeight;
                                Image picSignature;
                                string strSignature;

                                //We need to set a large height and width initially to grab the entire ink drawing area and ensure we get the full signature.
                                strWidth = 1700;
                                strHeight = 2000;

                                //Load the signature xml data into the ink control
                                strSignature = sig.Value;

                                // Create a new bitmap the size of the print output window
                                Bitmap mPrintBitmap = new Bitmap(strWidth, strHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                                // Create a graphics context from that bitmap image
                                Graphics graphics = System.Drawing.Graphics.FromImage(mPrintBitmap);

                                //Set the width and height of the control, which is used to define the width and heigth of the biometrics outline box.
                                sig.Width = 800;
                                sig.Height = 200;

                                // Print the InkSecureSignature to the bitmap
                                sig.Print(graphics, new Point(1, 1));

                                // Clean up
                                graphics.Dispose();

                                //Now that we have a printed image of the signature, we need to resize it

                                //set new width and height for image
                                int newWidth = 1200;
                                int newHeight = 1000;

                                //Create a new bitmap with the new dimensions for the resized image
                                Bitmap resizeBitmap = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                                resizeBitmap.SetResolution(mPrintBitmap.HorizontalResolution, mPrintBitmap.VerticalResolution);

                                //create a new graphics context from the resized image
                                graphics = Graphics.FromImage(resizeBitmap);
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                                //draw the resized image
                                graphics.DrawImage(mPrintBitmap,
                                    new Rectangle(0, 0, newWidth, newHeight),
                                    new Rectangle(0, 0, strWidth, strHeight),
                                    GraphicsUnit.Pixel);

                                // Clean up
                                graphics.Dispose();

                                // Put the bitmap in the print preview window
                                picSignature = resizeBitmap;

                                rptSignature.Subreport.Fields["SignatureImage"].Picture = picSignature;

                                auditReport.SignatureXML = sig.Value;

                                this.c1Report1.Fields["subSig"].Visible = true;
                                iPageCount++;
                            }
                            else
                            {
                                MessageBox.Show("Signature has not been set");
                                this.c1Report1.Fields["subSig"].Visible = false;
                            }
                        }
                        else
                        {
                            this.c1Report1.Fields["subSig"].Visible = false;
                        }

                        #endregion Signature report

                        if (showEnergyUsageChart)
                        {
                            this.c1Report1.Fields["subEUI"].Visible = true;
                            iPageCount++;
                        }

                        if (showEnergyUsageChart)
                        {
                            if (auditReport.EnergyUsage.Count() == 0)
                            {
                                throw new Exception("AuditReport has no EnergyUsages");
                            }
                            sChartDescription = auditReport.EnergyUsage[0].EUIFacilityDescription;
                        }

                        //Added 8/13/15 by Mike Crowell
                        // Right now the signature is only printed from the sign off tab
                        //   and the signature page is all that should print.
                        // In the future, when the signature is also included as an optional page on
                        //   the audit report, this code will hide all other sections when the print was initiated
                        //   from the sign off tab.
                        if (fromSignOffTab)
                        {
                            foreach (C1.C1Report.Field field in this.c1Report1.Fields)
                            {
                                field.Visible = false;
                            }
                            this.c1Report1.Fields["Field21"].Visible = true; //Cover page. Need to show, otherwise a blank page prints before signature page.
                            this.c1Report1.Fields["Field4"].Visible = true; //Company Info
                            this.c1Report1.Fields["subSig"].Visible = true;
                            iPageCount = 2;
                        }
                    }
                }
            }

            if (ReportBranding.HideKwh && !this.IsTableAdjusted)
            {
                rptDI.Subreport.Fields["KwHLbl"].Visible = false;
                rptDI.Subreport.Fields["KwHCtl"].Visible = false;
                rptDI.Subreport.Fields["fldKwhSum"].Visible = false;

                rptDI.Subreport.Fields["QuantityLbl"].Left = rptDI.Subreport.Fields["KwHLbl"].Left;
                rptDI.Subreport.Fields["QuantityCtl"].Left = rptDI.Subreport.Fields["KwHCtl"].Left;
                rptDI.Subreport.Fields["lblEmptyCell"].Left = rptDI.Subreport.Fields["fldKwhSum"].Left;

                rptDI.Subreport.Fields["DescriptionLbl"].Width += rptDI.Subreport.Fields["KwHLbl"].Width;
                rptDI.Subreport.Fields["DescriptionCtl"].Width += rptDI.Subreport.Fields["KwHCtl"].Width;
                rptDI.Subreport.Fields["lblTotalSavings"].Width += rptDI.Subreport.Fields["fldKwhSum"].Width;

                rptRec.Subreport.Fields["lblKwh"].Visible = false;
                rptRec.Subreport.Fields["fldKwh"].Visible = false;

                double splitWidth = rptRec.Subreport.Fields["lblKwh"].Width / 2;

                rptRec.Subreport.Fields["lblMcf"].Left = rptRec.Subreport.Fields["lblKwh"].Left;
                rptRec.Subreport.Fields["fldMcf"].Left = rptRec.Subreport.Fields["fldKwh"].Left;
                rptRec.Subreport.Fields["lblCost"].Left = rptRec.Subreport.Fields["lblCost"].Left - splitWidth;
                rptRec.Subreport.Fields["fldCost"].Left = rptRec.Subreport.Fields["fldCost"].Left - splitWidth;

                rptRec.Subreport.Fields["lblMcf"].Width += splitWidth;
                rptRec.Subreport.Fields["fldMcf"].Width += splitWidth;
                rptRec.Subreport.Fields["lblCost"].Width += splitWidth;
                rptRec.Subreport.Fields["fldCost"].Width += splitWidth;

                this.IsTableAdjusted = true;
            }
            if (ReportBranding.HideMCF && !this.IsTableAdjusted)
            {
                rptDI.Subreport.Fields["ThermsLbl"].Visible = false;
                rptDI.Subreport.Fields["ThermsCtl"].Visible = false;
                rptDI.Subreport.Fields["fldMcfSum"].Visible = false;

                rptDI.Subreport.Fields["QuantityLbl"].Left = rptDI.Subreport.Fields["KwHLbl"].Left;
                rptDI.Subreport.Fields["QuantityCtl"].Left = rptDI.Subreport.Fields["KwHCtl"].Left;
                rptDI.Subreport.Fields["lblEmptyCell"].Left = rptDI.Subreport.Fields["fldKwhSum"].Left;

                rptDI.Subreport.Fields["KwHLbl"].Left = rptDI.Subreport.Fields["ThermsLbl"].Left;
                rptDI.Subreport.Fields["KwHCtl"].Left = rptDI.Subreport.Fields["ThermsCtl"].Left;
                rptDI.Subreport.Fields["fldKwhSum"].Left = rptDI.Subreport.Fields["fldMcfSum"].Left;

                rptDI.Subreport.Fields["DescriptionLbl"].Width += rptDI.Subreport.Fields["ThermsLbl"].Width;
                rptDI.Subreport.Fields["DescriptionCtl"].Width += rptDI.Subreport.Fields["ThermsCtl"].Width;
                rptDI.Subreport.Fields["lblTotalSavings"].Width += rptDI.Subreport.Fields["fldMcfSum"].Width;

                rptRec.Subreport.Fields["lblMcf"].Visible = false;
                rptRec.Subreport.Fields["fldMcf"].Visible = false;

                double splitWidth = rptRec.Subreport.Fields["lblMcf"].Width / 2;

                rptRec.Subreport.Fields["lblCost"].Left = rptRec.Subreport.Fields["lblCost"].Left - splitWidth;
                rptRec.Subreport.Fields["fldCost"].Left = rptRec.Subreport.Fields["fldCost"].Left - splitWidth;

                //rptRec.Subreport.Fields["lblKwh"].Width += rptRec.Subreport.Fields["lblMcf"].Width;
                //rptRec.Subreport.Fields["fldKwh"].Width += rptRec.Subreport.Fields["fldMcf"].Width;
                rptRec.Subreport.Fields["lblKwh"].Width += splitWidth;
                rptRec.Subreport.Fields["fldKwh"].Width += splitWidth;
                rptRec.Subreport.Fields["lblCost"].Width += splitWidth;
                rptRec.Subreport.Fields["fldCost"].Width += splitWidth;

                this.IsTableAdjusted = true;
            }

            NewFormat.obj.Add(auditReport);

            this.c1Report1.Fields["subIntro"].Visible = showIntro;  //Introduction page

            File.Create(FilePath).Close();

            using (StreamWriter writer = new StreamWriter(FilePath, false, Encoding.UTF8))
            {
                using (TextWriter ssw = TextWriter.Synchronized(writer))
                {
                    XmlSerializer s = new XmlSerializer(typeof(Report.ReportCollection));//was company collection
                    s.Serialize(ssw, NewFormat);
                }
            }

            //this.c1PrintPreviewControl1.Document = this.c1Report1.Document;
            //ComponentOne bug-changed this to reference the c1report1 object instead of its document
            //It was causing the internal 'save to PDF' function to error out

            var currentReportXmlPath = @"C:\Users\Public\Documents\FE\EC\dat\Report.xml";
            var newReportXmlPath = this.FilePath;

            if (newReportXmlPath != currentReportXmlPath)
            {
                var currentDefString = this.c1Report1.ReportDefinition;
                var reportDefString = currentDefString.Replace(currentReportXmlPath, newReportXmlPath);
                this.c1Report1.ReportDefinition = reportDefString;
            }

            this.c1Report1.MaxPages = iPageCount;  //Limit max number of pages allowed to the actual number of pages defined to hopefully limit any blank pages that get generated.
            this.c1PrintPreviewControl1.Document = this.c1Report1.C1Document;
        }

        private string GetContactName()
        {
            string result;

            if (!String.IsNullOrWhiteSpace(this.ContactNameOverride))
            {
                result = this.ContactNameOverride;
            }
            else if (!String.IsNullOrWhiteSpace(this.PrimaryContact))
            {
                result = this.PrimaryContact;
            }
            else
            {
                result = "";
            }

            return result;
        }

        #endregion Private helper methods
    }
}