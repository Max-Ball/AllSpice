<template>
  <div class="modal fade" id="recipe-modal" tabindex="-1" aria-labelledby="recipe-modal" aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-4 p-0">
              <img :src="recipe.picture" class="img-fluid round-side" alt="...">
            </div>
            <div class="col-md-8">
              <div class="row align-items-center">
                <div class="col-md-12 d-flex justify-content-between">
                  <span v-if="!isFavorite">
                    <i class="mdi mdi-heart-outline fs-3 selectable" @click="addToFavorites()"></i>
                  </span>
                  <span v-else>
                    <i class="mdi mdi-heart fs-3 selectable" @click="removeFromFavorites()"></i>
                  </span>
                  <span v-if="recipe.creatorId == account.id">
                    <i class="mdi mdi-delete fs-3 selectable" @click="deleteRecipe()"></i>
                  </span>
                </div>
                <div class="fs-2">
                  {{recipe.title}}
                  <span class="fs-6 glass px-2 py-1 rounded-pill">
                    {{recipe.category}}
                  </span>
                </div>
                <div class="fs-4">
                  {{recipe.subtitle}}
                </div>
                <div class="col-md-6">
                  <div class="card my-3">
                    <div class="card-body">
                      <h5 class="card-title">Recipe Steps</h5>
                      <span class="card-text">
                        <div v-for="i in instructions" :key="i.id">
                          <Instructions :instruction="i" />
                        </div>
                      </span>
                      <div>
                        <form v-if="recipe.creatorId == account.id" @submit.prevent="addOrEditInstruction()">
                          <input type="text" placeholder="Add instructions..." class="form-control"
                            v-model="editable.body" required><br>
                          <input type="number" class="form-control" placeholder="Add number of step..."
                            v-model="editable.step" required>
                          <div class="mt-2">
                            <button class="btn btn-primary w-100">Add Step<i class="mdi mdi-plus"></i></button>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="card my-3">
                    <div class="card-body">
                      <h5 class="card-title">Recipe Ingredients</h5>
                      <span class="card-text">
                        <div v-for="i in ingredients" :key="i.id">
                          <Ingredients :ingredient="i" />
                        </div>
                      </span>
                    </div>
                    <form v-if="recipe.creatorId == account.id" @submit.prevent="addOrEditIngredient()">
                      <input type="text" placeholder="Add ingredient..." class="form-control" v-model="formEdit.name"
                        required>
                      <input type="text" placeholder="Add quantity..." class="form-control my-3"
                        v-model="formEdit.quantity" required>
                      <button class="btn btn-primary w-100"><i class="mdi mdi-plus"></i></button>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

      </div>
    </div>
  </div>
</template>



<script>
import { computed, ref } from '@vue/reactivity';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import { ingredientsService } from '../services/IngredientsService'
import Pop from '../utils/Pop';
import { watchEffect } from 'vue';
import Instructions from './Instructions.vue';
import Ingredients from './Ingredients.vue';
import { instructionsService } from '../services/InstructionsService';
import { recipesService } from '../services/RecipesService';


export default {
  setup() {
    const editable = ref({})
    const formEdit = ref({})

    watchEffect(() => {
      if (!AppState.activeInstruction) {
        return
      } editable.value = { ...AppState.activeInstruction }

      if (!AppState.activeIngredient) {
        return
      } formEdit.value = { ...AppState.activeIngredient }
    })
    return {
      editable,
      formEdit,
      recipe: computed(() => AppState.activeRecipe),
      instructions: computed(() => AppState.instructions),
      ingredients: computed(() => AppState.ingredients),
      account: computed(() => AppState.account),
      isFavorite: computed(() => {
        if (AppState.recipes.find(r => r.id == AppState.activeRecipe.id)) {
          return true
        }
        return false

      }),

      async deleteRecipe() {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this recipe?')
          const id = AppState.activeRecipe.id
          if (!yes) {
            return
          }
          await recipesService.deleteRecipe(id)
        } catch (error) {
          logger.error('[deleting recipe]', error)
          Pop.error(error)
        }
      },

      async addOrEditInstruction() {
        try {

          const id = AppState.activeRecipe.id
          console.log(editable.value);
          if (editable.value.id) {
            await instructionsService.editInstruction(editable.value)
          } else {
            await instructionsService.addInstruction(editable.value, id)
          }
        } catch (error) {
          logger.error('[adding instruction]', error)
          Pop.error(error)
        }
      },

      async addOrEditIngredient() {
        try {
          const id = AppState.activeRecipe.id
          debugger
          if (formEdit.value.id) {
            await ingredientsService.editIngredient(formEdit.value)
          } else {
            await ingredientsService.addIngredient(formEdit.value, id)
          }
        } catch (error) {
          logger.error('[adding ingredient]', error)
          Pop.error(error)
        }
      },

      async addToFavorites() {
        try {
          let newFavoriteRecipe = {
            recipeId: AppState.activeRecipe.id,
            profileId: AppState.account.id
          }
          await recipesService.addToFavorites(newFavoriteRecipe)
        } catch (error) {
          logger.log('[adding to favorites]')
          Pop.error(error)
        }
      },

      async removeFromFavorites() {
        try {
          const yes = await Pop.confirm('Are you sure you want to remove this from your favorites?')
          if (!yes) {
            return
          }
          await recipesService.removeFromFavorites(AppState.activeRecipe.id)
        } catch (error) {
          logger.error('[removing from favorites]', error)
          Pop.error(error)
        }
      }
    };
  },
  components: { Instructions, Ingredients }
};
</script>



<style scoped lang="scss">
.modal-header {
  background-color: #6da2a8;
}

.round-side {
  border-top-left-radius: 1%;
  border-bottom-left-radius: 1%;
}

.glass {
  background-color: rgb(216, 239, 240, );
  color: rgb(16, 17, 17);
  width: 100%;
}

.card {
  min-height: 60-vh;
}
</style>