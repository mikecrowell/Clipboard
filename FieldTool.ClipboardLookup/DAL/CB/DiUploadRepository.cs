using FieldTool.Entity;
using System;
using System.Linq;

namespace FieldTool.ClipboardLookup.DAL.CB
{
    public class DiUploadRepository : BaseCbRepository<DiUploadBackup>
    {
        public DiUploadRepository()
            : base()
        {
        }

        public DiUploadRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<DiUploadBackup> Entities
        {
            get { return Context.DiUploadBackups; }
        }
    }
}