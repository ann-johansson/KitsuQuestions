using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.AI
{
    public class KitsuEvaluationRequestDto
    {
        public int GoalId { get; set; }
        public List<string> Questions { get; set; } = new();
        public List<string> Answers { get; set; } = new();
    }
}
