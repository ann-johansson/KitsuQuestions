using KitsuQuestions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Repositories
{
    public interface IQuizAttemptRepository : IGenericRepository<QuizAttempt>
    {
        Task<IEnumerable<QuizAttempt>> GetAllByGoalIdAsync(int goalId);
    }
}
