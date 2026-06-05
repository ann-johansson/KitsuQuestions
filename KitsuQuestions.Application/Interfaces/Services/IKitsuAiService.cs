using KitsuQuestions.Application.DTOs.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitsuQuestions.Application.Interfaces.Services
{
    public interface IKitsuAiService
    {
        Task<IReadOnlyList<string>> GenerateQuestionsAsync(int goalId);
        Task<KitsuEvaluationDto> EvaluateAnswersAsync(KitsuEvaluationRequestDto request);
    }
}
