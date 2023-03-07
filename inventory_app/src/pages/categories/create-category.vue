<template>
  <q-page>
    <div>Create Category</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.categoryName"
            label="Category name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.categoryDescription"
            label="Category description"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/categories')"
            flat
            class="q-ml-sm"
          />
        </div>
      </q-form>
    </div>
    {{ selectedObject }}
  </q-page>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "CreateCategory",
  data() {
    return {
      input: {
        categoryName: null,
        categoryDescription: null,
      },
    };
  },
  mounted() {
    if (this.selectedObject) {
      this.input.categoryName = this.selectedObject.categoryName;
      this.input.categoryDescription = this.selectedObject.categoryDescription;
    }
  },
  methods: {
    async onSubmit() {
      let category = {
        CategoryName: this.input.categoryName,
        CategoryDescription: this.input.categoryDescription,
      };
      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/Category/${this.selectedObject.categoryId}`,
            category
          );
          this.$router.push("/categories");
        } else {
          await this.$axios.post(
            `https://localhost:7221/api/Category`,
            category
          );
          this.$router.push("/categories");
        }
      } catch (error) {}
    },
  },
  computed: {
    ...mapGetters(["selectedObject"]),
  },
};
</script>

<style></style>
