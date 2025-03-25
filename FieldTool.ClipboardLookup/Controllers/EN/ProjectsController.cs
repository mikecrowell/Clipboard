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
    [RoutePrefix("api/en/projects")]
    public class ProjectsController : BaseController<Building, ProjectInfo>
    {
        public IEmailHelper EmailHelper { get; set; }

        public ProjectsController(IRepository<Building, ProjectInfo> repository, ILogger logger, IEmailHelper emailHelper)
            : base(repository, logger)
        {
            EmailHelper = emailHelper;
        }

        //[Route("{id}")]
        //public async Task<IHttpActionResult> GetProject(string id) {
        //    return await GetById(id);
        //}

        //[Route("account/{accountId}")]
        //public async Task<IEnumerable<ProjectInfo>> GetProjectsByAccount(string accountId) {
        //    return await GetAll(x => x.AuditProject.CompanyBsid == accountId);
        //}

        [Route("property/{propertyId}")]
        public async Task<IEnumerable<ProjectInfo>> GetProjectsByProperty(string propertyId)
        {
            return await GetAll(x => x.BuildingBsid == propertyId);
        }
    }
}