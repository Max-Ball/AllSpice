using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _IngredientsRepo;
    private readonly RecipesService _recipesService;

    public IngredientsService(IngredientsRepository ingredientsRepo, RecipesService recipesService)
    {
      _IngredientsRepo = ingredientsRepo;
      _recipesService = recipesService;
    }

    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
      List<Ingredient> ingredients = _IngredientsRepo.GetRecipeIngredients(recipeId);
      if (ingredients == null)
      {
        throw new Exception("This recipe doesn't have any ingredients");
      }
      return ingredients;
    }

    internal Ingredient GetById(int id)
    {
      Ingredient ingredient = _IngredientsRepo.GetById(id);
      if (ingredient == null)
      {
        throw new Exception("No ingredient by that ID");
      }
      return ingredient;
    }

    internal Ingredient CreateIngredients(Account userInfo, Ingredient newIngredient)
    {
      Recipe recipe = _recipesService.GetById(newIngredient.RecipeId);
      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to add ingredients to");
      }
      return _IngredientsRepo.CreateIngredients(newIngredient);
    }

    internal Ingredient Update(Account userInfo, Ingredient update)
    {
      Ingredient original = GetById(update.Id);
      original.Name = update.Name ?? original.Name;
      original.Quantity = update.Quantity ?? original.Quantity;

      Recipe recipe = _recipesService.GetById(original.RecipeId);
      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to edit ingredients");
      }
      return _IngredientsRepo.Update(original);
    }

    internal string Delete(Account userInfo, int id)
    {
      Ingredient ingredient = GetById(id);
      Recipe recipe = _recipesService.GetById(ingredient.RecipeId);

      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to delete ingredients from");
      }
      _IngredientsRepo.Delete(id);
      return $"{ingredient.Name} has been deleted";
    }
  }
}