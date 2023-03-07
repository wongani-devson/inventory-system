<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Order Details</div>

      <q-space />

      <q-btn
        label="Create Order Details"
        @click="$router.push('/create_order_detail')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td key="quantity" :props="props">
          {{ props.row.quantity }}
        </q-td>
        <q-td key="product" :props="props">
          {{ props.row.product.productName }}
        </q-td>
        <q-td key="actions">
          <q-icon
            name="edit"
            class="cursor-pointer"
            @click="editRow(props.row)"
          />
          <q-icon
            name="delete"
            class="cursor-pointer"
            @click="openPopup(props.row)"
          />
        </q-td>
      </q-tr>
    </template>
  </q-table>

  <q-dialog v-model="confirm" persistent>
    <q-card>
      <q-card-section class="row items-center">
        <span class="q-ml-sm">Are you sure you want to delete this row?</span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn flat label="Cancel" color="primary" v-close-popup />
        <q-btn
          flat
          label="Delete"
          @click="deleteRow"
          color="primary"
          v-close-popup
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script>
import { mapMutations } from "vuex";
export default {
  name: "OrderDetailsPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "quantity",
          align: "center",
          label: "Quantity",
          field: "quantity",
          sortable: true,
        },
        { name: "product", label: "Product", field: "product", sortable: true },
      ],

      rows: [],
    };
  },
  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/OrderDetails`
    );
    this.rows = response.data.$values;
    console.log("row", this.rows);
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_order_detail");
    },
    async fetchOrderDetails() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/OrderDetails`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.orderDetailsId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/OrderDetails/${this.selectedRow}`
        );
        this.fetchOrderDetails();
      } catch (error) {}
    },
  },
};
</script>

<style scoped></style>
