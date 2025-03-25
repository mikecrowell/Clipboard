using FieldTool.ClipboardLookup.DAL;
using FieldTool.Constants.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace FieldTool.ClipboardLookup.Controllers
{
    public abstract class BaseController<TEntity, TEntityDTO> : BaseLoggingController
    {
        protected IRepository<TEntity, TEntityDTO> Repository { get; private set; }

        public BaseController(IRepository<TEntity, TEntityDTO> repository, ILogger logger) : base(logger)
        {
            Repository = repository;
        }

        public async virtual Task<IHttpActionResult> GetById(string id)
        {
            TEntityDTO entity = await Repository.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        public async Task<IHttpActionResult> GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            TEntityDTO entity = await Repository.GetSingle(filter);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        public async Task<IHttpActionResult> GetFirst(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            TEntityDTO entity = await Repository.GetFirst(filter, orderBy);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        public async Task<IEnumerable<TEntityDTO>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return await Repository.GetAll(filter, orderBy);
        }

        public async Task<IEnumerable<T>> GetAllOfType<T>(Func<TEntity, T> conversionFunction, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IEnumerable<TEntity> entities = await Repository.GetAllEntities(filter, orderBy);
            List<T> result = new List<T>();
            foreach (TEntity entity in entities)
            {
                result.Add(conversionFunction(entity));
            }

            return result;
        }
    }
}