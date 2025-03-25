using FieldTool.ClipboardLookup.Helpers;
using FieldTool.Entity;
using System.Collections.Generic;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models.EN
{
    public class AccountInfo
    {
        public string AccountBensightId { get; set; } // From Company
        public string Name { get; set; } // From Company
        public string Address1 { get; set; } // From Company
        public string Address2 { get; set; } // From Company
        public string ZipCode { get; set; } // From Company
        public string EmailAddress { get; set; } // Email from Account or First Contact
        public string ContactFirstName { get; set; } // Contact First Name
        public string ContactLastName { get; set; } // Contact Last Name
        public string ContactPhone { get; set; } // Contact Phone

        // internal collection used to create links but not serialized to the client
        internal List<string> BuildingIds { get; set; }

        public AccountInfo()
        {
        }

        public AccountInfo(Company company)
        {
            Contact contact = company.Contacts.FirstOrDefault();
            string contactEmail = DataHelper.GetPropertyIfNotNull<Contact, string>(contact, x => x.EmailAddress, string.Empty);
            string contactFirstName = DataHelper.GetPropertyIfNotNull<Contact, string>(contact, x => x.FirstName, string.Empty);
            string contactLastName = DataHelper.GetPropertyIfNotNull<Contact, string>(contact, x => x.LastName, string.Empty);
            string contactPhone = DataHelper.GetPropertyIfNotNull<Contact, string>(contact, x => x.PhoneNumber, string.Empty);

            AccountBensightId = company.CompanyBsid;
            Name = company.CompanyName;
            Address1 = company.Address1;
            Address2 = company.Address2;
            ZipCode = company.Zip;
            EmailAddress = company.EmailAddress ?? contactEmail;
            ContactFirstName = contactFirstName;
            ContactLastName = contactLastName;
            ContactPhone = contactPhone ?? company.PhoneNumber;

            BuildingIds = new List<string>();
            foreach (AuditProject project in company.AuditProjects)
            {
                foreach (Building building in project.Buildings)
                {
                    BuildingIds.Add(building.BuildingGuid);
                }
            }
        }
    }
}