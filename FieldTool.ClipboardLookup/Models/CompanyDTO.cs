using FieldTool.Entity;
using System.Collections.Generic;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models
{
    public class CompanyDTO
    {
        public string CompanyBsid { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ZipExt { get; set; }
        public string ElectricAccountNumber { get; set; }
        public string ElectricRateCode { get; set; }
        public string GasAccountNumber { get; set; }
        public string GasRateCode { get; set; }
        public string RecordType { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public List<BuildingDTO> Buildings { get; set; }

        public CompanyDTO()
        {
        }

        public CompanyDTO(Company company)
        {
            if (company != null)
            {
                CompanyBsid = company.CompanyBsid;
                CompanyName = company.CompanyName;
                Address1 = company.Address1;
                Address2 = company.Address2;
                Address3 = company.Address3;
                City = company.City;
                State = company.State;
                Zip = company.Zip;
                ZipExt = company.ZipExt;
                ElectricAccountNumber = company.ElectricAccountNumber;
                ElectricRateCode = company.ElectricRateCode;
                GasAccountNumber = company.GasAccountNumber;
                GasRateCode = company.GasRateCode;
                RecordType = company.RecordType;
                EmailAddress = GetEmailAddress(company);
                Contacts = company.Contacts;
                Buildings = GetBuildings(company);
            }
        }

        private string GetEmailAddress(Company company)
        {
            string result = null;

            if (company.Contacts != null)
            {
                // Find first contact with non-empty email address
                Contact firstContact = company.Contacts.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.EmailAddress));
                if (firstContact != null)
                {
                    result = firstContact.EmailAddress;
                }
            }

            return result;
        }

        private List<BuildingDTO> GetBuildings(Company company)
        {
            var result = new List<BuildingDTO>();
            foreach (AuditProject auditProject in company.AuditProjects)
            {
                foreach (Building building in auditProject.Buildings)
                {
                    result.Add(new BuildingDTO(building));
                }
            }

            return result;
        }
    }
}