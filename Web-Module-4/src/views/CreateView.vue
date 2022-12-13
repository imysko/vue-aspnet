<script setup>
import {onBeforeMount, reactive, ref} from "vue"
import axios from "axios"

const teamsList = ref([])

onBeforeMount(async () => {
  await fetchData()
})

async function fetchData() {
  teamsList.value = (await axios.get("/api/teams")).data
}

const createForm = ref(null)

const car = reactive({
  title: '',
  information: '',
  imageUrl: '',
  description: '',
  teamId: null,
})

const response = reactive({
  message: '',
  variant: '',
  statusCode: 404,
  errorMessage: ''
})

async function onSubmit() {
  if (car.teamId == null) {
    response.message = "Команда не выбрана!"
    response.variant = "warning"
    response.errorMessage = ''
    return
  }

  await axios.post('/api/cars', car)
      .then(res => {
        response.message = "Болид успешно добавлен!"
        response.variant = "info"
        response.statusCode = res.status
        response.errorMessage = ''
      })
      .catch(error => {
        response.message = "Ошибка при добавлении!"
        response.variant = "danger"
        response.statusCode = error.status
        response.errorMessage = error.message
      }).finally(() => {
      }
  )

  createForm.value.reset()
}
</script>

<template>
  <header>
    <b-container>
      <h1>Добавление чемпионского болида</h1>
      <hr>
    </b-container>
  </header>

  <main>
    <b-container>
      <b-alert :show="response.variant !== ''" :variant="response.variant">
        {{ response.message }}
        {{ response.errorMessage }}
      </b-alert>

      <form ref="createForm" @submit.prevent="onSubmit" method="POST" class="row g-3 justify-content-evenly">
        <b-col sm="3">
          <input v-model="car.title" required type="text" class="form-control" placeholder="Наименование">
        </b-col>

        <b-col sm="5">
          <input v-model="car.description" type="text" class="form-control" placeholder="Краткое описание">
        </b-col>

        <b-col sm="auto">
          <select v-model="car.teamId" required class="form-select">
            <option :value="null" disabled>Команда</option>
            <option v-for="team in teamsList" :value="team.id">{{ team.title }}</option>
          </select>
        </b-col>

        <b-col sm="12">
          <textarea v-model="car.information" placeholder="Полное описание..." class="form-control" rows="5"></textarea>
        </b-col>

        <b-col sm="12" class="text-end">
          <b-button type="submit" variant="outline-primary">Добавить</b-button>
        </b-col>
      </form>
    </b-container>
  </main>
</template>

<style scoped>
</style>