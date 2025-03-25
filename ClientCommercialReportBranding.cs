using System.Drawing;

namespace FieldTool.UI
{
    public class ClientCommercialReportBranding : ClientReportBranding
    {
        public string HeaderImageFileName { get; set; }
        public string CoverImageFileName { get; set; }
        public string ReportTitleText { get; set; }
        public string ReportTitleFontType { get; set; }
        public string ReportTitleFontSize { get; set; }
        public Color ReportTitleFontColor { get; set; }
        public bool IsReportTitleAllCaps { get; set; }
        public string ReportPreparedByText { get; set; }
        public string ReportPreparedByFontType { get; set; }
        public string ReportPreparedByFontSize { get; set; }
        public Color ReportPreparedByFontColor { get; set; }
        public bool IsReportPreparedByAllCaps { get; set; }
        public string PrimaryFontType { get; set; }
        public string PrimaryFontSize { get; set; }
        public Color PrimaryFontColor { get; set; }
        public string IntroPageFontType { get; set; }
        public string IntroPageFontSize { get; set; }
        public Color IntroPageFontColor { get; set; }
        public string IntroBodyPar1 { get; set; }
        public string IntroBodyPar2 { get; set; }
        public string IntroBodyPar3 { get; set; }
        public string IntroContactText { get; set; }
        public string IntroSignatureText { get; set; }
        public string SectionTitleFontStyle { get; set; }
        public string SectionTitleFontSize { get; set; }
        public Color SectionTitleFontColor { get; set; }
        public bool IsSectionTitleAllCaps { get; set; }
        public bool IsSectionTitleUnderline { get; set; }
        public string Section1DITitleText { get; set; }
        public string Section1DIIntroParagraphText { get; set; }
        public string Section2RecommendationTitleText { get; set; }
        public string Section2RecommendationIntroParagraphText { get; set; }
        public string Section3RecOptionsTitleText { get; set; }
        public string Section4HistoryTitleText { get; set; }
        public string Section5SignoffTitleText { get; set; }
        public string NextStepsImageFileName { get; set; }
        public Color TableHeaderBackgroundColor { get; set; }
        public Color TableHeaderFontColor { get; set; }
        public string TableHeaderFontSize { get; set; }
        public string TableHeaderFontStyle { get; set; }
        public bool IsTableHeaderAllCaps { get; set; }
        public Color TableCellFontColor { get; set; }
        public string TableCellFontSize { get; set; }
        public string TableCellFontStyle { get; set; }
        public Color TableBorderColor { get; set; }
        public bool UseCustomDIIcons { get; set; }
        public bool HideDIIcons { get; set; }
        public bool HideKwh { get; set; }
        public bool HideMCF { get; set; }
        public string TableCustomIconsFilePath { get; set; }
        public string SignOffDisclaimerFontSize { get; set; }
        public bool IncludeRecommendationsSection { get; set; }
        public bool IncludeDISummarySection { get; set; }
        public bool IncludeRecommendationOptionsSection { get; set; }
        public bool IncludeElectricHistorySection { get; set; }
        public bool IncludeGasHistorySection { get; set; }
        public bool IncludeSignaturePage { get; set; }
        public bool IncludeIntroPage { get; set; }
        public bool AutoSelectTopRecommendations { get; set; }
        public bool AutoSelectAllRecommendations { get; set; }
        public bool AutoSelectDISummarySection { get; set; }
        public bool AutoSelectRecommendationOptionsSection { get; set; }
        public bool AutoSelectElectricHistorySection { get; set; }
        public bool AutoSelectGasHistorySection { get; set; }
        public bool AutoSelectSignaturePage { get; set; }
        public bool AutoSelectIntroPage { get; set; }
    }
}