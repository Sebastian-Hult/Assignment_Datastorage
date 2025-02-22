using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    public override async Task<IEnumerable<ServiceEntity>> GetAsync()
    {
        var entities = await _context.Services
            .Include(x => x.Currency)
            .Include(x => x.Unit)
            .ToListAsync();

        return entities;
    }

    public override async Task<ServiceEntity?> GetAsync(Expression<Func<ServiceEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Services
            .Include(x => x.Currency)
            .Include(x => x.Unit)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
