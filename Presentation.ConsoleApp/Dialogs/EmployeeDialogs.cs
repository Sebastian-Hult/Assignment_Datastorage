
using Business.Factories;
using Business.Interfaces;
using Business.Models;

namespace Presentation.ConsoleApp.Dialogs;

public class EmployeeDialogs(IEmployeeService employeeService)
{
    private readonly IEmployeeService _employeeService = employeeService;

    //public async Task CustomerMenuOptions()
    //{

    //}

    public async Task<Employee> CreateNewEmployee()
    {
        var employee = EmployeeFactory.Create();

        Console.Clear();
        Console.WriteLine("-------- CREATE NEW EMPLOYEE --------");
        Console.WriteLine("\nTo create a new employee you need to input the following employee information: ");
        Console.Write("Employee first name: ");
        employee.FirstName = Console.ReadLine()!;
        Console.Write("Employee last name: ");
        employee.LastName = Console.ReadLine()!;

        const int employeeRoleId = 1;
        employee.RoleId = employeeRoleId;

        var result = await _employeeService.CreateEmployeeAsync(employee);
        if (result == null)
        {
            Console.WriteLine("Unable to create new employee, try again later.");
            Console.ReadKey();
            return null!;
        }

        Console.WriteLine("\nEmployee was created successfully!");
        Console.Write("Press any key to return to creating a project.");
        return result;
    }
}
