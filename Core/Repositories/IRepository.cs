using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Core.Repositories;

public interface IRepository<T, in TKey> where T : class, IDatabaseEntity<TKey>, new() where TKey : IEquatable<TKey>
{
    T? Get(Expression<Func<T, bool>> predicate);
    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
  
}