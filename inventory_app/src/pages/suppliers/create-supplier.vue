<template>
  <q-page>
    <div>Create Product</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-12 q-pa-sm">
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
        <div class="col-12 q-pa-sm">
          <q-input v-model="input.contactDetail" filled type="textarea" />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Cancel"
            type="button"
            color="primary"
            @click="$router.push('/suppliers')"
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
      input: {
        supplierName: null,
        contactDetail: null,
      },
    };
  },
  async mounted() {
    if (this.selectedObject) {
      this.input.supplierName = this.selectedObject.supplierName;
      this.input.contactDetail = this.selectedObject.contactInformation;
    }
  },
  methods: {
    async onSubmit() {
      let supplier = {
        SupplierName: this.input.supplierName,
        ContactInformation: this.input.contactDetail,
      };

      try {
        if (this.selectedObject) {
          await this.$axios.put(
            `https://localhost:7221/api/Supplier/${this.selectedObject.supplierId}`,
            supplier
          );
          this.$router.push("/suppliers");
        } else {
          await this.$axios.post(
            `https://localhost:7221/api/Supplier`,
            supplier
          );
          this.$router.push("/suppliers");
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
