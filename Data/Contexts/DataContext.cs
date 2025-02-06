using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CurrencyEntity> Currencies { get; set; }
    public DbSet<UnitEntity> Units { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProjectEntity>()
            .Property(p => p.Id)
            .UseIdentityColumn(100, 1);
    }
}
