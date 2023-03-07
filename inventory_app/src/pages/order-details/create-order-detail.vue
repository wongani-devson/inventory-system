<template>
  <q-page>
    <div>Create Order Details</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="input.quantity"
            label="Quantity"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.product"
            :options="products"
            label="Products"
            option-label="productName"
            option-value="productId"
            stack-label
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/order_details')"
            flat
            class="q-ml-sm"
          />
        </div>
      </q-form>
    </div>
  </q-page>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "CreateOrderDetail",
  data() {
    return {
      orders: [],
      products: [],
      input: {
        quantity: null,
        product: null,
      },
    };
  },
  mounted() {
    if (this.selectedObject) {
      this.input.quantity = this.selectedObject.quantity;
      this.input.product = this.selectedObject.product;
    }
  },
  methods: {
    async onSubmit() {
      let orderDetail = {
        Quantity: this.input.quantity,
        ProductId: this.input.product.productId,
      };
      console.log(orderDetail);
      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/OrderDetails${this.selectedObject.orderDetailId}`,
            orderDetail
          );
          this.$router.push("/order_details");
        } else {
          await this.$axios.post(
            `https://localhost:7221/api/OrderDetails`,
            orderDetail
          );
          this.$router.push("/order_details");
        }
      } catch (error) {}
    },
  },
  async created() {
    const productResult = await this.$axios.get(
      `https://localhost:7221/api/Product`
    );

    this.products = productResult.data.$values;

    // const orderResult = await this.$axios.get(
    //   `https://localhost:7221/api/Orders`
    // );
    // this.orders = orderResult.data.$values;
  },
  computed: {
    ...mapGetters(["selectedObject"]),
  },
};
</script>

<style></style>
