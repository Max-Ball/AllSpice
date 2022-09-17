using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }

    // internal List<Ingredient> GetRecipeIngredients(int recipeId)
    // {
    //   string sql = @"
    //   SELECT *
    //   FROM ingredients
    //   WHERE recipeId = @recipeId;
    //   ";
    //   List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    //   return ingredients;
    // }

    // TODO Recipe Object is not returning with ingredients
    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
      string sql = @"
      SELECT
      i.*,
      r.*
      FROM ingredients i
      JOIN recipes r ON r.id = i.recipeId
      WHERE i.recipeId = @recipeId;
      ";
      List<Ingredient> ingredients = _db.Query<Ingredient, Recipe, Ingredient>(sql, (ingredient, recipe) =>
      {
        ingredient.RecipeId = recipe.Id;
        return ingredient;
      }, new { recipeId }).ToList();
      return ingredients;
    }

    internal Ingredient CreateIngredients(Ingredient newIngredient)
    {
      string sql = @"
      INSERT INTO ingredients
      (name, quantity, recipeId)
      VALUES
      (@name, @quantity, @recipeId);
      SELECT LAST_INSERT_ID();
        ";
      int id = _db.ExecuteScalar<int>(sql, newIngredient);
      newIngredient.Id = id;
      return newIngredient;
    }

    internal Ingredient Update(Ingredient original)
    {
      string sql = @"
      UPDATE ingredients SET
      name = @name,
      quantity = @quantity
      WHERE id = @id;
      ";

      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected == 0)
      {
        throw new Exception("unable to edit this ingredient");
      }
      return original;
    }

    internal Ingredient GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM ingredients
      WHERE id = @id;
      ";
      Ingredient ingredient = _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
      return ingredient;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM ingredients
      WHERE id = @id
      ";
      _db.Execute(sql, new { id });
    }
  }

}