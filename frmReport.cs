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
    public partial class frmReport : Form
    {
        #region Private member variables

        private string _activeUserFullName;
        private string _auditId;
        private string _companyNameOverride;
        private string _contactNameOverride;
        private string _energyAdvisorNameOverride;
        private string _energyAdvisorPhone;
        private string _energyAdvisorEmail;
        private List<string> _filters = new List<string>();
        private bool fromSignOffTab = false;
        private Company.CompanyCollection _coList;
        private string _filePath;
        private string _primaryContact;
        private int _numRecommendations;

        #endregion Private member variables

        #region Constructors

        public frmReport(string activeUserFullName, string auditID, string companyNameOverride, string contactNameOverride, string energyAdvisorNameOverride, string energyAdvisorPhone, string energyAdvisorEmail, List<string> filters, bool fromSignOffTab,
            Company.CompanyCollection coList, string filePath, string primaryContact, int numRecommendations)
        {
            InitializeComponent();

            this.ActiveUserFullName = activeUserFullName;
            this.AuditID = auditID;
            this.CompanyNameOverride = companyNameOverride;
            this.ContactNameOverride = contactNameOverride;
            this.EnergyAdvisorNameOverride = energyAdvisorNameOverride;
            this.EnergyAdvisorPhone = energyAdvisorPhone;
            this.EnergyAdvisorEmail = energyAdvisorEmail;
            this.FilterOptions = filters;
            this.fromSignOffTab = fromSignOffTab;
            this.CoList = coList;
            this.FilePath = filePath;
            this.PrimaryContact = primaryContact;
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

        private void frmReport_Load(object sender, EventArgs e)
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
            C1.C1Report.Field rptIntro = this.c1Report1.Fields["subIntro"];

            Report.ReportCollection NewFormat = new Report.ReportCollection();
            bool showTop2Recommendations = false;
            bool showTop3Recommendations = false;
            bool showTopXRecommendations = false;
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
            bool hasBuilding = false;

            foreach (Company c in this.CoList)
            {
                iRecCount = 0;
                iCtr = 1;
                sField = "";

                showTop2Recommendations = false;
                showTop3Recommendations = false;
                showTopXRecommendations = false;
                showSignatureReport = false; //Added 8/13/15 by Mike Crowell
                showElectricHistoryGraph = false;
                showGasHistoryGraph = false;
                showDirectInstallSummary = false;

                this.c1Report1.Fields["subEUI"].Visible = false;

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
                                case "Top 2 Recommendations":
                                    showTop2Recommendations = true;
                                    showTop3Recommendations = false;
                                    showTopXRecommendations = false;
                                    iRecCount = 2;
                                    break;

                                case "Top 3 Recommendations":
                                    showTop3Recommendations = true;
                                    showTop2Recommendations = false;
                                    showTopXRecommendations = false;
                                    iRecCount = 3;
                                    break;

                                case "Top X Recommendations":
                                    showTopXRecommendations = true;
                                    showTop3Recommendations = false;
                                    showTop2Recommendations = false;
                                    if (NumRecommendations == 0)
                                    {
                                        iRecCount = 1;
                                    }
                                    else
                                    {
                                        iRecCount = NumRecommendations;
                                    }
                                    break;

                                case "Direct Install Summary":
                                    showDirectInstallSummary = true;
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

                        if (audit.Buildings.Count == 0)
                        {
                            MessageBox.Show("The report requires a building.");
                            throw new Exception("The report requires a building.");
                        }
                        else
                        {
                            hasBuilding = true;

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

                            auditReport.ContactName = this.GetContactName(c, audit);

                            if (c.Contacts != null)
                            {
                                foreach (Contact contact in c.Contacts)
                                {
                                    if (contact.Phones != null && contact.Phones.Count > 0)
                                    {
                                        Phone phoneInfo = (Phone)contact.Phones[0];
                                        auditReport.ContactPhone = phoneInfo.PhoneNumber;
                                    }
                                }
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

                            if (String.IsNullOrEmpty(auditReport.EnergyAdvisorEmail))
                            {
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Visible = false;
                                rptIntro.Subreport.Fields["fldContactLine"].Top -= (int)(rptIntro.Subreport.Fields["fldIntroEnergyAdvisorEmail"].Height / 2);
                            }

                            if (String.IsNullOrEmpty(auditReport.EnergyAdvisorPhone))
                            {
                                rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Visible = false;
                                rptIntro.Subreport.Fields["fldContactLine"].Top -= (int)(rptIntro.Subreport.Fields["fldIntroEnergyAdvsiorPhone"].Height / 2);
                            }

                            #region Direct Installs

                            if (showDirectInstallSummary && audit.Buildings != null)
                            {
                                bool hasRetrofits = false;

                                foreach (Building currentBuilding in audit.Buildings)
                                {
                                    if (currentBuilding.Retrofits != null && currentBuilding.Retrofits.Count > 0)
                                    {
                                        hasRetrofits = true;

                                        int retroPageCount = 1;
                                        int retrosPerPageCount = 0;

                                        foreach (Retrofit retrofit in currentBuilding.Retrofits)
                                        {
                                            if (String.Compare(retrofit.ComponentType.Trim(), "Process/Misc", true) != 0)
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
                                            }
                                        }

                                        iPageCount += retroPageCount;
                                    }
                                }

                                if (hasRetrofits)
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

                            this.c1Report1.Fields["subRec"].Visible = true;

                            bool noRecs = true;

                            foreach (Building building in audit.Buildings)
                            {
                                var zeroChecked = building.Recommendations.All(r => r.IncludeInReport == false);
                                // If Top 2 Recommendations and Top 3 Recommendations are not checked, don't show any recommendations.
                                if (!zeroChecked && (showTop2Recommendations || showTop3Recommendations || showTopXRecommendations) && building.Recommendations.Count > 0)
                                {
                                    noRecs = false;

                                    List<Recommendation> sortedRecs = building.Recommendations.ToList();
                                    sortedRecs.Sort(new RecommendationRankSort_ASC());
                                    int reportRankCount = 1;

                                    foreach (Recommendation rec in sortedRecs)
                                    {
                                        if (rec.IncludeInReport == true)
                                        {
                                            if (showTop2Recommendations == true && iCtr > iRecCount)
                                            {
                                                break;
                                            }

                                            if (showTop3Recommendations && iCtr > iRecCount)
                                            {
                                                break;
                                            }

                                            if (showTopXRecommendations && iCtr > iRecCount)
                                            {
                                                break;
                                            }

                                            recommendationid = rec.RecommendationId.ToString();
                                            ReportRecommendation Recommend = new ReportRecommendation();
                                            Recommend.RecommendationID = rec.RecommendationId.ToString();
                                            Recommend.RecommendationName = rec.RecommendationNameOnly;
                                            Recommend.RecommendationDescription = rec.RecommendationDescription;
                                            Recommend.BuildingID = building.Id;
                                            //Recommend.reportRank = rec.ReportRank;
                                            Recommend.reportRank = reportRankCount++;

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

                                            if (rec.RecommendationOptions != null)
                                            {
                                                if (rec.RecommendationOptions.Count > 0)
                                                {
                                                    // Iterate through each building and grab the recommendation info
                                                    // Add to the collection
                                                    foreach (RecommendationOption option in rec.RecommendationOptions)
                                                    {
                                                        RecommendationOpt recs = new RecommendationOpt();

                                                        if (showTop2Recommendations == true)
                                                        {
                                                            recs.RecommendationTitle = "Top 2 Recommendations";
                                                        }
                                                        else if (showTop3Recommendations)
                                                        {
                                                            recs.RecommendationTitle = "Top 3 Recommendations";
                                                        }
                                                        else
                                                        {
                                                            recs.RecommendationTitle = "All Recommendations";
                                                        }

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
                                                        //Recommend.reportRank = Convert.ToInt32(rec.ReportRank);
                                                        Recommend.KwhSaved = Convert.ToDecimal(option.KwhSaved);
                                                        Recommend.ThermsSaved = Convert.ToDecimal(option.ThermsSaved);

                                                        // SMM - 6/23/2015
                                                        //if (rec.RecommendationOptions != null && rec.RecommendationOptions.Count > 0) {
                                                        if (rec.IsOccupancySensor == true)
                                                        {
                                                            Recommend.TopOptionComponentName = "Add occupancy sensor to all";
                                                        }
                                                        else
                                                        {
                                                            Recommend.TopOptionComponentName = option.ReplacementEquipment.Name;// rec.RecommendationOptions[0].RecommendationOptionName;
                                                        }
                                                        //}

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
                                                        //auditReport.RecommendationOptions.Add(recs);
                                                    }
                                                }
                                                else
                                                {
                                                    Recommend.TopOptionComponentName = "";
                                                    Recommend.ReplacementAnnualSavingsFormatted = "$0.00";
                                                }
                                            }

                                            if (rec.IsGeneric)
                                            {
                                                Recommend.KwhSaved = Convert.ToDecimal(rec.ReportTotalKwh);
                                                Recommend.ThermsSaved = Convert.ToDecimal(rec.ReportTotalMcf);
                                                Recommend.ReplacementAnnualSavingsFormatted = "$" + Math.Round(rec.ReportTotalSavings, 0);
                                                Recommend.ReplacementIncentive = Convert.ToDecimal(rec.ReportTotalRebate);
                                            }

                                        //Format recommendation savings fields for report
                                        if (Recommend.KwhSaved > 0)
                                        {
                                            Recommend.EnergySaved = Recommend.KwhSaved.ToString("N0") + " kWh (electricity)";
                                        }

                                        if (Recommend.ThermsSaved > 0)
                                        {
                                            Recommend.EnergySaved = Recommend.ThermsSaved.ToString("N0") + " mcf (gas)";
                                        }

                                        Recommend.ReplacementTotalSavings = (Recommend.ReplacementAnnualSavings + Recommend.ReplacementIncentive).ToString("C0");
                                        string rebateFormatted = Recommend.ReplacementIncentive.ToString("C0");
                                        Recommend.ReplacementIncentiveFormatted = "+ " + rebateFormatted;
                                        Recommend.ReplacementAnnualSavingsFormatted = Recommend.ReplacementAnnualSavings.ToString("C0");

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

                                            // -------------------------------------------------
                                            //3/9/2016: Per Eric, even those options with zero rebates should show up in the report if it is selected by the user on the main form.  There
                                            //  is no reason to filter them out on the report ("It is better to not need it and have it than to need it and not have it").
                                            // So a change was made in the RecommendationOption constructor to go ahead and add the option to the collection so that it shows up in the tool.
                                            auditReport.Recommendations.Add(Recommend);

                                            iCtr++;
                                        }
                                    }
                                }
                            }

                            if (noRecs)
                            {
                                // There were no recommendations
                                this.c1Report1.Fields["subRec"].Visible = false;
                                ReportRecommendation Recommend = new ReportRecommendation();
                                Recommend.RecommendationID = "0";
                                Recommend.RecommendationName = "None";
                                Recommend.RecommendationDescription = "None";
                                auditReport.Recommendations.Add(Recommend);
                            }

                            if (this.c1Report1.Fields["subRec"].Visible)
                            {
                                iPageCount++;

                                // SMM - 12/31/15:
                                //  I added a 3rd recommendation and the Next Steps image changed to a larger image.  If I didn't add one more page, the rendering seemed to hang.
                                iPageCount++;
                            }

                            #endregion Recommendations

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
                                    this.c1Report1.Fields["brkRec"].ForcePageBreak = C1.C1Report.ForcePageBreakEnum.After;
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

                                    //Right now this value is a copy and paste of what is hard coded in the control on the Sign Off tab.
                                    // This should probably be moved to a variable.
                                    string disclaimer = "I have been informed, as the owner or authorized representative of this business, that there is no cost for the assessment or any products that may have been installed. I verify that the company named above performed the assessment and/or installed the specified equipment at the described property. I understand the company performing the installation is responsible for any defects for 12 months after the installation. I understand any problems related to the installed equipment must first be communicated to the installing contractor to arrange for service or repair by the appropriate party, and failure to follow this procedure will void the 12-month warranty and release the installing contractor and Consumers Energy Company from damage liability. I understand that in return for these free products and services offered above, I agree that the products will remain installed for a minimum of 12 months. I further agree to release and discharge Consumer Energy Company from any and all claims or damages whatsoever caused by such products and/or services.";
                                    rptSignature.Subreport.Fields["txtSignatureDisclaimer"].Value = disclaimer;

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
            }

            if (hasBuilding)
            {
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
        }

        private string GetContactName(Company company, Audit audit)
        {
            string result;

            if (!String.IsNullOrWhiteSpace(this.ContactNameOverride))
            {
                result = this.ContactNameOverride;
            }
            //else if (!String.IsNullOrWhiteSpace(this.PrimaryContact)) {
            //    result = this.PrimaryContact;
            //}
            else
            {
                if (String.IsNullOrWhiteSpace(audit.CompanyContact))
                {
                    foreach (Contact contact in company.Contacts)
                    {
                        if (contact.FirstName != "")
                        {
                            audit.CompanyContact = contact.FirstName + " " + contact.LastName;
                        }
                    }
                }

                result = audit.CompanyContact;
            }

            return result;
        }

        #endregion Private helper methods
    }
}
