using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Categories;
using KitsuQuestions.Application.Interfaces.Repositories;
using KitsuQuestions.Application.Interfaces.Services;
using KitsuQuestions.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(ToDto);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Category with id {id} was not found.");
            return ToDto(category);
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessRuleException("Category name cannot be empty.");

            var category = new Category { Name = dto.Name.Trim(), HexColor = dto.HexColor };
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return ToDto(category);
        }

        public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Category with id {id} was not found.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BusinessRuleException("Category name cannot be empty.");

            category.Name = dto.Name.Trim();
            category.HexColor = dto.HexColor;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            return ToDto(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id)
                ?? throw new NotFoundException($"Category with id {id} was not found.");
            _categoryRepository.Remove(category);
            await _categoryRepository.SaveChangesAsync();
        }

        private static CategoryDto ToDto(Category c) => new()
        { Id = c.Id, Name = c.Name, HexColor = c.HexColor };
    }
}
