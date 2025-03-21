﻿using AIPolicy.Application.InputModel;
using AIPolicy.Application.ViewModel;
using AIPolicy.Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AIPolicy.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PolicyController : ControllerBase
{
    [HttpPost]
    public ActionResult Post(PolicyInputModel policyInputModel)
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var policy = new Policy
        {
            Id = id,
            Version = 1,
            Triggers = new List<Trigger>(),
            LastChange = DateTime.Now
        };

        var policyViewModel = new PolicyViewModel();
        return Ok();
    }
}
