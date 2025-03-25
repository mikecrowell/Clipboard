using FieldTool.Entity;
using System.Collections.Generic;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models.EN
{
    public class ProjectInfo
    {
        // Required
        // Ties this Project to a Property
        public string BensightProjectId { get; set; }

        public string ProgramCode { get; set; }

        // Required
        public string ProjectName { get; set; }

        public List<MeasureInfo> Measures { get; set; }

        public ProjectInfo()
        {
        }

        public ProjectInfo(Building building)
        {
            ProjectName = string.Format("Energy Assessment Completed On {0}",
                (building.AuditProject.ScheduledStart != null && building.AuditProject.ScheduledStart.HasValue) ? building.AuditProject.ScheduledStart.Value.ToShortDateString() : string.Empty);
            BensightProjectId = building.BuildingGuid;
            ProgramCode = building.AuditProject.ProgramId;

            Measures = new List<MeasureInfo>();
            foreach (Recommendation recommendation in building.Recommendations.Where(x => x.IncludedInReport != null && x.IncludedInReport.HasValue && x.IncludedInReport.Value))
            {
                MeasureInfo measureInfo = new MeasureInfo(recommendation);
                if (!string.IsNullOrWhiteSpace(measureInfo.BensightComponentId))
                {
                    Measures.Add(measureInfo);
                }
            }
        }
    }
}