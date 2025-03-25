using FieldTool.ClipboardLookup.DAL.CB;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Logging;
using FieldTool.Constants.Models.CB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers.CB
{
    public abstract class BaseCbController<TEntity> : BaseLoggingController
    {
        public virtual ICbRepository<TEntity> Repository { get; private set; }
        public virtual IAzureStorageService AzureStorageService { get; private set; }

        public BaseCbController(ICbRepository<TEntity> repository, ILogger logger, IAzureStorageService azureService) : base(logger)
        {
            Repository = repository;
            azureService.Logger = logger;
            AzureStorageService = azureService;
        }

        public virtual Uri RequestUri { get { return Request.RequestUri; } }
        // GET backup by {backup_id}
        // if the url is populated, download, serialize, and return
        // if the data is populated, serialize and return
        // LIST backups by {list_type}

        public async Task<IEnumerable<BackupSelect>> GetBackups(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IQueryable<BackupSelect>> select = null
            )
        {
            return await Repository.GetAll(filter, orderBy, select);
        }

        internal async Task<IHttpActionResult> doUploadReport(string sourceType, string programId, string accountId, string projectId, string uploadedBy, string uploadFileType)
        {
            var uploadReturn = new UploadReturn();
            try
            {
                uploadReturn.Trace.Add("Check Params");
                // check all inputs
                if (String.IsNullOrEmpty(sourceType))
                {
                    uploadReturn.Errors.Add("Missing SOURCE_TYPE");
                }
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
                if (String.IsNullOrEmpty(uploadedBy))
                {
                    uploadReturn.Errors.Add("Missing UPLOADED_BY");
                }
                if (String.IsNullOrEmpty(uploadFileType))
                {
                    uploadReturn.Errors.Add("Missing UPLOAD_FILE_TYPE");
                }

                uploadReturn.Trace.Add("Get Report Data");

                Stream fileInputStream = await GetReportDataFromContext();

                uploadReturn.Trace.Add("Build Name");

                var encodedFileName = await GetEncodedFilenameFromContext(accountId, projectId);

                uploadReturn.Trace.Add("Store (" + encodedFileName + ")");

                uploadReturn.BackupUri = uploadFileToAzure(sourceType, programId, accountId, projectId, encodedFileName, fileInputStream);
                uploadReturn.Trace.Add("Azure (" + uploadReturn.BackupUri.AbsoluteUri + ")");
                uploadReturn.IsBackedUp = true;
                // insert the report date

                uploadReturn.Type = uploadFileType;
                var saved = 0;
                if (uploadFileType == LookupServiceConstants.UploadFileType.REPORT)
                {
                    uploadReturn.Trace.Add($"Save to DB type to ReportEntry({uploadFileType})");
                    saved = AddReportEntry(sourceType, accountId, projectId, uploadedBy, uploadReturn.BackupUri);
                }
                else
                {
                    uploadReturn.Trace.Add($"Save to DB type to UploadedFile({uploadFileType})");
                    saved = AddUploadedFileEntry(sourceType, accountId, projectId, uploadedBy, uploadReturn.BackupUri, uploadFileType);
                }
                if (saved == 1)
                {
                    uploadReturn.IsSaved = true;
                    uploadReturn.Trace.Add("Finished");
                }
                else
                {
                    uploadReturn.Errors.Add("DB Save failed");
                }
            }
            catch (Exception ex)
            {
                var cause = ExceptionHelper.Innermost(ex);
                uploadReturn.Errors.Add(cause.Message);
            }

            uploadReturn.ValidateOrDie();
            return Ok(uploadReturn);
        }

        public int AddReportEntry(string sourceType, string accountId, string projectId, string uploadedBy, Uri uri)
        {
            using (var context = this.Repository.Context)
            {
                context.ReportTrackings.Add(new Entity.ReportTracking
                {
                    Type = sourceType,
                    AccountId = accountId,
                    ProjectId = projectId,
                    UploadedBy = uploadedBy,
                    UploadedDateTime = DateTime.Now,
                    Url = uri.AbsoluteUri
                });
                return context.SaveChanges();
            }
        }

        public int AddUploadedFileEntry(string sourceType, string accountId, string projectId, string uploadedBy, Uri uri, string uploadedFileType)
        {
            using (var context = this.Repository.Context)
            {
                context.UploadedFiles.Add(new Entity.UploadedFile
                {
                    Type = sourceType,
                    AccountId = accountId,
                    ProjectId = projectId,
                    UploadedBy = uploadedBy,
                    UploadedDateTime = DateTime.Now,
                    Url = uri.AbsoluteUri,
                    UploadedFileType = uploadedFileType
                });
                return context.SaveChanges();
            }
        }

        public string ReturnData(BackupSelect backup)
        {
            if (backup == null)
            {
                throw new Exception("No backup found");
            }
            // if the there is a url, then go get the data and return the stream
            if (!String.IsNullOrEmpty(backup.url))
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(backup.url);
                request.AutomaticDecompression = System.Net.DecompressionMethods.GZip;

                using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            // else if there is a data in the db, return the data as a stream
            else if (!String.IsNullOrEmpty(backup.data))
            {
                return backup.data;
            }
            // otherwise explode.
            else
            {
                throw new Exception("No url or data for backup");
            }
        }

        public virtual async Task<Stream> GetReportDataFromContext()
        {
            var content = await GetFileContent();
            Stream fileInputStream = await content.ReadAsStreamAsync();
            if (fileInputStream.Length == 0)
            {
                throw new Exception("Report file is empty.");
            }
            return fileInputStream;
        }

        public virtual async Task<string> GetEncodedFilenameFromContext(string accountId, string projectId)
        {
            var content = await GetFileContent();
            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(content.Headers.ContentDisposition.FileName);
            var filenameExtension = Path.GetExtension(content.Headers.ContentDisposition.FileName);
            var uploadedFileName = $"{accountId}_{projectId}_{filenameWithoutExtension}_{DateTime.Now:yyyyMMdd-HHmmss}{filenameExtension}";
            var encodedName = HttpUtility.UrlEncode(uploadedFileName, Encoding.UTF8);
            return encodedName;
        }

        // need to override because the DI or Projects use diffferent function calls
        public virtual Uri uploadXmlToAzure(string type, string programId, string accountId, string projectId, Stream dataAsStream)
        {
            return this.AzureStorageService.SaveUploadBackupXmlFile(type, programId, accountId, projectId, dataAsStream);
        }

        public virtual Uri uploadFileToAzure(string type, string programId, string accountId, string projectId, string filename, Stream dataAsStream)
        {
            return this.AzureStorageService.SaveReportFile(type, programId, accountId, projectId, filename, dataAsStream);
        }

        public Uri BuildLinkFromBackupSelect(BackupSelect s)
        {
            Uri u = null;
            if (!String.IsNullOrEmpty(s.url))
            {
                Uri.TryCreate(s.url, UriKind.Absolute, out u);
            }
            else if (!String.IsNullOrEmpty(s.data))
            {
                var prefix = RequestUri.AbsoluteUri.ToLower().Split(new string[] { "backup" }, StringSplitOptions.RemoveEmptyEntries);

                Uri.TryCreate(String.Format("{0}/backup/{1}",
                    prefix[0].ToString().TrimEnd('/'),
                    s.id), UriKind.Absolute, out u);
            }
            return u;
        }

        public virtual IHttpActionResult OkOrDie(UploadReturn r)
        {
            if (r.IsValid())
            {
                return Ok(r);
            }
            var dataAsString = JsonConvert.SerializeObject(r) ?? String.Empty;
            var maxResultLength = dataAsString.Length > 512 ? 512 : dataAsString.Length;
            throw new HttpResponseException(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ReasonPhrase = dataAsString.Substring(0, maxResultLength),
                Content = new StringContent(dataAsString)
            });
        }

        internal List<BackupReturn> CreateReturnsFrom(List<BackupSelect> selects)
        {
            var returns = new List<BackupReturn>();
            foreach (BackupSelect item in selects)
            {
                var u = BuildLinkFromBackupSelect(item);
                if (u != default(Uri))
                {
                    returns.Add(new BackupReturn
                    {
                        Id = item.id,
                        Uri = u,
                        Error = item.error,
                        Display = item.display,
                        Key = item.key,
                        Type = item.type,
                        UploadedOn = item.uploadedOn,
                        UploadedBy = item.uploadedBy,
                        isDeleted = item.isDeleted
                    });
                }
            }
            return returns;
        }

        private HttpContent _fileContent = null;

        internal async Task<HttpContent> GetFileContent()
        {
            if (_fileContent == null)
            {
                _fileContent = await DoAsyncGetFileContent();
            }
            return _fileContent;
        }

        private async Task<HttpContent> DoAsyncGetFileContent()
        {
            if (Request.Content.IsMimeMultipartContent())
            {
                MultipartMemoryStreamProvider provider = await Request.Content.ReadAsMultipartAsync();
                _fileContent = provider.Contents.FirstOrDefault(x => x.Headers.ContentDisposition.Name.ToLower() == "reportfile");
                if (_fileContent == null)
                {
                    throw new Exception("Required content 'ReportFile' not found.");
                }
                return _fileContent;
            }
            else
            {
                throw new Exception("Content is not a 'MimeMultipartContent'");
            }
        }
    }
}