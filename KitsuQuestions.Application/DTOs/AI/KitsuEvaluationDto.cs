using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.AI
{
    public class KitsuEvaluationDto
    {
        public int Score { get; set; }
        public bool Passed { get; set; }
        public string Feedback { get; set; } = string.Empty;
    }
}
