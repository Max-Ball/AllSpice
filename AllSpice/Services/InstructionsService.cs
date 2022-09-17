using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class InstructionsService
  {
    private readonly InstructionsRepository _instRepo;
    private readonly RecipesService _recipesService;

    public InstructionsService(InstructionsRepository instRepo, RecipesService recipesService)
    {
      _instRepo = instRepo;
      _recipesService = recipesService;
    }

    internal List<Instruction> GetRecipeInstructions(int recipeId)
    {
      List<Instruction> instructions = _instRepo.GetRecipeInstructions(recipeId);
      if (instructions == null)
      {
        throw new Exception("This recipe doesn't have any instructions");
      }
      return instructions;
    }

    internal Instruction GetById(int id)
    {
      Instruction instruction = _instRepo.GetById(id);
      if (instruction == null)
      {
        throw new Exception("No instructions by that Id");
      }
      return instruction;
    }

    internal object CreateInstructions(Account userInfo, Instruction newInstruction)
    {
      Recipe recipe = _recipesService.GetById(newInstruction.RecipeId);
      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not recipe to add instructions to");
      }
      return _instRepo.CreateInstructions(newInstruction);
    }

    internal string Delete(Account userInfo, int id)
    {
      Instruction instruction = GetById(id);
      Recipe recipe = _recipesService.GetById(instruction.RecipeId);
      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to delete instructions from");
      }
      _instRepo.Delete(id);
      return $"This instruction has been deleted";
    }

    internal Instruction Update(Account userInfo, Instruction update)
    {
      Instruction original = GetById(update.Id);
      original.Step = update.Step ?? original.Step;
      original.Body = update.Body ?? original.Body;

      Recipe recipe = _recipesService.GetById(original.RecipeId);

      if (userInfo.Id != recipe.CreatorId)
      {
        throw new Exception("This is not your recipe to edit instructions");
      }
      return _instRepo.Update(original);

    }
  }
}