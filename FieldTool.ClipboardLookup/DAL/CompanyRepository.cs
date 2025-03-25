using FieldTool.Bsi.Helpers;
using FieldTool.ClipboardLookup.Models;
using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL
{
    public class CompanyRepository : BaseRepository<Company, CompanyDTO>
    {
        public CompanyRepository()
            : base()
        {
        }

        public CompanyRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<Company> Entities
        {
            get { return Context.Companies; }
        }

        protected override CompanyDTO ConvertToDTO(Company entity)
        {
            return new CompanyDTO(entity);
        }

        public async Task SaveEmailRequestLog(string companyID, bool emailSentSuccessfully, string toAddress, string requestedBy)
        {
            using (var context = this.Context)
            {
                context.EmailRequestLogs.Add(new EmailRequestLog()
                {
                    DateTime = DateTime.Now,
                    CompanyBsid = companyID,
                    EmailSent = emailSentSuccessfully,
                    ToAddress = toAddress,
                    RequestedBy = requestedBy
                });

                await context.SaveChangesAsync();
            }
        }

        public string GetCompanyXml(string auditId)
        {
            using (var context = this.Context)
            {
                EntityCRUDForAudit entityCrud = new EntityCRUDForAudit(context);
                string data = entityCrud.LoadAuditFromLastBackup(auditId);
                // this will either be a URI to a datafile or a string that is the serialized collection
                FieldTool.DAL.DTO.DIandCompanyDTO.CompanyCollection companyCollection = new FieldTool.DAL.DTO.DIandCompanyDTO.CompanyCollection();
                List<FieldTool.DAL.DTO.CompanyDTO> l = new List<FieldTool.DAL.DTO.CompanyDTO>();

                Uri uri = default(Uri);
                if (Uri.TryCreate(data, UriKind.Absolute, out uri))
                {
                    l = new HttpRetriever().RetrieveDataXml<List<FieldTool.DAL.DTO.CompanyDTO>>(uri.AbsoluteUri);
                }
                else
                {
                    l = new DataParser().RetrieveAs<List<FieldTool.DAL.DTO.CompanyDTO>>(data);
                }

                foreach (var c in l)
                {
                    companyCollection.ItemsCompany.Add(c);
                }

                return new DataParser().SerializeToString<FieldTool.DAL.DTO.DIandCompanyDTO.CompanyCollection>(companyCollection);
            }
        }
    }
}