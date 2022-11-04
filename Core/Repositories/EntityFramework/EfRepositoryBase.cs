using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.Repositories.EntityFramework;

public abstract class EfRepositoryBase<TEntity, TContext> : IUnitOfWork ,IRepository<TEntity, int> where TEntity : DatabaseEntity, new()

{
    private readonly DbContext Context;

    protected EfRepositoryBase(DbContext context)
    {
        Context = context;
    }
    public async Task<DatabaseEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync();
        return entity;
    }
    public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().FirstOrDefault(predicate);
    }
    public TEntity Add(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        return entity;
    }
    public TEntity Update(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public TEntity Delete(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        return entity;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public int Commit()
    {
        return Context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await Context.SaveChangesAsync();
    }
}