using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IEmployeeService
{
    Task<Employee> CreateEmployeeAsync(EmployeeRegistrationForm form);
    Task<bool> DeleteEmployeeAsync(int id);
    Task<Employee> GetEmployeeAsync(Expression<Func<EmployeeEntity, bool>> expression);
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee> UpdateEmployeeAsync(EmployeeUpdateForm form);
}