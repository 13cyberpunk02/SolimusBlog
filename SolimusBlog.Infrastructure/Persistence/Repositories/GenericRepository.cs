using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SolimusBlog.Domain.Interfaces;
using SolimusBlog.Infrastructure.Persistence.Context;

namespace SolimusBlog.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(SolimusAppContext context) : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    
    public async Task<TEntity?> GetByIdAsync(string id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => 
        await _dbSet.Where(predicate).AsNoTracking().ToListAsync();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        var entry = _dbSet.Update(entity);
        return entry.Entity;
    }

    public void Delete(TEntity entity) => _dbSet.Remove(entity);
}