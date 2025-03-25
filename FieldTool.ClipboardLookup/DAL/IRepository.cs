using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL
{
    public interface IRepository<TEntity, TEntityDTO>
    {
        IClipBoardUpload Context { get; }

        Task<IEnumerable<TEntityDTO>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<IEnumerable<TEntity>> GetAllEntities(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntityDTO> GetSingle(Expression<Func<TEntity, bool>> filter);

        Task<TEntityDTO> GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntityDTO> FindAsync(string key);
    }
}