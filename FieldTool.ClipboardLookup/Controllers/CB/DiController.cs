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
    [RoutePrefix("api/cb/di")]
    public class DiController : BaseCbController<DiUploadBackup>
    {
        public DiController(ICbRepository<DiUploadBackup> repository, ILogger logger, IAzureStorageService azureService)
            : base(repository, logger, azureService)
        {
        }

        // Get Last for auditId
        // * only works for an audit that was successfully saved.
        [Route("backup/on/diId/{diId}")]
        [HttpGet]
        public async Task<IHttpActionResult> ListBackupsByDiId(string diId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.DiProjectInfo != null && x.DiProjectInfo.DiProjectInfoId == diId),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = diId,
                    type = LookupServiceConstants.UploadSourceType.DI,
                    display = y.DiId,
                    url = y.DiDataUrl,
                    data = y.DiDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedDi
                })));
            return Ok(CreateReturnsFrom(backups.ToList<BackupSelect>()));
        }

        // Get Last for accountId
        [Route("backup/for/accountId/{accountId}")]
        [HttpGet]
        public async Task<IHttpActionResult> ListBackupsByExternalId(string accountId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.DiAccountBsid == accountId && !String.IsNullOrEmpty(accountId)),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = accountId,
                    type = LookupServiceConstants.UploadSourceType.DI,
                    display = y.DiId,
                    url = y.DiDataUrl,
                    data = y.DiDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedDi
                })));

            return Ok(CreateReturnsFrom(backups.ToList<BackupSelect>()));
        }

        // Get Specific for auditId
        [Route("backup/{uploadId}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSpecificDiProjectBackup(long uploadId)
        {
            IEnumerable<BackupSelect> backups = await GetBackups(
                (x => x.Id == uploadId),
                (x => x.OrderByDescending(y => y.UploadedDate)),
                (x => x.Select(y => new BackupSelect
                {
                    id = y.Id,
                    key = uploadId.ToString(),
                    type = LookupServiceConstants.UploadSourceType.DI,
                    display = y.DiId,
                    url = y.DiDataUrl,
                    data = y.DiDataXml,
                    error = y.ErrorMessage,
                    uploadedOn = y.UploadedDate,
                    uploadedBy = y.UploadedBy,
                    isDeleted = y.IsDeletedDi
                })));

            List<BackupSelect> l = backups.ToList<BackupSelect>();
            BackupSelect f = l.FirstOrDefault();
            return Ok(ReturnData(f));
        }

        // Upload and then save
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

            if (uploadReturn.IsValid())
            {
                uploadReturn.Trace.Add("Backup data");
                // use the serialized data to send to Azure as a backup
                try
                {
                    uploadReturn.BackupUri = uploadXmlToAzure(LookupServiceConstants.UploadSourceType.DI, programId, accountId, projectId, new DataParser().SerializeToStream<DIandCompanyDTO.CompanyCollection>(data));
                    uploadReturn.IsBackedUp = true;
                }
                catch (Exception e)
                {
                    var inner = ExceptionHelper.Innermost(e);
                    uploadReturn.Errors.Add("Error uploading to Azure: " + inner.Message);
                }
            }
            if (uploadReturn.IsValid())
            {
                uploadReturn.Trace.Add("Save data to DB");
                // try saving
                try
                {
                    var crud = new EntityCRUDForDI(Repository.Context);
                    var dis = new List<DIDTO>();
                    dis.AddRange(data.ItemsDI);
                    crud.Save(dis, accountId, uploadReturn.BackupUri, user, deleted);
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
            return await doUploadReport(LookupServiceConstants.UploadSourceType.DI, programId, accountId, projectId, uploadedBy, LookupServiceConstants.UploadFileType.REPORT);
        }

        // Upload Dependency files
        [HttpPost]
        [Route("{programId}/for/{accountId}/on/{projectId}/by/{uploadedBy}/of/{uploadFileType}/upload")]
        public async Task<IHttpActionResult> SaveFile(string programId, string accountId, string projectId, string uploadedBy, string uploadFileType)
        {
            return await doUploadReport(LookupServiceConstants.UploadSourceType.DI, programId, accountId, projectId, uploadedBy, uploadFileType?.ToLower());
        }
    }
}