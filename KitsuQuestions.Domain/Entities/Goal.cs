using KitsuQuestions.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Domain.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;   // required string → init to empty
        public string? Description { get; set; }             // optional → nullable
        public GoalStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FulfilledAt { get; set; }

        // "many" side: a goal belongs to zero or one category
        public int? CategoryId { get; set; }     // int? = optional relationship
        public Category? Category { get; set; }  // reference nav

        // "one" side: a goal owns MANY of these
        public ICollection<Resource> Resources { get; set; } = new List<Resource>();
        public ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();
    }
}
