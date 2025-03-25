using FieldTool.Bsi.Helpers;
using FieldTool.Bsi.Models;
using FieldTool.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models.EN
{
    public class PropertyInfo
    {
        // Required
        // Bensight's unique identifier for this Property
        public string BensightPropertyId { get; set; }

        // Required
        // Bensight Account to which this Property is associated
        public string BensightAccountId { get; set; }

        // Required
        // User-provided name of the property
        public string Name { get; set; }

        public string BusinessName { get; set; }

        // Optional
        //  - Residential
        //  - Multifamily
        //  - Commercial
        //  - Industrial
        public string FacilityType { get; set; }

        // Required
        //  - Existing
        //  - New
        public string PropertyStatus { get; set; }

        // Optional
        public int BuildingTypeId { get; set; }

        // Optional
        public string BuildingType { get; set; }

        // Required
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        // Required
        public string ZipCode { get; set; }

        // Defaults to 0
        public int SquareFootage { get; set; }

        public UtilityInfo ElectricUtility { get; set; }
        public UtilityInfo GasUtility { get; set; }

        // Required
        // Minimum of 1 required
        public List<PropertyContact> Contacts { get; set; }

        public List<Link> ReportUrls { get; set; }

        // internal collection used to create links but not serialized to the client
        internal List<ProjectInfo> Projects { get; set; }

        public PropertyInfo(Building building)
        {
            BensightPropertyId = building.BuildingBsid;
            BensightAccountId = building.AuditProject.CompanyBsid;
            Name = building.BuildingName;
            BusinessName = building.AuditProject.Company.CompanyName;
            FacilityType = "";  // get based on project type?
            PropertyStatus = "New";
            BuildingType = building.BuildingType;

            Address1 = building.Address1;
            Address2 = building.Address2;
            ZipCode = building.Zip + building.ZipExt;
            SquareFootage = (int)building.FloorAreaGross;

            ElectricUtility = new UtilityInfo()
            {
                BensightId = building.BuildingBsid,
                ProgramCode = building.AuditProject.ProgramId,
            };

            GasUtility = new UtilityInfo()
            {
                BensightId = building.BuildingBsid,
                ProgramCode = building.AuditProject.ProgramId,
            };

            if (building.BuildingBsid == building.AuditProject.Company.CompanyBsid)
            {
                // if the account on the building is the account of its parent company, use the company account information
                ElectricUtility.AccountNumber = building.AuditProject.Company.ElectricAccountNumber;
                ElectricUtility.Name = building.AuditProject.Company.CompanyName;
                ElectricUtility.RateClass = building.AuditProject.Company.ElectricRateCode;

                GasUtility.AccountNumber = building.AuditProject.Company.GasAccountNumber;
                GasUtility.Name = building.AuditProject.Company.CompanyName;
                GasUtility.RateClass = building.AuditProject.Company.GasRateCode;
            }
            else
            {
                // if the account on the building is its own account, look up the account information
                BsiService bsiService = new BsiService(ConfigurationManager.AppSettings);
                ApiAccount apiAccount = bsiService.FindAccountForBackfill(new Bsi.Models.BsiBackfillTerms()
                {
                    AccountId = building.BuildingBsid,
                    AuditId = building.AuditProjectBsid,
                });

                ElectricUtility.AccountNumber = apiAccount.ElectricAccountNumber;
                ElectricUtility.Name = apiAccount.Name;
                ElectricUtility.RateClass = apiAccount.ElectricRateCode;

                GasUtility.AccountNumber = apiAccount.GasAccountNumber;
                GasUtility.Name = apiAccount.Name;
                GasUtility.RateClass = apiAccount.GasRateCode;
            }

            Contacts = new List<PropertyContact>();
            if (building.AuditProject.Company.Contacts.Count > 0)
            {
                Contact contact = building.AuditProject.Company.Contacts.First();
                Contacts.Add(new PropertyContact()
                {
                    Name = contact.FirstName + " " + contact.LastName,
                    Email = contact.EmailAddress,
                    Phone = contact.PhoneNumber
                });
            }

            ReportUrls = building.AuditProject.AuditProjectReports.Select<AuditProjectReport, Link>(x => new Link("report", x.Url)).ToList();
            Projects = new List<ProjectInfo>();
        }
    }
}