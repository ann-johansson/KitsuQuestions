using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Resources;
using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Application.Interfaces.Services;
using KitsuQuestions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IGoalRepository _goalRepository;

        public ResourceService(IResourceRepository resourceRepository, IGoalRepository goalRepository)
        {
            _resourceRepository = resourceRepository;
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<ResourceDto>> GetAllByGoalAsync(int goalId)
        {
            var resources = await _resourceRepository.GetAllByGoalIdAsync(goalId);
            return resources.Select(ToDto);
        }

        public async Task<ResourceDto> GetByIdAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Resource with id {id} was not found.");
            return ToDto(resource);
        }

        public async Task<ResourceDto> CreateAsync(CreateResourceDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new BusinessRuleException("Resource title cannot be empty.");

            _ = await _goalRepository.GetByIdAsync(dto.GoalId)
                ?? throw new NotFoundException($"Goal with id {dto.GoalId} was not found.");

            var resource = new Resource
            {
                Title = dto.Title.Trim(),
                Url = dto.Url,
                Notes = dto.Notes,
                GoalId = dto.GoalId
            };

            await _resourceRepository.AddAsync(resource);
            await _resourceRepository.SaveChangesAsync();
            return ToDto(resource);
        }

        public async Task<ResourceDto> UpdateAsync(int id, UpdateResourceDto dto)
        {
            var resource = await _resourceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Resource with id {id} was not found.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new BusinessRuleException("Resource title cannot be empty.");

            resource.Title = dto.Title.Trim();
            resource.Url = dto.Url;
            resource.Notes = dto.Notes;
            _resourceRepository.Update(resource);
            await _resourceRepository.SaveChangesAsync();
            return ToDto(resource);
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await _resourceRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Resource with id {id} was not found.");
            _resourceRepository.Remove(resource);
            await _resourceRepository.SaveChangesAsync();
        }

        private static ResourceDto ToDto(Resource r) => new()
        { Id = r.Id, Title = r.Title, Url = r.Url, Notes = r.Notes, GoalId = r.GoalId };
    }
}
