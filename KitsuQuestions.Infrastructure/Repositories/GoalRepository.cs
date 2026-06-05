using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Domain.Enums;
using KitsuQuestions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Infrastructure.Repositories
{
    public class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        public GoalRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Goal>> GetAllWithDetailsAsync() =>
            await _context.Goals
                .Include(g => g.Category)
                .Include(g => g.Resources)
                .ToListAsync();

        public async Task<Goal?> GetByIdWithDetailsAsync(int id) =>
            await _context.Goals
                .Include(g => g.Category)
                .Include(g => g.Resources)
                .Include(g => g.QuizAttempts)
                .FirstOrDefaultAsync(g => g.Id == id);

        public async Task<int> CountByStatusAsync(GoalStatus status) =>
            await _context.Goals.CountAsync(g => g.Status == status);
    }
}
