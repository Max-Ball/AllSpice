<template>
  <div class="modal fade" id="create-recipe-modal" tabindex="-1" aria-labelledby="create-recipe-modalLabel"
    aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="modal-title" id="create-recipe-modalLabel">New Recipe</h3>
        </div>
        <div class="modal-body row">
          <form @submit.prevent="createRecipe()">
            <div class="col-md-12">
              <div class="row">
                <div class="col-md-6">
                  <label for="title">Title</label>
                  <input class="form-control" type="text" placeholder="Title..." v-model="editable.title" required>
                </div>
                <div class="col-md-6">
                  <label for="category">Category</label>
                  <select name="category" class="form-control selectable" v-model="editable.category" required>
                    <option value="">Choose a category below...</option>
                    <option value="breakfast">Breakfast</option>
                    <option value="lunch">Lunch</option>
                    <option value="dinner">Dinner</option>
                    <option value="appetizer">Appetizer</option>
                    <option value="salad">Salad</option>
                    <option value="main-course">Main-course</option>
                    <option value="side-dish">Side-dish</option>
                    <option value="baked-goods">Baked-goods</option>
                  </select>
                </div>
              </div>
            </div>
            <div class="col-md-12 my-3">
              <label for="details">Details</label>
              <input type="text" class="form-control" placeholder="Details..." v-model="editable.subtitle" required>
              <small class="form-text text-muted px-1">A brief description of the recipe</small>
            </div>
            <div class="col-md-12 my-3">
              <label for="image">Image</label>
              <input type="text" class="form-control" placeholder="Add an image..." v-model="editable.picture" required>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              <button type="submit" class="btn btn-primary" data-bs-dismiss="modal">Add new recipe</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>



<script>
import { ref } from 'vue';
import { recipesService } from '../services/RecipesService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {

  setup() {
    const editable = ref({})

    return {
      editable,

      async createRecipe() {
        try {

          await recipesService.createRecipe(editable.value)
          Pop.toast('Recipe Created!')

        } catch (error) {
          logger.error('[creating recipe]', error)
          Pop.error(error)
        }
      }
    };
  },
};
</script>



<style scoped lang="scss">
.modal-header {
  background-color: #6da2a8;
}
</style>