using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(ICustomerService customerService, IProjectService projectService, IEmployeeService employeeService)
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;
    private readonly IEmployeeService _employeeService = employeeService;

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
        Console.Write("Total Price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal totalPrice))
            project.TotalPrice = totalPrice;
        else
            Console.WriteLine("Invalid input, try using two decimals.");

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
            Console.WriteLine("------------------------------------------------");
        }

        Console.Write("Press any key to return to the Main Menu.");
        Console.ReadKey();
    }

    public async Task ViewProject()
    {
        Console.Clear();

        foreach (var project in await _projectService.GetProjectsAsync())
        {
            Console.WriteLine("---------- CURRENT CREATED PROJECTS ----------");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Project Name: {project.ProjectName}");
            Console.WriteLine("------------------------------------------------");
        }

        var form = new ProjectRegistrationForm();
        var result = await _projectService.GetProjectAsync(x => x.ProjectName == form.ProjectName);

        Console.WriteLine("Above is a list of all the created projects by their name. Write down the name of the project you would like to view: ");
        result.ProjectName = Console.ReadLine()!;

        Console.WriteLine("Here is the information on the chosen project: ");
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Id: #{result.Id}");
        Console.WriteLine($"Project Name: {result.ProjectName}");
        Console.WriteLine($"Description: {result.Description}");
        Console.WriteLine($"Start Date: {result.StartDate}");
        Console.WriteLine($"End Date: {result.EndDate}");
        Console.WriteLine($"Total Price: {result.TotalPrice}");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine();

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
