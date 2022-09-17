using System;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class InstructionsController : ControllerBase
  {
    private readonly InstructionsService _instService;

    public InstructionsController(InstructionsService instService)
    {
      _instService = instService;
    }

    [HttpGet("{id}")]
    public ActionResult<Instruction> GetById(int id)
    {
      try
      {
        Instruction instruction = _instService.GetById(id);
        return Ok(instruction);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Instruction>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string message = _instService.Delete(userInfo, id);
        return Ok(message);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Instruction>> Update(int id, [FromBody] Instruction update)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.Id = id;
        return Ok(_instService.Update(userInfo, update));
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

  }
}