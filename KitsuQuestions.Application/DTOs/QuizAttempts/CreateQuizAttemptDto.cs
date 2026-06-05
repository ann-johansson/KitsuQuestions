using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.DTOs.QuizAttempts
{
    public class CreateQuizAttemptDto
    {
        public int Score { get; set; }
        public bool Passed { get; set; }
        public string? Feedback { get; set; }
        public int GoalId { get; set; }
    }
}
