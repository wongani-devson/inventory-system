<template>
  <q-page>
    <div>Create Product</div>
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
            label="Reset"
            type="reset"
            color="primary"
            flat
            class="q-ml-sm"
          />
        </div>
      </q-form>
    </div>
  </q-page>
</template>

<script>
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
  methods: {
    async onSubmit() {
      let category = {
        CategoryName: this.input.categoryName,
        CategoryDescription: this.input.categoryDescription,
      };

      await this.$axios.post(`https://localhost:7221/api/Category`, category);
    },
  },
};
</script>

<style></style>
