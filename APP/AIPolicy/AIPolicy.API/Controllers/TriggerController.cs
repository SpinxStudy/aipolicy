using Microsoft.AspNetCore.Mvc;

namespace AIPolicy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TriggerController : ControllerBase
{
    private readonly ILogger<TriggerController> _logger;
    public TriggerController(ILogger<TriggerController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Trigger trigger)
    {
        return Ok();
    }
}
