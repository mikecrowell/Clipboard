using FieldTool.Entity;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models.EN
{
    public class MeasureInfo
    {
        // Required
        public string RecommendationId { get; set; }

        public string BensightRetrofitId { get; set; }

        public string BensightComponentId { get; set; }

        public int Quantity { get; set; }

        public MeasureInfo()
        {
        }

        public MeasureInfo(Recommendation recommendation)
        {
            RecommendationId = recommendation.InstanceId;
            RecommendationOption recOption = recommendation.RecommendationOptions.OrderByDescending(x => x.Savings).FirstOrDefault();
            if (recOption != null)
            {
                BensightRetrofitId = recOption.RecommendationOptionGuid;
                BensightComponentId = recOption.RebateBsid;
            }
            Quantity = (recOption.Quantity != null && recOption.Quantity.HasValue) ? recOption.Quantity.Value : 1;
        }
    }
}