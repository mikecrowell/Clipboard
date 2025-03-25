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
    public class EntityCRUDForAudit : Contextual
    {
        public EntityCRUDForAudit() : base()
        {
        }

        public EntityCRUDForAudit(string connectionString) : base(connectionString)
        {
        }

        public EntityCRUDForAudit(IClipBoardUpload context) : base(context)
        {
        }

        public void Save(List<CompanyDTO> audits, string externalId, Uri backupUrl, string uploadedBy, bool isDeletedAudit)
        {
            using (var context = this.Context)
            {
                var xmlSerializer = new XmlSerializer(typeof(CompanyDTO));
                foreach (CompanyDTO companyDto in audits)
                {
                    if (companyDto.Audits.Items.Count() > 0 && companyDto.Audits.Items[0].AuditStatus == AuditStatus.COMPLETE)
                    {
                        // step 1: save the raw data as an XML
                        var raw = new AuditUploadBackup();
                        raw.UploadedDate = DateTime.Now;
                        raw.UploadedBy = uploadedBy;
                        raw.ExternalId = externalId ?? companyDto.Id ?? companyDto.CompanyId ?? companyDto.ExternalId;
                        raw.IsDeletedAudit = isDeletedAudit;
                        raw.AuditDataUrl = backupUrl.AbsoluteUri;

                        context.AuditUploadBackups.Add(raw);

                        context.SaveChanges();
                        // skip after deletion
                        if (raw.IsDeletedAudit)
                        {
                            continue;
                        }
                        // blanket exception catch and report
                        try
                        {
                            // step 2: denormalize the data into referential db
                            Company c = SaveCompany(context, companyDto);
                            context.SaveChanges();

                            // now link them
                            raw.CompanyBsid = c.CompanyBsid;
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
                                var rawMatches = errorOnlyContext.AuditUploadBackups.Where(x => x.Id == raw.Id && x.ExternalId == externalId && x.UploadedBy == uploadedBy);
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
                                    throw new Exception("Missing AuditUploadBackup to update");
                                }
                            }
                            throw cause;
                        }
                    }
                }
            }
        }

        private static string GetCompanyProgram(CompanyDTO companyDto)
        {
            if (!string.IsNullOrWhiteSpace(companyDto.Program))
            {
                return companyDto.Program;
            }

            if (companyDto.Audits.Items.Count > 0)
            {
                return companyDto.Audits.Items[0].ProgramId;
            }

            return string.Empty;
        }

        public void UpdateCompany(string companyBsid)
        {
            using (var context = this.Context)
            {
                var auditUploadBackup = context.AuditUploadBackups
                    .Where(x => x.CompanyBsid == companyBsid && string.IsNullOrEmpty(x.ErrorMessage))
                    .OrderByDescending(x => x.UploadedDate)
                    .FirstOrDefault();

                if (auditUploadBackup != null)
                {
                    SaveCompany(context, XmlDataProvider.GetCompanyFromXml(auditUploadBackup.AuditDataXml));
                    context.SaveChanges();
                }
                else
                {
                    // TODO: throw "not found" exception?
                }
            }
        }

        private Company SaveCompany(IClipBoardUpload context, CompanyDTO dto)
        {
            // first check for an existing company
            var id = dto.Id;
            var companies = context.Companies.Where(x => x.CompanyBsid == id);

            Company c = default(Company);
            if (companies.Count() == 0)
            {
                var postalCode = new ParsedPostalCode(dto.PostalCode, dto.PostalCodeExtension);
                c = new Company
                {
                    CompanyBsid = dto.Id,
                    CompanyName = dto.CompanyName,
                    Address1 = nullIfEmpty(dto.AddressLine1),
                    Address2 = nullIfEmpty(dto.AddressLine2),
                    Address3 = nullIfEmpty(dto.AddressLine3),
                    City = nullIfEmpty(dto.City),
                    State = StateCodeLookup.Find(dto.State),
                    Zip = nullIfEmpty(postalCode.Zip),
                    ZipExt = nullIfEmpty(postalCode.Ext),
                    ElectricAccountNumber = nullIfEmpty(dto.ElectricAccountNumber),
                    ElectricRateCode = nullIfEmpty(dto.ElectricRateCode),
                    GasAccountNumber = nullIfEmpty(dto.GasAccountNumber),
                    GasRateCode = nullIfEmpty(dto.GasRateCode),
                    RecordType = nullIfEmpty(dto.RecordType),
                    EmailAddress = dto.Email,
                    PhoneNumber = dto.Phone
                };

                // for company, die if we don't have valid data
                if (c.Validate().Count > 0)
                {
                    throw new Exception(c.ValidationMessages());
                }

                context.Companies.Add(c);
            }
            else
            {
                c = companies.First();
                c.EmailAddress = dto.Email;
            }

            foreach (ContactDTO contactDto in dto.Contacts.Items)
            {
                try
                {
                    var ct = SaveContact(context, contactDto, c);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // TODO: Log these errors are they are NOT something the user can effect and should have been valid in the first place
                }
            }

            //Save Audits
            foreach (AuditDTO auditDto in dto.Audits.Items)
            {
                // if an audit currently exists then skip it
                var a = SaveAudit(context, auditDto, c);
            }

            return c;
        }

        private Contact SaveContact(IClipBoardUpload context, ContactDTO dto, Company c)
        {
            // find all
            var matches = context.Contacts.Where(x => x.ContactGuid == dto.Id).ToList();
            var o = new Contact();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.ContactGuid = dto.Id;
                o.CompanyBsid = c.CompanyBsid;
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
                context.Contacts.Add(o);
            }

            return o;
        }

        private AuditProject SaveAudit(IClipBoardUpload context, AuditDTO dto, Company c)
        {
            var audits = context.AuditProjects.Where(x => x.AuditProjectBsid == dto.Id);
            AuditProject a = default(AuditProject);
            if (audits.Count() > 0)
            {
                // used for backfilling data from and audit
                a = audits.First();
            }
            else
            {
                // only when the audit didn't exist already
                a = new AuditProject
                {
                    AuditProjectBsid = dto.Id,
                    CompanyBsid = c.CompanyBsid,
                    ProgramId = dto.ProgramId,
                    AuditProjectName = dto.AuditName,
                    AuditProjectDescription = nullIfEmpty(dto.AuditDescription),
                    IsReportEmailSent = dto.IsReportEmailSent,
                    ScheduledStart = dto.ScheduleStartTimeStamp,
                    CompanyContact = dto.CompanyContact,
                    AuditStatus = dto.AuditStatus,
                    EnergyAdvisorName = dto.EnergyAdvisorName,
                    ClientAccountId = dto.ClientAccountId,
                    ElectricDisplayAs = dto.ElectricDisplayAs,
                    GasDisplayAs = dto.GasDisplayAs,
                    IsAdHocAudit = dto.IsAdHocAudit
                };

                if (!String.IsNullOrEmpty(dto.EndTimeStamp))
                {
                    a.ScheduledEnd = DateTime.Parse(dto.EndTimeStamp);
                }

                if (dto.AuditCompleteDate.HasValue)
                {
                    a.AuditCompleteDate = dto.AuditCompleteDate.Value;
                }
                // for audits, if they are not correcct, blow up.
                if (a.Validate().Count > 0)
                {
                    throw new Exception(a.ValidationMessages());
                }

                context.AuditProjects.Add(a);
            }
            //save buildings
            foreach (BuildingDTO buildingDto in dto.Buildings.Items)
            {
                // explodes if the building was not valid
                Building b = SaveBuilding(context, buildingDto, a);

                foreach (RetrofitDTO retrofitDto in buildingDto.Retrofits.Items)
                {
                    a.Retrofits.Add(SaveRetrofits(context, retrofitDto, a, b));
                }
            }

            if (dto.InkSecureSignatureData != default(InkSecureSignatureDataDTO))
            {
                var InkSecureSignatureData = SaveInkSecureSignatureData(context, dto.InkSecureSignatureData, a);
            }

            return a;
        }

        private InkSecureSignatureData SaveInkSecureSignatureData(IClipBoardUpload context, InkSecureSignatureDataDTO dto, AuditProject a)
        {
            // find all
            var matches = context.InkSecureSignatureData.Where(x => x.AuditProjectBsid == a.AuditProjectBsid).ToList();
            var o = new InkSecureSignatureData();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.AuditProjectBsid = a.AuditProjectBsid;
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
                context.InkSecureSignatureData.Add(o);
            }

            return o;
        }

        private Building SaveBuilding(IClipBoardUpload context, BuildingDTO dto, AuditProject a)
        {
            // find all
            var matches = context.Buildings.Where(x => x.BuildingGuid == dto.Id).ToList();
            var o = new Building();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                var postalCode = new ParsedPostalCode(dto.PostalCode, dto.PostalCodeExtension);
                o.BuildingGuid = dto.Id;
                o.AuditProjectBsid = a.AuditProjectBsid;
                o.BuildingBsid = dto.BuildingBSID ?? a.CompanyBsid;

                // add common stuff

                o.BuildingName = dto.BuildingName;
                o.BuildingCategory = nullIfEmpty(dto.BuildingCategory);
                o.BuildingType = nullIfEmpty(dto.BuildingType);
                o.BuildingProjectId = !GuidGenerator.IsGuid(dto.Id) ? dto.BuildingProjectId : null;
                o.Address1 = nullIfEmpty(dto.AddressLine1);
                o.Address2 = nullIfEmpty(dto.AddressLine2);
                o.Address3 = nullIfEmpty(dto.AddressLine3);
                o.City = nullIfEmpty(dto.City);
                o.State = StateCodeLookup.Find(dto.State);
                o.Zip = nullIfEmpty(postalCode.Zip);
                o.ZipExt = nullIfEmpty(postalCode.Ext);
                o.OccupantCount = dto.NumberOfOccupants;
                o.FloorsAbove = dto.NumberOfFloorsAboveGround;
                o.FloorsBelow = dto.NumberOfFloorsBelowGround;
                o.FloorAreaGross = Convert.ToDouble(dto.GrossFloorArea);
                o.FloorAreaOccupied = Convert.ToDouble(dto.OccupiedFloorArea);
                o.BuildingHoursEquivalent = nullIfEmpty(dto.BuildingHoursEquivalent);
                o.RateCode = nullIfEmpty(dto.RateCode);
                o.ZipZone = dto.ZipZone;
                o.NumberOfUnits = dto.NumberOfUnits;
                o.UnitNumbering = dto.UnitNumbering;
            };

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.Buildings.Add(o);
            }

            //save gas history
            foreach (GasHistoryDTO gashistoryDto in dto.GasHistories.Items)
            {
                try
                {
                    SaveBuildingGasHistory(context, gashistoryDto, o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // TODO: Log these errors are they are NOT something the user can effect and should have been valid in the first place
                }
            }

            //Save Electric History
            foreach (ElectricHistoryDTO electrichistoryDto in dto.ElectricHistories.Items)
            {
                try
                {
                    SaveBuildingElectricHistory(context, electrichistoryDto, o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // TODO: Log these errors are they are NOT something the user can effect and should have been valid in the first place
                }
            }

            //save building spaces
            foreach (BuildingSpaceDTO buildingspaceDto in dto.BuildingSpaces.Items)
            {
                SaveBuildingSpaces(context, buildingspaceDto, o);
            }

            //save attachments
            //foreach (AttachmentDTO buildingattachmentDto in dto.Attachments.Items) {
            //    SaveBuildingAttachments(context, buildingattachmentDto, o);
            //}

            //EquipmentMaster
            foreach (EquipmentMasterDTO equipmasterDto in dto.Equipments.Items)
            {
                SaveBuildingEquipment(context, equipmasterDto, o);
            }

            ////recommendations
            foreach (RecommendationDTO recommendationDto in dto.Recommendations.Items)
            {
                SaveRecommendations(context, recommendationDto, a, o);
            }

            //Multi-Family
            if (dto.MultiFamily != default(MultiFamilyDTO))
            {
                SaveMultiFamily(context, dto.MultiFamily, o);
            }
            return o;
        }

        private Recommendation SaveRecommendations(IClipBoardUpload context, RecommendationDTO dto, AuditProject a, Building ab)
        {
            // find all
            var matches = context.Recommendations.Where(x => x.InstanceId == dto.InstanceId).ToList();
            var o = new Recommendation();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.RecommendationGuid = dto.Id;
                o.InstanceId = dto.InstanceId;
                o.BuildingGuid = ab.BuildingGuid;
                o.AuditProjectBsid = a.AuditProjectBsid;
            }

            // add common stuff
            o.RecommendationName = nullIfEmpty(dto.RecommendationName);
            o.RecommendationDescription = nullIfEmpty(dto.RecommendationDescription);
            o.IncludedInReport = dto.IncludeInReport;
            o.ReportRank = dto.ReportRank;
            o.IsOccupancySensor = dto.IsOccupancySensor;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.Recommendations.Add(o);
            }

            foreach (RecommendationOptionDTO recEquipDto in dto.RecommendationOptions.Items)
            {
                var ro = SaveRecommendationOptions(context, recEquipDto, o);
            }

            return o;
        }

        private RecommendationOption SaveRecommendationOptions(IClipBoardUpload context, RecommendationOptionDTO dto, Recommendation br)
        {
            // find all
            var matches = context.RecommendationOptions.Where(x => x.RecommendationOptionGuid == dto.Id).ToList();
            var o = new RecommendationOption();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.RecommendationOptionGuid = dto.Id;
                o.RecommendationGuid = br.InstanceId; // this is NOT pointing to the RecommendationId because they are not unique.
            }

            // add common stuff
            o.RecommendationName = nullIfEmpty(dto.RecommendationName);
            o.OptionDescription = nullIfEmpty(dto.RecommendationOptionDescription);
            o.OptionId = nullIfEmpty(dto.RecommendationOptionId);
            o.OptionName = nullIfEmpty(dto.RecommendationOptionName);
            o.Cop = dto.Cop;
            o.ElectricDisplayAs = nullIfEmpty(dto.ElectricDisplayAs);
            o.EnergyFactor = dto.EnergyFactor;
            o.EnergySource = nullIfEmpty(dto.EnergySource);
            o.GasDisplayAs = nullIfEmpty(dto.GasDisplayAs);
            o.HeatingCoolingHours = dto.HeatingCoolingHours;
            o.IsOccupancySensor = dto.IsOccupancySensor;
            o.IsZeroSavings = dto.IsZeroSavings;
            o.KwhSaved = dto.KwhSaved;
            o.KwhSavedWithRateCode = dto.KwhSavedWithRateCode;
            o.OccupancySensorDisplayAs = nullIfEmpty(dto.OccupancySensorDisplayAs);
            // This is saved under the assumption that you don't actually care anout the OriginalEquipment
            o.OriginalEquipmentMfid = (dto.OriginalEquipment != default(EquipmentMasterDTO)) ? dto.OriginalEquipment.Id : null;

            o.Quantity = dto.Quantity;
            o.RebateBsid = nullIfEmpty(dto.RebateBensightID);
            o.RebateCalculationEquation = nullIfEmpty(dto.RebateCalculationEquation);
            o.RebateClientRefId = nullIfEmpty(dto.RebateClientRefID);
            o.RebateValue = dto.Rebate;
            o.Savings = dto.Saving;
            o.SavingsCalculationEquationSaving = nullIfEmpty(dto.SavingsCalculationEquation);
            o.ThermsSaved = dto.ThermsSaved;
            o.ThermsSavedWithRateCode = dto.ThermsSavedWithRateCode;
            o.TypeOfEnergy = dto.TypeOfEnergy.ToString();

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.RecommendationOptions.Add(o);
            }

            // OriginalEquipment
            if (dto.RecommendationEquipment != null)
            {
                var roReplace = SaveRecommendationOptionEquipment(context, dto.RecommendationEquipment, o);
            }

            return o;
        }

        private RecommendationOptionEquipment SaveRecommendationOptionEquipment(IClipBoardUpload context, RecommendationEquipmentDTO dto, RecommendationOption ro)
        {
            var cid = String.Format("{0}:{1}", ro.RecommendationOptionGuid, dto.Id);
            // find all
            var matches = context.RecommendationOptionEquipments.Where(x => x.RecommendationOptionEquipmentCid == cid).ToList();
            var o = new RecommendationOptionEquipment();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.RecommendationOptionEquipmentCid = cid;
                o.RecommendationOptionEquipmentMfid = dto.Id;
                o.RecommendationOptionGuid = ro.RecommendationOptionGuid;
            }

            // add common stuff
            o.ActualWattage = nullIfEmpty(dto.ActualWattage);
            o.AnnualHours = dto.AnnualHours;
            o.Efficiency = dto.Efficiency;
            o.EfficiencyUnit = nullIfEmpty(dto.EfficiencyUnit);
            o.EquipmentDescription = nullIfEmpty(dto.EquipmentDescription);
            o.EquipmentName = nullIfEmpty(dto.EquipmentName);
            o.Quantity = dto.Quantity;
            o.RecommendationEquipmentId = nullIfEmpty(dto.RecommendationEquipmentId);
            o.Size = dto.Size;
            o.SizeUnit = nullIfEmpty(dto.SizeUnit);
            o.SystemType = (dto.SystemType != null ? dto.SystemType.Id : -1).ToString();
            o.ThermalEfficiency = dto.ThermalEfficiency;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.RecommendationOptionEquipments.Add(o);
            }

            return o;
        }

        private BuildingEquipment SaveBuildingEquipment(IClipBoardUpload context, EquipmentMasterDTO dto, Building ab)
        {
            var cid = String.Format("{0}:{1}", ab.BuildingGuid, dto.Id);
            // find all
            var matches = context.BuildingEquipments.Where(x => x.BuildingEquipmentCid == cid).ToList();
            var o = new BuildingEquipment();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.BuildingEquipmentCid = cid; // Calculated for DB integrity
                o.BuildingEquipmentMfid = dto.Id;
                o.BuildingGuid = ab.BuildingGuid;
            }

            // add common stuff
            o.BallastManufacturerModel = nullIfEmpty(dto.BallastManufacturerModel);
            o.BallastManufacturerName = nullIfEmpty(dto.BallastManufacturerName);
            o.BurnerControlType = nullIfEmpty(dto.ControlType);
            o.BurnerManufacturerModel = nullIfEmpty(dto.BurnerManufacturerModel);
            o.BurnerManufacturerName = nullIfEmpty(dto.BurnerManufacturerName);
            o.BurnerType = nullIfEmpty(dto.BurnerType);
            o.Capacity = dto.Capacity;
            //o.CapacityUnit = nullIfEmpty(dto.CapacityUnit);
            //o.CompressorType = nullIfEmpty(dto.CompressorType);
            o.ControlSubType = nullIfEmpty(dto.ControlSubType);
            //o.EfficiencyDescription = nullIfEmpty(dto.EfficiencyDescription);
            o.EfficiencyAmt = dto.Efficiency;
            o.EquipmentName = nullIfEmpty(dto.EquipmentName);
            o.EquipmentDescription = nullIfEmpty(dto.EquipmentDescription);
            o.LightHeatCoolingTypeDescription = nullIfEmpty(dto.LightingHeatingCoolingType);
            o.ManufacturerModel = nullIfEmpty(dto.ManufacturerModel);
            o.ManufacturerName = nullIfEmpty(dto.ManufacturerName);
            o.PartLoadEfficiency = dto.PartLoadEfficiency;
            o.PartLoadEfficiencyUnit = dto.PartLoadEfficiencyUnit;
            o.Quantity = dto.Quantity;
            o.Size = dto.Size;
            o.SizeUnit = nullIfEmpty(dto.SizeUnit);
            o.SupplementalControlType = nullIfEmpty(dto.SupplementalControlType);
            o.SystemType = dto.SystemType.Id.ToString();
            o.SystemTypeDescription = nullIfEmpty(dto.SystemType.Description);
            o.SystemControlType = nullIfEmpty(dto.SystemControlType);
            o.ActualWattage = nullIfEmpty(dto.ActualWattage);
            o.ComponentId = nullIfEmpty(dto.ComponentId);
            o.ComponentName = nullIfEmpty(dto.ComponentName);
            o.CompressorType = nullIfEmpty(dto.CompressorType);
            o.ControlFactor = dto.ControlFactor;
            o.ControlQuantity = dto.ControlQuantity;
            o.EfficiencyUnit = nullIfEmpty(dto.EfficiencyUnit);
            o.FilterString = nullIfEmpty(dto.FilterString);
            o.IsOccupencySensor = dto.IsOccupancySensor;
            o.PresetSchedule = nullIfEmpty(dto.PresetSchedule);
            o.TypeOfEnergy = dto.TypeOfEnergy.ToString();
            o.WaterCoolingControlType = dto.WaterCoolingControlType;
            o.RestoreUrl = dto.RestoreUrl;
            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.BuildingEquipments.Add(o);
            }

            SaveBuildingEquipmentSchedule(context, dto.Schedule, o);

            return o;
        }

        private BuildingGasHistory SaveBuildingGasHistory(IClipBoardUpload context, GasHistoryDTO dto, Building ab)
        {
            // find all
            var matches = context.BuildingGasHistories.Where(x => x.GasHistoryReadDate == dto.ReadDate.Date && x.BuildingGuid == ab.BuildingGuid).ToList();
            var o = new BuildingGasHistory();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.GasHistoryReadDate = dto.ReadDate.Date;
                o.BuildingGuid = ab.BuildingGuid;
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
                context.BuildingGasHistories.Add(o);
            }

            return o;
        }

        private BuildingEquipmentSchedule SaveBuildingEquipmentSchedule(IClipBoardUpload context, ScheduleDTO dto, BuildingEquipment be)
        {
            var cid = String.Format("{0}:{1}", be.BuildingEquipmentCid, dto.Id);
            // find all
            var matches = context.BuildingEquipmentSchedules.Where(x => x.BuildingEquipmentScheduleCid == cid).ToList();
            var o = new BuildingEquipmentSchedule();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.BuildingEquipmentScheduleCid = cid;
                o.BuildingEquipmentCid = be.BuildingEquipmentCid;
                o.BuildingEquipmentScheduleGuid = dto.Id;
            }

            // add common stuff
            o.ScheduleDescription = nullIfEmpty(dto.ScheduleDescription);
            o.ScheduleId = nullIfEmpty(dto.ScheduleId);
            o.ScheduleName = nullIfEmpty(dto.ScheduleName);
            o.ScheduleType = nullIfEmpty(dto.ScheduleType);
            o.NumberDaysPerWeek = dto.NumberOfDaysPerWeek;
            o.NumberHolidays = dto.NumberOfHolidays;
            o.NumberWeeksPerYear = dto.NumberOfDaysPerWeek;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.BuildingEquipmentSchedules.Add(o);
            }

            foreach (DurationDTO durationDto in dto.Durations.Items)
            {
                SaveBuildingEquipmentScheduleDuration(context, durationDto, o);
            }

            return o;
        }

        private BuildingEquipmentScheduleDuration SaveBuildingEquipmentScheduleDuration(IClipBoardUpload context, DurationDTO dto, BuildingEquipmentSchedule bes)
        {
            // find all
            var matches = context.BuildingEquipmentScheduleDurations.Where(x => x.BuildingEquipmentScheduleDurationGuid == dto.Id).ToList();
            var o = new BuildingEquipmentScheduleDuration();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.BuildingEquipmentScheduleDurationGuid = dto.Id;
                o.BuildingEquipmentScheduleCid = bes.BuildingEquipmentScheduleCid;
                o.BuildingEquipmentSchedule = bes;
            }

            // add common stuff
            o.StartTicks = dto.StartTicks;
            o.EndTicks = dto.EndTicks;
            o.DurationType = dto.DurationType;
            o.StartTime = dto.StartTime;
            o.EndTime = dto.EndTime;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.BuildingEquipmentScheduleDurations.Add(o);
            }

            return o;
        }

        //private BuildingAttachment SaveBuildingAttachments(IClipBoardUpload context, AttachmentDTO dto, Building ab) {
        //    // find all
        //    var matches = context.BuildingAttachments.Where(x => x.BuildingAttachmentGuid == dto.Id).ToList();
        //    var o = new BuildingAttachment();
        //    // get existing
        //    if (matches.Count > 0) {
        //        o = matches.First();
        //    }
        //    else {
        //        // add keys
        //        o.BuildingAttachmentGuid = dto.Id;
        //        o.BuildingGuid = ab.BuildingGuid;
        //    }

        //    // add common stuff
        //    o.Code = nullIfEmpty(dto.AttachmentType);
        //    o.Path = nullIfEmpty(dto.AttachmentPath);
        //    o.Name = nullIfEmpty(dto.AttachmentName);

        //    // check for errors
        //    if (o.Validate().Count > 0) {
        //        throw new Exception(o.ValidationMessages());
        //    }

        //    // add if new
        //    if (matches.Count == 0) {
        //        context.BuildingAttachments.Add(o);
        //    }

        //    return o;
        //}

        private BuildingElectricHistory SaveBuildingElectricHistory(IClipBoardUpload context, ElectricHistoryDTO dto, Building ab)
        {
            // find all
            var matches = context.BuildingElectricHistories.Where(x => x.ElectricHistoryReadDate == dto.ReadDate.Date && x.BuildingGuid == ab.BuildingGuid).ToList();
            var o = new BuildingElectricHistory();

            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                o.ElectricHistoryReadDate = dto.ReadDate.Date;
                o.BuildingGuid = ab.BuildingGuid;
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
                context.BuildingElectricHistories.Add(o);
            }

            return o;
        }

        private BuildingSpace SaveBuildingSpaces(IClipBoardUpload context, BuildingSpaceDTO dto, Building ab)
        {
            var cid = String.Format("{0}:{1}", ab.BuildingGuid, dto.Id);
            // find all
            var matches = context.BuildingSpaces.Where(x => x.BuildingSpaceCid == cid).ToList();
            var o = new BuildingSpace();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.BuildingSpaceCid = cid;
            }

            // add common stuff
            o.BuildingSpaceMfid = dto.Id;
            o.BuildingGuid = ab.BuildingGuid;
            o.SpaceCode = nullIfEmpty(dto.Space);
            o.SpaceLabel = nullIfEmpty(dto.SpaceLabel);
            o.Note = nullIfEmpty(dto.Note);

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.BuildingSpaces.Add(o);
            }

            return o;
        }

        private Retrofit SaveRetrofits(IClipBoardUpload context, RetrofitDTO dto, AuditProject a, Building b)
        {
            // find all
            var matches = context.Retrofits.Where(x => x.RetrofitGuid == dto.Id).ToList();
            var o = new Retrofit();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.RetrofitGuid = dto.Id;
                o.BuildingGuid = b.BuildingGuid;
                o.AuditProjectBsid = a.AuditProjectBsid;
            }

            // add common stuff
            o.ComponentBsid = dto.EligibleComponentId;
            o.ProgramId = nullIfEmpty(dto.ProgramID);
            o.Description = nullIfEmpty(dto.MeasureDescription);
            o.Quantity = Convert.ToInt32(dto.Quantity);
            o.Kwh = dto.KWh;
            o.Therms = dto.Therms;
            o.Water = dto.Water;
            o.Savings = dto.Savings;
            o.Space = nullIfEmpty(dto.Space);
            o.Incentive = dto.Incentive;
            o.IconPath = nullIfEmpty(dto.IconPath);

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.Retrofits.Add(o);
            }

            return o;
        }

        private MultiFamily SaveMultiFamily(IClipBoardUpload context, MultiFamilyDTO dto, Building b)
        {
            // find all
            var matches = context.MultiFamilies.Where(x => x.MultiFamilyId == dto.MultiFamilyId).ToList();
            var o = new MultiFamily();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.MultiFamilyId = dto.MultiFamilyId;
                o.BuildingGuid = b.BuildingGuid;
            }

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.MultiFamilies.Add(o);
            }

            foreach (BuildingUnitTypeDTO buildingUnitTypeDto in dto.BuildingUnitTypes.Items)
            {
                SaveBuildingUnitType(context, buildingUnitTypeDto, o);
            }

            return o;
        }

        private BuildingUnitType SaveBuildingUnitType(IClipBoardUpload context, BuildingUnitTypeDTO dto, MultiFamily mf)
        {
            // find all
            var matches = context.BuildingUnitTypes.Where(x => x.BuildingUnitTypeId == dto.BuildingUnitTypeId).ToList();
            var o = new BuildingUnitType();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.BuildingUnitTypeId = dto.BuildingUnitTypeId;
                o.MultiFamilyId = mf.MultiFamilyId;
            }

            // add common stuff
            o.AdditionalDetails = nullIfEmpty(dto.AdditionalDetails);
            o.CoolingType = nullIfEmpty(dto.CoolingType);
            o.DiOpportunity = nullIfEmpty(dto.DIOpportunity);
            o.HeatingType = nullIfEmpty(dto.HeatingType);
            o.Inventory = nullIfEmpty(dto.Inventory);
            o.Location = nullIfEmpty(dto.Location);
            o.NumBathrooms = dto.NumBathrooms;
            o.NumBedrooms = dto.NumBedrooms;
            o.SquareFeet = dto.SquareFeet;
            o.UnitType = nullIfEmpty(dto.UnitType);
            o.UnitTypeName = nullIfEmpty(dto.UnitTypeName);
            o.UnitTypeQuantity = dto.UnitTypeQty;

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.BuildingUnitTypes.Add(o);
            }

            foreach (RetrofitEstimateDTO retrofitEstimateDto in dto.RetrofitEstimates.Items)
            {
                SaveRetrofitEstimate(context, retrofitEstimateDto, o);
            }

            return o;
        }

        private RetrofitEstimate SaveRetrofitEstimate(IClipBoardUpload context, RetrofitEstimateDTO dto, BuildingUnitType bu)
        {
            // find all
            var matches = context.RetrofitEstimates.Where(x => x.RetrofitGuid == dto.Id).ToList();
            var o = new RetrofitEstimate();
            // get existing
            if (matches.Count > 0)
            {
                o = matches.First();
            }
            else
            {
                // add keys
                o.RetrofitGuid = dto.Id;
                o.BuildingUnitTypeId = bu.BuildingUnitTypeId;
            }

            // add common stuff
            o.ComponentBsid = dto.EligibleComponentId;
            o.ProgramId = nullIfEmpty(dto.ProgramID);
            o.Description = nullIfEmpty(dto.MeasureDescription);
            o.Quantity = Convert.ToInt32(dto.Quantity);
            o.Kwh = dto.KWh;
            o.Therms = dto.Therms;
            o.Water = dto.Water;
            o.Savings = dto.Savings;
            o.Space = nullIfEmpty(dto.Space);
            o.Incentive = dto.Incentive;
            o.IconPath = nullIfEmpty(dto.IconPath);

            // check for errors
            if (o.Validate().Count > 0)
            {
                throw new Exception(o.ValidationMessages());
            }

            // add if new
            if (matches.Count == 0)
            {
                context.RetrofitEstimates.Add(o);
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

        public CompanyDTO ParseAsCompanyDTO(string contents)
        {
            var data = default(CompanyDTO);
            byte[] byteArray = Encoding.UTF8.GetBytes(contents);
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                using (TextReader reader = new StreamReader(stream))
                {
                    XmlSerializer gen = new XmlSerializer(typeof(CompanyDTO));
                    data = (CompanyDTO)gen.Deserialize(reader);
                }
            }
            return data;
        }

        public string LoadAuditFromLastBackup(string auditId)
        {
            using (var context = this.Context)
            {
                var companyId = context.AuditProjects.Where(x => x.AuditProjectBsid == auditId).Select(x => x.CompanyBsid).FirstOrDefault();
                var matches = context.AuditUploadBackups.Where(x => x.CompanyBsid == companyId && String.IsNullOrEmpty(x.ErrorMessage)).OrderByDescending(x => x.UploadedDate);
                if (matches.Count() == 0)
                {
                    throw new Exception("LoadAuditFromLastBackup found no valid audit for " + auditId);
                }
                else
                {
                    var o = matches.First();
                    return String.IsNullOrEmpty(o.AuditDataUrl) ? o.AuditDataXml : o.AuditDataUrl;
                }
            }
        }
    }
}