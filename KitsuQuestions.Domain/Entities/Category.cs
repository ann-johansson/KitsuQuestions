using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? HexColor { get; set; }

        public ICollection<Goal> Goals { get; set; } = new List<Goal>();
    }
}
