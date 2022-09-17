using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class FavoritesRepository
  {
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<FavoritesRecipe> GetAllFavorites(string profileId)
    {
      string sql = @"
      SELECT
        f.*,
        r.*,
        a.*
        FROM favorites f
        JOIN recipes r ON f.recipeId = r.id
        JOIN accounts a ON r.creatorId = a.id
        WHERE f.profileId = @profileId;
      ";

      List<FavoritesRecipe> recipes = _db.Query<Favorites, FavoritesRecipe, Account, FavoritesRecipe>(sql, (f, r, a) =>
      {
        r.CreatorId = a.Id;
        r.FavoritesRecipeId = f.Id;
        r.Creator = a;
        return r;
      }, new { profileId }).ToList();
      return recipes;
    }

    internal int Create(Favorites newFavoriteRecipe)
    {
      string sql = @"
      INSERT INTO favorites
      (recipeId, profileId)
      VALUES
      (@recipeId, @profileId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newFavoriteRecipe);
      return id;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM favorites
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }
  }
}