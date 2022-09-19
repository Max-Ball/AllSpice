<template>
  <div class="my-2">
    {{instruction.step}}
    {{instruction.body}}
    <div v-if="recipe.creatorId == account.id">
      <i class="mdi mdi-delete selectable" @click="deleteInstruction()"></i>
      <i class="mdi mdi-pencil fs-5 dropdown-item selectable" @click="adjustInstructions(instruction)"></i>
    </div>
  </div>
</template>



<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import { instructionsService } from '../services/InstructionsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
  props: {
    instruction: { type: Object, required: true }
  },
  setup(props) {

    return {
      recipe: computed(() => AppState.activeRecipe),
      account: computed(() => AppState.account),

      async deleteInstruction() {
        try {
          const yes = await Pop.confirm('Are you sure you want to delete this instruction?')
          if (!yes) {
            return
          }
          await instructionsService.deleteInstruction(props.instruction.id)
        } catch (error) {
          logger.log('[deleting instruction]', error)
          Pop.error(error)
        }
      },

      adjustInstructions(instruction) {
        instructionsService.setActiveInstruction(instruction)
      }
    };
  },
};
</script>



<style>

</style>