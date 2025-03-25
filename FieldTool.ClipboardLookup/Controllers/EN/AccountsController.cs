using FieldTool.ClipboardLookup.DAL;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Models.EN;
using FieldTool.Constants.Logging;
using FieldTool.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers.EN
{
    [RoutePrefix("api/en/accounts")]
    public class AccountsController : BaseController<Company, AccountInfo>
    {
        public IEmailHelper EmailHelper { get; set; }

        public AccountsController(IRepository<Company, AccountInfo> repository, ILogger logger, IEmailHelper emailHelper)
            : base(repository, logger)
        {
            EmailHelper = emailHelper;
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GetCompany(string id)
        {
            return await GetById(id);
        }

        //[Route("email/{emailAddress}")]
        //public async Task<IEnumerable<AccountInfo>> GetCompaniesByContactEmail(string emailAddress) {
        //    return await GetAll(x => x.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase) || x.Contacts.Any(y => y.EmailAddress.Equals(emailAddress, StringComparison.InvariantCultureIgnoreCase)), x => x.OrderBy(y => y.CompanyName));
        //}
    }
}