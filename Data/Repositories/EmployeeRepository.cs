using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context), IEmployeeRepository
{
    public override async Task<IEnumerable<EmployeeEntity>> GetAsync()
    {
        var entities = await _context.Employees
            .Include(x => x.Projects)
            .ToListAsync();

        return entities;
    }

    public override async Task<EmployeeEntity?> GetAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Employees
            .Include(x => x.Projects)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
