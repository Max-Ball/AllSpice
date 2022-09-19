import { AppState } from "../AppState";
import { api } from "./AxiosService";

class IngredientsService {

  async getIngredients(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}/ingredients`)
    AppState.ingredients = res.data
  }

  async addIngredient(newIngredient, id) {
    const res = await api.post(`/api/recipes/${id}/ingredients`, newIngredient)
    AppState.ingredients.push(res.data)
  }

  async editIngredient(ingredientData) {
    const res = await api.put(`/api/ingredients/${ingredientData.id}`, ingredientData)
    AppState.ingredients = res.data
  }

  setIngredient(ingredient) {
    AppState.activeIngredient = ingredient
  }

  async deleteIngredient(id) {
    await api.delete(`/api/ingredients/${id}`)
    AppState.ingredients = AppState.ingredients.filter(i => i.id != id)
  }
}

export const ingredientsService = new IngredientsService();