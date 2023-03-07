<template>
  <q-page>
    <div>Create Product</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
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
            label="Stock quantity"
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
            label="Categories"
            option-label="categoryName"
            option-value="categoryId"
            map-options
            stack-label
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.supplier"
            :options="suppliers"
            label="Supplier"
            option-label="supplierName"
            option-value="supplierId"
            stack-label
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/products')"
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
    if (this.selectedObject) {
      this.input.productName = this.selectedObject.productName;
      this.input.productDescription = this.selectedObject.productDescription;
      this.input.unitPrice = this.selectedObject.unitPrice;
      this.input.stockQuantity = this.selectedObject.stockQuantity;
      this.input.category = this.selectedObject.category;
      this.input.supplier = this.selectedObject.supplier;
    }
  },
  methods: {
    async onSubmit() {
      let product = {
        ProductName: this.input.productName,
        ProductDescription: this.input.productDescription,
        UnitPrice: this.input.unitPrice,
        StockQuantity: this.input.stockQuantity,
        CategoryId: this.input.category.categoryId,
        SupplierId: this.input.supplier.supplierId,
      };

      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/Product/${this.selectedObject.productId}`,
            product
          );
          this.$router.push("/products");
        } else {
          await this.$axios.post(`https://localhost:7221/api/Product`, product);
          this.$router.push("/products");
        }
      } catch (error) {}
    },
  },
  async created() {
    const categoriesResult = await this.$axios.get(
      `https://localhost:7221/api/Category`
    );
    this.categories = categoriesResult.data.$values;
    const suppliersResult = await this.$axios.get(
      `https://localhost:7221/api/Supplier`
    );
    this.suppliers = suppliersResult.data.$values;
    console.log("supplier", this.suppliers);
  },
  computed: {
    ...mapGetters(["selectedObject"]),
  },
};
</script>

<style></style>
