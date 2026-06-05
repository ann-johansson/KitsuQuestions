using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Domain.Entities;
using KitsuQuestions.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
