using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Repositories
{
    public interface IGoalRepository : IGenericRepository<Goal>
    {
        Task<IEnumerable<Goal>> GetAllWithDetailsAsync();
        Task<Goal?> GetByIdWithDetailsAsync(int id);
        Task<int> CountByStatusAsync(GoalStatus status);
    }
}
