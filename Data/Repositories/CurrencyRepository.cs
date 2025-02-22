using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CurrencyRepository(DataContext context) : BaseRepository<CurrencyEntity>(context), ICurrencyRepository
{
    public override async Task<IEnumerable<CurrencyEntity>> GetAsync()
    {
        var entities = await _context.Currencies
            .Include(x => x.Services)
            .ToListAsync();

        return entities;
    }

    public override async Task<CurrencyEntity?> GetAsync(Expression<Func<CurrencyEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Currencies
            .Include(x => x.Services)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
