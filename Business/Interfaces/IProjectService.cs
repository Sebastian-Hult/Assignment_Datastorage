using System.Linq.Expressions;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(ProjectRegistrationForm form);
    Task<bool> DeleteProjectAsync(int id);
    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project?> UpdateProjectAsync(int id, ProjectUpdateForm form);
}
