<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Products</div>

      <q-space />

      <q-btn
        label="Create Product"
        @click="$router.push('/create_product')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td key="productName" :props="props">
          {{ props.row.productName }}
        </q-td>
        <q-td key="productDescription" :props="props">
          {{ props.row.productDescription }}
        </q-td>
        <q-td key="stockQuantity" :props="props">
          {{ props.row.stockQuantity }}
        </q-td>
        <q-td key="supplierName" :props="props">
          {{ props.row.supplier.supplierName }}
        </q-td>
        <q-td key="unitPrice" :props="props">
          {{ props.row.unitPrice }}
        </q-td>
        <q-td key="categoryName" :props="props">
          {{ props.row.category.categoryName }}
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
  name: "ProductsPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "productName",
          align: "center",
          label: "Product",
          field: "productName",
          sortable: true,
        },
        {
          name: "productDescription",
          label: "Product Description",
          field: "productDescription",
          sortable: true,
        },
        {
          name: "stockQuantity",
          label: "Stock Quantity",
          field: "stockQuantity",
        },
        {
          name: "supplierName",
          label: "Supplier",
          field: "supplierName",
        },
        { name: "unitPrice", label: "Unit Price", field: "unitPrice" },
        {
          name: "categoryName",
          label: "Category",
          field: "categoryName",
        },
      ],

      rows: [],
    };
  },
  methods: {
    onRowClick(val) {
      console.log(val);
    },
  },
  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/Product`
    );
    this.rows = response.data.$values;
    console.log("row", this.rows);
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_product");
    },
    async fetchProducts() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/Product`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.productId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/Product/${this.selectedRow}`
        );
        this.fetchProducts();
      } catch (error) {}
    },
  },
};
</script>

<style scoped></style>
