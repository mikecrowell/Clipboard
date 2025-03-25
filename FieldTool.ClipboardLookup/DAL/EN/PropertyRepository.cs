using FieldTool.Bsi.Helpers;
using FieldTool.Bsi.Models;
using FieldTool.ClipboardLookup.Helpers;
using FieldTool.ClipboardLookup.Models.EN;
using FieldTool.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FieldTool.ClipboardLookup.DAL.EN
{
    public class PropertyRepository : BaseRepository<Building, PropertyInfo>
    {
        public PropertyRepository()
            : base()
        {
        }

        public PropertyRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<Building> Entities
        {
            get { return Context.Buildings; }
        }

        protected override PropertyInfo ConvertToDTO(Building entity)
        {
            PropertyInfo propertyInfo = new PropertyInfo(entity);

            using (var context = this.Context)
            {
                propertyInfo.BuildingTypeId = DataHelper.GetBuildingTypeId(context, entity.BuildingCategory, entity.BuildingType);
            }

            foreach (Building building in Context.Buildings.Where(x => x.BuildingBsid == entity.BuildingBsid).ToList())
            {
                propertyInfo.Projects.Add(new ProjectInfo(building));
            }

            return propertyInfo;
        }

        protected override void BackfillData(IEnumerable<Building> entities)
        {
            IEnumerable<Building> buildingsWithMissingData = entities.Where(x => MissingData(x));
            foreach (Building building in buildingsWithMissingData)
            {
                // get data from BSI service
                BsiService bsiService = new BsiService(ConfigurationManager.AppSettings);
                ApiAccount apiAccount = bsiService.FindAccountForBackfill(new Bsi.Models.BsiBackfillTerms()
                {
                    AccountId = building.AuditProject.CompanyBsid,
                    AuditId = building.AuditProjectBsid,
                    ElectricAccountNumber = building.AuditProject.Company.ElectricAccountNumber,
                    GasAccountNumber = building.AuditProject.Company.GasAccountNumber
                });

                // save missing data
                using (IClipBoardUpload context = this.Context)
                {
                    Company company = context.Companies.Single(x => x.CompanyBsid == building.AuditProject.CompanyBsid);
                    if (string.IsNullOrWhiteSpace(company.ElectricRateCode))
                    {
                        company.ElectricRateCode = apiAccount.ElectricRateCode;
                    }
                    if (string.IsNullOrWhiteSpace(company.GasRateCode))
                    {
                        company.GasRateCode = apiAccount.GasRateCode;
                    }
                    context.SaveChanges();
                }

                // set field values on collection objects
                building.AuditProject.Company.ElectricRateCode = apiAccount.ElectricRateCode;
                building.AuditProject.Company.GasRateCode = apiAccount.GasRateCode;
            }
        }

        private bool MissingData(Building building)
        {
            return
                // has electric account number but no electric rate code
                (!string.IsNullOrWhiteSpace(building.AuditProject.Company.ElectricAccountNumber) && string.IsNullOrWhiteSpace(building.AuditProject.Company.ElectricRateCode)) ||
                // has gas account number but no gas rate code
                (!string.IsNullOrWhiteSpace(building.AuditProject.Company.GasAccountNumber) && string.IsNullOrWhiteSpace(building.AuditProject.Company.GasRateCode));
        }
    }
}