import { AppState } from "../AppState";
import { api } from "./AxiosService";

class IngredientsService {

  async getIngredients(recipeId) {
    const res = await api.get(`/api/recipes/${recipeId}/ingredients`)
    AppState.ingredients = res.data
  }
}

export const ingredientsService = new IngredientsService();