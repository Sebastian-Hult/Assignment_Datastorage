using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Dialogs;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Datalagring\\Assignment_Datalagring\\Data\\Contexts\\assignment_database.mdf;Integrated Security=True"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IEmployeeRepository, EmployeeRepository>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProjectService, ProjectService>()
    .AddScoped<IEmployeeService, EmployeeService>()
    .AddScoped<IStatusTypeRepository, StatusTypeRepository>()
    .AddScoped<IServiceRepository, ServiceRepository>()
    .AddScoped<MenuDialogs>()
    .AddScoped<CustomerDialogs>()
    .AddScoped<EmployeeDialogs>()
    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();