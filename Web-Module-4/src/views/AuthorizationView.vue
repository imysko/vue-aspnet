<script setup>
import {reactive, ref} from "vue"
import {useRouter} from "vue-router"
import useAuthenticationStore from "@/store/authenticationStore"

const loginForm = ref(null)
const router = useRouter()

const user = reactive({
  username: '',
  password: ''
})

const authStore = useAuthenticationStore()
const login = authStore.login

const showAlert = reactive({
  wrongUsernameOrPassword: false
})

async function onLogin() {
  showAlert.usernameNotFound = false
  showAlert.wrongPassword = false

  let request = await login(user)

  if (request.status_code === 200) {
    await router.push("/")
  }
  else if (request.status_code === 401) {
    showAlert.wrongUsernameOrPassword = true
  }
}
</script>

<template>
  <b-container class="d-flex justify-content-center mt-4">
    <div class="wrapper">
      <h2>Авторизация</h2>

      <b-alert v-if="showAlert.wrongUsernameOrPassword" show variant="danger">
        Неверное имя пользователя или пароль
      </b-alert>

      <form ref="loginForm" @submit.prevent="onLogin" method="POST" class="row g-3 mt-2">
        <div class="form-floating">
          <input v-model="user.username" required type="text" class="form-control" placeholder="username">
          <label for="floatingInput">Имя пользователя</label>
        </div>

        <div class="form-floating">
          <input v-model="user.password" required type="password" class="form-control" placeholder="Password">
          <label for="floatingPassword">Пароль</label>
        </div>

        <b-col sm="12" class="d-flex justify-content-sm-between">
          <b-button to="/registration" type="button" variant="outline-primary">Ещё не разегистрированы?</b-button>
          <b-button type="submit" variant="outline-danger">Войти</b-button>
        </b-col>
      </form>
    </div>
  </b-container>
</template>

<style scoped>
.wrapper {
  width: 500px;
}
</style>