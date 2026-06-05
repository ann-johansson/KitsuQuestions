using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.Resources
{
    public class CreateResourceDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Notes { get; set; }
        public int GoalId { get; set; }
    }
}
