using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.Models
{
    public class BuildingDTO
    {
        public string BuildingGuid { get; set; }
        public string AuditProjectBsid { get; set; }
        public string BuildingName { get; set; }
        public string BuildingCategory { get; set; }
        public string BuildingType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ZipExt { get; set; }
        public int? OccupantCount { get; set; }
        public int? FloorsAbove { get; set; }
        public int? FloorsBelow { get; set; }
        public double? FloorAreaGross { get; set; }
        public double? FloorAreaOccupied { get; set; }
        public string BuildingHoursEquivalent { get; set; }
        public string RateCode { get; set; }
        public int? ZipZone { get; set; }

        public BuildingDTO()
        {
        }

        public BuildingDTO(Building building)
        {
            BuildingGuid = building.BuildingGuid;
            AuditProjectBsid = building.AuditProjectBsid;
            BuildingName = building.BuildingName;
            BuildingCategory = building.BuildingCategory;
            BuildingType = building.BuildingType;
            Address1 = building.Address1;
            Address2 = building.Address2;
            Address3 = building.Address3;
            City = building.City;
            State = building.State;
            Zip = building.Zip;
            ZipExt = building.ZipExt;
            OccupantCount = building.OccupantCount;
            FloorsAbove = building.FloorsAbove;
            FloorsBelow = building.FloorsBelow;
            FloorAreaGross = building.FloorAreaGross;
            FloorAreaOccupied = building.FloorAreaOccupied;
            BuildingHoursEquivalent = building.BuildingHoursEquivalent;
            RateCode = building.RateCode;
            ZipZone = building.ZipZone;
        }
    }
}