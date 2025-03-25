using FieldTool.ClipboardLookup.DAL;
using FieldTool.ClipboardLookup.Models;
using FieldTool.Constants.Logging;
using FieldTool.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers
{
    [RoutePrefix("api/measures")]
    public class RetrofitsController : BaseController<Retrofit, RetrofitDTO>
    {
        public RetrofitsController(IRepository<Retrofit, RetrofitDTO> repository, ILogger logger)
            : base(repository, logger)
        {
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> GetRetrofit(string id)
        {
            return await GetById(id);
        }

        [Route("auditProject/{auditProjectId}")]
        [HttpPost]
        public async Task<IEnumerable<RetrofitDTO>> GetRetrofitsByAuditProject(string auditProjectId)
        {
            return await GetAll(x => x.AuditProjectBsid == auditProjectId);
        }
    }
}