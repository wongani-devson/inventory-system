<template>
  <div>Products</div>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Supplier</div>

      <q-space />

      <q-btn
        label="Create supplier"
        @click="$router.push('/create_supplier')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props" @click="onRowClick(props.row)">
        <q-td key="supplierName" :props="props">
          {{ props.row.supplierName }}
        </q-td>
        <q-td key="contactInformation" :props="props">
          <q-badge color="green">
            {{ props.row.contactInformation }}
          </q-badge>
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
          name: "supplierName",
          align: "center",
          label: "Supplier Name",
          field: "supplierName",
        },
        {
          name: "contactInformation",
          label: "Address",
          field: "contactInformation",
          sortable: true,
        },
      ],

      rows: [],
    };
  },
  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/Supplier`
    );
    this.rows = response.data.$values;
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_supplier");
    },
    async fetchSuppliers() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/Supplier`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.supplierId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/Supplier/${this.selectedRow}`
        );
        this.fetchSuppliers();
      } catch (error) {}
    },
  },
};
</script>

<style scoped></style>
