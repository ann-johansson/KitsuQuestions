using KitsuQuestions.Application.DTOs.Goals;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Services
{
    public interface IGoalService
    {
        Task<IEnumerable<GoalDto>> GetAllAsync();
        Task<GoalDto> GetByIdAsync(int id);
        Task<GoalDto> CreateAsync(CreateGoalDto dto);
        Task<GoalDto> UpdateAsync(int id, UpdateGoalDto dto);
        Task DeleteAsync(int id);
        Task<GoalDto> MoveToLearningAsync(int id);
        Task<GoalDto> MoveToToLearnAsync(int id);
        Task<GoalDto> FulfillAsync(int id);
    }
}
