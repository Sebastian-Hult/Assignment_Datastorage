
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.Dialogs;

public class CustomerDialogs(ICustomerService customerService)
{
    private readonly ICustomerService _customerService = customerService;

    //public async Task CustomerMenuOptions()
    //{

    //}

    public async Task<Customer> CreateNewCustomer()
    {
        var customer = CustomerFactory.Create();

        Console.Clear();
        Console.WriteLine("-------- CREATE NEW CUSTOMER --------");
        Console.WriteLine("\nTo create a new customer you need to input the following customer information: ");
        Console.Write("Customer Name: ");
        customer.Name = Console.ReadLine()!;
        Console.Write("Customer email: ");
        customer.Email = Console.ReadLine()!;
        Console.Write("Customer phone number: ");
        customer.PhoneNumber = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(customer);
        if (result == null)
        {
            Console.WriteLine("Unable to create new customer, try again later.");
            Console.ReadKey();
            return null!;
        }

        Console.WriteLine("\nCustomer was created successfully!");
        Console.Write("Press any key to return to creating a project.");
        return result;
    }
}
