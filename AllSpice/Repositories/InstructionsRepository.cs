using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AllSpice.Models;
using Dapper;

namespace AllSpice.Repositories
{
  public class InstructionsRepository
  {
    private readonly IDbConnection _db;

    public InstructionsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Instruction> GetRecipeInstructions(int recipeId)
    {
      string sql = @"
      SELECT
      i.*,
      r.*
      FROM instructions i
      JOIN recipes r ON r.id = i.recipeId
      WHERE i.recipeId = @recipeId;
      ";
      List<Instruction> instructions = _db.Query<Instruction, Recipe, Instruction>(sql, (instruction, recipe) =>
      {
        instruction.RecipeId = recipe.Id;
        return instruction;
      }, new { recipeId }).ToList();
      return instructions;
    }

    internal Instruction GetById(int id)
    {
      string sql = @"
      SELECT *
      FROM instructions
      WHERE id = @id;
      ";
      Instruction instruction = _db.Query<Instruction>(sql, new { id }).FirstOrDefault();
      return instruction;
    }

    internal Instruction CreateInstructions(Instruction newInstruction)
    {
      string sql = @"
      INSERT INTO instructions
      (step, body, recipeId)
      VALUES
      (@step, @body, @recipeId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newInstruction);
      newInstruction.Id = id;
      return newInstruction;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM instructions
      WHERE id = @id;
      ";
      _db.Execute(sql, new { id });
    }

    internal Instruction Update(Instruction original)
    {
      string sql = @"
      UPDATE instructions SET
      step = @step,
      body = @body
      WHERE id = @id;
      ";

      int rowsAffected = _db.Execute(sql, original);
      if (rowsAffected == 0)
      {
        throw new Exception("unable to edit this ingredient");
      }
      return original;
    }
  }
}