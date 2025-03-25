using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.Models
{
    public class RetrofitDTO
    {
        public string AuditProjectBsid { get; set; }
        public decimal? RebatePerComponent { get; set; }
        public int Quantity { get; set; }
        public string ComponentId { get; set; }

        public RetrofitDTO()
        {
        }

        public RetrofitDTO(Retrofit retrofit)
        {
            if (retrofit != null)
            {
                AuditProjectBsid = retrofit.AuditProjectBsid;
                RebatePerComponent = retrofit.Savings;
                Quantity = retrofit.Quantity;
                ComponentId = retrofit.ComponentBsid;
            }
        }
    }
}