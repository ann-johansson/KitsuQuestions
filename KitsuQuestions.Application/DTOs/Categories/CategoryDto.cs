using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? HexColor { get; set; }
    }
}
