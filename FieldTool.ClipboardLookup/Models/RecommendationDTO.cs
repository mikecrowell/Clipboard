using FieldTool.Entity;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models
{
    public class RecommendationDTO
    {
        public string RecommendationGuid { get; set; }
        public string BuildingGuid { get; set; }
        public string AuditProjectBsid { get; set; }
        public string RecommendationName { get; set; }
        public string RecommendationDescription { get; set; }
        public bool? IncludedInReport { get; set; }
        public int? ReportRank { get; set; }
        public bool? IsOccupancySensor { get; set; }
        public RecommendationOptionDTO RecommendationOption { get; set; }

        public RecommendationDTO()
        {
        }

        public RecommendationDTO(Recommendation recommendation)
        {
            RecommendationGuid = recommendation.RecommendationGuid;
            BuildingGuid = recommendation.BuildingGuid;
            AuditProjectBsid = recommendation.AuditProjectBsid;
            RecommendationName = recommendation.RecommendationName;
            RecommendationDescription = recommendation.RecommendationDescription;
            IncludedInReport = recommendation.IncludedInReport;
            ReportRank = recommendation.ReportRank;
            IsOccupancySensor = recommendation.IsOccupancySensor;

            // include only the top recommendation option, based on highest savings
            if (recommendation.RecommendationOptions.Count > 0)
            {
                RecommendationOption = new RecommendationOptionDTO(recommendation.RecommendationOptions.OrderByDescending(x => x.Savings).First());
            }
        }
    }
}