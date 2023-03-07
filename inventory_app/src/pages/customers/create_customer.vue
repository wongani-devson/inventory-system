<template>
  <q-page>
    <div>Create Customer</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-12 q-pa-sm">
          <q-input
            filled
            v-model="input.customerName"
            label="Customer name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-12 q-pa-sm">
          <q-input v-model="input.contactDetail" filled type="textarea" />
        </div>
        <div class="col-12 q-pa-sm">
          <q-input
            filled
            v-model="input.totalSpend"
            label="Total spend"
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
            @click="$router.push('/customers')"
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
import { mapGetters, mapMutations } from "vuex";
export default {
  name: "CreateCustomer",
  data() {
    return {
      input: {
        customerName: null,
        contactDetail: null,
        totalSpend: null,
      },
    };
  },
  async mounted() {
    if (this.selectedObject) {
      this.input.customerName = this.selectedObject.customerName;
      this.input.contactDetail = this.selectedObject.contactInformation;
      this.input.totalSpend = this.selectedObject.totalSpend;
    }
  },
  methods: {
    async onSubmit() {
      let customer = {
        CustomerName: this.input.customerName,
        ContactInformation: this.input.contactDetail,
        TotalSpend: this.input.totalSpend,
      };

      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/Customer/${this.selectedObject.customerId}`,
            customer
          );
          this.$router.push("/customers");
        } else {
          await this.$axios.post(
            `https://localhost:7221/api/Customer`,
            customer
          );
          this.$router.push("/customers");
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
