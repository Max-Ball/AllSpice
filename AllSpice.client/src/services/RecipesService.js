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

  async getUserRecipes() {
    const res = await api.get('/api/recipes')
    console.log(res.data);
    AppState.recipes = res.data.filter(r => r.creatorId == AppState.account.id)
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
    console.log(AppState.favoriteRecipes);
  }

  async getFavorites() {
    const res = await api.get('/account/recipes')
    AppState.recipes = res.data
    console.log('favorites', AppState.favoriteRecipes);
    console.log('recipes', AppState.recipes);
  }

  async removeFromFavorites(recipeId) {
    await api.delete(`/api/favorites/${recipeId}`)
    AppState.favoriteRecipes = AppState.favoriteRecipes.filter(f => f.id != recipeId)
  }

  async search(query) {
    debugger
    const res = await api.get('/api/recipes', {
      params: {
        query: query
      }
    })
    AppState.recipes = res.data.filter(r => r.category == query)
  }

}

export const recipesService = new RecipesService();