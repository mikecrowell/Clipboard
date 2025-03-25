using FieldTool.BLL;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class Report
    {
        [Serializable()]
        public class ReportCollection
        {
            [XmlElement("ReportData")]
            public List<Report> obj = new List<Report>();
        }

        public string EnergyAdvisorFullName { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public object AddressLine3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AssessmentDate { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public bool HideGasChart { get; set; }
        public bool HideElectricChart { get; set; }
        public string GasChartImagePath { get; set; }
        public string EUIChartImagePath { get; set; }
        public string EnergyAdvisorPhone { get; set; }
        public string EnergyAdvisorEmail { get; set; }
        public string TopRecommendation { get; set; }

        public string ElectricChartImagePath { get; set; }
        public bool hasGasElectricHistory { get; set; }
        public bool hasEUIData { get; set; }

        public string SignatureXML { get; set; }

        // public Overview OverviewSection = new Overview();
        public ExecutiveSummary ExecSummarySection = new ExecutiveSummary();

        public EnergyCostBreakdown EnergyCostBreakdown = new EnergyCostBreakdown();
        public NextSteps NextSteps = new NextSteps();
        public ElectricHistory ElectricHist = new ElectricHistory();
        public GasHistory GasHist = new GasHistory();
        public DirectInstalls Retrofits = new DirectInstalls();
        public DirectInstalls UnitDIs = new DirectInstalls();

        [XmlElement(IsNullable = true)]
        public string ElectricAccountNumber { get; set; }

        [XmlElement(IsNullable = true)]
        public string GasAccountNumber { get; set; }

        [XmlIgnore]
        public ReplacementEquipment ReplacementEquip = new ReplacementEquipment();

        [XmlIgnore]
        public ExistingEquipment ExistingEquip = new ExistingEquipment();

        public List<ReportRecommendationOption> RecommendationOptions = new List<ReportRecommendationOption>();
        public List<ElectricHistory> ElectricHistConsumption = new List<ElectricHistory>();
        public List<GasHistory> GasHistConsumption = new List<GasHistory>();
        public List<ReportRecommendation> Recommendations = new List<ReportRecommendation>();
        public List<DirectInstalls> DirectInstall = new List<DirectInstalls>();
        public List<UnitDirectInstalls> UnitDirectInstall = new List<UnitDirectInstalls>();
        public List<EUIData> EnergyUsage = new List<EUIData>();
    }

    [Serializable()]
    public class RecommendationOpt
    {
        public string RecommendationName { get; set; }
        public string ReportRank { get; set; }
        public decimal Savings { get; set; }
        public decimal Rebate { get; set; }
        public string RecommendationOptionID { get; set; }
        public string RecommendationTitle { get; set; }
        public double kWHSaved { get; set; }
        public double ThermsSaved { get; set; }
    }

    [Serializable()]
    public class ExistingEquipment
    {
        public string RecommendationID { get; set; }
        public int ExistingEquipmentQuantity { get; set; }
        public int ExistingEquipmentWattage { get; set; }
        public string Existingspace { get; set; }
        public string ExistingEquipmentDesc { get; set; }
        public string ImagePath { get; set; }
        public string RecommendationDescription { get; set; }
    }

    [Serializable()]
    public class ReplacementEquipment
    {
        public string RecommendationID { get; set; }
        public string ReplacementDescription { get; set; }
        public double ReplacementSavingsTherms { get; set; }
        public double ReplacementAnnualSavings { get; set; }
        public double ReplacementIncentive { get; set; }

        //public string ImagePath { get; set; }
    }

    [Serializable()]
    public class RecOptExistingEquipment
    {
        public string RecommendationID { get; set; }
        public int ExistingEquipmentQuantity { get; set; }
        public int ExistingEquipmentWattage { get; set; }
        public string Existingspace { get; set; }
        public string ExistingEquipmentDesc { get; set; }
        public string ImagePath { get; set; }
        public string RecommendationDescription { get; set; }
    }

    [Serializable()]
    public class RecOptReplacementEquipment
    {
        public string RecommendationID { get; set; }
        public string ReplacementDescription { get; set; }
        public double ReplacementSavingsTherms { get; set; }
        public double ReplacementAnnualSavings { get; set; }
        public double ReplacementIncentive { get; set; }

        //public string ImagePath { get; set; }
    }

    [Serializable()]
    public class EUIData
    {
        public string EUIFacilityDescription { get; set; }
        public string RankingStatement { get; set; }
        public string facilityType { get; set; }
        public string AverageUsage { get; set; }
        public string YourUsage { get; set; }
        public string YourCost { get; set; }
        public string TargetUsage { get; set; }
        public double TargetCost { get; set; }
        public string TargetSavings { get; set; }

        // public string EDChartImagePath { get; set; }
        public string TotalElectricUsage { get; set; }

        public string TotalGasUsage { get; set; }
        public string TotalEnergyCosts { get; set; }
        public string TotalFloorArea { get; set; }
        public string EUIChartImagePath { get; set; }
    }

    [Serializable()]
    public class ReportRecommendationOption
    {
        public string RecommendationID { get; set; }
        public string RecommendationDescription { get; set; }
        public string RecommendationName { get; set; }
        public int reportRank { get; set; }
        public string ImagePath { get; set; }
        public string RecommendationTitle { get; set; }

        //adding for new report format
        public string ExistingEquipDescription { get; set; }

        public string EquipmentSpace { get; set; }
        public double ReplacementSavingsTherms { get; set; }
        public decimal ReplacementAnnualSavings { get; set; }
        public decimal ReplacementIncentive { get; set; }
        public double MCF { get; set; }
        public int Quantity { get; set; }
        public decimal KwhSaved { get; set; }
        public decimal ThermsSaved { get; set; }
        public string BuildingID { get; set; }

        // SMM - 6/23/2015
        public string ExistingComponentName { get; set; }

        public string TopOptionComponentName { get; set; }

        public List<RecOptReplacementEquipment> RecOptReplacementEquipments = new List<RecOptReplacementEquipment>();
        public List<RecOptExistingEquipment> RecOptExistingEquipments = new List<RecOptExistingEquipment>();
        // public List<RecommendationDetails> RecommendationDetail = new List<RecommendationDetails>();
        //public List<RecommendationOption> RecommendationOptions = new List<RecommendationOption>();

        public string ReplacementAnnualSavingsFormatted { get; set; }
    }

    [Serializable()]
    public class ReportRecommendation
    {
        public string RecommendationID { get; set; }
        public string RecommendationDescription { get; set; }
        public string RecommendationName { get; set; }
        public int reportRank { get; set; }
        public string ImagePath { get; set; }
        public string RecommendationTitle { get; set; }

        //adding for new report format
        public string ExistingEquipDescription { get; set; }

        public string EquipmentSpace { get; set; }
        public double ReplacementSavingsTherms { get; set; }
        public decimal ReplacementAnnualSavings { get; set; }
        public decimal ReplacementIncentive { get; set; }
        public double MCF { get; set; }
        public int Quantity { get; set; }
        public decimal KwhSaved { get; set; }
        public decimal ThermsSaved { get; set; }
        public string BuildingID { get; set; }

        // SMM - 6/23/2015
        public string ExistingComponentName { get; set; }

        public string TopOptionComponentName { get; set; }

        public List<ReplacementEquipment> ReplacementEquipment = new List<ReplacementEquipment>();
        public List<ExistingEquipment> ExistingEquipment = new List<ExistingEquipment>();
        // public List<RecommendationDetails> RecommendationDetail = new List<RecommendationDetails>();
        //public List<RecommendationOption> RecommendationOptions = new List<RecommendationOption>();

        public string ReplacementAnnualSavingsFormatted { get; set; }
        public string ReplacementIncentiveFormatted { get; set; }
        public string ReplacementTotalSavings { get; set; }
        public string EnergySaved { get; set; }
    }

    [Serializable()]
    public class DirectInstalls
    {
        public DirectInstalls()
        {
            ;
        }

        public DirectInstalls(string description, double therms, int quantity, double kWh, string iconPath)
        {
            this.Description = description;
            this.Therms = therms;
            this.Quantity = quantity;
            this.KwH = kWh;
            this.IconPath = iconPath;
        }

        public DirectInstalls(string description, double therms, int quantity, double kWh, string iconPath, string iconFileName)
        {
            this.Description = description;
            this.Therms = therms;
            this.Quantity = quantity;
            this.KwH = kWh;
            this.IconPath = iconPath;
            this.IconFileName = iconFileName;
        }

        public DirectInstalls(Retrofit retrofit, string assessmentDate)
        {
            this.Description = retrofit.Description;
            this.KwH = retrofit.KWh;
            this.Therms = retrofit.Therms;
            this.CostSavings = retrofit.Savings;
            this.Quantity = Convert.ToInt32(retrofit.Quantity);
            this.AssessmentDate = assessmentDate;
            this.IconPath = retrofit.IconPath;
            this.IconFileName = retrofit.IconFileName;
        }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public double KwH { get; set; }
        public double Therms { get; set; }
        public decimal CostSavings { get; set; }
        public string AssessmentDate { get; set; }
        public string IconPath { get; set; }
        public string IconFileName { get; set; }
    }

    [Serializable()]
    public class UnitDirectInstalls
    {
        public UnitDirectInstalls()
        {
            ;
        }

        public UnitDirectInstalls(string description, double therms, int quantity, double kWh, string iconPath)
        {
            this.Description = description;
            this.Therms = therms;
            this.Quantity = quantity;
            this.KwH = kWh;
            this.IconPath = iconPath;
        }

        public UnitDirectInstalls(string description, double therms, int quantity, double kWh, string iconPath, string iconFileName)
        {
            this.Description = description;
            this.Therms = therms;
            this.Quantity = quantity;
            this.KwH = kWh;
            this.IconPath = iconPath;
            this.IconFileName = iconFileName;
        }

        public UnitDirectInstalls(Retrofit retrofit, string assessmentDate)
        {
            this.Description = retrofit.Description;
            this.KwH = retrofit.KWh;
            this.Therms = retrofit.Therms;
            this.CostSavings = retrofit.Savings;
            this.Quantity = Convert.ToInt32(retrofit.Quantity);
            this.AssessmentDate = assessmentDate;
            this.IconPath = retrofit.IconPath;
            this.IconFileName = retrofit.IconFileName;
        }

        public UnitDirectInstalls(RetrofitEstimate retrofit, string assessmentDate)
        {
            this.Description = retrofit.Description;
            this.KwH = retrofit.KWh;
            this.Therms = retrofit.Therms;
            this.CostSavings = retrofit.Savings;
            this.Quantity = Convert.ToInt32(retrofit.Quantity);
            this.AssessmentDate = assessmentDate;
            this.IconPath = retrofit.IconPath;
            this.IconFileName = retrofit.IconFileName;
        }

        public string Description { get; set; }
        public int Quantity { get; set; }
        public double KwH { get; set; }
        public double Therms { get; set; }
        public decimal CostSavings { get; set; }
        public string AssessmentDate { get; set; }
        public string IconPath { get; set; }
        public string IconFileName { get; set; }
    }

    [Serializable()]
    public class ExecutiveSummary
    {
        public string SummaryDescription { get; set; }
        public int RecommendationCount { get; set; }
        public float AnnualSavings { get; set; }
        public float PotentialSavings { get; set; }
    }

    [Serializable()]
    public class NextSteps
    {
        public string sDescription { get; set; }
        public string ImageFilePath { get; set; }
    }

    [Serializable()]
    public class ElectricHist
    {
        public ElectricHistory add { get; set; }
    }

    [Serializable()]
    public class ElectricHistory
    {
        public ElectricHistory()
        {
            ;
        }

        public ElectricHistory(ElectricHistoryRecord electricHistoryRecord, ref int degreeDays)
        {
            if (electricHistoryRecord != null)
            {
                this.CoolDegreeDays = electricHistoryRecord.CoolDays;
                this.HeatDegreeDays = electricHistoryRecord.HeatDays;

                this.HistoryMonth = electricHistoryRecord.ReadDate.ToString();
                this.KWh = Convert.ToDecimal(electricHistoryRecord.TotalKwh);
                this.TotalBill = (decimal)electricHistoryRecord.TotalBill;

                degreeDays += this.DegreeDays;
            }
        }

        public int HeatDegreeDays { get; set; }
        public int CoolDegreeDays { get; set; }
        public decimal KWh { get; set; }
        public string HistoryMonth { get; set; }
        public decimal TotalBill { get; set; }

        public int DegreeDays
        {
            get
            {
                return (this.CoolDegreeDays + this.HeatDegreeDays);
            }
        }

        public bool Hide { get; set; }
    }

    [Serializable()]
    public class GasHistory
    {
        public GasHistory()
        {
            ;
        }

        public GasHistory(GasHistoryRecord gasHistoryRecord, ref int degreeDays)
        {
            if (gasHistoryRecord != null)
            {
                this.HistoryMonth = gasHistoryRecord.ReadDate.ToString();
                this.Therms = (int)gasHistoryRecord.Therms;
                this.TotalBill = (decimal)gasHistoryRecord.TotalBill;

                degreeDays += this.DegreeDays;
            }
        }

        public int HeatDegreeDays { get; set; }
        public decimal TotalBill { get; set; }
        public decimal Therms { get; set; }
        public string HistoryMonth { get; set; }

        public int DegreeDays
        {
            get
            {
                return this.HeatDegreeDays;
            }
        }

        public bool Hide { get; set; }
    }

    [Serializable()]
    public class EnergyCostBreakdown
    {
        public string BreakdownDescription { get; set; }
        public decimal TotalElectric { get; set; }
        public decimal TotalGas { get; set; }
    }

    //public class RecommendationDetails
    //{
    //    public string RecommendationID { get; set; }
    //    public string ExistingEquipDescription { get; set; }
    //    public decimal Space { get; set; }
    //    public decimal ReplacementSavingsTherms { get; set; }
    //    public decimal ReplacementAnnualSavings { get; set; }
    //    public decimal ReplacementIncentive { get; set; }
    //    public decimal MCF { get; set; }
    //    public int Quantity { get; set; }

    //    //  public string ImagePath { get; set; }
    //}

    //[Serializable()]
    //public class EnergyCostBreakdown
    //{
    //    public string ECBSummary { get; set; }
    //    public decimal NaturalGasPct { get; set; }
    //    public decimal ElectricDemandPct {get;set;}
    //    public decimal ElectricOffPeakPct { get; set; }
    //    public decimal ElectricOnPeakPct { get; set; }
    //    public decimal TaxesFees { get; set; }

    //}

    //[Serializable()]
    //public class Overview
    //{
    //    public string OverviewSummary { get; set; }
    //    public string CompanyName { get; set; }
    //    public string BuildingName { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Address3 { get; set; }
    //    public string city { get; set; }
    //    public string state {get;set;}
    //    public string zip {get;set;}
    //    public string SiteContacts { get; set; }
    //}
}