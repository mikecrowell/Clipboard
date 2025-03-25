using FieldTool.Entity;
using System.Collections.Generic;

namespace FieldTool.ClipboardLookup.Models
{
    public class AuditProjectDTO
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectStatus { get; set; }
        public string CompanyBsid { get; set; }
        public long ClientRateClassElectricId { get; set; }
        public long ClientRateClassGasId { get; set; }
        public ICollection<RecommendationDTO> Recommendations { get; set; }
        public ICollection<AuditReportDTO> AuditReports { get; set; }

        public AuditProjectDTO()
        {
        }

        public AuditProjectDTO(AuditProject auditProject)
        {
            if (auditProject != null)
            {
                ProjectId = auditProject.AuditProjectBsid;
                ProjectName = auditProject.AuditProjectName;
                ProjectStatus = auditProject.AuditStatus;
                ClientRateClassElectricId = 0; // TODO: figure out where to get ClientRateClassElectricId
                ClientRateClassGasId = 0; // TODO: figure out where to get ClientRateClassGasId
                CompanyBsid = auditProject.CompanyBsid;

                Recommendations = new List<RecommendationDTO>();
                foreach (Recommendation recommendation in auditProject.Recommendations)
                {
                    Recommendations.Add(new RecommendationDTO(recommendation));
                }

                AuditReports = new List<AuditReportDTO>();
                foreach (AuditProjectReport auditReport in auditProject.AuditProjectReports)
                {
                    AuditReports.Add(new AuditReportDTO(auditReport));
                }
            }
        }
    }
}