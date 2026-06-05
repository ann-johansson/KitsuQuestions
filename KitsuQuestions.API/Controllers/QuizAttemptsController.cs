using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.QuizAttempts;
using KitsuQuestions.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitsuQuestions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizAttemptsController : ControllerBase
    {
        private readonly IQuizAttemptService _quizAttemptService;

        public QuizAttemptsController(IQuizAttemptService quizAttemptService)
        {
            _quizAttemptService = quizAttemptService;
        }

        [HttpGet("by-goal/{goalId}")]
        public async Task<ActionResult<IEnumerable<QuizAttemptDto>>> GetAllByGoal(int goalId) =>
            Ok(await _quizAttemptService.GetAllByGoalAsync(goalId));

        [HttpGet("{id}")]
        public async Task<ActionResult<QuizAttemptDto>> GetById(int id)
        {
            try { return Ok(await _quizAttemptService.GetByIdAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult<QuizAttemptDto>> Create(CreateQuizAttemptDto dto)
        {
            try
            {
                var created = await _quizAttemptService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}
