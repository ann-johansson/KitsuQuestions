using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.Goals
{
    public class CreateGoalDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
