<script setup>
import CreateTeamModal from "@/components/Modals/CreateTeamModal.vue"
import LogoutModal from "@/components/Modals/LogoutModal.vue"
import useAuthenticationStore from "@/store/authenticationStore"

import {onBeforeMount, reactive, ref, watch} from "vue"
import axios from "axios"
import {useRoute} from "vue-router"

const route = useRoute()
const type = ref(undefined)
const teams = ref([])

watch(route, () => {
  switch (route.path) {
    case '/search':
      type.value = 'search'
      break
    case '/authorization':
    case '/registration':
      type.value = 'authorization'
      break
    default:
      type.value = undefined
      break
  }
})

onBeforeMount(async () => {
  await fetchData()
})

async function fetchData() {
  teams.value = (await axios.get("/api/teams")).data
}

const authStore = useAuthenticationStore()
const logoutModalRef = ref()

const createModalRef = ref()

const response = reactive({
  message: '',
  variant: '',
  statusCode: 404,
  errorMessage: ''
})

function onCreateClick() {
  createModalRef.value.show()
}

async function createTeam(team) {
  try {
    let res = await axios.post('/api/teams', team)
    response.message = "Команда успешно добавлена!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при добавлении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }

  createModalRef.value.close()

  await fetchData()
}

function onLogoutClick() {
  logoutModalRef.value.show()
}

async function onLogout() {
  await authStore.logout()
}
</script>

<template>
  <CreateTeamModal @create="createTeam" ref="createModalRef"/>
  <LogoutModal @logout="onLogout" ref="logoutModalRef"/>

  <b-container>
    <b-navbar toggleable="lg" type="light" variant="faded">
      <b-navbar-brand href="#" to="/">
        <img class="logo" src="../../public/images/f1-logo.svg" alt="">
      </b-navbar-brand>

      <b-navbar-toggle target="navbar-toggle-collapse"/>

      <b-collapse id="navbar-toggle-collapse" is-nav>
        <b-navbar-nav v-for="team in teams">
            <b-nav-item :to="`/?teamId=${team.id}`">{{ team.title }}</b-nav-item>
        </b-navbar-nav>

        <b-navbar-nav class="ms-auto">
          <b-nav-item to="/search" right>
            <b-button variant="outline-danger" :active="type === 'search'">Поиск</b-button>
          </b-nav-item>
          <b-nav-item v-if="authStore.canAction('User')" right>
            <b-button variant="outline-danger" @click="onCreateClick">Добавить команду</b-button>
          </b-nav-item>
        </b-navbar-nav>

        <b-navbar-nav class="ms-2">
          <b-nav-item v-if="!authStore.checkAuth" to="/authorization" right>
            <b-button class="fas fa-user" variant="outline-primary" :active="type === 'authorization'">
              <div class="m-1 ms-2">Войти</div>
            </b-button>
          </b-nav-item>

          <b-nav-item v-else right>
            <b-button class="fas fa-angle-right" variant="outline-primary" @click="onLogoutClick">
              <div class="m-1 ms-2">Выйти</div>
            </b-button>
          </b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
  </b-container>
</template>

<style scoped>
.logo {
  width: 80px;
  height: 20px;
}
</style>