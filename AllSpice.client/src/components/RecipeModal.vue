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
                  <div class="card">
                    <div class="card-body">
                      <h5 class="card-title">Recipe Steps</h5>
                      <span class="card-text">
                        instructions1
                        instructions2
                        instructions3
                      </span>
                    </div>
                  </div>
                </div>
                <div class="col-md-6">
                  Card1
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
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { logger } from '../utils/Logger';
import { ingredientsService } from '../services/IngredientsService'
import Pop from '../utils/Pop';
import { onMounted } from 'vue';

export default {
  props: {
    recipe: { type: Object, required: true }
  },

  setup(props) {
    async function getIngredients() {
      try {
        await ingredientsService.getIngredients(props.recipe.id);
      } catch (error) {
        logger.error('[getting ingredients]', error)
        Pop.error(error)
      }
    }

    onMounted(() => {
      getIngredients();
    })
    return {
      recipe: computed(() => AppState.activeRecipe)
    };
  },
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
</style>