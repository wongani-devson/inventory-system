const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      {
        path: "",
        component: () => import("pages/IndexPage.vue"),
      },
      {
        path: "/products",
        name: "products",
        component: () => import("pages/products/products.vue"),
      },
      {
        path: "/create_product",
        name: "create_product",
        component: () => import("pages/products/create-product.vue"),
      },
      {
        path: "/create_category",
        name: "create_category",
        component: () => import("pages/categories/create-category.vue"),
      },
      {
        path: "/categories",
        name: "categories",
        component: () => import("pages/categories/categories.vue"),
      },
      {
        path: "/suppliers",
        name: "suppliers",
        component: () => import("pages/suppliers/suppliers.vue"),
      },
      {
        path: "/create_supplier",
        name: "create_supplier",
        component: () => import("pages/suppliers/create_supplier.vue"),
      },
      {
        path: "/customers",
        name: "customers",
        component: () => import("pages/customers/customers.vue"),
      },
      {
        path: "/create_customer",
        name: "create_customer",
        component: () => import("pages/customers/create_customer.vue"),
      },
    ],
  },
  {
    path: "/sign",
    component: () => import("layouts/AuthLayout.vue"),
    children: [
      {
        path: "/register",
        name: "register",
        component: () => import("pages/auth/register.vue"),
      },
      {
        path: "/login",
        component: () => import("pages/auth/login.vue"),
      },
      {
        path: "/sign-up",
        component: () => import("pages/auth/sign-up.vue"),
      },
    ],
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/ErrorNotFound.vue"),
  },
];

export default routes;
