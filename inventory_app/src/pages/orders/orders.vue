<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Orders</div>

      <q-space />

      <q-btn
        label="Create Order"
        @click="$router.push('/create_order')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td key="orderDate" :props="props">
          {{ formattedDate(props.row.orderDate) }}
        </q-td>
        <q-td key="totalCost" :props="props">
          {{ props.row.totalCost }}
        </q-td>
        <q-td key="customer" :props="props">
          {{ props.row.customer.customerName }}
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
import { mapActions, mapMutations } from "vuex";
export default {
  name: "OrderPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "orderDate",
          align: "center",
          label: "Order Date",
          field: "orderDate",
          sortable: true,
        },
        {
          name: "totalCost",
          label: "Total Cost",
          field: "totalCost",
          sortable: true,
        },
        { name: "customer", label: "Customer", field: "customer" },
      ],

      rows: [],
    };
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_order");
    },
    formattedDate(value) {
      const date = new Date(value);
      const formattedDate = date.toLocaleDateString("en-US", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
      });
      return formattedDate;
    },
    async fetchOrders() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/Orders`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.orderId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/Orders/${this.selectedRow}`
        );
        this.fetchOrders();
      } catch (error) {}
    },
  },
  async created() {
    const response = await this.$axios.get(`https://localhost:7221/api/Orders`);
    this.rows = response.data.$values;
  },
};
</script>

<style scoped></style>
