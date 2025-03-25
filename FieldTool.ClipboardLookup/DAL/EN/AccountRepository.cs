using FieldTool.ClipboardLookup.Models.EN;
using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.DAL.EN
{
    public class AccountRepository : BaseRepository<Company, AccountInfo>
    {
        public AccountRepository()
            : base()
        {
        }

        public AccountRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<Company> Entities
        {
            get { return Context.Companies; }
        }

        protected override AccountInfo ConvertToDTO(Company entity)
        {
            return new AccountInfo(entity);
        }
    }
}