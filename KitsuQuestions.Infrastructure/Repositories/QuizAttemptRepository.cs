using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Infrastructure.Repositories
{
    public class QuizAttemptRepository : GenericRepository<QuizAttempt>, IQuizAttemptRepository
    {
        public QuizAttemptRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<QuizAttempt>> GetAllByGoalIdAsync(int goalId) =>
            await _context.QuizAttempts.Where(q => q.GoalId == goalId).ToListAsync();
    }
}
