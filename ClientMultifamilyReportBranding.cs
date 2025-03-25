namespace FieldTool.UI
{
    public class ClientMultifamilyReportBranding : ClientReportBranding
    {
        public string LogoFileName { get; set; }
        public bool UseCustomDIIcons { get; set; }
        public int ColorValueRed { get; set; }
        public int ColorValueGreen { get; set; }
        public int ColorValueBlue { get; set; }
        public bool HasValidRGB { get; set; }
        public string CoverTitle { get; set; }
        public string IntroBodyPar1 { get; set; }
        public string IntroBodyPar2 { get; set; }
        public string IntroBodyPar3 { get; set; }
        public string IntroContactText { get; set; }
        public string IntroSignatureText { get; set; }
        public string NextStepsContactText { get; set; }
        public string NextStepsStep1Header { get; set; }
        public string NextStepsStep2Header { get; set; }
        public string NextStepsStep3Header { get; set; }
        public string NextStepsStep4Header { get; set; }
        public string NextStepsStep5Header { get; set; }
        public string NextStepsStep1Detail { get; set; }
        public string NextStepsStep2Detail { get; set; }
        public string NextStepsStep3Detail { get; set; }
        public string NextStepsStep4Detail { get; set; }
        public string NextStepsStep5Detail { get; set; }
        public string SignOffStepTwo { get; set; }
        public string RecommendationOptionsTitle { get; set; }
        public bool ShowSubRecSavings { get; set; }
        public bool ShowOnlyTopRecOption { get; set; }
        public string SubDIInstallsDescriptionText { get; set; }
        public string DirectInstallTitle { get; set; }
        public string RecommendationTitle { get; set; }
        public bool IncludeRecommendationsSection { get; set; }
        public bool IncludeRecommendationOptionsSection { get; set; }
        public bool IncludeNextStepsSection { get; set; }
        public bool IncludeBuildingDISummarySection { get; set; }
        public bool IncludeUnitDIEstimatorSection { get; set; }
        public bool IncludeEnergyUsageChartSection { get; set; }
        public bool IncludeElectricHistorySection { get; set; }
        public bool IncludeGasHistorySection { get; set; }
        public bool IncludeSignaturePage { get; set; }
        public bool IncludeIntroPage { get; set; }
        public bool AutoSelectRecommendationsSection { get; set; }
        public bool AutoSelectRecommendationOptionsSection { get; set; }
        public bool AutoSelectNextStepsSection { get; set; }
        public bool AutoSelectBuildingDISummarySection { get; set; }
        public bool AutoSelectUnitDIEstimatorSection { get; set; }
        public bool AutoSelectEnergyUsageChartSection { get; set; }
        public bool AutoSelectElectricHistorySection { get; set; }
        public bool AutoSelectGasHistorySection { get; set; }
        public bool AutoSelectSignaturePage { get; set; }
        public bool AutoSelectIntroPage { get; set; }
        public string TableHeaderBackColor { get; set; }
        public string TableHeaderForeColor { get; set; }
        public string RetrofitTotalBackColor { get; set; }
        public bool ShowFacebookIcon { get; set; }
        public bool ShowTwitterIcon { get; set; }
        public string UnitDIEstimatorTitle { get; set; }
        public string UnitDIEstimatorDescription { get; set; }
        public string RecommendationDescription { get; set; }
        public bool HideKwh { get; set; }
        public bool HideMCF { get; set; }
    }
}