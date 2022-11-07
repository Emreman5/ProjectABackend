using System.Linq.Expressions;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.EntityFramework;

public abstract class EfRepositoryBase<TEntity, TContext> : IUnitOfWork ,IRepository<TEntity, int> where TEntity : DatabaseEntity, new()

{
    private readonly DbContext _context;

    protected EfRepositoryBase(DbContext context)
    {
        _context = context;
    }
    public async Task<DatabaseEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
        return entity;
    }
    public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().FirstOrDefault(predicate);
    }
    public TEntity Add(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Added;
        return entity;
    }
    public TEntity Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return entity;
    }
    public TEntity Delete(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        return entity;
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    // 
}