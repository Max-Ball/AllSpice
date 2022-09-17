<template>
  <div class="row">
    <div class="col-md-12">
      <div class="banner-img rounded elevation-5">
        <div class="row">
          <div class="col-md-5 px-4 py-3">
            <form class="input-group">
              <input type="text" class="form-control" placeholder="Search Recipes">
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
          <div class="row justify-content-center align-items-end">
            <div class="col-md-6">
              <div class="filter-menu d-flex align-items-center text-center elevation-5">
                <div class="selectable filter-btn fb-left d-flex align-items-center justify-content-center">
                  HOME
                </div>
                <div class="selectable filter-btn d-flex align-items-center justify-content-center">
                  MY RECIPES
                </div>
                <div class="selectable filter-btn fb-right d-flex align-items-center justify-content-center">
                  FAVORITES
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
import Login from '../components/Login.vue';
export default {
  name: "Home",
  components: { Login }
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
