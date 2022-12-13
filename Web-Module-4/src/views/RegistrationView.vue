<script setup>
import { reactive, ref } from "vue"
import {useRouter} from "vue-router"
import useAuthenticationStore from "@/store/authenticationStore"

const registrationForm = ref(null)
const router = useRouter()

const user = reactive({
  username: '',
  password: ''
})

const confirmPassword = ref('')

const authStore = useAuthenticationStore()
const login = authStore.login
const registration = authStore.registration

function confirmed () {
  return user.password === confirmPassword.value
}

const showAlert = reactive({
  confirmedPassword: false,
  usernameExist: false
})

async function onRegistration() {
  showAlert.confirmedPassword = !confirmed()

  if (!confirmed()) {
    return
  }

  let request = await registration(user)

  if (request.result) {
    await login(user)
    await router.push("/")
  }

  if (request.message === 'username-exist') {
    showAlert.usernameExist = true
  }
}
</script>

<template>
  <b-container class="d-flex justify-content-center mt-4">
    <div class="wrapper">
      <h2>Регистрация</h2>

      <b-alert v-if="showAlert.usernameExist" show variant="warning">
        Такое имя пользовтеля уже существует
      </b-alert>

      <b-alert v-if="showAlert.confirmedPassword" show variant="warning">
        Пароли не совпадают!
      </b-alert>

      <form ref="registrationForm" @submit.prevent="onRegistration" method="POST" class="row g-3 mt-2">
        <div class="form-floating">
          <input v-model="user.username" required type="text" class="form-control" placeholder="username">
          <label for="floatingInput">Имя пользователя</label>
        </div>

        <div class="form-floating">
          <input v-model="user.password" required type="password" class="form-control" placeholder="Password">
          <label for="floatingPassword">Пароль</label>
        </div>

        <div class="form-floating">
          <input v-model="confirmPassword" required type="password" class="form-control" placeholder="Password">
          <label for="floatingPassword">Подтверждение пароля</label>
        </div>

        <b-col sm="12" class="text-end">
          <b-button type="submit" variant="outline-danger">Зарегистрироваться</b-button>
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