import { store } from "quasar/wrappers";
import { createStore } from "vuex";
import axios from "axios";

// import example from './module-example'

/*
 * If not building with SSR mode, you can
 * directly export the Store instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Store instance.
 */

// export default store(function (/* { ssrContext } */) {
export default createStore({
  modules: {
    // example
  },
  state() {
    return {
      selectedObject: null,
      user: null,
      token: null,
    };
  },
  mutations: {
    setSelectedObject(state, payload) {
      state.selectedObject = payload;
    },
    login(state, user) {
      state.isLoggedIn = true;
      state.user = user;
    },
    logout(state) {
      state.isLoggedIn = false;
      state.user = null;
    },
    setToken(state, token) {
      state.token = token;
    },
    setUser(state, user) {
      state.user = user;
    },
    logout(state) {
      state.token = null;
      state.user = null;
    },
  },
  actions: {
    setSelectedOjbect({ commit }, data) {
      commit("setSelectedObject", data);
    },
    async login({ commit }, user) {
      // Make a request to the backend API to authenticate the user
      try {
        const response = await axios.post(
          "https://jsonplaceholder.typicode.com/todos",
          user
        );
        const todos = response.data;
        commit("setTodos", todos);
      } catch (error) {
        console.error(error);
      }

      // If authentication is successful, update the state
      commit("login", user);
    },
    logout({ commit }) {
      // Make a request to the backend API to log out the user
      // ...

      // Update the state
      commit("logout");
    },
  },
  getters: {
    selectedObject(state) {
      return state.selectedObject;
    },
    isLoggedIn: (state) => {
      return state.token !== null;
    },
    user: (state) => state.user,
  },

  // enable strict mode (adds overhead!)
  // for dev mode and --debug builds only
  strict: process.env.DEBUGGING,
});

// return Store;
// });
