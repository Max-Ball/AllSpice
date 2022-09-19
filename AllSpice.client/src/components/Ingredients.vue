<template>
  <div class="my-2">
    {{ingredient.name}}
    {{ingredient.quantity}}
    <div v-if="recipe.creatorId == account.id">
      <div class="dropdown">
        <i class="mdi mdi-dots-horizontal f-20" type="button" data-bs-toggle="dropdown" aria-expanded="false">
        </i>
        <ul class="dropdown-menu">
          <li>
            <i class="mdi mdi-delete fs-5 dropdown-item selectable" @click="deleteIngredient()">
              Delete Ingredient</i>
          </li>
          <li>
            <i class="mdi mdi-pencil fs-5 dropdown-item selectable" @click="setIngredient(ingredient)">
              Edit Ingredient
            </i>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>



<script>
import { popper } from '@popperjs/core';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { ingredientsService } from '../services/IngredientsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
  props: {
    ingredient: { type: Object, required: true }
  },

  setup(props) {

    return {
      recipe: computed(() => AppState.activeRecipe),
      account: computed(() => AppState.account),

      async deleteIngredient() {
        try {
          const yes = await Pop.confirm("Are you sure you want to delete this ingredient?")
          if (!yes) {
            return
          }
          await ingredientsService.deleteIngredient(props.ingredient.id)
        } catch (error) {
          logger.log('[deleting ingredient]', error)
          Pop.error(error)
        }
      },

      setIngredient(ingredient) {
        ingredientsService.setIngredient(ingredient)
      }
    };
  },
};
</script>



<style>

</style>