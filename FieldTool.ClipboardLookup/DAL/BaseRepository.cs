using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL
{
    public abstract class BaseRepository<TEntity, TEntityDTO> : Contextual, IRepository<TEntity, TEntityDTO> where TEntity : class
    {
        public BaseRepository()
            : this(new ClipBoardUpload())
        {
        }

        public BaseRepository(IClipBoardUpload context)
            : base(context)
        {
        }

        protected abstract DbSet<TEntity> Entities { get; }

        protected abstract TEntityDTO ConvertToDTO(TEntity entity);

        public async Task<IEnumerable<TEntityDTO>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = BuildQuery(filter, orderBy);
            IEnumerable<TEntity> entities = await GetAllEntities(filter, orderBy);
            BackfillData(entities);
            return ConvertToDTO(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = BuildQuery(filter, orderBy);
            IEnumerable<TEntity> entities = await query.ToListAsync();
            BackfillData(entities);
            return entities;
        }

        public async Task<TEntityDTO> GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            TEntity entity = await Entities.SingleOrDefaultAsync(filter);

            if (entity != null)
            {
                BackfillData(new List<TEntity>() { entity });
            }

            return entity != null ? ConvertToDTO(entity) : default(TEntityDTO);
        }

        public async Task<TEntityDTO> GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = BuildQuery(filter, orderBy);
            TEntity entity = await query.FirstOrDefaultAsync();

            if (entity != null)
            {
                BackfillData(new List<TEntity>() { entity });
            }

            return entity != null ? ConvertToDTO(entity) : default(TEntityDTO);
        }

        public async Task<TEntityDTO> FindAsync(string key)
        {
            TEntity entity = await Entities.FindAsync(key);

            if (entity != null)
            {
                BackfillData(new List<TEntity>() { entity });
            }

            return entity != null ? ConvertToDTO(entity) : default(TEntityDTO);
        }

        protected virtual void BackfillData(IEnumerable<TEntity> entities)
        {
            // by default, do nothing
            // override in child classes to perform specific tasks
        }

        #region Helpers

        protected IEnumerable<TEntityDTO> ConvertToDTO(IEnumerable<TEntity> entities)
        {
            List<TEntityDTO> result = new List<TEntityDTO>();

            foreach (TEntity entity in entities)
            {
                result.Add(ConvertToDTO(entity));
            }

            return result;
        }

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