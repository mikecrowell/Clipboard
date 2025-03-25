using FieldTool.Constants.Models.CB;
using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL.CB
{
    public abstract class BaseCbRepository<TEntity> : Contextual, ICbRepository<TEntity> where TEntity : class
    {
        public BaseCbRepository()
            : this(new ClipBoardUpload())
        {
        }

        public BaseCbRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected abstract DbSet<TEntity> Entities { get; }

        public async Task<IEnumerable<BackupSelect>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<BackupSelect>> select = null)
        {
            IQueryable<TEntity> query = BuildQuery(filter, orderBy);
            return await select(query).ToListAsync();
        }

        #region Helpers

        private IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }

        #endregion Helpers
    }
}