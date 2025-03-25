using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.Helpers
{
    public interface IEmailHelper
    {
        EmailMessage BuildEfficiencyNavigatorRegisterEmail(string emailTo, string companyId);

        Task<bool> SendEmail(EmailMessage message);
    }
}