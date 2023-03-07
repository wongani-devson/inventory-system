<template>
  <q-page>
    <div>Create Stock Movements</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            borderless
            input-class="text-center q-pl-lg"
            placeholder="Date "
            v-model="input.date"
            mask="date"
            class="col-12 createInput q-my-sm q-pl-md"
          >
            <template #append>
              <q-icon name="event" class="cursor-pointer">
                <q-popup-proxy
                  ref="qDateProxy"
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
                    mask="YYYY-MM-DD"
                    v-model="input.date"
                    subtitle="Date"
                    :default-year-month-day="defaultYearMonth"
                    @input="() => $refs.qDateProxy.hide()"
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
        </div>

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

        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.orderType"
            :options="orderTypes"
            label="Order Types"
            option-label="orderTypeName"
            option-value="orderTypeId"
            stack-label
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/stock_movements')"
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
      products: [],
      orderTypes: [],
      input: {
        date: null,
        quantity: null,
        product: null,
        orderType: null,
      },
    };
  },
  async mounted() {
    if (this.selectedObject) {
      this.input.date = this.selectedObject.date;
      this.input.quantity = this.selectedObject.quantity;
      this.input.product = this.selectedObject.product;
      this.input.orderType = this.selectedObject.orderType;
    }
  },
  methods: {
    async onSubmit() {
      let stockMevement = {
        Quantity: this.input.quantity,
        Date: this.input.date.replace(/\//g, "-"),
        ProductId: this.input.product.productId,
        OrderTypeId: this.input.orderType.orderTypeId,
      };
      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/StockMovements/${this.selectedObject.stockMovementsId}`,
            stockMevement
          );
          this.$router.push("/stock_movements");
        } else {
          await this.$axios.post(
            `https://localhost:7221/api/StockMovements`,
            stockMevement
          );
          this.$router.push("/stock_movements");
        }
      } catch (error) {}
    },
  },
  async created() {
    const productResult = await this.$axios.get(
      `https://localhost:7221/api/Product`
    );

    this.products = productResult.data.$values;
    const orderTypesResult = await this.$axios.get(
      `https://localhost:7221/api/OrderType`
    );

    this.orderTypes = orderTypesResult.data.$values;
  },
  computed: {
    ...mapGetters(["selectedObject"]),
  },
};
</script>

<style></style>
