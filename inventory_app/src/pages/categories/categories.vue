<template>
  <q-table :rows="rows" :columns="columns" row-key="name">
    <template v-slot:top>
      <div class="col-2 q-table__title">Categories</div>

      <q-space />

      <q-btn
        label="Create Category"
        @click="$router.push('/create_category')"
        class="q-ml-md"
      />
    </template>
    <template v-slot:body="props">
      <q-tr :props="props">
        <q-td key="categoryName" :props="props">
          {{ props.row.categoryName }}
        </q-td>
        <q-td key="categoryDescription" :props="props">
          {{ props.row.categoryDescription }}
        </q-td>
        <q-td :props="{ key: 'actions' }">
          <q-icon
            name="edit"
            class="cursor-pointer"
            @click="editRow(props.row)"
          />
          <q-icon
            name="delete"
            class="cursor-pointer"
            @click="deleteRow(props.row)"
          />
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
  name: "CategoriesPage",
  data() {
    return {
      confirm: false,
      selectedRow: null,
      columns: [
        {
          name: "categoryName",
          align: "center",
          label: "Category Name",
          field: "categoryName",
          sortable: true,
        },
        {
          name: "categoryDescription",
          label: "Category Description",
          field: "categoryDescription",
          sortable: true,
        },
      ],

      rows: [],
    };
  },
  async created() {
    const response = await this.$axios.get(
      `https://localhost:7221/api/Category`
    );
    this.rows = response.data.$values;
  },

  methods: {
    ...mapMutations(["setSelectedObject"]),
    editRow(row) {
      this.setSelectedObject(row);
      this.$router.push("/create_category");
    },
    async fetchCategory() {
      const response = await this.$axios.get(
        `https://localhost:7221/api/Category`
      );
      this.rows = response.data.$values;
    },
    openPopup(row) {
      console.log(row);
      this.confirm = true;
      this.selectedRow = row.categoryId;
    },
    async deleteRow() {
      console.log(this.selectedRow);
      try {
        await this.$axios.delete(
          `https://localhost:7221/api/Category/${this.selectedRow}`
        );
        this.fetchCategory();
      } catch (error) {}
    },
  },
};
</script>

<style scoped></style>
