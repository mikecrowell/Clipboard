using FieldTool.ClipboardLookup.DAL;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Models.EN;
using FieldTool.Constants.Logging;
using FieldTool.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers.EN
{
    [RoutePrefix("api/en/properties")]
    public class PropertiesController : BaseController<Building, PropertyInfo>
    {
        public IEmailHelper EmailHelper { get; set; }

        public PropertiesController(IRepository<Building, PropertyInfo> repository, ILogger logger, IEmailHelper emailHelper)
            : base(repository, logger)
        {
            EmailHelper = emailHelper;
        }

        //[Route("{id}")]
        //public async Task<IHttpActionResult> GetProperty(string id) {
        //    return await GetById(id);
        //}

        [Route("account/{accountId}")]
        public async Task<IEnumerable<PropertyInfo>> GetPropertiesByAccount(string accountId)
        {
            return await GetAll(x => x.AuditProject.CompanyBsid == accountId);
        }

        //[Route("project/{projectId}")]
        //public async Task<IEnumerable<PropertyInfo>> GetPropertiesByProject(string projectId) {
        //    return await GetAll(x => x.AuditProjectBsid == projectId);
        //}
    }
}