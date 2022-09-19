using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class FavoritesService
  {
    private readonly FavoritesRepository _favoritesRepo;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoritesRepository favoritesRepo, AccountService accountService, RecipesService recipesService)
    {
      _favoritesRepo = favoritesRepo;
      _recipesService = recipesService;
    }

    internal List<FavoritesRecipe> GetAllFavorites(string profileId)
    {
      return _favoritesRepo.GetAllFavorites(profileId);
    }

    internal FavoritesRecipe Create(Favorites newFavoriteRecipe, string userId)
    {
      if (newFavoriteRecipe.profileId != userId)
      {
        throw new Exception("You cannot add this to another users favorites");
      }

      int id = _favoritesRepo.Create(newFavoriteRecipe);

      FavoritesRecipe favoritesRecipe = _recipesService.GetFavoriteRecipeById(newFavoriteRecipe.recipeId);

      favoritesRecipe.FavoritesRecipeId = id;
      return favoritesRecipe;
    }

    internal string Delete(int id)
    {
      FavoritesRecipe favoritesRecipe = _recipesService.GetFavoriteRecipeById(id);
      _favoritesRepo.Delete(id);
      return $"{favoritesRecipe.Title} has been removed from favorites";
    }
  }


}