using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UnitRepository(DataContext context) : BaseRepository<UnitEntity>(context), IUnitRepository
{
}
