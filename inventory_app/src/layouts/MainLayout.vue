<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn
          flat
          round
          icon="menu"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />

        <q-toolbar-title>Inventory Management </q-toolbar-title>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above bordered>
      <q-list class="q-pa-sm" v-for="link in essentialLinks" :key="link.title">
        <!-- <q-btn @click="$router.push(link.route)">
          {{ link.title }}
        </q-btn> -->
        <q-item clickable @click="$router.push(link.route)">
          <q-item-section>
            <q-item-label class="text-weight-bold">{{
              link.title
            }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import { defineComponent, ref } from "vue";
import EssentialLink from "components/EssentialLink.vue";

const linksList = [
  {
    title: "products",
    route: "/products",
  },
  {
    title: "categories",
    route: "/categories",
  },
  {
    title: "customers",
    route: "/customers",
  },
  {
    title: "suppliers",
    route: "/suppliers",
  },
  {
    title: "orders",
    route: "/orders",
  },
  {
    title: "orders details",
    route: "/order_details",
  },
  {
    title: "stock movements",
    route: "/stock_movements",
  },
];

export default defineComponent({
  name: "MainLayout",

  components: {
    // EssentialLink,
  },

  setup() {
    const leftDrawerOpen = ref(false);

    return {
      essentialLinks: linksList,
      leftDrawerOpen,
      toggleLeftDrawer() {
        leftDrawerOpen.value = !leftDrawerOpen.value;
      },
    };
  },
});
</script>
