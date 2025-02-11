using Business.Factories;
using Business.Models;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        // expand with if-statement to cehck if customerr already exists, add try/catch
        var customerEntity = CustomerFactory.Create(form);
        await _customerRepository.AddAsync(customerEntity!);
    }

    public async Task<IEnumerable<Customer?>> GetCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Email == email);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<bool> UpdateCustomerAsync(Customer customer)
    {

    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {

    }
}
