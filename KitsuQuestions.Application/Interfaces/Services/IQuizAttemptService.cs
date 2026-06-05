using KitsuQuestions.Application.DTOs.QuizAttempts;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Services
{
    public interface IQuizAttemptService
    {
        Task<IEnumerable<QuizAttemptDto>> GetAllByGoalAsync(int goalId);
        Task<QuizAttemptDto> GetByIdAsync(int id);
        Task<QuizAttemptDto> CreateAsync(CreateQuizAttemptDto dto);
    }
}
