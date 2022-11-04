using System.Linq.Expressions;
using Core.Entities.Concrete;

namespace Core.Repositories;

public interface IAsyncRepository<T>  where T : DatabaseEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
}