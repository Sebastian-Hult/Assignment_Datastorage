using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class EmployeeFactory
{
    public static EmployeeRegistrationForm Create() => new();

    public static EmployeeUpdateForm Update() => new();

    public static EmployeeEntity Create(EmployeeRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName
    };

    public static Employee Create(EmployeeEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName
    };

    public static EmployeeEntity Create(EmployeeUpdateForm form) => new()
    {
        Id = form.Id,
        FirstName = form.FirstName,
        LastName = form.LastName
    };
}
