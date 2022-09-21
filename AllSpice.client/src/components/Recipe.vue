<template>
  <div class="card text-bg-dark elevation-4 selectable" data-bs-toggle="modal" data-bs-target="#recipe-modal"
    @click="getRecipeById()">
    <img :src="recipe.picture" class="card-img recipe-pic" alt="...">
    <div class="card-img-overlay p-0 d-flex flex-column justify-content-between">
      <div class="row justify-content-between">
        <div class="col-md-12 d-flex justify-content-between align-items-center">
          <div class="m-3">
            <span class="fs-6 glass px-2 py-1 rounded-pill">
              {{recipe.category}}
            </span>
          </div>
        </div>
      </div>
      <div class="glass rounded px-2 py-1 elevation-5">
        <span class="card-title fs-4">{{recipe.title}}</span><br>
        <span class="card-text">{{recipe.subtitle}}</span>
      </div>
    </div>
  </div>
  <RecipeModal />
</template>

<script>
import { recipesService } from '../services/RecipesService';
import { ingredientsService } from '../services/IngredientsService';
import { instructionsService } from '../services/InstructionsService'
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import RecipeModal from './RecipeModal.vue';
import { AppState } from '../AppState';

export default {
  props: {
    recipe: { type: Object, required: true },
  },
  setup(props) {
    return {
      async getRecipeById() {
        try {
          await recipesService.getRecipeById(props.recipe.id)
          this.getIngredients()
          this.getInstructions()
        } catch (error) {
          logger.error('[getting recipe by id]', error)
          Pop.error(error)
        }
      },

      async getIngredients() {
        try {
          await ingredientsService.getIngredients(props.recipe.id);
        } catch (error) {
          logger.error('[getting ingredients]', error)
          Pop.error(error)
        }
      },

      async getInstructions() {
        try {
          await instructionsService.getInstructions(props.recipe.id)
        } catch (error) {
          logger.error('[getting instructions]')
          Pop.error(error)
        }
      }
    };
  },
  components: { RecipeModal }
};
</script>



<style scoped lang="scss">
.recipe-pic {
  height: 40vh;
  background-position: center;
  background-size: cover;
  display: grid;

}

.glass {
  background-color: rgb(216, 239, 240, );
  color: rgb(16, 17, 17);
  width: 100%;
}
</style>