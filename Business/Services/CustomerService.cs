using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        // kollar om en entitet existerar eller inte
        var entity = await _customerRepository.GetAsync(x => x.Name == form.Name);
        entity ??= await _customerRepository.CreateAsync(CustomerFactory.Create(form));

        return CustomerFactory.Create(entity);
    }

    public async Task<IEnumerable<Customer>> GetCustomersAsync()
    {
        var entities = await _customerRepository.GetAsync();
        var customers = entities.Select(CustomerFactory.Create);
        return customers ?? [];
    }

    public async Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        var entity = await _customerRepository.GetAsync(expression);
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<Customer> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        var entity = await _customerRepository.UpdateAsync(x => x.Id == form.Id, CustomerFactory.Create(form));
        var customer = CustomerFactory.Create(entity);
        return customer ?? null!;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var result = await _customerRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
