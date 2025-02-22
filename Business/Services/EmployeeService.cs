using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async Task<Employee> CreateEmployeeAsync(EmployeeRegistrationForm form)
    {
        var entity = await _employeeRepository.GetAsync(x => x.FirstName == form.FirstName && x.LastName == form.LastName);
        if (entity != null)
        {
            Console.WriteLine("An employee with this name already exists.");
            return EmployeeFactory.Create(entity);
        }

        entity = await _employeeRepository.CreateAsync(EmployeeFactory.Create(form));

        return EmployeeFactory.Create(entity);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
        var entities = await _employeeRepository.GetAsync();
        var employees = entities.Select(EmployeeFactory.Create);
        return employees ?? [];
    }

    public async Task<Employee> GetEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression)
    {
        var entity = await _employeeRepository.GetAsync(expression);
        var employee = EmployeeFactory.Create(entity!);
        return employee ?? null!;
    }

    public async Task<Employee> UpdateEmployeeAsync(EmployeeUpdateForm form)
    {
        var entity = await _employeeRepository.UpdateAsync(x => x.Id == form.Id, EmployeeFactory.Create(form));
        var employee = EmployeeFactory.Create(entity);
        return employee ?? null!;
    }

    public async Task<bool> DeleteEmployeeAsync(int id)
    {
        var result = await _employeeRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
