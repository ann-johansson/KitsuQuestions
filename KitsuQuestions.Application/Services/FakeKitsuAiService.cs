using KitsuQuestions.Application.DTOs.AI;
using KitsuQuestions.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Services
{
    public class FakeKitsuAiService : IKitsuAiService
    {
        public Task<IReadOnlyList<string>> GenerateQuestionsAsync(int goalId)
        {
            IReadOnlyList<string> questions = new List<string>
        {
            "What is the main concept you learned?",
            "Can you explain it in your own words?",
            "What was the most surprising thing you discovered?"
        };
            return Task.FromResult(questions);
        }

        public Task<KitsuEvaluationDto> EvaluateAnswersAsync(KitsuEvaluationRequestDto request)
        {
            return Task.FromResult(new KitsuEvaluationDto
            {
                Score = 75,
                Passed = true,
                Feedback = "Kitsu nods approvingly. 🦊 You seem to have grasped the basics!"
            });
        }
    }
}
