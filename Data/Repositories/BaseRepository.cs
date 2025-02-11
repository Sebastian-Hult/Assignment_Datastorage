using System.Linq.Expressions;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity>(DataContext context) where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(expression);
        return entity;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
