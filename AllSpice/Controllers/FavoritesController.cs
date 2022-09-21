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
  public class FavoritesController : ControllerBase
  {
    private readonly FavoritesService _favoritesService;

    public FavoritesController(FavoritesService favoritesService)
    {
      _favoritesService = favoritesService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<FavoritesRecipe>> Create([FromBody] Favorites newFavoriteRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        FavoritesRecipe recipe = _favoritesService.Create(newFavoriteRecipe, userInfo.Id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        string message = _favoritesService.Delete(id);
        return Ok(message);
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }
  }
}