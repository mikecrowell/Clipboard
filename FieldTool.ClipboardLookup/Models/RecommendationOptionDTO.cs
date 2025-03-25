using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.Models
{
    public class RecommendationOptionDTO
    {
        public string RecommendationOptionGuid { get; set; }
        public string RecommendationGuid { get; set; }
        public string RecommendationName { get; set; }
        public string OptionDescription { get; set; }
        public string OptionId { get; set; }
        public string OptionName { get; set; }
        public double? Cop { get; set; }
        public string ElectricDisplayAs { get; set; }
        public double? EnergyFactor { get; set; }
        public string EnergySource { get; set; }
        public string GasDisplayAs { get; set; }
        public double? HeatingCoolingHours { get; set; }
        public bool? IsOccupancySensor { get; set; }
        public bool? IsZeroSavings { get; set; }
        public double? KwhSaved { get; set; }
        public double? KwhSavedWithRateCode { get; set; }
        public string OccupancySensorDisplayAs { get; set; }
        public string OriginalEquipmentMfid { get; set; }
        public int? Quantity { get; set; }
        public string RebateBsid { get; set; }
        public string RebateCalculationEquation { get; set; }
        public string RebateClientRefId { get; set; }
        public double? RebateValue { get; set; }
        public double? Savings { get; set; }
        public string SavingsCalculationEquationSaving { get; set; }
        public double? ThermsSaved { get; set; }
        public double? ThermsSavedWithRateCode { get; set; }
        public string TypeOfEnergy { get; set; }

        public RecommendationOptionDTO()
        {
        }

        public RecommendationOptionDTO(RecommendationOption recommendationOption)
        {
            RecommendationOptionGuid = recommendationOption.RecommendationOptionGuid;
            RecommendationGuid = recommendationOption.RecommendationGuid;
            RecommendationName = recommendationOption.RecommendationName;
            OptionDescription = recommendationOption.OptionDescription;
            OptionId = recommendationOption.OptionId;
            OptionName = recommendationOption.OptionName;
            Cop = recommendationOption.Cop;
            ElectricDisplayAs = recommendationOption.ElectricDisplayAs;
            EnergyFactor = recommendationOption.EnergyFactor;
            EnergySource = recommendationOption.EnergySource;
            GasDisplayAs = recommendationOption.GasDisplayAs;
            HeatingCoolingHours = recommendationOption.HeatingCoolingHours;
            IsOccupancySensor = recommendationOption.IsOccupancySensor;
            IsZeroSavings = recommendationOption.IsZeroSavings;
            KwhSaved = recommendationOption.KwhSaved;
            KwhSavedWithRateCode = recommendationOption.KwhSavedWithRateCode;
            OccupancySensorDisplayAs = recommendationOption.OccupancySensorDisplayAs;
            OriginalEquipmentMfid = recommendationOption.OriginalEquipmentMfid;
            Quantity = recommendationOption.Quantity;
            RebateBsid = recommendationOption.RebateBsid;
            RebateCalculationEquation = recommendationOption.RebateCalculationEquation;
            RebateClientRefId = recommendationOption.RebateClientRefId;
            RebateValue = recommendationOption.RebateValue;
            Savings = recommendationOption.Savings;
            SavingsCalculationEquationSaving = recommendationOption.SavingsCalculationEquationSaving;
            ThermsSaved = recommendationOption.ThermsSaved;
            ThermsSavedWithRateCode = recommendationOption.ThermsSavedWithRateCode;
            TypeOfEnergy = recommendationOption.TypeOfEnergy;
        }
    }
}