using FieldTool.Constants;
using FieldTool.Constants.Helpers;
using FieldTool.Constants.Lookup;
using FieldTool.DAL.DataProvider;
using FieldTool.DAL.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FieldTool.Entity
{
    public class EntityCRUDForDI : Contextual, FieldTool.Entity.IEntityCRUDForDI
    {
        public EntityCRUDForDI() : base()
        {
        }

        public EntityCRUDForDI(string connectionString) : base(connectionString)
        {
        }

        public EntityCRUDForDI(IClipBoardUpload context) : base(context)
        {
        }

        // dis, externalId, uploadReturn.BackupUri.AbsoluteUri, uploadedBy, false
        public void Save(List<DIDTO> dis, string externalId, Uri uploadedUrl, string userName, bool isDeleted = false)
        {
            using (var context = this.Context)
            {
                var xmlSerializer = new XmlSerializer(typeof(DIDTO));
                if (dis.Count() < 1)
                {
                    throw new Exception("There is no DI object to save");
                }
                foreach (DIDTO diDto in dis)
                {
                    if (diDto.ProjectInfos.Items.Count() < 1)
                    {
                        throw new Exception("There can only be 1 ProjectInfo in a DI object, but there were " + diDto.ProjectInfos.Items.Count);
                    }
                    ProjectInfoDTO projectInfo = diDto.ProjectInfos.Items[0];
                    if (diDto.Accounts.Items.Count() < 1)
                    {
                        throw new Exception("There can only be 1 Account in a DI object, but there were " + diDto.Accounts.Items.Count);
                    }
                    AccountDTO account = diDto.Accounts.Items[0];
                    if (uploadedUrl == default(Uri) || !uploadedUrl.IsAbsoluteUri)
                    {
                        throw new Exception("The uploadedUrl must be a valid uri, but was " + uploadedUrl);
                    }
                    if (projectInfo.ProjectStatus == AuditStatus.COMPLETE || isDeleted)
                    {
                        // step 1: save the raw data as an XML
                        var raw = new DiUploadBackup();
                        raw.UploadedDate = DateTime.Now;
                        raw.UploadedBy = userName;
                        raw.DiDataUrl = uploadedUrl.AbsoluteUri;
                        raw.ExternalId = externalId;
                        // save the initial data
                        try
                        {
                            context.DiUploadBackups.Add(raw);
                            context.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            throw new Exception(JsonConvert.SerializeObject(ex.EntityValidationErrors));
                        }
                        // blanket exception catch and report
                        try
                        {
                            // step 2: denormalize the data into referential db
                            if (isDeleted)
                            {
                                raw.IsDeletedDi = true;
                            }
                            else
                            {
                                DiReport o = SaveDi(context, diDto);
                                context.SaveChanges();

                                // now link them
                                raw.DiId = diDto.DIId;
                                raw.DiAccountBsid = o.DiAccount.DiAccountBsid;
                                raw.DiProjectInfoId = o.DiProjectInfo.DiProjectInfoId;
                            }
                            context.SaveChanges();
                        }
                        // or catch specific errors as to why they failed
                        catch (Exception ex)
                        {
                            var cause = ExceptionHelper.Innermost(ex);
                            object validations = null;
                            string message = null;
                            string errorObject = null;

                            if (ex.GetType() == typeof(DbEntityValidationException))
                            {
                                validations = ((DbEntityValidationException)ex).EntityValidationErrors
                                    .Select(x => new
                                    {
                                        Class = x.Entry.Entity.GetType().Name,
                                        Errors = x.ValidationErrors.Select(y => String.Format("{0} : {1}", y.PropertyName, y.ErrorMessage)).ToList()
                                    }).ToList();
                                message = JsonConvert.SerializeObject(validations);
                                errorObject = JsonConvert.SerializeObject(validations);
                            }
                            else
                            {
                                message = cause.Message;
                                errorObject = JsonConvert.SerializeObject(cause);
                            }
                            // Error only context
                            using (var errorOnlyContext = this.Context)
                            {
                                // should already exist
                                var rawMatches = errorOnlyContext.DiUploadBackups.Where(x => x.Id == raw.Id && x.ExternalId == externalId && x.UploadedBy == userName);
                                if (rawMatches.Count() > 0)
                                {
                                    var rawToMatch = rawMatches.OrderByDescending(x => x.UploadedDate).First();
                                    rawToMatch.ErrorMessage = message;
                                    rawToMatch.ErrorObject = errorObject;
                                    errorOnlyContext.SaveChanges();
                                    // now throw the error back out
                                    throw new Exception(rawToMatch.ErrorMessage);
                                }
                                else
                                {
                                    throw new Exception("Missing DiUploadBackups to update");
                                }
                            }
                            throw cause;
                        }
                    }
                }
            }
        }

        private string GetDIProgramId(DIDTO diDto)
        {
            foreach (var project in diDto.ProjectInfos.Items)
            {
                if (!string.IsNullOrWhiteSpace(project.ProgramID))
                {
                    return project.ProgramID;
                }
            }

            throw new Exception(string.Format("ProgramID not found on DI: [DIID: {0}].", diDto.DIId));
        }

        private DiReport SaveDi(IClipBoardUpload context, DIDTO dto)
        {
            /*
             * Clipboard has a strucure for DIs that loooks like this:
             * DI
             *  ->  L<Account>
             *  ->  L<ProjectInfo>
             *  ->  L<Contact>
             *  ->  L<Retrofit>
             *  ->  L<Answer>
             *  ->  L<DiGasHistory>
             *  ->  L<DiElectricHistory>
             *
             * This gets massaged into a DB structure like this:
             *
             * (DI) virtual, not actually save in a table
             *  -> (1)DiAccount
             *      -> L<DiContact>
             *      -> L<DiGasHistory>
             *      -> L<DiElectricHistory>
             *      -> L<DiContact>
             *      -> (1)DiProject
             *          -> L<DiRetrofit>
             *          -> L<DiAnswer>
             *          -> (1)DiInkSignature
             *
             *
             * Further, there should only be 1 ProjectInfo and 1 Account in a DI project, so it is designed to explode if this is not true.
             * Also, there should only be 1 InkSignature per Project (so essentially, only 1 per DI)
             * */

            DiReport report = new DiReport();

            // DTO objects
            AccountDTO aDto = dto.Accounts.Items[0];
            ProjectInfoDTO pDto = dto.ProjectInfos.Items[0];
            // DB objects
            DiAccount a = SaveDiAccount(context, aDto);
            DiProjectInfo p = SaveDiProjectInfo(context, pDto, a);

            foreach (DIElectricHistoryDTO historyDto in dto.ElectricHistories.Items)
            {
                SaveDiElectricHistory(context, historyDto, a);
            }

            foreach (DIGasHistoryDTO historyDto in dto.GasHistories.Items)
            {
                SaveDiGasHistory(context, historyDto, a);
            }

            foreach (ContactDTO contactDto in dto.Contacts.Items)
            {
                SaveDiContact(context, contactDto, a);
            }

            foreach (RetrofitDTO retrofitDto in dto.Retrofits.Items)
            {
                SaveDiRetrofit(context, retrofitDto, p);
            }

            foreach (AnswerDTO answerDto in dto.Answers.Items)
            {
                SaveDiAnswer(context, answerDto, p);
            }

            foreach (AttachmentDTO attachmentDto in dto.Attachments.Items)
            {
                SaveDiAttachment(context, attachmentDto, p);
            }

            // should have only had 1 ProjectInfo and 1 Account
            report.DiAccount = a;
            report.DiProjectInfo = p;

            return report;
        }

        private DiAccount SaveDiAccount(IClipBoardUpload context, AccountDTO dto)
        {
            // find all
            var matches = context.DiAccounts.Where(x => x.DiAccountBsid == dto.Id).ToList();
            var o = new DiAccount();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiAccountBsid = dto.Id;
                // add the rest
                var postalCode = new ParsedPostalCode(dto.PostalCode, dto.PostalCodeExtension);
                o.Name = dto.Name;
                o.Address1 = nullIfEmpty(dto.AddressLine1);
                o.Address2 = nullIfEmpty(dto.AddressLine2);
                o.Address3 = nullIfEmpty(dto.AddressLine3);
                o.City = nullIfEmpty(dto.City);
                o.State = StateCodeLookup.Find(dto.State);
                o.Zip = nullIfEmpty(postalCode.Zip);
                o.ZipExt = nullIfEmpty(postalCode.Ext);
                o.ElectricAccountNumber = nullIfEmpty(dto.ElectricAccountNumber);
                o.ElectricRateCode = nullIfEmpty(dto.ElectricRateCode);
                o.GasAccountNumber = nullIfEmpty(dto.GasAccountNumber);
                o.GasRateCode = nullIfEmpty(dto.GasRateCode);
                o.RecordType = nullIfEmpty(dto.RecordType);
                o.EmailAddress = dto.Email;
                o.PhoneNumber = dto.Phone;

                if (o.Validate().Count > 0)
                {
                    throw new Exception(o.ValidationMessages());
                }

                context.DiAccounts.Add(o);
            }

            return o;
        }

        private DiElectricHistory SaveDiElectricHistory(IClipBoardUpload context, DIElectricHistoryDTO dto, DiAccount a)
        {
            // find all
            var matches = context.DiElectricHistories.Where(x => x.ElectricHistoryReadDate == dto.ReadDate.Date && x.DiAccountBsid == a.DiAccountBsid).ToList();
            var o = new DiElectricHistory();

            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                o.ElectricHistoryReadDate = dto.ReadDate.Date;
                o.DiAccountBsid = a.DiAccountBsid;
            }
            // add common stuff
            o.BillDays = dto.BillDays;
            o.CoolDays = dto.CoolDays;
            o.HeatDays = dto.HeatDays;
            o.TotalKwh = dto.TotalKwh;
            o.OffPeakKwh = dto.OffPeakKwh;
            o.OnPeakKwh = dto.OnPeakKwh;
            o.BilledKw = dto.BilledKw;
            o.MaximumCustomerKw = dto.MaximumCustomerKw;
            o.TotalBill = dto.TotalBill;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiElectricHistories.Add(o);
            }

            return o;
        }

        private DiGasHistory SaveDiGasHistory(IClipBoardUpload context, DIGasHistoryDTO dto, DiAccount a)
        {
            // find all
            var matches = context.DiGasHistories.Where(x => x.GasHistoryReadDate == dto.ReadDate.Date && x.DiAccountBsid == a.DiAccountBsid).ToList();
            var o = new DiGasHistory();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.GasHistoryReadDate = dto.ReadDate.Date;
                o.DiAccountBsid = a.DiAccountBsid;
            }
            // add common stuff
            o.Therms = dto.Therms;
            o.TotalBill = dto.TotalBill;
            o.CoolDegreeDays = dto.CoolDegreeDays;
            o.HeatDegreeDays = dto.HeatDegreeDays;
            o.BillingDays = dto.BillingDays;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiGasHistories.Add(o);
            }

            return o;
        }

        private DiContact SaveDiContact(IClipBoardUpload context, ContactDTO dto, DiAccount c)
        {
            // find all
            var matches = context.DiContacts.Where(x => x.DiContactGuid == dto.Id).ToList();
            var o = new DiContact();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiContactGuid = dto.Id;
                o.DiAccountBsid = c.DiAccountBsid;
            }

            // add common stuff
            o.FirstName = dto.FirstName;
            o.LastName = dto.LastName;
            o.MiddleName = nullIfEmpty(dto.MiddleName);
            o.Note = nullIfEmpty(dto.Note);
            o.JobRole = nullIfEmpty(dto.JobRole);

            if (dto.Emails != null && dto.Emails.Items != null && dto.Emails.Items.Count() > 0)
            {
                o.EmailAddress = dto.Emails.Items.Count > 0 ? dto.Emails.Items.First().EmailAddress : null;
            }

            if (dto.Phones != null && dto.Phones.Items != null && dto.Phones.Items.Count() > 0)
            {
                o.PhoneNumber = dto.Phones.Items.Count > 0 ? dto.Phones.Items.First().PhoneNumber : null;
            }

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiContacts.Add(o);
            }

            return o;
        }

        private DiProjectInfo SaveDiProjectInfo(IClipBoardUpload context, ProjectInfoDTO dto, DiAccount a)
        {
            var audits = context.DiProjectInfoes.Where(x => x.DiProjectInfoId == dto.Id);
            DiProjectInfo o = default(DiProjectInfo);
            if (audits.Count() > 0)
            {
                // used for backfilling data from and audit
                o = audits.First();
            }
            else
            {
                // only when the audit didn't exist already
                o = new DiProjectInfo
                {
                    DiProjectInfoId = dto.ProjectInfoId,
                    DiAccountBsid = a.DiAccountBsid,
                    ExternalId = dto.ExternalId,
                    ProgramId = dto.ProgramID,
                    ProjectName = dto.ProjectName,
                    EnergyAdvisorName = dto.EnergyAdvisorName,
                    ActualStartTime = dto.ActualStartTimeStamp,
                    ScheduledStartTime = dto.ScheduleStartTime,
                    IsReportEmailSent = dto.IsReportEmailSent
                };

                if (!String.IsNullOrEmpty(dto.ProjectCompleteDate))
                {
                    o.ProjectCompleteDate = DateTime.Parse(dto.ProjectCompleteDate);
                }

                // for audits, if they are not correcct, blow up.
                if (o.Validate().Count > 0)
                {
                    throw new Exception(o.ValidationMessages());
                }

                context.DiProjectInfoes.Add(o);
            }

            if (dto.InkSecureSignatureData != default(InkSecureSignatureDataDTO))
            {
                var InkSecureSignatureData = SaveDiInkSecureSignatureData(context, dto.InkSecureSignatureData, o);
            }

            return o;
        }

        private DiInkSecureSignatureData SaveDiInkSecureSignatureData(IClipBoardUpload context, InkSecureSignatureDataDTO dto, DiProjectInfo p)
        {
            // find all
            var matches = context.DiInkSecureSignatureDatas.Where(x => x.DiProjectInfoId == p.DiProjectInfoId).ToList();
            var o = new DiInkSecureSignatureData();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiProjectInfoId = p.DiProjectInfoId;
            }

            // add common stuff
            o.AcquiredSignatureStartOn = dto.AcquiredSignatureStartOn;
            o.BiometricEncryptionCompletedOn = dto.BiometricEncryptionCompletedOn;
            o.BiometricEncryptionSubmittedOn = dto.BiometricEncryptionSubmittedOn;
            o.EncryptedBiometricData = dto.EncryptedBiometricData;
            o.HardwareInfo = dto.HardwareInfo;
            o.InkWashedSignature = dto.InkWashedSignature;
            o.SignerAcceptedOn = dto.SignerAcceptedOn;
            o.SignersName = dto.SignersName;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiInkSecureSignatureDatas.Add(o);
            }

            return o;
        }

        private DiRetrofit SaveDiRetrofit(IClipBoardUpload context, RetrofitDTO dto, DiProjectInfo p)
        {
            // find all
            var matches = context.DiRetrofits.Where(x => x.DiProjectInfoId == p.DiProjectInfoId && x.DiRetrofitGuid == dto.Id).ToList();
            var o = new DiRetrofit();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiRetrofitGuid = dto.Id;
                o.DiProjectInfoId = p.DiProjectInfoId;
            }

            // add common stuff
            o.ComponentBsid = dto.EligibleComponentId;
            o.ProgramId = nullIfEmpty(dto.ProgramID);
            o.Description = nullIfEmpty(dto.MeasureDescription);
            o.Quantity = Convert.ToDouble(dto.Quantity);
            o.Kwh = dto.KWh;
            o.KWhUnit = dto.KWhUnit;
            o.Therms = dto.Therms;
            o.ThermsUnit = dto.ThermsUnit;
            o.Water = dto.Water;
            o.Savings = dto.Savings;
            o.Space = nullIfEmpty(dto.Space);
            o.Incentive = dto.Incentive;
            o.IconPath = nullIfEmpty(dto.IconPath);
            o.IconFileName = dto.IconFileName;
            o.LineItem1 = dto.LineItem1;
            o.LineItem2 = dto.LineItem2;
            o.LineItem3 = dto.LineItem3;
            o.LineItem4 = dto.LineItem4;
            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiRetrofits.Add(o);
            }

            return o;
        }

        private DiAnswer SaveDiAnswer(IClipBoardUpload context, AnswerDTO dto, DiProjectInfo p)
        {
            // find all
            var matches = context.DiAnswers.Where(x => x.DiQuestionId == dto.Id && x.DiProjectInfoId == p.DiProjectInfoId).ToList();
            var o = new DiAnswer();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiQuestionId = dto.Id;
                o.DiProjectInfoId = p.DiProjectInfoId;
            }

            // add common stuff
            o.AnsweredOn = dto.AnsweredOn;
            o.Value = dto.Value;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiAnswers.Add(o);
            }

            return o;
        }

        private DiAttachment SaveDiAttachment(IClipBoardUpload context, AttachmentDTO dto, DiProjectInfo p)
        {
            // find all
            var matches = context.DiAttachments.Where(x => x.DiAttachmentId == dto.Id && x.DiProjectInfoId == p.DiProjectInfoId).ToList();
            var o = new DiAttachment();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.DiAttachmentId = dto.Id;
                o.DiProjectInfoId = p.DiProjectInfoId;
            }

            // add common stuff
            o.AttachmentFile = dto.AttachmentFile;
            o.Category = dto.Category;
            o.Comments = dto.Comments;
            o.Description = dto.Description;
            o.IncludeInReport = dto.IncludeInReport;
            o.IncludeOnCover = dto.IncludeOnCover;
            o.Name = dto.Name;
            o.RestoreUrl = dto.RestoreUrl;
            o.Type = dto.Type;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.DiAttachments.Add(o);
            }

            return o;
        }

        /* Helpers */

        private string nullIfEmpty(string val)
        {
            return String.IsNullOrEmpty(val) ? null : val;
        }

        private byte[] nullOrByteArrayFromFilePath(string fileWithPath)
        {
            if (nullIfEmpty(fileWithPath) == null || !File.Exists(fileWithPath))
            {
                return null;
            }
            return System.IO.File.ReadAllBytes(fileWithPath);
        }

        public DIDTO ParseAsDIDTO(string contents)
        {
            var data = default(DIDTO);
            byte[] byteArray = Encoding.UTF8.GetBytes(contents);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    XmlSerializer gen = new XmlSerializer(typeof(DIDTO));
                    data = (DIDTO)gen.Deserialize(reader);
                }
            }
            return data;
        }

        public string GetXmlTempDataFilePath()
        {
            return Path.Combine(XmlDataProvider.XmlReportDirectory,
                string.Format("temp_{0}_{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), XmlDataProvider.XmlDataFile));
        }

        protected class DiReport
        {
            public DiAccount DiAccount { get; set; }
            public DiProjectInfo DiProjectInfo { get; set; }
        }
    }
}