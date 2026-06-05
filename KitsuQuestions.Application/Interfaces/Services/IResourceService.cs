using KitsuQuestions.Application.DTOs.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceDto>> GetAllByGoalAsync(int goalId);
        Task<ResourceDto> GetByIdAsync(int id);
        Task<ResourceDto> CreateAsync(CreateResourceDto dto);
        Task<ResourceDto> UpdateAsync(int id, UpdateResourceDto dto);
        Task DeleteAsync(int id);
    }
}
