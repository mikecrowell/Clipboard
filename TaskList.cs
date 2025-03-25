using FieldTool.BLL;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace FieldTool.UI
{
    [Serializable()]
    public class TaskList : IComparable<TaskList>
    {
        [Serializable()]
        public class MyTaskCollection
        {
            [XmlElement("MyTasks")]
            public List<TaskList> obj = new List<TaskList>();
        }

        public class Building
        {
            public string buildingname { get; set; }
        }

        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string PropertyManagement { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public string ScheduleDuration { get; set; }
        public string AppointmentType { get; set; }
        public string Status { get; set; }

        public string AuditID { get; set; }
        public string AuditExternalID { get; set; }
        public string AuditName { get; set; }
        public string ProgramID { get; set; }
        public string ProgramName { get; set; }
        public DateTime? AuditDate { get; set; }

        [XmlElement("AuditContact")]
        public string AuditContact { get; set; }

        public string AuditStatus { get; set; }

        public List<Building> Buildings = new List<Building>();

        public string CompanyId { get; set; }
        public string CompanyExternalId { get; set; }

        [XmlIgnore()]
        public bool IsValidated
        {
            get
            {
                return !String.IsNullOrWhiteSpace(this.CompanyExternalId);
            }
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        #region Constructors

        public TaskList()
        {
            ;
        }

        public TaskList(Company company)
        {
            if (company != null)
            {
                this.CompanyId = company.CompanyId;
                this.CompanyExternalId = company.ExternalId;

                this.Street = company.AddressLine1;
                this.City = company.City;
                this.State = company.State;
                this.Zip = company.PostalCode;

                this.AccountNumber = (String.IsNullOrWhiteSpace(company.ElectricAccountNumber) ? (String.IsNullOrWhiteSpace(company.GasAccountNumber) ? "" : company.GasAccountNumber) : company.ElectricAccountNumber);
                this.AccountName = company.Name;
                this.PropertyManagement = company.PropertyManagement;
                this.Address = company.AddressLine1 + " " + company.AddressLine2 + " " + company.City + ", " + company.State + " " + company.PostalCode + (String.IsNullOrWhiteSpace(company.PostalCodeExtension) ? "" : "-" + company.PostalCodeExtension);

                if (company.Contacts != null && company.Contacts.Count > 0)
                {
                    if (company.Contacts[0].Phones != null && company.Contacts[0].Phones.Count > 0)
                    {
                        this.Phone = ((Phone)company.Contacts[0].Phones[0]).PhoneNumber;
                        this.AuditContact = company.Contacts[0].FirstName + " " + company.Contacts[0].LastName;
                    }
                    else
                    {
                        this.Phone = "";
                        this.AuditContact = "";
                    }
                }
                else
                {
                    this.Phone = (company.Phone ?? "");
                    this.AuditContact = "";
                }

                if (company.Audits != null && company.Audits.Count > 0)
                {
                    this.ScheduleDate = company.Audits[0].ScheduledStartTimeStamp.ToShortDateString();
                    this.ScheduleTime = company.Audits[0].ScheduledStartTimeStamp.ToShortTimeString();

                    this.AuditID = company.Audits[0].Id;
                    this.AuditName = company.Audits[0].Name;
                    this.AuditExternalID = company.Audits[0].ExternalId;
                    this.AuditDate = company.Audits[0].ScheduledStartTimeStamp;
                    this.ProgramID = company.Audits[0].ProgramId;
                    this.ProgramName = company.Audits[0].ProgramType;
                    this.AuditStatus = (company.Audits[0].AuditStatus ?? Constants.AuditStatus.INCOMPLETE);
                }
            }
        }

        public TaskList(MyTasks.CompanyCollectionCompany company)
        {
            if (company != null)
            {
                this.CompanyId = company.CompanyId;
                this.CompanyExternalId = company.ExternalId;

                this.Street = company.AddressLine1;
                this.City = company.City;
                this.State = company.State;
                this.Zip = company.PostalCode;

                this.AccountNumber = (String.IsNullOrWhiteSpace(company.ElectricAccountNumber) ? (String.IsNullOrWhiteSpace(company.GasAccountNumber) ? "" : company.GasAccountNumber) : company.ElectricAccountNumber);
                this.AccountName = company.CompanyName;
                this.PropertyManagement = company.PropertyManagement;
                this.Address = company.AddressLine1 + " " + company.AddressLine2 + " " + company.City + ", " + company.State + " " + company.PostalCode + (String.IsNullOrWhiteSpace(company.PostalCodeExtension) ? "" : "-" + company.PostalCodeExtension);

                // SMM - 5/27/15: Was getting a null reference exception on this object.
                if (company.Contacts != null && company.Contacts.Contact != null)
                {
                    if (company.Contacts.Contact.Phones != null && company.Contacts.Contact.Phones.Phone != null && company.Contacts.Contact.Phones.Phone.PhoneNumber != null)
                    {
                        this.Phone = company.Contacts.Contact.Phones.Phone.PhoneNumber;
                        this.AuditContact = company.Contacts.Contact.FirstName + " " + company.Contacts.Contact.LastName;
                    }
                    else
                    {
                        this.Phone = "";
                        this.AuditContact = "";
                    }
                }
                else
                {
                    this.Phone = (company.Phone ?? "");
                    this.AuditContact = "";
                }

                if (company.Audits != null && company.Audits.Audit != null)
                {
                    DateTime dt = default(DateTime);
                    DateTime.TryParse(company.Audits.Audit.ScheduleStartTime, null, System.Globalization.DateTimeStyles.AssumeLocal, out dt);

                    if (dt != default(DateTime))
                    {
                        this.ScheduleDate = dt.ToShortDateString();
                        this.ScheduleTime = dt.ToShortTimeString();
                    }

                    this.AuditID = company.Audits.Audit.AuditId;
                    this.AuditName = company.Audits.Audit.AuditName;
                    this.AuditExternalID = company.Audits.Audit.ExternalId;
                    this.AuditDate = dt;
                    this.ProgramID = company.Audits.Audit.ProgramID;
                    this.ProgramName = company.Audits.Audit.programName;
                    this.AuditStatus = (company.Audits.Audit.AuditStatus ?? Constants.AuditStatus.INCOMPLETE);
                }
            }
        }

        #endregion Constructors

        internal void UpdateCompanyIds(string newId)
        {
            this.CompanyExternalId = newId;
            this.CompanyId = newId;
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            unchecked
            {
                result += 3 * (this.CompanyId ?? "").GetHashCode();
                result += 3 * (this.AccountNumber ?? "").GetHashCode();
                result += 3 * (this.AccountName ?? "").GetHashCode();
                result += 3 * (this.PropertyManagement ?? "").GetHashCode();
                result += 3 * (this.Address ?? "").GetHashCode();
                result += 3 * (this.Phone ?? "").GetHashCode();
                result += 3 * (this.ScheduleDate ?? "").GetHashCode();
                result += 3 * (this.ScheduleTime ?? "").GetHashCode();
                result += 3 * (this.ScheduleDuration ?? "").GetHashCode();
                result += 3 * (this.AppointmentType ?? "").GetHashCode();
                result += 3 * (this.Status ?? "").GetHashCode();
                result += 3 * (this.AuditID ?? "").GetHashCode();
                result += 3 * (this.AuditExternalID ?? "").GetHashCode();
                result += 3 * (this.AuditName ?? "").GetHashCode();
                result += 3 * (this.ProgramID ?? "").GetHashCode();
                result += 3 * (this.ProgramName ?? "").GetHashCode();
                result += 3 * (this.AuditContact ?? "").GetHashCode();
                result += 3 * (this.AuditStatus ?? "").GetHashCode();
                result += 3 * (this.CompanyId ?? "").GetHashCode();
                result += 3 * (this.CompanyExternalId ?? "").GetHashCode();
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                TaskList t = obj as TaskList;
                if (t == null)
                {
                    return false;
                }
                else
                {
                    return this.CompanyId == t.CompanyId;
                }
            }
        }

        #region IComparable<TaskList> Members

        public int CompareTo(TaskList other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return this.CompanyId.CompareTo(other.CompanyId);
            }
        }

        #endregion IComparable<TaskList> Members
    }
}