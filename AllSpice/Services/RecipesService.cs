using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _RecipesRepo;

    public RecipesService(RecipesRepository recipesRepo)
    {
      _RecipesRepo = recipesRepo;

    }

    internal List<Recipe> GetAll()
    {
      return _RecipesRepo.GetAll();
    }
    internal Recipe GetById(int id)
    {
      Recipe recipe = _RecipesRepo.GetById(id);
      if (recipe == null)
      {
        throw new Exception("no recipe by that id");
      }
      return recipe;
    }

    internal Recipe Create(Recipe newRecipe)
    {
      return _RecipesRepo.Create(newRecipe);
    }

    internal string Delete(Account userInfo, int id)
    {
      Recipe recipe = GetById(id);

      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to delete");
      }
      _RecipesRepo.Delete(id);
      return $"Deleted the {recipe.Title}";

    }

    internal Recipe Update(Account userInfo, Recipe update)
    {
      Recipe original = GetById(update.Id);
      original.Title = update.Title ?? original.Title;
      original.Subtitle = update.Subtitle ?? original.Subtitle;
      original.Picture = update.Picture ?? original.Picture;
      original.Category = update.Category ?? original.Category;

      if (userInfo.Id != update.CreatorId)
      {
        throw new Exception("This is not your recipe to delete");
      }

      return _RecipesRepo.Update(original);
    }

    internal FavoritesRecipe GetFavoriteRecipeById(int recipeId)
    {
      FavoritesRecipe favoritesRecipe = _RecipesRepo.GetFavoriteRecipe(recipeId);
      if (favoritesRecipe == null)
      {
        throw new Exception("no favorite recipes by that id");
      }
      return favoritesRecipe;
    }
  }
}