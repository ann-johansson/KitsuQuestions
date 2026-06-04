using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Domain.Entities
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public int Score { get; set; } = 0;
        public bool Passed { get; set; } = false;
        public string? Feedback { get; set; } = null;
        public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;

        public int GoalId { get; set; }
        public Goal? Goal { get; set; }
    }
}
