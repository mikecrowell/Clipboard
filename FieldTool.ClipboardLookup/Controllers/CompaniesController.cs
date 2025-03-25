using FieldTool.ClipboardLookup.DAL;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Models;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers
{
    [RoutePrefix("api/accounts")]
    public class CompaniesController : BaseController<Company, CompanyDTO>
    {
        public IEmailHelper EmailHelper { get; set; }

        public CompaniesController(IRepository<Company, CompanyDTO> repository, ILogger logger, IEmailHelper emailHelper)
            : base(repository, logger)
        {
            EmailHelper = emailHelper;
        }

        [Route("{id}")]
        public async Task<IHttpActionResult> GetCompany(string id)
        {
            return await GetById(id);
        }

        [Route("email/{emailAddress}")]
        public async Task<IEnumerable<CompanyInfoDTO>> GetCompaniesByContactEmail(string emailAddress)
        {
            return await GetAllOfType<CompanyInfoDTO>(x => new CompanyInfoDTO(x), x => x.Contacts.Any(y => y.EmailAddress == emailAddress), x => x.OrderBy(y => y.CompanyName));
        }

        [Route("{CompanyBsid}/sendRegistrationEmail/{EmailAddress}/{RequestedByUserName}")]
        [HttpGet]
        public async Task<IHttpActionResult> SendRegistrationEmail([FromUri] EmailRegistrationParams parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.RequestedByUserName))
            {
                return BadRequest("Missing URL parameter 'RequestedByUserName'");
            }

            EmailMessage emailMessage = EmailHelper.BuildEfficiencyNavigatorRegisterEmail(parameters.EmailAddress, parameters.CompanyBsid);
            bool emailSent = await EmailHelper.SendEmail(emailMessage);

            // Log email request
            await ((CompanyRepository)Repository).SaveEmailRequestLog(parameters.CompanyBsid, emailSent, parameters.EmailAddress, parameters.RequestedByUserName);

            return StatusCode(System.Net.HttpStatusCode.Accepted);
        }

        [Route("save")]
        [HttpGet]
        public async Task<IHttpActionResult> SaveAllCompanies()
        {
            int companyUpdateCount = 0;
            using (var context = Repository.Context)
            {
                EntityCRUDForAudit entityCrud = new EntityCRUDForAudit(context);
                IEnumerable<CompanyDTO> companies = await Repository.GetAll();

                try
                {
                    foreach (CompanyDTO company in companies)
                    {
                        entityCrud.UpdateCompany(company.CompanyBsid);
                        companyUpdateCount++;

                        //#if DEBUG
                        //                    if (companyUpdateCount >= 10) {
                        //                        break;
                        //                    }
                        //#endif
                    }
                }
                catch (System.Exception ex)
                {
                    var cause = ExceptionHelper.Innermost(ex);
                    object validations = null;
                    string message = null;
                    string errorObject = null;

                    if (ex.GetType() == typeof(DbEntityValidationException))
                    {
                        validations = ((DbEntityValidationException)ex).EntityValidationErrors
                            .Select(x => new
                            {
                                Class = x.Entry.Entity.GetType().Name,
                                Errors = x.ValidationErrors.Select(y => String.Format("{0} : {1}", y.PropertyName, y.ErrorMessage)).ToList()
                            }).ToList();
                        message = JsonConvert.SerializeObject(validations);
                        errorObject = JsonConvert.SerializeObject(validations);
                    }
                    else
                    {
                        message = cause.Message;
                        errorObject = JsonConvert.SerializeObject(cause);
                    }
                    throw cause;
                }
            }

            return Ok(string.Format("{0} company record(s) updated.", companyUpdateCount));
        }

        [Route("download/{auditId}")]
        [HttpGet]
        public IHttpActionResult DownloadDataFile(string auditId)
        {
            try
            {
                return Ok(((CompanyRepository)Repository).GetCompanyXml(auditId));
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception(e.Message));
            }
        }
    }
}