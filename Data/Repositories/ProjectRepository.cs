﻿using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public override async Task<IEnumerable<ProjectEntity>> GetAsync()
    {
        var entities = await _context.Projects
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .Include(x => x.Status)
            .Include(x => x.Service)
            .ToListAsync();

        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        var entity = await _context.Projects
            .Include(x => x.Customer)
            .Include(x => x.Employee)
            .Include(x => x.Status)
            .Include(x => x.Service)
            .FirstOrDefaultAsync(expression);

        return entity;
    }
}
