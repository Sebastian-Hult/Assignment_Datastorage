using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(ICustomerService customerService, IProjectService projectService, IEmployeeService employeeService, IStatusTypeRepository statusTypeRepository, IServiceRepository serviceRepository)
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProjectService _projectService = projectService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;
    private readonly IServiceRepository _serviceRepository = serviceRepository;

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
                case "4":
                    await UpdateProject();
                    break;
                case "5":
                    await DeleteProject();
                    break;
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

        Console.WriteLine();
        foreach (var status in await _statusTypeRepository.GetAsync())
        {
            Console.WriteLine($"Id: #{status.Id}, Status: -{status.Status}-");
        }
        Console.WriteLine("\nChoose a status to put on your project by entering the corresponding Id: ");
        if (int.TryParse(Console.ReadLine(), out int statusType))
            project.StatusId = statusType;
        else
            Console.WriteLine("Invalid input, try using two decimals.");

        var services = await _serviceRepository.GetAsync();

        if (!services.Any())
        {
            Console.WriteLine("No services available.");
            return;
        }

        Console.Write("To create a project you need to connect it to a service.");
        Console.WriteLine("Available services: ");
        foreach (var service in services)
        {
            Console.WriteLine($"{service.Id}. {service.ServiceName} - {service.Price} {service.Currency.Currency}/{service.Unit.Unit}");
        }

        Console.Write("Enter the Service Id to apply that service to your project: ");
        if (int.TryParse(Console.ReadLine(), out int selectedServiceId) && services.Any(x => x.Id == selectedServiceId))
            project.ServiceId = selectedServiceId;
        else
            Console.WriteLine("Invalid input. Please try again.");

        Console.WriteLine();

        Console.WriteLine("How much of the service is needed? (ex. x => x hours): ");
        if (int.TryParse(Console.ReadLine(), out int unitAmount))
        {
            var selectedService = services.FirstOrDefault(x => x.Id == selectedServiceId);

            if (selectedService != null)
            {
                project.TotalPrice = unitAmount * selectedService.Price;
            }
            else
                Console.WriteLine("Something went wrong...");
        }
        else
            Console.WriteLine("Invalid input. Please enter a number.");

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
            Console.WriteLine($"Id: P-{project.Id}");
            Console.WriteLine($"Project Name: {project.ProjectName}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate.ToShortDateString()}");
            Console.WriteLine($"End Date: {project.EndDate.ToShortDateString()}");
            //Console.WriteLine($"Project Manager: {project.Employee.FirstName} {project.Employee.LastName}");
            //Console.WriteLine($"Customer: {project.Customer.Name}");
            //Console.WriteLine($"Service: {project.Service.ServiceName} - {project.Service.Price} {project.Service.Currency}/{project.Service.Unit}");
            Console.WriteLine($"Total Price: {project.TotalPrice}");
            //Console.WriteLine($"Status: {project.Status.Status}");
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

    public async Task UpdateProject()
    {
        Console.Clear();

        var projects = (await _projectService.GetProjectsAsync()).ToList();

        if (!projects.Any())
        {
            Console.WriteLine("No projects available.");
            return;
        }

        Console.WriteLine("-------- UPDATE PROJECT --------");
        Console.WriteLine("\nSelect a project you want to update: ");

        int projectNumber = 1;
        foreach (var project in projects)
        {
            Console.WriteLine($"{projectNumber}. {project.ProjectName}");
            projectNumber++;
        }

        Console.Write("\nEnter the number of a project you want to update: ");
        int selectedProjectNumber = int.Parse(Console.ReadLine()!);

        if (selectedProjectNumber < 1 || selectedProjectNumber > projects.Count())
        {
            Console.WriteLine("Invalid selection. Try again");
            return;
        }

        var selectedProject = projects[selectedProjectNumber - 1];

        var updateForm = ProjectFactory.Update();

        Console.Clear();
        Console.WriteLine($"-------- UPDATE PROJECT: {selectedProject.ProjectName} --------");
        Console.WriteLine($"Project Name: {selectedProject.ProjectName}");
        Console.WriteLine($"Description: {selectedProject.Description}");
        Console.WriteLine($"Start Date: {selectedProject.StartDate.ToShortDateString()}");
        Console.WriteLine($"End Date: {selectedProject.EndDate.ToShortDateString()}");
        Console.WriteLine($"Total Price: {selectedProject.TotalPrice}");

        Console.WriteLine("\n-------- UPDATE PROJECT FIELDS --------");

        Console.Write($"Current Project Name: {selectedProject.ProjectName}. Type in new name (or press ENTER to leave field unchanged): ");
        var newProjectName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newProjectName))
            updateForm.ProjectName = newProjectName;
        else
            updateForm.ProjectName = selectedProject.ProjectName;

        Console.Write($"Current Description: {selectedProject.Description}. Type in new description (or press ENTER to leave field unchanged): ");
        var newDescription = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDescription))
            updateForm.Description = newDescription;
        else
            updateForm.Description = selectedProject.Description;

        Console.Write($"Current Start Date: {selectedProject.StartDate.ToShortDateString()}. Type in new start date (or press ENTER to leave field unchanged): ");
        var newStartDateInput = Console.ReadLine();
        if (DateTime.TryParse(newStartDateInput, out DateTime newStartDate))
            updateForm.StartDate = newStartDate;
        else
            updateForm.StartDate = selectedProject.StartDate;

        Console.Write($"Current End Date: {selectedProject.EndDate.ToShortDateString()}. Type in new end date (or press ENTER to leave field unchanged): ");
        var newEndDateInput = Console.ReadLine();
        if (DateTime.TryParse(newEndDateInput, out DateTime newEndDate))
            updateForm.EndDate = newEndDate;
        else
            updateForm.EndDate = selectedProject.EndDate;

        Console.Write($"Current Total Price: {selectedProject.TotalPrice}. Type in new total price (or press ENTER to leave field unchanged): ");
        var newTotalPriceInput = Console.ReadLine();
        if (decimal.TryParse(newTotalPriceInput, out decimal newTotalPrice))
            updateForm.TotalPrice = newTotalPrice;
        else
            updateForm.TotalPrice = selectedProject.TotalPrice;

        Console.Clear();
        Console.WriteLine("-------- UPDATED PROJECT --------");
        Console.WriteLine($"Project Name: {selectedProject.ProjectName}");
        Console.WriteLine($"Description: {selectedProject.Description}");
        Console.WriteLine($"Start Date: {selectedProject.StartDate.ToShortDateString()}");
        Console.WriteLine($"End Date: {selectedProject.EndDate.ToShortDateString()}");
        Console.WriteLine($"Total Price: {selectedProject.TotalPrice}");

        //var updatedProject = ProjectFactory.Create(updateForm);
        await _projectService.UpdateProjectAsync(updateForm);
        Console.WriteLine("Project was updated successfully");

        //Console.WriteLine("\nPress Enter to return to the Main Menu or any key to stay in the Update menu.");
        //var userInput = Console.ReadKey();
        //if (userInput.Key == ConsoleKey.Enter)
        //    return;

        Console.WriteLine("Press any key to return to the Main Menu.");
        Console.ReadKey();

    }

    public async Task DeleteProject()
    {
        Console.Clear();

        var projects = (await _projectService.GetProjectsAsync()).ToList();

        if (projects.Count == 0)
        {
            Console.WriteLine("No projects available.");
            return;
        }

        Console.WriteLine("-------- DELETE PROJECT --------");
        Console.WriteLine("\nSelect a project you want to delete: ");

        int projectNumber = 1;
        foreach (var project in projects)
        {
            Console.WriteLine($"{projectNumber}. {project.ProjectName}");
            projectNumber++;
        }

        Console.Write("\nEnter the number of a project you want to delete: ");
        int selectedProjectNumber = int.Parse(Console.ReadLine()!);

        if (selectedProjectNumber < 1 || selectedProjectNumber > projects.Count())
        {
            Console.WriteLine("Invalid selection. Try again");
            return;
        }

        var selectedProject = projects[selectedProjectNumber - 1];

        Console.Clear();
        Console.WriteLine($"-------- DELETE PROJECT--------");
        Console.WriteLine($"Project Name: {selectedProject.ProjectName}");
        Console.WriteLine($"Description: {selectedProject.Description}");
        Console.WriteLine($"Start Date: {selectedProject.StartDate.ToShortDateString()}");
        Console.WriteLine($"End Date: {selectedProject.EndDate.ToShortDateString()}");
        Console.WriteLine($"Total Price: {selectedProject.TotalPrice}");

        Console.WriteLine("\nWould you like to delete this project? (y/n): ");

        var userInput = Console.ReadKey();
        if (userInput.Key == ConsoleKey.Y)
        {
            await _projectService.DeleteProjectAsync(selectedProject.Id);
            Console.WriteLine("Project was deleted successfully.");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Something went wrong... Try again later");
            Console.ReadKey();
        }

        if (userInput.Key == ConsoleKey.N)
        {
            Console.WriteLine("Press any key to return to the Main Menu.");
            Console.ReadKey();
        }
            
    }
}
