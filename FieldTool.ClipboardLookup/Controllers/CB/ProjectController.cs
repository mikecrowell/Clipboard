using FieldTool.Bsi.Helpers;
using FieldTool.ClipboardLookup.DAL.CB;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Models.CB;
using FieldTool.DAL.DTO;
using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers.CB
{
    [RoutePrefix("api/cb/project")]
    public class ProjectController : BaseCbController<AuditUploadBackup>
    {
        public ProjectController(ICbRepository<AuditUploadBackup> repository, ILogger logger, IAzureStorageService azureService)
            : base(repository, logger, azureService)
        {
        }

        // Get Last for auditId
        // * only works for an audit that was successfully saved.
        [Route("backup/on/auditId/{auditId}")]
        [HttpGet]
        public async Task<IHttpActionResult> ListBackupsByAuditId(string auditId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.Company != null && x.Company.AuditProjects.Count > 0 && x.Company.AuditProjects.Any(y => y.AuditProjectBsid == auditId)),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = auditId,
                    type = LookupServiceConstants.UploadSourceType.PROJECT,
                    display = y.CompanyBsid,
                    url = y.AuditDataUrl,
                    data = y.AuditDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedAudit
                })));
            return Ok(CreateReturnsFrom(backups.ToList<BackupSelect>()));
        }

        // Get Last for auditId
        // * only works for an audit that was successfully saved.
        [Route("backup/for/accountId/{externalId}")]
        [HttpGet]
        public async Task<IHttpActionResult> ListBackupsByExternalId(string externalId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.ExternalId == externalId && !String.IsNullOrEmpty(externalId)),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = externalId,
                    type = LookupServiceConstants.UploadSourceType.PROJECT,
                    display = y.CompanyBsid,
                    url = y.AuditDataUrl,
                    data = y.AuditDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedAudit
                })));

            return Ok(CreateReturnsFrom(backups.ToList<BackupSelect>()));
        }

        // Get Specific for auditId
        [Route("backup/{uploadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSpecificAuditProjectBackup(long uploadId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.Id == uploadId),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = uploadId.ToString(),
                    type = LookupServiceConstants.UploadSourceType.PROJECT,
                    display = y.CompanyBsid,
                    url = y.AuditDataUrl,
                    data = y.AuditDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedAudit
                })));

            List<BackupSelect> l = backups.ToList<BackupSelect>();
            BackupSelect f = l.FirstOrDefault();
            return Ok(ReturnData(f));
        }

        [HttpPost]
        [Route("{programId}/for/{accountId}/on/{projectId}/by/{uploadedBy}/save")]
        public async Task<IHttpActionResult> UploadProject(string programId, string accountId, string projectId, string uploadedBy, DIandCompanyDTO.CompanyCollection data)
        {
            return await doPost(programId, accountId, projectId, uploadedBy, data, false);
        }

        // Upload and then mark as deleted
        [HttpPost]
        [Route("{programId}/for/{accountId}/on/{projectId}/by/{deletedBy}/delete")]
        public async Task<IHttpActionResult> DeleteProject(string programId, string accountId, string projectId, string deletedBy, DIandCompanyDTO.CompanyCollection data)
        {
            return await doPost(programId, accountId, projectId, deletedBy, data, true);
        }

        internal async Task<IHttpActionResult> doPost(string programId, string accountId, string projectId, string user, DIandCompanyDTO.CompanyCollection data, bool deleted)
        {
            var uploadReturn = new UploadReturn();
            // check all inputs
            uploadReturn.Trace.Add("Check params");
            if (String.IsNullOrEmpty(programId))
            {
                uploadReturn.Errors.Add("Missing PROGRAM_ID");
            }
            if (String.IsNullOrEmpty(accountId))
            {
                uploadReturn.Errors.Add("Missing ACCOUNT_ID");
            }
            if (String.IsNullOrEmpty(projectId))
            {
                uploadReturn.Errors.Add("Missing PROJECT_ID");
            }
            if (String.IsNullOrEmpty(user))
            {
                uploadReturn.Errors.Add("Missing USER_WHO_REQUESTED");
            }
            if (data == null)
            {
                uploadReturn.Errors.Add("Missing data");
            }

            // use the serialized data to send to Azure as a backup
            if (uploadReturn.IsValid())
            {
                uploadReturn.Trace.Add("Backup data");
                try
                {
                    var o = new DataParser().SerializeToStream<DIandCompanyDTO.CompanyCollection>(data);
                    uploadReturn.BackupUri = uploadXmlToAzure(LookupServiceConstants.UploadSourceType.PROJECT, programId, accountId, projectId, new DataParser().SerializeToStream<DIandCompanyDTO.CompanyCollection>(data));
                    uploadReturn.IsBackedUp = true;
                }
                catch (Exception e)
                {
                    var inner = ExceptionHelper.Innermost(e);
                    uploadReturn.Errors.Add("Error uploading to Azure: " + inner.Message);
                }
            }
            // try saving
            if (uploadReturn.IsValid())
            {
                uploadReturn.Trace.Add("Save data to DB");
                try
                {
                    var crud = new EntityCRUDForAudit(Repository.Context);
                    var audits = new List<CompanyDTO>();
                    audits.AddRange(data.ItemsCompany);
                    crud.Save(audits, accountId, uploadReturn.BackupUri, user, deleted);
                    uploadReturn.IsSaved = !deleted;
                    uploadReturn.IsDeleted = deleted;
                }
                catch (Exception e)
                {
                    var inner = ExceptionHelper.Innermost(e);
                    uploadReturn.Errors.Add("Error performing Db SAVE: " + inner.Message);
                }
            }
            return OkOrDie(uploadReturn);
        }

        // Upload report
        [HttpPost]
        [Route("{programId}/for/{accountId}/on/{projectId}/by/{uploadedBy}/report")]
        public async Task<IHttpActionResult> SaveReport(string programId, string accountId, string projectId, string uploadedBy)
        {
            return await doUploadReport(LookupServiceConstants.UploadSourceType.PROJECT, programId, accountId, projectId, uploadedBy, LookupServiceConstants.UploadFileType.REPORT);
        }

        // Upload files
        [HttpPost]
        [Route("{programId}/for/{accountId}/on/{projectId}/by/{uploadedBy}/of/{uploadFileType}/upload")]
        public async Task<IHttpActionResult> SaveFile(string programId, string accountId, string projectId, string uploadedBy, string uploadFileType)
        {
            return await doUploadReport(LookupServiceConstants.UploadSourceType.PROJECT, programId, accountId, projectId, uploadedBy, uploadFileType?.ToLower());
        }
    }
}