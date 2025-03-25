using C1.C1Report;
using FieldTool.BLL;
using FieldTool.Bsi.Models;
using FieldTool.Constants.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

//using System.recordsourceCollections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    public partial class frmMultiFamilyCustomFirstEnergyOHReport : Form
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
        private ClientMultifamilyReportBranding _reportBranding;
        private ApiProgramMetadata _program;
        private bool _isTableAdjusted;

        #endregion Private member variables

        #region Constructors

        public frmMultiFamilyCustomFirstEnergyOHReport(string activeUserFullName, string auditID, string companyNameOverride, string contactNameOverride, string energyAdvisorNameOverride, string energyAdvisorPhone, string energyAdvisorEmail, string topRecommendation, List<string> filters, bool fromSignOffTab, Company.CompanyCollection coList, string filePath, ClientMultifamilyReportBranding reportBranding)
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
            this.ReportBranding = reportBranding;
            this.EnergyAdvisorPhone = energyAdvisorPhone;
            this.EnergyAdvisorEmail = energyAdvisorEmail;
            this.TopRecommendation = topRecommendation;
            this.IsTableAdjusted = false;

            _program = DataStore.GetProgramByAuditId(auditID);

            ApplyBranding(reportBranding);
        }

        #endregion Constructors

        private void ApplyBranding(ClientMultifamilyReportBranding b)
        {
            Lg.Info("Applying Branding");
            try
            {
                // SubDI
                Field subDI = c1Report1.Fields["subDI"];
                SetTableHeader(subDI.Subreport.Fields["DescriptionLbl"], b);
                SetTableHeader(subDI.Subreport.Fields["QuantityLbl"], b);
                SetTableHeader(subDI.Subreport.Fields["KwHLbl"], b);
                SetTableHeader(subDI.Subreport.Fields["CostSavingsLbl"], b);
                SetColor(subDI.Subreport.Fields["DISumBackground"], b.RetrofitTotalBackColor, "");

                // SubUnitDIEstimator
                Field subUnitDI = c1Report1.Fields["subUnitDI"];
                SetTableHeader(subUnitDI.Subreport.Fields["DescriptionLbl"], b);
                SetTableHeader(subUnitDI.Subreport.Fields["QuantityLbl"], b);
                SetTableHeader(subUnitDI.Subreport.Fields["KwHLbl"], b);
                SetTableHeader(subUnitDI.Subreport.Fields["CostSavingsLbl"], b);
                SetColor(subUnitDI.Subreport.Fields["DIEstimatorSumBackground"], b.RetrofitTotalBackColor, "");

                // subRec
                Field subRec = c1Report1.Fields["subRec"];
                //SetTableHeader(subRec.Subreport.Fields["Field1"], b);
                SetTableHeader(subRec.Subreport.Fields["lblExisting"], b);
                SetTableHeader(subRec.Subreport.Fields["lblProposed"], b);
                SetTableHeader(subRec.Subreport.Fields["lblSpace"], b);

                // SubRecSavings
                Field subrecSavings = subRec.Subreport.Fields["subrecSavings"];
                SetColor(subrecSavings.Subreport.Fields["Field1"], b.RetrofitTotalBackColor, "");
                SetTableHeader(subrecSavings.Subreport.Fields["Field2"], b);
                SetTableHeader(subrecSavings.Subreport.Fields["Field3"], b);
                SetTableHeader(subrecSavings.Subreport.Fields["Field4"], b);
                SetColor(subrecSavings.Subreport.Fields["Field9"], b.RetrofitTotalBackColor, "");
                SetColor(subrecSavings.Subreport.Fields["Field10"], b.RetrofitTotalBackColor, "");
                SetColor(subrecSavings.Subreport.Fields["Field11"], b.RetrofitTotalBackColor, "");

                // SubReplacementEquipment
                Field subRecOptions = c1Report1.Fields["subRecOptions"];
                Field subOptions = subRecOptions.Subreport.Fields["Field4"];
                SetTableHeader(subOptions.Subreport.Fields["Field9"], b);
                SetTableHeader(subOptions.Subreport.Fields["Field12"], b);
                SetTableHeader(subOptions.Subreport.Fields["Field11"], b);
            }
            catch (Exception e)
            {
                Lg.FatalError(e, "ApplyBranding");
                throw;
            }
        }

        private void SetColor(Field f, string backColor, string foreColor)
        {
            try
            {
                f.BackColor = string.IsNullOrEmpty(backColor) ? f.BackColor : ColorTranslator.FromHtml(backColor);
                f.ForeColor = string.IsNullOrEmpty(foreColor) ? f.ForeColor : ColorTranslator.FromHtml(foreColor);
            }
            catch (Exception e)
            {
                Lg.FatalError(e, "Failed to set color");
                throw;
            }
        }

        private void SetTableHeader(Field f, ClientMultifamilyReportBranding b)
        {
            SetColor(f, b.TableHeaderBackColor, b.TableHeaderForeColor);
        }

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

        private ClientMultifamilyReportBranding ReportBranding
        {
            get
            {
                return this._reportBranding;
            }
            set
            {
                this._reportBranding = (value ?? new ClientMultifamilyReportBranding());
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

        #endregion Properties

        #region Events

        private void frmMultiFamilyCustomFirstEnergyOHReport_Load(object sender, EventArgs e)
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

        private string SetSubDITextWithVariables(string text)
        {
            var kwh = "";
            var mcf = "";

            if (_program != null)
            {
                if (_program.program != null)
                {
                    kwh = _program.program.UtilityRatePerKWH != null ? _program.program.UtilityRatePerKWH : "";
                    mcf = _program.program.UtilityRatePerMCF != null ? _program.program.UtilityRatePerMCF : "";
                }
            }

            if (!string.IsNullOrEmpty(kwh) && !string.IsNullOrEmpty(mcf))
            {
                text = text.Replace("@VAR_ONE@", $"{Convert.ToDecimal(kwh).ToString("C", CultureInfo.CurrentCulture)}/kWh");
                text = text.Replace("@VAR_TWO@", $"{Convert.ToDecimal(mcf).ToString("C", CultureInfo.CurrentCulture)}/MCF");
            }
            else
            {
                text = text.Replace("@VAR_ONE@", $"{Convert.ToDecimal(Constants.ReportConstants.DEFAULT_KWH).ToString("C", CultureInfo.CurrentCulture)}/kWh");
                text = text.Replace("@VAR_TWO@", $"{Convert.ToDecimal(Constants.ReportConstants.DEFAULT_MCF).ToString("C", CultureInfo.CurrentCulture)}/MCF");
            }

            return text;
        }

        private ReportRecommendationOption ConvertRecommendationToOption(ReportRecommendation r)
        {
            var opt = new ReportRecommendationOption();

            opt.RecommendationID = r.RecommendationID;
            opt.RecommendationDescription = r.RecommendationDescription;
            opt.RecommendationName = r.RecommendationName;
            opt.ImagePath = r.ImagePath;
            opt.reportRank = r.reportRank;
            opt.RecommendationTitle = r.RecommendationTitle;
            opt.ExistingEquipDescription = r.ExistingEquipDescription;
            opt.EquipmentSpace = r.EquipmentSpace;
            opt.ReplacementSavingsTherms = r.ReplacementSavingsTherms;
            opt.ReplacementAnnualSavings = r.ReplacementAnnualSavings;
            opt.ReplacementIncentive = r.ReplacementIncentive;
            opt.MCF = r.MCF;
            opt.Quantity = r.Quantity;
            opt.KwhSaved = r.KwhSaved;
            opt.ThermsSaved = r.ThermsSaved;

            opt.BuildingID = r.BuildingID;
            opt.ExistingComponentName = r.ExistingComponentName;
            opt.TopOptionComponentName = r.TopOptionComponentName;

            foreach (ReplacementEquipment re in r.ReplacementEquipment)
            {
                var rore = new RecOptReplacementEquipment();
                rore.RecommendationID = re.RecommendationID;
                rore.ReplacementAnnualSavings = re.ReplacementAnnualSavings;
                rore.ReplacementDescription = re.ReplacementDescription;
                rore.ReplacementIncentive = re.ReplacementIncentive;
                rore.ReplacementSavingsTherms = re.ReplacementSavingsTherms;

                opt.RecOptReplacementEquipments.Add(rore);
            }

            foreach (ExistingEquipment ee in r.ExistingEquipment)
            {
                var roee = new RecOptExistingEquipment();
                roee.RecommendationID = ee.RecommendationID;
                roee.ExistingEquipmentDesc = ee.ExistingEquipmentDesc;
                roee.ExistingEquipmentQuantity = ee.ExistingEquipmentQuantity;
                roee.ExistingEquipmentWattage = ee.ExistingEquipmentWattage;
                roee.Existingspace = ee.Existingspace;
                roee.ImagePath = ee.ImagePath;
                roee.RecommendationDescription = ee.RecommendationDescription;

                opt.RecOptExistingEquipments.Add(roee);
            }

            opt.ReplacementAnnualSavingsFormatted = r.ReplacementAnnualSavingsFormatted;

            return opt;
        }

        private void CreateReportXml()
        {
            // create an xml file for reporting

            Report auditReport = new Report();

            C1.C1Report.Field rpt = this.c1Report1.Fields["subGasElectric"];
            C1.C1Report.Field rptEUI = this.c1Report1.Fields["subEUI"];
            C1.C1Report.Field rptSignature = this.c1Report1.Fields["subSig"]; //Added 8/13/15 by Mike Crowell
            C1.C1Report.Field rptUnitDI = this.c1Report1.Fields["subUnitDI"];
            C1.C1Report.Field rptCover = this.c1Report1.Fields["subCover"];
            C1.C1Report.Field rptCompanyInfo = this.c1Report1.Fields["subCompanyInfo"];
            C1.C1Report.Field rptIntro = this.c1Report1.Fields["subIntro"];
            C1.C1Report.Field rptDI = this.c1Report1.Fields["subDI"];
            C1.C1Report.Field rptReportRec = this.c1Report1.Fields["subReportRec"];
            C1.C1Report.Field rptMeasureSummary = this.c1Report1.Fields["subMeasureSummary"];
            C1.C1Report.Field rptRec = this.c1Report1.Fields["subRec"];
            C1.C1Report.Field rptNextSteps = this.c1Report1.Fields["subNext"];
            C1.C1Report.Field rptRecOptions = this.c1Report1.Fields["subRecOptions"];

            Report.ReportCollection NewFormat = new Report.ReportCollection();
            bool showRecommendations = false;
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
            bool showUnitDirectInstall = false;
            bool showNextSteps = false;
            bool showIntro = false;
            bool auditFound = false;
            bool showRecommendationOptions = false;
            bool showOnlyTopRecOption = false;

            foreach (Company c in this.CoList)
            {
                iCtr = 1;
                sField = "";

                showRecommendations = false;
                showRecommendationOptions = false;
                showSignatureReport = false; //Added 8/13/15 by Mike Crowell
                showElectricHistoryGraph = false;
                showGasHistoryGraph = false;
                showDirectInstallSummary = false;
                showUnitDirectInstall = false;
                showNextSteps = false;

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
                                case "Recommendations":
                                    showRecommendations = true;
                                    break;

                                case "Recommendation Options":
                                    showRecommendationOptions = true;
                                    break;

                                case "Next Steps":
                                    showNextSteps = true;
                                    iPageCount++;
                                    break;

                                case "Building Direct Install Summary":
                                    showDirectInstallSummary = true;
                                    break;

                                case "Unit Direct Install Estimator":
                                    showUnitDirectInstall = true;
                                    break;

                                case "Show Energy Usage Chart":
                                    showEnergyUsageChart = true;
                                    break;

                                case "Show Electric History":
                                    showElectricHistoryGraph = true;
                                    break;

                                case "Show Gas History":
                                    showGasHistoryGraph = true;
                                    break;

                                case "Signature":
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

                        if (c.Contacts != null)
                        {
                            foreach (Contact contact in c.Contacts)
                            {
                                try
                                {
                                    auditReport.ContactPhone = string.Format("{0:(###) ###-####}", double.Parse(contact.PhoneNumber));
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                        auditReport.AddressLine1 = c.AddressLine1;
                        auditReport.AddressLine2 = c.AddressLine2;
                        auditReport.AddressLine3 = c.AddressLine3;
                        auditReport.City = c.City;
                        auditReport.State = c.State;
                        auditReport.Zip = (String.IsNullOrEmpty(c.PostalCode) ? String.Empty : c.PostalCode.ToString() + (String.IsNullOrWhiteSpace(c.PostalCodeExtension) ? String.Empty : "-" + c.PostalCodeExtension));
                        auditReport.AssessmentDate = audit.ScheduledStartTimeStamp.ToShortDateString();

                        auditReport.ElectricAccountNumber = c.ElectricAccountNumber;
                        auditReport.GasAccountNumber = c.GasAccountNumber;
                        auditReport.HideElectricChart = true;
                        auditReport.HideGasChart = true;

                        #region Apply Client ReportBranding

                        if (this.ReportBranding != null)
                        {
                            if (!String.IsNullOrEmpty(ReportBranding.BrandingFilePath) && !String.IsNullOrEmpty(ReportBranding.LogoFileName))
                            {
                                rptCover.Subreport.Fields["CoverBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptIntro.Subreport.Fields["IntroBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptDI.Subreport.Fields["DIBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rpt.Subreport.Fields["GasElectBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptUnitDI.Subreport.Fields["UnitDIBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptSignature.Subreport.Fields["SigBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptRec.Subreport.Fields["RecBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptEUI.Subreport.Fields["EUIBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptNextSteps.Subreport.Fields["NextStepsBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                                rptRecOptions.Subreport.Fields["RecOptionsBannerImage"].Picture = ReportBranding.BrandingFilePath + "\\" + ReportBranding.LogoFileName;
                            }

                            rptIntro.Subreport.Fields["IntroBodyPar1"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar1) ? ReportBranding.IntroBodyPar1 : "";
                            rptIntro.Subreport.Fields["IntroBodyPar2"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar2) ? ReportBranding.IntroBodyPar2 : "";
                            rptIntro.Subreport.Fields["IntroBodyPar3"].Text = !String.IsNullOrEmpty(ReportBranding.IntroBodyPar3) ? ReportBranding.IntroBodyPar3 : "";
                            rptIntro.Subreport.Fields["IntroContactText"].Text = !String.IsNullOrEmpty(ReportBranding.IntroContactText) ? ReportBranding.IntroContactText : "";
                            rptIntro.Subreport.Fields["IntroSignatureText"].Text = !String.IsNullOrEmpty(ReportBranding.IntroSignatureText) ? ReportBranding.IntroSignatureText : "";
                            rptIntro.Subreport.Fields["fldEnergyAdvsiorPhone"].Align = FieldAlignEnum.LeftMiddle;

                            //lblDescription

                            rptRec.Subreport.Fields["RecommendationsTitle"].Text = !string.IsNullOrEmpty(ReportBranding.RecommendationTitle) ? ReportBranding.RecommendationTitle : "";
                            rptRecOptions.Subreport.Fields["RecommendationOptionsTitle"].Text = !string.IsNullOrEmpty(ReportBranding.RecommendationOptionsTitle) ? ReportBranding.RecommendationOptionsTitle : "";
                            rptUnitDI.Subreport.Fields["UnitDIEstimatorTitle"].Text = !string.IsNullOrEmpty(ReportBranding.UnitDIEstimatorTitle) ? ReportBranding.UnitDIEstimatorTitle : "";
                            rptDI.Subreport.Fields["DIInstallsTitle"].Text = !string.IsNullOrEmpty(ReportBranding.DirectInstallTitle) ? ReportBranding.DirectInstallTitle : "";

                            rptRec.Subreport.Fields["lblRecommendationsDescription"].Text = !string.IsNullOrEmpty(ReportBranding.RecommendationDescription) ? ReportBranding.RecommendationDescription : "";
                            rptUnitDI.Subreport.Fields["lblDIEstimatorDescription"].Text = !string.IsNullOrEmpty(ReportBranding.UnitDIEstimatorDescription) ? SetSubDITextWithVariables(ReportBranding.UnitDIEstimatorDescription) : "";
                            rptDI.Subreport.Fields["lblDIInstallsDescription"].Text = !string.IsNullOrEmpty(ReportBranding.SubDIInstallsDescriptionText) ? SetSubDITextWithVariables(ReportBranding.SubDIInstallsDescriptionText) : "";

                            rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Text = !String.IsNullOrEmpty(ReportBranding.SignOffDisclaimerText) ? ReportBranding.SignOffDisclaimerText : "";
                            rptNextSteps.Subreport.Fields["NextStepsContactText"].Text = !String.IsNullOrEmpty(ReportBranding.NextStepsContactText) ? ReportBranding.NextStepsContactText : "";

                            rptRec.Subreport.Fields["subrecSavings"].Visible = ReportBranding.ShowSubRecSavings;

                            if (ReportBranding.HasValidRGB)
                            {
                                Color foreColor = Color.FromArgb(ReportBranding.ColorValueRed, ReportBranding.ColorValueGreen, ReportBranding.ColorValueBlue);
                                rptDI.Subreport.Fields["DIInstallsTitle"].ForeColor = foreColor;
                                rptRec.Subreport.Fields["RecommendationsTitle"].ForeColor = foreColor;
                                rptUnitDI.Subreport.Fields["UnitDIEstimatorTitle"].ForeColor = foreColor;
                                rptNextSteps.Subreport.Fields["NextStepsTitle"].ForeColor = foreColor;
                                rptRecOptions.Subreport.Fields["RecommendationOptionsTitle"].ForeColor = foreColor;
                            }

                            if (!String.IsNullOrEmpty(ReportBranding.NextStepsStep1Header))
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep1Header"].Text = ReportBranding.NextStepsStep1Header;
                                rptNextSteps.Subreport.Fields["NextStepsStep1Detail"].Text = ReportBranding.NextStepsStep1Detail;
                                rptNextSteps.Subreport.Fields["NextStepsStep1Num"].Visible = true;
                            }
                            else
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep1Header"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep1Detail"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep1Num"].Visible = false;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.NextStepsStep2Header))
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep2Header"].Text = ReportBranding.NextStepsStep2Header;
                                rptNextSteps.Subreport.Fields["NextStepsStep2Detail"].Text = ReportBranding.NextStepsStep2Detail;
                                rptNextSteps.Subreport.Fields["NextStepsStep2Num"].Visible = true;
                            }
                            else
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep2Header"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep2Detail"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep2Num"].Visible = false;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.NextStepsStep3Header))
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep3Header"].Text = ReportBranding.NextStepsStep3Header;
                                rptNextSteps.Subreport.Fields["NextStepsStep3Detail"].Text = ReportBranding.NextStepsStep3Detail;
                                rptNextSteps.Subreport.Fields["NextStepsStep3Num"].Visible = true;
                            }
                            else
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep3Header"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep3Detail"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep3Num"].Visible = false;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.NextStepsStep4Header))
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep4Header"].Text = ReportBranding.NextStepsStep4Header;
                                rptNextSteps.Subreport.Fields["NextStepsStep4Detail"].Text = ReportBranding.NextStepsStep4Detail;
                                rptNextSteps.Subreport.Fields["NextStepsStep4Num"].Visible = true;
                            }
                            else
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep4Header"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep4Detail"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep4Num"].Visible = false;
                            }
                            if (!String.IsNullOrEmpty(ReportBranding.NextStepsStep5Header))
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep5Header"].Text = ReportBranding.NextStepsStep5Header;
                                rptNextSteps.Subreport.Fields["NextStepsStep5Detail"].Text = ReportBranding.NextStepsStep5Detail;
                                rptNextSteps.Subreport.Fields["NextStepsStep5Num"].Visible = true;
                            }
                            else
                            {
                                rptNextSteps.Subreport.Fields["NextStepsStep5Header"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep5Detail"].Text = "";
                                rptNextSteps.Subreport.Fields["NextStepsStep5Num"].Visible = false;
                            }

                            rptNextSteps.Subreport.Fields["picSocialMedia1"].Visible = ReportBranding.ShowFacebookIcon;
                            rptNextSteps.Subreport.Fields["picSocialMedia2"].Visible = ReportBranding.ShowTwitterIcon;

                            rptCover.Subreport.Fields["CoverTitle"].Text =
                                (!String.IsNullOrEmpty(ReportBranding.CoverTitle)) ?
                                    ReportBranding.CoverTitle :
                                    "Multi family Savings Report";

                            showOnlyTopRecOption = ReportBranding.ShowOnlyTopRecOption;
                        }
                        else
                        {
                            rptIntro.Subreport.Fields["IntroBody"].Text = "";
                        }

                        #endregion Apply Client ReportBranding

                        #region Direct Installs

                        if (audit.Buildings.Count == 0)
                        {
                            MessageBox.Show("The report requires a building.");
                            throw new Exception("The report requires a building.");
                        }
                        else
                        {
                            bool hasDI = false;
                            int retroPageCount = 1;
                            int retrosPerPageCount = 0;

                            foreach (Building b in audit.Buildings)
                            {
                                if (b.Retrofits.Count > 0 && showDirectInstallSummary)
                                {
                                    foreach (Retrofit retrofit in b.Retrofits)
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

                            if (hasDI)
                            {
                                this.c1Report1.Fields["subDI"].Visible = true;
                                this.c1Report1.Fields["subDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
                                //this.c1Report1.Fields["brkDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;   // Page break control
                            }
                            else
                            {
                                auditReport.DirectInstall.Add(new DirectInstalls("None Selected", 0, 0, 0, ""));
                                this.c1Report1.Fields["subDI"].Visible = false;
                                this.c1Report1.Fields["subDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.None;
                                //this.c1Report1.Fields["brkDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.None;    // Page break control
                            }
                        }

                        #endregion Direct Installs

                        #region Building Unit DI Estimator

                        if (audit.Buildings.Count == 0)
                        {
                            MessageBox.Show("The report requires a building.");
                            throw new Exception("The report requires a building.");
                        }
                        else
                        {
                            bool hasUnitDI = false;
                            int unitDIPageCount = 1;
                            int dIsPerPageCount = 0;

                            List<RetrofitEstimate> retrofits = new List<RetrofitEstimate>();
                            foreach (Building b in audit.Buildings)
                            {
                                if (b.MultiFamily != null)
                                {
                                    foreach (BuildingUnitType u in b.MultiFamily.BuildingUnitTypes)
                                    {
                                        if (u.RetrofitEstimates.Count > 0 && showUnitDirectInstall)
                                        {
                                            foreach (RetrofitEstimate retrofit in u.RetrofitEstimates)
                                            {
                                                bool foundRetrofit = false;
                                                foreach (RetrofitEstimate r in retrofits)
                                                {
                                                    if (retrofit.EligibleComponentId.Equals(r.EligibleComponentId))
                                                    {
                                                        double qty = retrofit.Quantity * u.UnitTypeQty;
                                                        decimal savings = retrofit.Savings * u.UnitTypeQty;
                                                        double kwh = retrofit.KWh * u.UnitTypeQty;
                                                        double therms = retrofit.Therms * u.UnitTypeQty;
                                                        r.Quantity += qty;
                                                        r.Savings += savings;
                                                        r.KWh += kwh;
                                                        r.Therms += therms;
                                                        foundRetrofit = true;
                                                        break;
                                                    }
                                                }
                                                if (!foundRetrofit)
                                                {
                                                    RetrofitEstimate newRetrofit = new RetrofitEstimate();
                                                    newRetrofit.Description = retrofit.Description;
                                                    newRetrofit.KWh = retrofit.KWh;
                                                    newRetrofit.Therms = retrofit.Therms;
                                                    newRetrofit.IconFileName = retrofit.IconFileName;
                                                    newRetrofit.IconPath = retrofit.IconPath;
                                                    newRetrofit.EligibleComponentId = retrofit.EligibleComponentId;
                                                    newRetrofit.Quantity = retrofit.Quantity * u.UnitTypeQty;
                                                    newRetrofit.Savings = retrofit.Savings * u.UnitTypeQty;
                                                    newRetrofit.KWh = retrofit.KWh * u.UnitTypeQty;
                                                    newRetrofit.Therms = retrofit.Therms * u.UnitTypeQty;
                                                    retrofits.Add(newRetrofit);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (RetrofitEstimate retrofit in retrofits)
                            {
                                auditReport.UnitDirectInstall.Add(new UnitDirectInstalls(retrofit, audit.ScheduledStartTimeStamp.ToString()));
                                dIsPerPageCount++;
                                if (unitDIPageCount == 1)
                                {
                                    if (dIsPerPageCount >= 12)
                                    {
                                        unitDIPageCount++;
                                        dIsPerPageCount = 0;
                                    }
                                }
                                else
                                {
                                    if (dIsPerPageCount >= 26)
                                    {
                                        unitDIPageCount++;
                                        dIsPerPageCount = 0;
                                    }
                                }
                                hasUnitDI = true;
                            }

                            iPageCount += unitDIPageCount;
                            if (hasUnitDI)
                            {
                                this.c1Report1.Fields["subUnitDI"].Visible = true;
                                //this.c1Report1.Fields["subUnitDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
                            }
                            else
                            {
                                auditReport.UnitDirectInstall.Add(new UnitDirectInstalls("None Selected", 0, 0, 0, ""));
                                this.c1Report1.Fields["subUnitDI"].Visible = false;
                                this.c1Report1.Fields["subUnitDI"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.None;
                            }
                        }

                        #endregion Building Unit DI Estimator

                        #region Recommendations

                        if (showRecommendations)
                        {
                            this.c1Report1.Fields["subRec"].Visible = true;
                        }
                        else
                        {
                            this.c1Report1.Fields["subRec"].Visible = false;
                        }

                        int recPageCount = 1;
                        int recOptionPageCount = 0;
                        int recsPerPageCount = 0;
                        int recRank = 1;

                        foreach (Building building in audit.Buildings)
                        {
                            // If Recommendations is not checked, don't show any recommendations.
                            if (building.Recommendations.Count > 0)
                            {
                                var sortedrecs = building.Recommendations.ToList();
                                sortedrecs.Sort(new RecommendationRankSort_ASC());

                                iRecCount = building.Recommendations.Count();

                                foreach (Recommendation rec in sortedrecs)
                                {
                                    if (iCtr > iRecCount)
                                    {
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
                                    //if (rec.IncludeDetailInReport)

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
                                        //if (rec.IncludeDetailInReport) {
                                        //    auditReport.RecommendationOptions.Add(recs);
                                        //}
                                        //else {
                                        //    auditReport.RecommendationDetails.Add(recs);
                                        //}

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

                                    if (showOnlyTopRecOption)
                                    {
                                        if (rec.RecommendationOptions.Count > 0)
                                        {
                                            int topIndex = rec.RecommendationOptions.Count - 1;

                                            ReplacementEquipment ReplacementEquip = new ReplacementEquipment();
                                            ReplacementEquip.ReplacementDescription = rec.RecommendationOptions[topIndex].ReplacementEquipment.Name;
                                            if (rec.IsOccupancySensor == true)
                                            {
                                                ReplacementEquip.ReplacementIncentive = dTotalRebates;
                                                ReplacementEquip.ReplacementAnnualSavings = dTotalAnnualCostSavings;
                                            }
                                            else
                                            {
                                                //ReplacementEquip.ReplacementIncentive = recoptions.ReplacementEquipment.Efficiency;//.Rebate;
                                                ReplacementEquip.ReplacementIncentive = rec.RecommendationOptions[topIndex].Rebate;
                                                ReplacementEquip.ReplacementAnnualSavings = rec.RecommendationOptions[topIndex].Saving;//   RecReplace.sa   RecExisting.Saving;
                                            }
                                            ReplacementEquip.ReplacementSavingsTherms = rec.RecommendationOptions[topIndex].ReplacementEquipment.ThermalEfficiency;
                                            ReplacementEquip.RecommendationID = rec.RecommendationId.ToString();
                                            Recommend.ReplacementEquipment.Add(ReplacementEquip);
                                        }
                                    }
                                    else
                                    {
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
                                                //ReplacementEquip.ReplacementIncentive = recoptions.ReplacementEquipment.Efficiency;//.Rebate;
                                                ReplacementEquip.ReplacementIncentive = recoptions.Rebate;
                                                ReplacementEquip.ReplacementAnnualSavings = recoptions.Saving;//   RecReplace.sa   RecExisting.Saving;
                                            }
                                            ReplacementEquip.RecommendationID = rec.RecommendationId.ToString();// rec.RecommendationId;

                                            Recommend.ReplacementEquipment.Add(ReplacementEquip);

                                            dTotalAnnualCostSavings = 0;
                                            dTotalRebates = 0;
                                            dTotalkWhSaved = 0;
                                            iControlQuantity = 0;
                                        }
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
                                        //rptRecOptions.Subreport.Fields["txtImagePath"].Text = Recommend.ImagePath;
                                    }
                                    else
                                    {
                                        rptRecOptions.Subreport.Fields["RecImage"].Picture = null;
                                        //rptRecOptions.Subreport.Fields["txtImagePath"].Text = "";
                                    }

                                    Recommend.reportRank = recRank++;

                                    if (rec.IncludeDetailInReport)
                                    {
                                        auditReport.RecommendationOptions.Add(ConvertRecommendationToOption(Recommend));
                                    }
                                    if (rec.IncludeInReport)
                                    {
                                        auditReport.Recommendations.Add(Recommend);
                                    }

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

                        if (auditReport.Recommendations.Count == 0)
                        {
                            // There were no recommendations
                            this.c1Report1.Fields["subRec"].Visible = false;
                            ReportRecommendation Recommend = new ReportRecommendation();
                            Recommend.RecommendationID = "0";
                            Recommend.RecommendationName = "None";
                            Recommend.RecommendationDescription = "None";
                            ReplacementEquipment ReplacementEquip = new ReplacementEquipment();
                            ReplacementEquip.ReplacementDescription = "None";
                            ReplacementEquip.RecommendationID = "0";
                            Recommend.ReplacementEquipment.Add(ReplacementEquip);
                            auditReport.Recommendations.Add(Recommend);
                            showRecommendationOptions = false;
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
                            this.c1Report1.Fields["subNext"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.Before;
                            this.c1Report1.Fields["subGasElectric"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.Before;
                        }
                        else
                        {
                            this.c1Report1.Fields["subRecOptions"].Visible = false;
                            this.c1Report1.Fields["subRecOptions"].CanShrink = true;
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
                                    auditReport.GasChartImagePath = DataStore.ChartImagePath + "gashistory" + audit.Id + ".bmp";
                                    rpt.Subreport.Fields["chtGas"].Picture = auditReport.GasChartImagePath.ToString();
                                    foreach (GasHistoryRecord gasHistoryRecord in building.GasHistory)
                                    {
                                        auditReport.GasHistConsumption.Add(new GasHistory(gasHistoryRecord, ref iDegreeDays));
                                    }
                                    auditReport.HideGasChart = false;
                                    sChartDescription = "The line graph below illustrates previous 12 months of annual natural gas usage for " + auditReport.CompanyName + " - ";
                                }
                                if (showElectricHistoryGraph)
                                {
                                    auditReport.ElectricChartImagePath = DataStore.ChartImagePath + "electrichistory" + audit.Id + ".bmp";
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
                                    sChartDescription = "The line graphs below illustrate previous 12 months of annual natural gas and electric usage for " + auditReport.CompanyName + " - ";
                                }

                                auditReport.EUIChartImagePath = DataStore.ChartImagePath + "EUI" + audit.Id + ".bmp";

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
                                    UsageHistory.EUIChartImagePath = DataStore.ChartImagePath + "EUI" + audit.Id + ".bmp";

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

                        this.c1Report1.Fields["brkRec"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
                        auditReport.NextSteps.sDescription = "Next Steps";

                        #endregion Next Steps

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

                                //Right now this value is a copy and paste of what is hard coded in the control on the Sign Off tab.
                                // This should probably be moved to a variable.
                                //string disclaimer = "I have been informed, as the owner or authorized representative of this business, that there is no cost for installation of the specified products, but I am responsible for verifying that products were installed. I verify that the company named above installed the specified equipment at the described property. I understand the company performing the installation is responsible for proper installation and for any defects for 12 months after installation. I understand any problems related to the installed equipment must first be communicated to the installing contractor to arrange for service or repair by the appropriate party, and failure to follow this procedure will void the 12-month product warranty and release the installing contractor and Consumers Energy Company from damage liability. I understand that in return for these free products and services offered above, I agree that the products will remain installed for a minimum of 12 months. I further agree to release and discharge Consumers Energy Company from any and all claims or damages whatsoever caused by such products and/or services.";
                                //rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Value = disclaimer;

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
                            this.c1Report1.Fields["subCover"].Visible = true; //Cover page. Need to show, otherwise a blank page prints before signature page.
                            this.c1Report1.Fields["subCompanyInfo"].Visible = true; //Company Info
                            this.c1Report1.Fields["subSig"].Visible = true;
                            iPageCount = 2;
                        }
                        auditFound = true;
                        break; //exit for loop after finding the matching audit
                    }
                    if (auditFound)
                        break;
                }
                if (auditFound)
                    break;
            }

            NewFormat.obj.Add(auditReport);

            this.c1Report1.Fields["subIntro"].Visible = showIntro;  //Introduction page

            this.c1Report1.Fields["subNext"].Visible = showNextSteps;

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

            this.c1Report1.MaxPages = 20; //iPageCount;  //Limit max number of pages allowed to the actual number of pages defined to hopefully limit any blank pages that get generated.
            this.c1PrintPreviewControl1.Document = this.c1Report1.C1Document;
        }

        #endregion Private helper methods
    }
}