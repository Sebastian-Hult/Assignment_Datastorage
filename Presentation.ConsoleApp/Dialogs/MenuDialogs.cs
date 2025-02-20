using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(ICustomerService customerService, IProjectService projectService, IEmployeeService employeeService, IStatusTypeRepository statusTypeRepository)
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-------- MAIN MENU --------");
            Console.WriteLine("1. Create New Project");
            Console.WriteLine("2. View All Projects");
            Console.WriteLine("3. View Specific Project");
            Console.WriteLine("4. Update Project");
            Console.WriteLine("5. Delete Project");
            Console.WriteLine("Q. Quit Application");
            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateNewProject();
                    break;
                case "2":
                    await ViewAllProjects();
                    break;
                case "3":
                    await ViewProject();
                    break;
                //case "4":
                //    UpdateProject();
                //    break;
                //case "5":
                //    DeleteProject();
                //    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }

    public async Task CreateNewProject()
    {
        var project = ProjectFactory.Create();

        Console.Clear();

        var customerDialogs = new CustomerDialogs(_customerService);
        var customer = await customerDialogs.CreateNewCustomer();

        if (customer == null)
        {
            Console.WriteLine("Customer creation failed. Returning to the main menu.");
            Console.ReadKey();
            return;
        }

        project.CustomerId = customer.Id;

        var employeeDialogs = new EmployeeDialogs(_employeeService);
        var employee = await employeeDialogs.CreateNewEmployee();

        if (employee == null)
        {
            Console.WriteLine("Employee creation failed. Returning to the main menu.");
            Console.ReadKey();
            return;
        }

        project.EmployeeId = employee.Id;

        Console.Clear();
        Console.WriteLine("-------- CREATE NEW PROJECT --------");
        Console.WriteLine();
        Console.WriteLine("To create a new project you need to input the following project information: ");
        Console.Write("Project Name: ");
        project.ProjectName = Console.ReadLine()!;
        Console.Write("Description of the project: ");
        project.Description = Console.ReadLine()!;
        Console.Write("Start Date (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            project.StartDate = startDate;
        else
            Console.WriteLine("Invalid input, try again.");
        Console.Write("End Date (YYYY-MM-DD): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            project.EndDate = endDate;
        else
            Console.WriteLine("Invalid input, try again.");
        Console.Write("Total Price (x,xx): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal totalPrice))
            project.TotalPrice = totalPrice;
        else
            Console.WriteLine("Invalid input, try using two decimals.");

        //var status = await _statusTypeRepository.GetAsync();
        foreach (var status in await _statusTypeRepository.GetAsync())
        {
            Console.WriteLine($"Id: #{status.Id}");
            Console.WriteLine($"Status: -{status.Status}-");
        }
        Console.WriteLine("Choose a status to put on your project by entering the corresponding Id: ");
        if (int.TryParse(Console.ReadLine(), out int statusType))
            project.StatusId = statusType;
        else
            Console.WriteLine("Invalid input, try using two decimals.");
        // get på statusrepository, foreach för varje int id, koppla statusnamn till console.readline()

        var result = await _projectService.CreateProjectAsync(project);
        if (result != null)
            Console.WriteLine("Project was created successfully!");
        else
            Console.WriteLine("Unable to create new project, try again later. Press any key to go back to the Main Menu.");

        Console.ReadKey();
    }

    public async Task ViewAllProjects()
    {
        Console.Clear();

        foreach (var project in await _projectService.GetProjectsAsync())
        {
            Console.WriteLine("---------- ALL PROJECTS ----------");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Id: #{project.Id}");
            Console.WriteLine($"Project Name: {project.ProjectName}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate}");
            Console.WriteLine($"End Date: {project.EndDate}");
            Console.WriteLine($"Total Price: {project.TotalPrice}");
            Console.WriteLine($"Status: {project.Status.Status}");
            Console.WriteLine("------------------------------------------------");
        }

        Console.Write("Press any key to return to the Main Menu.");
        Console.ReadKey();
    }

    public async Task ViewProject()
    {
        Console.Clear();

        Console.WriteLine("---------- CURRENT CREATED PROJECTS ----------");

        foreach (var project in await _projectService.GetProjectsAsync())
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Id: #{project.Id}   Project Name: {project.ProjectName}");
            Console.WriteLine("------------------------------------------------");
        }

        Console.WriteLine("\nAbove is a list of all the created projects by their name and Id.");
        Console.Write("Write down the name of the project you would like to view: ");
        var projectName = Console.ReadLine()!.ToLower();

        var result = await _projectService.GetProjectAsync(x => x.ProjectName == projectName);

        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Here is the information on the chosen project:\n");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Id: #{result.Id}");
            Console.WriteLine($"Project Name: {result.ProjectName}");
            Console.WriteLine($"Description: {result.Description}");
            Console.WriteLine($"Start Date: {result.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {result.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Total Price: {result.TotalPrice}");
            Console.WriteLine("------------------------------------------------");
        }
        else
        {
            Console.WriteLine($"\nNo project with the name '{projectName}' was found. Please try again.");
        }

        Console.WriteLine("Press any key to return to the Main Menu.");
        Console.ReadKey();
    }

    //public async Task UpdateProject()
    //{
    //    Console.Clear();
    //}

    //public async Task DeleteProject()
    //{
    //    Console.Clear();
    //}
}
