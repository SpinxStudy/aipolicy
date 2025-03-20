using AIPolicy.Application.Service;
using AIPolicy.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AIPolicy.API;

[Route("api/[controller]")]
[ApiController]
public class TriggerController : ControllerBase
{
    private readonly TriggerService _service;

    public TriggerController(TriggerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trigger>>> GetAll()
    {
        var triggers = await _service.GetAllTriggersAsync();
        return Ok(triggers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trigger>> GetById(int id)
    {
        var trigger = await _service.GetTriggerByIdAsync(id);
        if (trigger == null)
            return NotFound();
        return Ok(trigger);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] Trigger trigger)
    {
        var id = await _service.CreateTriggerAsync(trigger);
        return Ok(id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Trigger trigger)
    {
        if (id != trigger.Id)
            return BadRequest("ID not found");

        try
        {
            await _service.UpdateTriggerAsync(trigger);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteTriggerAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}