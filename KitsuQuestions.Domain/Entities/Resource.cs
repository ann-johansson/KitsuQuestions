using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Domain.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? Notes { get; set; }

        // --- "many" side: a resource belongs to exactly ONE goal ---
        public int GoalId { get; set; }   // int (not int?) = required relationship
        public Goal? Goal { get; set; }   // reference nav back to the parent
    }
}
