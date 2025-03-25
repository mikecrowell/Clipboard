using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.Models
{
    public class AuditProjectInfoDTO : LinkedResource
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatus { get; set; }
        public string CompanyBsid { get; set; }

        public AuditProjectInfoDTO() : base()
        {
        }

        public AuditProjectInfoDTO(AuditProject auditProject)
            : base()
        {
            if (auditProject != null)
            {
                ProjectId = auditProject.AuditProjectBsid;
                ProjectName = auditProject.AuditProjectName;
                ProjectStatus = auditProject.AuditStatus;
                CompanyBsid = auditProject.CompanyBsid;
            }
        }
    }
}