using FieldTool.Entity;
using System;
using System.Linq;

namespace FieldTool.ClipboardLookup.DAL.CB
{
    public class ProjectUploadRepository : BaseCbRepository<AuditUploadBackup>
    {
        public ProjectUploadRepository()
            : base()
        {
        }

        public ProjectUploadRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<AuditUploadBackup> Entities
        {
            get { return Context.AuditUploadBackups; }
        }
    }
}