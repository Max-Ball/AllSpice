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

  async deleteRecipe(id) {
    await api.delete(`/api/recipes/${id}`)
    AppState.recipes = AppState.recipes.filter(r => r.id != id)
  }

  async addToFavorites(newFavoriteRecipe) {
    const res = await api.post('/api/favorites', newFavoriteRecipe)
    AppState.favoriteRecipes.push(res.data)
    AppState.isFavorite = false;
    console.log(AppState.favoriteRecipes);
  }

  async getFavorites() {
    debugger
    const res = await api.get('/account/recipes')
    AppState.favoriteRecipes = res.data
  }

}

export const recipesService = new RecipesService();