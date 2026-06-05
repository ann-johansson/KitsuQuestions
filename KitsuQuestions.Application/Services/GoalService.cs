using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Goals;
using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Application.Interfaces.Services;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private const int MaxActiveLearningGoals = 8;

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<GoalDto>> GetAllAsync()
        {
            var goals = await _goalRepository.GetAllWithDetailsAsync();
            return goals.Select(ToDto);
        }

        public async Task<GoalDto> GetByIdAsync(int id)
        {
            var goal = await _goalRepository.GetByIdWithDetailsAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");
            return ToDto(goal);
        }

        public async Task<GoalDto> CreateAsync(CreateGoalDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new BusinessRuleException("Goal title cannot be empty.");

            var goal = new Goal
            {
                Title = dto.Title.Trim(),
                Description = dto.Description,
                CategoryId = dto.CategoryId,
                Status = GoalStatus.ToLearn,
                CreatedAt = DateTime.UtcNow
            };

            await _goalRepository.AddAsync(goal);
            await _goalRepository.SaveChangesAsync();
            return ToDto(goal);
        }

        public async Task<GoalDto> UpdateAsync(int id, UpdateGoalDto dto)
        {
            var goal = await _goalRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new BusinessRuleException("Goal title cannot be empty.");

            goal.Title = dto.Title.Trim();
            goal.Description = dto.Description;
            goal.CategoryId = dto.CategoryId;

            _goalRepository.Update(goal);
            await _goalRepository.SaveChangesAsync();
            return ToDto(goal);
        }

        public async Task DeleteAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");

            _goalRepository.Remove(goal);
            await _goalRepository.SaveChangesAsync();
        }

        public async Task<GoalDto> MoveToLearningAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");

            if (goal.Status == GoalStatus.Fulfilled)
                throw new BusinessRuleException("A fulfilled goal cannot be moved to Learning.");

            var learningCount = await _goalRepository.CountByStatusAsync(GoalStatus.Learning);
            if (learningCount >= MaxActiveLearningGoals)
                throw new BusinessRuleException($"You can have at most {MaxActiveLearningGoals} goals in Learning at once.");

            goal.Status = GoalStatus.Learning;
            _goalRepository.Update(goal);
            await _goalRepository.SaveChangesAsync();
            return ToDto(goal);
        }

        public async Task<GoalDto> MoveToToLearnAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");

            goal.Status = GoalStatus.ToLearn;
            goal.FulfilledAt = null;
            _goalRepository.Update(goal);
            await _goalRepository.SaveChangesAsync();
            return ToDto(goal);
        }

        public async Task<GoalDto> FulfillAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Goal with id {id} was not found.");

            goal.Status = GoalStatus.Fulfilled;
            goal.FulfilledAt = DateTime.UtcNow;
            _goalRepository.Update(goal);
            await _goalRepository.SaveChangesAsync();
            return ToDto(goal);
        }

        private static GoalDto ToDto(Goal goal) => new()
        {
            Id = goal.Id,
            Title = goal.Title,
            Description = goal.Description,
            Status = goal.Status.ToString(),
            CreatedAt = goal.CreatedAt,
            FulfilledAt = goal.FulfilledAt,
            CategoryId = goal.CategoryId
        };
    }
}
