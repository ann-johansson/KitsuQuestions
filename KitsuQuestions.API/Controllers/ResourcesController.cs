using KitsuQuestions.Application.Common.Exceptions;
using KitsuQuestions.Application.DTOs.Resources;
using KitsuQuestions.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitsuQuestions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet("by-goal/{goalId}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAllByGoal(int goalId) =>
            Ok(await _resourceService.GetAllByGoalAsync(goalId));

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetById(int id)
        {
            try { return Ok(await _resourceService.GetByIdAsync(id)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> Create(CreateResourceDto dto)
        {
            try
            {
                var created = await _resourceService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (BusinessRuleException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResourceDto>> Update(int id, UpdateResourceDto dto)
        {
            try { return Ok(await _resourceService.UpdateAsync(id, dto)); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
            catch (BusinessRuleException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try { await _resourceService.DeleteAsync(id); return NoContent(); }
            catch (NotFoundException ex) { return NotFound(ex.Message); }
        }
    }
}
