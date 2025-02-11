using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class EmployeeRepository(DataContext context) : BaseRepository<EmployeeEntity>(context)
{
}
