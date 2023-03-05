<template>
  <q-page>
    <div>Create Product</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" @reset="onReset" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.supplierName"
            label="Supplier name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.contactDetail"
            label="Contact detail"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="create_product.unitPrice"
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
            v-model="create_product.stockQuantity"
            label="Stock quanity"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <!-- <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.Password"
            label="Your name *"
            hint="Name and surname"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="name"
            label="Your name *"
            hint="Name and surname"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div> -->
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
      input: {
        supplierName: null,
        contactDetail: null,
      },
    };
  },
  async mounted() {
    const result = await this.$axios.get(
      `https://localhost:7221/api/OrderType`
    );
    console.log("result", result);
  },
};
</script>

<style></style>
