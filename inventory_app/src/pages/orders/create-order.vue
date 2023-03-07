<template>
  <q-page>
    <div>Create Product</div>
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
              <q-icon name="event" color="#666666" class="cursor-pointer">
                <q-popup-proxy
                  ref="qDateProxy"
                  transition-show="scale"
                  transition-hide="scale"
                >
                  <q-date
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
            v-model="input.totalCost"
            label="Total cost"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>

        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.customer"
            :options="customers"
            label="Customer"
            option-label="customerName"
            option-value="customerId"
            stack-label
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-select
            filled
            v-model="input.orderDetail"
            :options="orderDetails"
            label="Order Detail"
            :option-label="(option) => option.product.productName"
            option-value="orderDetailsId"
            stack-label
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/orders')"
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
  name: "CreateOrder",
  data() {
    return {
      customers: [],
      orderDetails: [],
      input: {
        date: null,
        totalCost: null,
        customer: null,
        orderDetail: null,
      },
    };
  },
  async mounted() {
    if (this.selectedObject) {
      this.input.date = this.selectedObject.orderDate;
      this.input.totalCost = this.selectedObject.totalCost;
      this.input.customer = this.selectedObject.customer;
      this.input.orderDetail = this.selectedObject.orderDetails;

      this.orderDetails.find(
        (order) => order.orderDetailsId == this.input.orderDetail.orderDetailsId
      );
    }
    console.log(this.input);
  },
  methods: {
    async onSubmit() {
      let order = {
        OrderDate: this.input.date.replace(/\//g, "-"),
        totalCost: this.input.totalCost,
        CustomerId: this.input.customer.customerId,
        orderDetailsId: this.input.orderDetail.orderDetailsId,
      };
      console.log(order);
      try {
        if (this.selectedObject) {
          await this.$axios.put(`https://localhost:7221/api/Orders`, order);
          this.$router.push("/orders");
        } else {
          await this.$axios.post(`https://localhost:7221/api/Orders`, order);
          this.$router.push("/orders");
        }
      } catch (error) {}
    },
  },
  async created() {
    const customersResults = await this.$axios.get(
      `https://localhost:7221/api/Customer`
    );

    this.customers = customersResults.data.$values;

    const orderDetailsResults = await this.$axios.get(
      `https://localhost:7221/api/OrderDetails`
    );
    this.orderDetails = orderDetailsResults.data.$values;
    console.log("order", this.orderDetails);
  },
  computed: {
    ...mapGetters(["selectedObject"]),
  },
};
</script>

<style></style>
