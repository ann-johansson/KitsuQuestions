using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Infrastructure.Repositories
{
    public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Resource>> GetAllByGoalIdAsync(int goalId) =>
            await _context.Resources.Where(r => r.GoalId == goalId).ToListAsync();
    }
}
