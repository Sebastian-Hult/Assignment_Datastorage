using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UnitRepository(DataContext context) : BaseRepository<UnitEntity>(context), IUnitRepository
{
    public override async Task<IEnumerable<UnitEntity>> GetAsync()
    {
        var entities = await _context.Units
            .Include(x => x.Services)
            .ToListAsync();

        return entities;
    }

    public override async Task<UnitEntity?> GetAsync(Expression<Func<UnitEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Units
            .Include(x => x.Services)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
