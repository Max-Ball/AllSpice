import { AppState } from "../AppState";
import { api } from "./AxiosService";

class RecipesService {

  async getRecipes() {
    const res = await api.get('/api/recipes')
    AppState.recipes = res.data
    console.log(res.data);
  }

  async getRecipeById(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}`)
    AppState.activeRecipe = res.data
  }

  async createRecipe(newRecipe) {
    const res = await api.post('/api/recipes', newRecipe)
    AppState.recipes.push(res.data)
  }
}

export const recipesService = new RecipesService();