using System;

namespace FieldTool.UI
{
    public class MyTasks
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class CompanyCollection
        {
            private CompanyCollectionCompany[] companyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Company")]
            public CompanyCollectionCompany[] Company
            {
                get
                {
                    return this.companyField;
                }
                set
                {
                    this.companyField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompany
        {
            private CompanyCollectionCompanyAudits auditsField;

            private CompanyCollectionCompanyContacts contactsField;

            private string companyIDField;

            private string externalIDField;

            private string companyNameField;

            private string propertyManagementField;

            private string addressLine1Field;

            private string addressLine2Field;

            private string addressLine3Field;

            private string cityField;

            private string postalCodeField;

            private string postalCodeExtensionField;

            private string recordTypeField;

            private string stateField;

            private string electricAccountNumberField;
            private string electricRateCodeField;
            private string gasAccountNumberField;
            private string gasRateCodeField;
            private string _phone;

            /// <remarks/>
            public CompanyCollectionCompanyAudits Audits
            {
                get
                {
                    return this.auditsField;
                }
                set
                {
                    this.auditsField = value;
                }
            }

            /// <remarks/>
            public CompanyCollectionCompanyContacts Contacts
            {
                get
                {
                    return this.contactsField;
                }
                set
                {
                    this.contactsField = value;
                }
            }

            /// <remarks/>
            public string CompanyId
            {
                get
                {
                    return this.companyIDField;
                }
                set
                {
                    this.companyIDField = value;
                }
            }

            /// <remarks/>
            public string ExternalId
            {
                get
                {
                    return this.externalIDField;
                }
                set
                {
                    this.externalIDField = value;
                }
            }

            /// <remarks/>
            public string CompanyName
            {
                get
                {
                    return this.companyNameField;
                }
                set
                {
                    this.companyNameField = value;
                }
            }

            /// <remarks/>
            public string PropertyManagement
            {
                get
                {
                    return this.propertyManagementField;
                }
                set
                {
                    this.propertyManagementField = value;
                }
            }

            /// <remarks/>
            public string AddressLine1
            {
                get
                {
                    return this.addressLine1Field;
                }
                set
                {
                    this.addressLine1Field = value;
                }
            }

            /// <remarks/>
            public string AddressLine2
            {
                get
                {
                    return this.addressLine2Field;
                }
                set
                {
                    this.addressLine2Field = value;
                }
            }

            /// <remarks/>
            public string AddressLine3
            {
                get
                {
                    return this.addressLine3Field;
                }
                set
                {
                    this.addressLine3Field = value;
                }
            }

            /// <remarks/>
            public string City
            {
                get
                {
                    return this.cityField;
                }
                set
                {
                    this.cityField = value;
                }
            }

            /// <remarks/>
            public string PostalCode
            {
                get
                {
                    return this.postalCodeField;
                }
                set
                {
                    this.postalCodeField = value;
                }
            }

            /// <remarks/>
            public string PostalCodeExtension
            {
                get
                {
                    return this.postalCodeExtensionField;
                }
                set
                {
                    this.postalCodeExtensionField = value;
                }
            }

            /// <remarks/>
            public string RecordType
            {
                get
                {
                    return this.recordTypeField;
                }
                set
                {
                    this.recordTypeField = value;
                }
            }

            /// <remarks/>
            public string State
            {
                get
                {
                    return this.stateField;
                }
                set
                {
                    this.stateField = value;
                }
            }

            public string ElectricAccountNumber
            {
                get
                {
                    return this.electricAccountNumberField;
                }
                set
                {
                    this.electricAccountNumberField = value;
                }
            }

            public string ElectricRateCode
            {
                get
                {
                    return this.electricRateCodeField;
                }
                set
                {
                    this.electricRateCodeField = value;
                }
            }

            public string GasAccountNumber
            {
                get
                {
                    return this.gasAccountNumberField;
                }
                set
                {
                    this.gasAccountNumberField = value;
                }
            }

            public string GasRateCode
            {
                get
                {
                    return this.gasRateCodeField;
                }
                set
                {
                    this.gasRateCodeField = value;
                }
            }

            public bool IsValidated
            {
                get
                {
                    return !String.IsNullOrWhiteSpace(this.ExternalId);
                }
            }

            public string Phone
            {
                get
                {
                    return this._phone;
                }
                set
                {
                    this._phone = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyAudits
        {
            private CompanyCollectionCompanyAuditsAudit auditField;

            /// <remarks/>
            public CompanyCollectionCompanyAuditsAudit Audit
            {
                get
                {
                    return this.auditField;
                }
                set
                {
                    this.auditField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyAuditsAudit
        {
            private string auditIdField;

            private string externalIdField;

            private string auditNameField;

            private string scheduleStartTimeField;

            private string actualStartTimeStampField;

            private string endTimeStampField;

            private string retrofitsField;

            private string programIDField;

            private string programNameField;

            private DateTime auditAppointmentField;

            private string auditCompletionTimeField;

            private string auditArrivalTimeField;

            private string auditStatus;

            /// <remarks/>
            public string AuditId
            {
                get
                {
                    return this.auditIdField;
                }
                set
                {
                    this.auditIdField = value;
                }
            }

            /// <remarks/>
            public string ExternalId
            {
                get
                {
                    return this.externalIdField;
                }
                set
                {
                    this.externalIdField = value;
                }
            }

            /// <remarks/>
            public string AuditName
            {
                get
                {
                    return this.auditNameField;
                }
                set
                {
                    this.auditNameField = value;
                }
            }

            /// <remarks/>
            public string ScheduleStartTime
            {
                get
                {
                    return this.scheduleStartTimeField;
                }
                set
                {
                    this.scheduleStartTimeField = value;
                }
            }

            /// <remarks/>
            public string ActualStartTimeStamp
            {
                get
                {
                    return this.actualStartTimeStampField;
                }
                set
                {
                    this.actualStartTimeStampField = value;
                }
            }

            /// <remarks/>
            public string EndTimeStamp
            {
                get
                {
                    return this.endTimeStampField;
                }
                set
                {
                    this.endTimeStampField = value;
                }
            }

            /// <remarks/>
            public string Retrofits
            {
                get
                {
                    return this.retrofitsField;
                }
                set
                {
                    this.retrofitsField = value;
                }
            }

            /// <remarks/>
            public string ProgramID
            {
                get
                {
                    return this.programIDField;
                }
                set
                {
                    this.programIDField = value;
                }
            }

            /// <remarks/>
            public string programName
            {
                get
                {
                    return this.programNameField;
                }
                set
                {
                    this.programNameField = value;
                }
            }

            /// <remarks/>
            public DateTime AuditAppointment
            {
                get
                {
                    return this.auditAppointmentField;
                }
                set
                {
                    this.auditAppointmentField = value;
                }
            }

            /// <remarks/>
            public string AuditCompletionTime
            {
                get
                {
                    return this.auditCompletionTimeField;
                }
                set
                {
                    this.auditCompletionTimeField = value;
                }
            }

            /// <remarks/>
            public string AuditArrivalTime
            {
                get
                {
                    return this.auditArrivalTimeField;
                }
                set
                {
                    this.auditArrivalTimeField = value;
                }
            }

            /// <remarks/>
            public string AuditStatus
            {
                get
                {
                    return this.auditStatus;
                }
                set
                {
                    this.auditStatus = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContacts
        {
            private CompanyCollectionCompanyContactsContact contactField;

            /// <remarks/>
            public CompanyCollectionCompanyContactsContact Contact
            {
                get
                {
                    return this.contactField;
                }
                set
                {
                    this.contactField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContactsContact
        {
            private CompanyCollectionCompanyContactsContactPhones phonesField;

            private CompanyCollectionCompanyContactsContactEmails emailsField;

            private string contactIdField;

            private string externalIdField;

            private string firstNameField;

            private object middleNameField;

            private string lastNameField;

            private object noteField;

            /// <remarks/>
            public CompanyCollectionCompanyContactsContactPhones Phones
            {
                get
                {
                    return this.phonesField;
                }
                set
                {
                    this.phonesField = value;
                }
            }

            /// <remarks/>
            public CompanyCollectionCompanyContactsContactEmails Emails
            {
                get
                {
                    return this.emailsField;
                }
                set
                {
                    this.emailsField = value;
                }
            }

            /// <remarks/>
            public string ContactId
            {
                get
                {
                    return this.contactIdField;
                }
                set
                {
                    this.contactIdField = value;
                }
            }

            /// <remarks/>
            public string ExternalId
            {
                get
                {
                    return this.externalIdField;
                }
                set
                {
                    this.externalIdField = value;
                }
            }

            /// <remarks/>
            public string FirstName
            {
                get
                {
                    return this.firstNameField;
                }
                set
                {
                    this.firstNameField = value;
                }
            }

            /// <remarks/>
            public object MiddleName
            {
                get
                {
                    return this.middleNameField;
                }
                set
                {
                    this.middleNameField = value;
                }
            }

            /// <remarks/>
            public string LastName
            {
                get
                {
                    return this.lastNameField;
                }
                set
                {
                    this.lastNameField = value;
                }
            }

            /// <remarks/>
            public object Note
            {
                get
                {
                    return this.noteField;
                }
                set
                {
                    this.noteField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContactsContactPhones
        {
            private CompanyCollectionCompanyContactsContactPhonesPhone phoneField;

            /// <remarks/>
            public CompanyCollectionCompanyContactsContactPhonesPhone Phone
            {
                get
                {
                    return this.phoneField;
                }
                set
                {
                    this.phoneField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContactsContactPhonesPhone
        {
            private string contactMediumField;

            private string phoneidField;

            private string phoneNumberField;

            private string phoneExtensionField;

            /// <remarks/>
            public string ContactMedium
            {
                get
                {
                    return this.contactMediumField;
                }
                set
                {
                    this.contactMediumField = value;
                }
            }

            /// <remarks/>
            public string Phoneid
            {
                get
                {
                    return this.phoneidField;
                }
                set
                {
                    this.phoneidField = value;
                }
            }

            /// <remarks/>
            public string PhoneNumber
            {
                get
                {
                    return this.phoneNumberField;
                }
                set
                {
                    this.phoneNumberField = value;
                }
            }

            /// <remarks/>
            public string PhoneExtension
            {
                get
                {
                    return this.phoneExtensionField;
                }
                set
                {
                    this.phoneExtensionField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContactsContactEmails
        {
            private CompanyCollectionCompanyContactsContactEmailsEmail emailField;

            /// <remarks/>
            public CompanyCollectionCompanyContactsContactEmailsEmail Email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class CompanyCollectionCompanyContactsContactEmailsEmail
        {
            private string emailIdField;

            private string emailAddressField;

            private string contactMediumField;

            /// <remarks/>
            public string EmailId
            {
                get
                {
                    return this.emailIdField;
                }
                set
                {
                    this.emailIdField = value;
                }
            }

            /// <remarks/>
            public string EmailAddress
            {
                get
                {
                    return this.emailAddressField;
                }
                set
                {
                    this.emailAddressField = value;
                }
            }

            /// <remarks/>
            public string ContactMedium
            {
                get
                {
                    return this.contactMediumField;
                }
                set
                {
                    this.contactMediumField = value;
                }
            }
        }
    }
}