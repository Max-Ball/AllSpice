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
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _recipesService;
    private readonly IngredientsService _ingredientsService;
    private readonly InstructionsService _instructionsService;

    public RecipesController(RecipesService recipesService, IngredientsService ingredientsService, InstructionsService instructionsService)
    {
      _recipesService = recipesService;
      _ingredientsService = ingredientsService;
      _instructionsService = instructionsService;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll()
    {
      try
      {
        List<Recipe> recipe = _recipesService.GetAll();
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        Recipe recipe = _recipesService.GetById(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetRecipeIngredients(int recipeId)
    {
      try
      {
        List<Ingredient> ingredients = _ingredientsService.GetRecipeIngredients(recipeId);
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{recipeId}/instructions")]
    public ActionResult<List<Instruction>> GetRecipeInstructions(int recipeId)
    {
      try
      {
        List<Instruction> instructions = _instructionsService.GetRecipeInstructions(recipeId);
        return Ok(instructions);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = userInfo.Id;
        Recipe recipe = _recipesService.Create(newRecipe);
        recipe.Creator = userInfo;
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("{recipeId}/ingredients")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> CreateIngredients(int recipeId, [FromBody] Ingredient newIngredient)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newIngredient.RecipeId = recipeId;
        return Ok(_ingredientsService.CreateIngredients(userInfo, newIngredient));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("{recipeId}/instructions")]
    public async Task<ActionResult<Instruction>> CreateInstructions(int recipeId, [FromBody] Instruction newInstruction)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newInstruction.RecipeId = recipeId;
        return Ok(_instructionsService.CreateInstructions(userInfo, newInstruction));
      }
      catch (Exception e)
      {
        throw new Exception(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string message = _recipesService.Delete(userInfo, id);
        return Ok(message);
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Update(int id, [FromBody] Recipe update)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.CreatorId = userInfo.Id;
        update.Id = id;
        return Ok(_recipesService.Update(userInfo, update));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }



}