using KitsuQuestions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Repositories
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<IEnumerable<Resource>> GetAllByGoalIdAsync(int goalId);
    }
}
