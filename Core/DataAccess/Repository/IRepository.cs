using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;
using Core.Utilities.Pagination;

namespace Core.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : class,IDatabaseEntity
    {
        IQueryable<TEntity> Query();

        Task<ICollection<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<ICollection<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetPagedData(PaginationFilter filter);
        Task<int> GetTotalRecords();
    }
}
