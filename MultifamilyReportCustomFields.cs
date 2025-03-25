using System;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class MultifamilyReportCustomFields
    {
        [XmlElement(IsNullable = true)]
        public string IsFirstEnergyOH { get; set; }

        [XmlElement(IsNullable = true)]
        public string UseCustomDIIcons { get; set; }

        [XmlElement(IsNullable = true)]
        public string CoverTitle { get; set; }

        [XmlElement(IsNullable = true)]
        public string SlogunText { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroBodyPar1 { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroBodyPar2 { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroBodyPar3 { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroContactText { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroSignatureText { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep1Header { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep1Detail { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep2Header { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep2Detail { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep3Header { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep3Detail { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep4Header { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep4Detail { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep5Header { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsStep5Detail { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsContactText { get; set; }

        [XmlElement(IsNullable = true)]
        public string NextStepsPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string SignOff { get; set; }

        [XmlElement(IsNullable = true)]
        public string SignOffStepTwo { get; set; }

        [XmlElement(IsNullable = true)]
        public string SignOffPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string GasElectricHistoryPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string DIPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string UnitDIPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string RecommendationPageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string EnergyUseagePageHeader { get; set; }

        [XmlElement(IsNullable = true)]
        public string RecommendationOptionsTitle { get; set; }

        [XmlElement(IsNullable = true)]
        public string UnitDIEstimatorTitle { get; set; }

        [XmlElement(IsNullable = true)]
        public string UnitDIEstimatorDescription { get; set; }

        [XmlElement(IsNullable = true)]
        public string RecommendationDescription { get; set; }

        [XmlElement(IsNullable = false)]
        public bool ShowSubRecSavings { get; set; }

        [XmlElement(IsNullable = false)]
        public bool ShowOnlyTopRecOption { get; set; }

        [XmlElement(IsNullable = true)]
        public string SubDIInstallsDescriptionText { get; set; }

        [XmlElement(IsNullable = true)]
        public string DirectInstallTitle { get; set; }

        [XmlElement(IsNullable = true)]
        public string RecommendationTitle { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeRecommendationsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeRecommendationOptionsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeNextStepsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeBuildingDISummarySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeUnitDIEstimatorSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeEnergyUsageChartSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeElectricHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeGasHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeSignaturePage { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeIntroPage { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectRecommendationsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectRecommendationOptionsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectNextStepsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectBuildingDISummarySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectUnitDIEstimatorSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectEnergyUsageChartSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectElectricHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectGasHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectSignaturePage { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectIntroPage { get; set; }

        [XmlElement(IsNullable = true)]
        public string ShowFacebookIcon { get; set; }

        [XmlElement(IsNullable = true)]
        public string ShowTwitterIcon { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderBackColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderForeColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string RetrofitTotalBackColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string HideKwh { get; set; }

        [XmlElement(IsNullable = true)]
        public string HideMCF { get; set; }
    }
}