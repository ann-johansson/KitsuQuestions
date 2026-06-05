using System.Text.Json.Serialization;

namespace KitsuQuestions.BlazorClient.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GoalStatus
    {
        ToLearn = 0,
        Learning = 1,
        Fulfilled = 2
    }

    public class GoalDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public GoalStatus Status { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

    public class CreateGoalDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
