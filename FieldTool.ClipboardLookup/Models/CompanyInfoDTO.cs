using FieldTool.Entity;
using System.Linq;

namespace FieldTool.ClipboardLookup.Models
{
    public class CompanyInfoDTO : LinkedResource
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
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public CompanyInfoDTO()
            : base() { }

        public CompanyInfoDTO(Company company)
            : base()
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
                EmailAddress = GetEmailAddress(company);
                PhoneNumber = GetPhoneNumber(company);
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

        private string GetPhoneNumber(Company company)
        {
            string result = null;

            if (company.Contacts != null)
            {
                // Find first contact with non-empty phone number
                Contact firstContact = company.Contacts.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
                if (firstContact != null)
                {
                    result = firstContact.PhoneNumber;
                }
            }

            return result;
        }
    }
}