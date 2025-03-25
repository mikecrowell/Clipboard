using System;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class CommercialReportCustomFields
    {
        [XmlElement(IsNullable = true)]
        public string IsConsumersEnergy { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportTitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportTitleFontType { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportTitleFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportTitleFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string IsReportTitleAllCaps { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportPreparedByText { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportPreparedByFontType { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportPreparedByFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string ReportPreparedByFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string IsReportPreparedByAllCaps { get; set; }

        [XmlElement(IsNullable = true)]
        public string PrimaryFontType { get; set; }

        [XmlElement(IsNullable = true)]
        public string PrimaryFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string PrimaryFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroPageFontType { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroPageFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string IntroPageFontColor { get; set; }

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
        public string SectionTitleFontStyle { get; set; }

        [XmlElement(IsNullable = true)]
        public string SectionTitleFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string SectionTitleFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string IsSectionTitleAllCaps { get; set; }

        [XmlElement(IsNullable = true)]
        public string IsSectionTitleUnderline { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section1DITitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section1DIIntroParagraphText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section2RecommendationTitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section2RecommendationIntroParagraphText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section3RecOptionsTitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section4HistoryTitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string Section5SignoffTitleText { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderBackgroundColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableHeaderFontStyle { get; set; }

        [XmlElement(IsNullable = true)]
        public string IsTableHeaderAllCaps { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableCellFontColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableCellFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableCellFontStyle { get; set; }

        [XmlElement(IsNullable = true)]
        public string TableBorderColor { get; set; }

        [XmlElement(IsNullable = true)]
        public string UseCustomDIIcons { get; set; }

        [XmlElement(IsNullable = true)]
        public string HideDIIcons { get; set; }

        [XmlElement(IsNullable = true)]
        public string HideKwh { get; set; }

        [XmlElement(IsNullable = true)]
        public string HideMCF { get; set; }

        [XmlElement(IsNullable = true)]
        public string SignOffDisclaimerText { get; set; }

        [XmlElement(IsNullable = true)]
        public string SignOffDisclaimerFontSize { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeRecommendationsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeDISummarySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeRecommendationOptionsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeElectricHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeGasHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeSignaturePage { get; set; }

        [XmlElement(IsNullable = true)]
        public string IncludeIntroPage { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectTopRecommendations { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectAllRecommendations { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectDISummarySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectRecommendationOptionsSection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectElectricHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectGasHistorySection { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectSignaturePage { get; set; }

        [XmlElement(IsNullable = true)]
        public string AutoSelectIntroPage { get; set; }
    }
}