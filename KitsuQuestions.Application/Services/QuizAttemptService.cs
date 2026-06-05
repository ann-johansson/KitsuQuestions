using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.QuizAttempts;
using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Application.Interfaces.Services;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly IQuizAttemptRepository _quizAttemptRepository;
        private readonly IGoalRepository _goalRepository;

        public QuizAttemptService(IQuizAttemptRepository quizAttemptRepository, IGoalRepository goalRepository)
        {
            _quizAttemptRepository = quizAttemptRepository;
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<QuizAttemptDto>> GetAllByGoalAsync(int goalId)
        {
            var attempts = await _quizAttemptRepository.GetAllByGoalIdAsync(goalId);
            return attempts.Select(ToDto);
        }

        public async Task<QuizAttemptDto> GetByIdAsync(int id)
        {
            var attempt = await _quizAttemptRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"QuizAttempt with id {id} was not found.");
            return ToDto(attempt);
        }

        public async Task<QuizAttemptDto> CreateAsync(CreateQuizAttemptDto dto)
        {
            var goal = await _goalRepository.GetByIdAsync(dto.GoalId)
                ?? throw new NotFoundException($"Goal with id {dto.GoalId} was not found.");

            var attempt = new QuizAttempt
            {
                Score = dto.Score,
                Passed = dto.Passed,
                Feedback = dto.Feedback,
                AttemptedAt = DateTime.UtcNow,
                GoalId = dto.GoalId
            };

            await _quizAttemptRepository.AddAsync(attempt);

            if (dto.Passed)
            {
                goal.Status = GoalStatus.Fulfilled;
                goal.FulfilledAt = DateTime.UtcNow;
                _goalRepository.Update(goal);
            }

            await _quizAttemptRepository.SaveChangesAsync();
            return ToDto(attempt);
        }

        private static QuizAttemptDto ToDto(QuizAttempt a) => new()
        { Id = a.Id, Score = a.Score, Passed = a.Passed, Feedback = a.Feedback, AttemptedAt = a.AttemptedAt, GoalId = a.GoalId };
    }
}
