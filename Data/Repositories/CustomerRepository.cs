using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    public override async Task<IEnumerable<CustomerEntity>> GetAsync()
    {
        var entities = await _context.Customers
            .Include(x => x.Projects)
            .ToListAsync();

        return entities;
    }

    public override async Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Customers
            .Include(x => x.Projects)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
