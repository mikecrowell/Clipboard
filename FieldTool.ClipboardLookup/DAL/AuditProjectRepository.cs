using FieldTool.ClipboardLookup.Models;
using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL
{
    public class AuditProjectRepository : BaseRepository<AuditProject, AuditProjectDTO>
    {
        public AuditProjectRepository()
            : base()
        {
        }

        public AuditProjectRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<AuditProject> Entities
        {
            get { return Context.AuditProjects; }
        }

        protected override AuditProjectDTO ConvertToDTO(AuditProject entity)
        {
            return new AuditProjectDTO(entity);
        }

        public async Task SaveAuditProjectReport(string auditId, string uploadedBy, string url)
        {
            using (var context = this.Context)
            {
                AuditProjectReport report = new AuditProjectReport()
                {
                    AuditProjectBsid = auditId,
                    UploadedBy = uploadedBy,
                    Url = url,
                    UploadedDateTime = DateTime.Now
                };

                context.AuditProjectReports.Add(report);
                await context.SaveChangesAsync();
            }
        }

        internal void SaveAuditProject(string externalId, List<FieldTool.DAL.DTO.CompanyDTO> companies, Uri backupUri, string uploadedBy, bool isDeletedAudit)
        {
            using (var context = this.Context)
            {
                EntityCRUDForAudit entityCrud = new EntityCRUDForAudit(context);
                entityCrud.Save(companies, externalId, backupUri, uploadedBy, isDeletedAudit);
            }
        }
    }
}