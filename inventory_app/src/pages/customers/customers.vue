<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Customers</div>

      <q-space />

      <q-btn
        label="Create Customer"
        @click="$router.push('/create_customer')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props" @click="onRowClick(props.row)">
        <q-td key="customerName" :props="props">
          {{ props.row.customerName }}
        </q-td>
        <q-td key="contactInformation" :props="props">
          {{ props.row.contactInformation }}
        </q-td>
        <q-td key="totalSpend" :props="props">
          {{ props.row.totalSpend }}
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
  name: "CustomerPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "customerName",
          align: "left",
          label: "Customer Name",
          field: "customerName",
          sortable: true,
        },
        {
          name: "contactInformation",
          label: "Address",
          field: "contactInformation",
          sortable: true,
        },
        { name: "totalSpend", label: "Total Spend", field: "totalSpend" },
      ],

      rows: [],
    };
  },
  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/Customer`
    );
    this.rows = response.data.$values;
    console.log("row", this.rows);
  },
  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_customer");
    },
    async fetchCustomers() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/Customer`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.customerId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/Customer/${this.selectedRow}`
        );
        this.fetchCustomers();
      } catch (error) {}
    },
  },
};
</script>

<style scoped></style>
