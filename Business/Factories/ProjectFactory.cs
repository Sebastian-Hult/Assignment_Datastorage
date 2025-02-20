using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    public static ProjectUpdateForm Update() => new();

    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        ProjectName = form.ProjectName,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        TotalPrice = form.TotalPrice,
        CustomerId = form.CustomerId,
        EmployeeId = form.EmployeeId,
        StatusId = form.StatusId,
        ServiceId = form.ServiceId
    };

    public static Project Create(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        ProjectName = entity.ProjectName,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
        TotalPrice = entity.TotalPrice,
        CustomerId = entity.CustomerId,
        EmployeeId = entity.EmployeeId,
        StatusId = entity.StatusId,
        ServiceId = entity.ServiceId
    };

    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        ProjectName = form.ProjectName,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
        TotalPrice = form.TotalPrice,
        CustomerId = form.CustomerId,
        EmployeeId = form.EmployeeId,
        StatusId = form.StatusId,
        ServiceId = form.ServiceId
    };
}
