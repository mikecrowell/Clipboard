using FieldTool.ClipboardLookup.Models;
using FieldTool.Entity;

namespace FieldTool.ClipboardLookup.DAL
{
    public class RetrofitRepository : BaseRepository<Retrofit, RetrofitDTO>
    {
        public RetrofitRepository()
            : base()
        {
        }

        public RetrofitRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected override System.Data.Entity.DbSet<Retrofit> Entities
        {
            get { return Context.Retrofits; }
        }

        protected override RetrofitDTO ConvertToDTO(Retrofit entity)
        {
            return new RetrofitDTO(entity);
        }
    }
}