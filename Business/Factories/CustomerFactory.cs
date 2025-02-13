using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create() => new();

    public static CustomerUpdateForm Update() => new();

    public static CustomerEntity Create(CustomerRegistrationForm form) =>  new()
    {
        Name = form.Name,
        Email = form.Email,
        PhoneNumber = form.PhoneNumber
    };

    public static Customer Create(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Email = entity.Email,
        PhoneNumber = entity.PhoneNumber
    };

    public static CustomerEntity Create(CustomerUpdateForm form) => new()
    {
        Id = form.Id,
        Name = form.Name,
        Email = form.Email,
        PhoneNumber = form.PhoneNumber
    };
}
