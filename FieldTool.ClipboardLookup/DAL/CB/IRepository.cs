using FieldTool.Constants.Models.CB;
using FieldTool.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FieldTool.ClipboardLookup.DAL.CB
{
    public interface ICbRepository<TEntity>
    {
        IClipBoardUpload Context { get; }

        Task<IEnumerable<BackupSelect>> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IQueryable<BackupSelect>> select = null);
    }
}