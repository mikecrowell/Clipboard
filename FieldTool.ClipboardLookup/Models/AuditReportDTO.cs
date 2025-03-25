using FieldTool.Entity;
using System.Collections.Generic;

namespace FieldTool.ClipboardLookup.Models
{
    public class AuditReportDTO
    {
        public string ProjectId { get; set; }
        public string Url { get; set; }
        public List<string> Errors { get; set; }

        public AuditReportDTO()
        {
        }

        public AuditReportDTO(AuditProjectReport auditProjectReport)
        {
            ProjectId = auditProjectReport.AuditProjectBsid;
            Url = auditProjectReport.Url;
        }
    }
}