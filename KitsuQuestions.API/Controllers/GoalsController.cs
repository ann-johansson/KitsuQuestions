using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Goals;
using KitsuQuestions.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitsuQuestions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalsController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoalDto>>> GetAll() =>
            Ok(await _goalService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetById(int id)
        {
            try { return Ok(await _goalService.GetByIdAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult<GoalDto>> Create(CreateGoalDto dto)
        {
            try
            {
                var created = await _goalService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (BusinessRuleException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<GoalDto>> Update(int id, UpdateGoalDto dto)
        {
            try { return Ok(await _goalService.UpdateAsync(id, dto)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (BusinessRuleException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _goalService.DeleteAsync(id); return NoContent(); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("{id}/move-to-learning")]
        public async Task<ActionResult<GoalDto>> MoveToLearning(int id)
        {
            try { return Ok(await _goalService.MoveToLearningAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (BusinessRuleException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("{id}/move-to-tolearn")]
        public async Task<ActionResult<GoalDto>> MoveToToLearn(int id)
        {
            try { return Ok(await _goalService.MoveToToLearnAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost("{id}/fulfill")]
        public async Task<ActionResult<GoalDto>> Fulfill(int id)
        {
            try { return Ok(await _goalService.FulfillAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}
