<template>
  <div class="row justify-content-center">
    <div class="col-md-12">
      <div class="banner-img rounded elevation-5">
        <div class="row">
          <div class="col-md-5 px-4 py-3">
            <form class="input-group" @submit.prevent="search()">
              <input type="text" class="form-control" placeholder="Search Recipes" v-model="query" required>
              <button class="btn cs-btn">
                <i class="mdi mdi-magnify text-light"></i>
              </button>
            </form>
          </div>
          <div class="col-md-7 d-flex justify-content-end p-2">
            <Login />
          </div>
          <div class="row justify-content-center align-items-center">
            <div class="col-md-6">
              <h1>
                ALLSPICE
              </h1>
              <h3>
                Cherish Your Family<br>
                And Their Cooking
              </h3>
            </div>
          </div>
          <div class="row align-items-end">
            <div class="col-md-3">
              <div class="mx-3 my-2">
                <button type="button" class="btn add-btn" data-bs-toggle="modal" data-bs-target="#create-recipe-modal">
                  <i class="mdi mdi-plus fs-2"></i>
                </button>
                <CreateRecipeModal />
              </div>
            </div>
            <div class="col-md-6">
              <div class="filter-menu d-flex align-items-center text-center elevation-5">
                <div class="selectable filter-btn fb-left d-flex align-items-center justify-content-center"
                  @click="getRecipes()">
                  HOME
                </div>
                <div class="selectable filter-btn d-flex align-items-center justify-content-center"
                  @click="getUserRecipes()">
                  MY RECIPES
                </div>
                <div class="selectable filter-btn fb-right d-flex align-items-center justify-content-center"
                  @click="getFavorites()">
                  FAVORITES
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-11 my-5">
      <div class="row justify-content-evenly">
        <div class="col-md-3 col-6 my-3" v-for="r in recipes" :key="r.id">
          <Recipe :recipe="r" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Login from '../components/Login.vue';
import { logger } from '../utils/Logger';
import { recipesService } from '../services/RecipesService'
import Pop from '../utils/Pop';
import { ref, onMounted } from 'vue';
import Recipe from '../components/Recipe.vue';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState';
import CreateRecipeModal from '../components/CreateRecipeModal.vue';
export default {
  setup() {
    const query = ref('')

    async function getRecipes() {
      try {
        await recipesService.getRecipes();
        console.log(AppState.favoriteRecipes);
      } catch (error) {
        logger.error('[getting recipes]', error)
        Pop.error(error)
      }
    }

    onMounted(() => {
      getRecipes();
    })
    return {
      getRecipes,
      query,
      recipes: computed(() => AppState.recipes),
      favoriteRecipes: computed(() => AppState.favoriteRecipes),

      async search() {
        try {
          await recipesService.search(query.value)
        } catch (error) {
          logger.error('[searching]', error)
          Pop.error(error)
        }
      },

      async getFavorites() {
        try {
          await recipesService.getFavorites()
        } catch (error) {
          logger.error('[getting favorites]', error)
          Pop.error(error)
        }
      },

      async getUserRecipes() {
        try {
          await recipesService.getUserRecipes()
          console.log(AppState.recipes);
        } catch (error) {
          logger.error('[getting user recipes]', error)
          Pop.error(error)
        }
      }


    }
  },
  components: { Login, Recipe, CreateRecipeModal }
}
</script>

<style scoped lang="scss">
.banner-img {
  min-height: 50vh;
  background-position: 0% 60%;
  background-size: cover;
  display: grid;
  background-image: url(https://images.unsplash.com/photo-1515003197210-e0cd71810b5f?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=870&q=80);
  margin-top: 1em;
}

.filter-menu {
  transform: translateY(24px);
  border-radius: 5px;
  font-weight: 700;
  background-color: #d8eff0;
}

.filter-menu:hover {
  border-radius: 5px;
}

.filter-btn {
  height: 3em;
  width: 100%;
  // border-radius: 5px;
}

.add-btn {
  background-color: #d8eff0;
  padding-left: 3%;
  padding-right: 3%;
  border-radius: 50%;
}

.fb-left:hover {
  border-top-left-radius: 5px;
  border-bottom-left-radius: 5px;
}

.fb-right:hover {
  border-top-right-radius: 5px;
  border-bottom-right-radius: 5px;
}

.filter-btn:hover {
  background-color: #6da2a8;
  color: #d8eff0;
}

.cs-btn {
  background-color: #6da2a8;
}
</style>
