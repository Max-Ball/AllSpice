using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly FavoritesService _favoritesService;

    public AccountController(AccountService accountService, FavoritesService favoritesService)
    {
      _accountService = accountService;
      _favoritesService = favoritesService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{recipes}")]
    [Authorize]
    public async Task<ActionResult<List<FavoritesRecipe>>> GetFavorites()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string profileId = userInfo.Id;
        List<FavoritesRecipe> favoriteRecipes = _favoritesService.GetAllFavorites(profileId);
        return Ok(favoriteRecipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }



}