<template>
  <q-page>
    <div>Create Product</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" @reset="onReset" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.productName"
            label="Product name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.productDescription"
            label="Produt description"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.unitPrice"
            label="Unit price"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.stockQuantity"
            label="Stock quanity"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.category"
            :options="categories"
            label="Label (stacked)"
            stack-label
            :dense="dense"
            :options-dense="denseOpts"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.supplier"
            :options="suppliers"
            label="Label (stacked)"
            stack-label
            :dense="dense"
            :options-dense="denseOpts"
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
  name: "CreateProduct",
  data() {
    return {
      suppliers: [],
      categories: [],
      input: {
        productName: null,
        productDescription: null,
        unitPrice: null,
        stockQuantity: null,
        category: null,
        supplier: null,
      },
    };
  },
  async mounted() {
    const result = await this.$axios.get(
      `https://localhost:7221/api/OrderType`
    );
    console.log("result", result);
  },
  methods: {
    onSubmit() {
      let p;
    },
  },
  async created() {},
};
</script>

<style></style>
