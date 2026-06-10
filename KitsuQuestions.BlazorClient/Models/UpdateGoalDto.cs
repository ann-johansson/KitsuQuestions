namespace KitsuQuestions.BlazorClient.Models
{
    public class UpdateGoalDto
    {
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
    }
}
