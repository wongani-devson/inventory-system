<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Stock Movements</div>

      <q-space />

      <q-btn
        label="Create Stock Movement"
        @click="$router.push('/create_stock_movement')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props" @click="onRowClick(props.row)">
        <q-td key="date" :props="props">
          {{ formattedDate(props.row.date) }}
        </q-td>
        <q-td key="quantity" :props="props">
          {{ props.row.quantity }}
        </q-td>
        <q-td key="orderType" :props="props">
          {{ props.row.orderType.orderTypeName }}
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
import { mapActions, mapMutations } from "vuex";
export default {
  name: "ProductsPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "date",
          required: true,
          label: "Date",
          align: "left",
          field: "date",
        },
        {
          name: "quantity",
          align: "center",
          label: "Quantity",
          field: "quantity",
          sortable: true,
        },
        {
          name: "orderType",
          label: "Order Type",
          field: "orderType",
          sortable: true,
        },
        { name: "product", label: "Product", field: "product" },
      ],

      rows: [],
    };
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_stock_movement");
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
    async fetchStockMovements() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/StockMovements`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.stockMovementsId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/StockMevements/${this.selectedRow}`
        );
        this.fetchStockMovements();
      } catch (error) {}
    },
  },

  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/StockMovements`
    );
    this.rows = response.data.$values;
  },
};
</script>

<style scoped></style>
