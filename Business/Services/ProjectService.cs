using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var entity = await _projectRepository.GetAsync(x => x.ProjectName == form.ProjectName);
        entity ??= await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        return ProjectFactory.Create(entity);
    }

    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var entities = await _projectRepository.GetAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects ?? [];
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(entity!);
        return project ?? null!;
    }

    public async Task<Project?> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {
        //var existingProject = await GetProjectEntityAsync(x => x.Id == id);
        //if (existingProject == null)
        //    return null;

        //existingProject.ProjectName = string.IsNullOrWhiteSpace(form.ProjectName) ? existingProject.ProjectName : form.ProjectName;
        //existingProject.Description = string.IsNullOrWhiteSpace(form.Description) ? existingProject.Description : form.Description;
        //existingProject.StartDate = string.IsNullOrWhiteSpace(form.StartDate) ? existingProject.StartDate : form.StartDate;
        //existingProject.EndDate = string.IsNullOrWhiteSpace(form.EndDate) ? existingProject.EndDate : form.EndDate;
        //existingProject.Status = string.IsNullOrWhiteSpace(form.Status) ? existingProject.Status : form.Status;
        //existingProject.TotalPrice = string.IsNullOrWhiteSpace(form.TotalPrice) ? existingProject.TotalPrice : form.TotalPrice;

        //var result = await _projectRepository.UpdateAsync(x => x.Id == id, existingProject);
        //return result ? ProjectFactory.Create(existingProject) : null;


        var entity = await _projectRepository.UpdateAsync(x => x.Id == form.Id, ProjectFactory.Create(form));
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == id);
        return result;
    }

    private async Task<ProjectEntity?> GetProjectEntityAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var project = await _projectRepository.GetAsync(expression);
        return project;
    }
}
