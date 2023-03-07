<template>
  <q-page>
    <div>Register page</div>
    <div class="q-pa-md">
      <q-form @submit="onSubmit" class="row col-12">
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.FirstName"
            label="First name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.LastName"
            label="Last name"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.UserName"
            label="Username"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.Email"
            label="Email"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            dense
            filled
            ref="password"
            placeholder="Create Password"
            input-class="text-center text-white"
            class="col-12 input q-my-sm"
            :type="isPwd ? 'password' : 'text'"
            v-model="user_register.Password"
          >
            <template #append>
              <q-icon
                :name="isPwd ? 'visibility_off' : 'visibility'"
                color="white"
                class="cursor-pointer"
                @click="isPwd = !isPwd"
              />
            </template>
          </q-input>
        </div>
        <div class="col-6 q-pa-sm">
          <q-input
            filled
            v-model="user_register.PhoneNumber"
            label="Phone number"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Please type something',
            ]"
          />
        </div>
        <div>
          <q-btn label="Submit" type="submit" color="primary" />
          <q-btn
            label="Reset"
            type="reset"
            color="primary"
            flat
            class="q-ml-sm"
          />
        </div>
      </q-form>
    </div>
  </q-page>
</template>

<script>
export default {
  name: "RegisterPage",
  data() {
    return {
      isPwd: true,
      user_register: {
        UserName: null,
        Email: null,
        FirstName: null,
        LastName: null,
        PhoneNumber: null,
        Password: null,
      },
    };
  },
  methods: {
    async onSubmit() {
      try {
        await this.$axios.post(
          `https://localhost:7221/api/Auth/register`,
          this.user_register
        );
      } catch (error) {}
    },
  },
};
</script>

<style></style>
