using AIPolicy.Application.InputModel;
using AIPolicy.Application.Service;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AIPolicy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase
{
    #region Dependencies
    private readonly IPolicyService _policyService;
    #endregion

    #region Constructor
    public PolicyController(IPolicyService policyService)
    {
        _policyService = policyService;
    }
    #endregion

    #region Endpoints
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var policies = await _policyService.GetAllAsync();
        return Ok(policies);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var policy = await _policyService.GetByIdAsync(id);
        return Ok(policy);
    }
    [HttpGet("{id}/complete")]
    public async Task<IActionResult> GetCompletePolicy(int id)
    {
        var policy = await _policyService.GetCompletePolicyAsync(id);
        return Ok(policy);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PolicyInputModel policyInput)
    {
        var id = await _policyService.CreateAsync(policyInput);
        var policyView = await _policyService.GetByIdAsync(id); // Retorna o objeto criado
        return CreatedAtAction(nameof(GetById), new { id }, policyView);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] PolicyInputModel policyInput)
    {
        await _policyService.UpdateAsync(id, policyInput);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _policyService.DeleteAsync(id);
        return NoContent();
    }
    #endregion
}
