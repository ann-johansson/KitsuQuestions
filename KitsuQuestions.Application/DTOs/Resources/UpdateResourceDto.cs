using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.Resources
{
    public class UpdateResourceDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Notes { get; set; }
    }
}
