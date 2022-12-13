<script setup>
import TeamList from '@/components/TeamList.vue'

import {onBeforeMount, reactive, ref} from "vue"
import axios from "axios"
import _ from "lodash";

const teamsList = ref([])
const carsList = ref([])
const groupedCars = ref({})

const selectedTeam = ref()
const carTitle = ref('')
const carInformation = ref('')

onBeforeMount(async () => {
  await fetchData()
});

async function update() {
  await fetchData()
}

async function fetchData() {
  teamsList.value = (await axios.get("/api/teams")).data
  carsList.value = (await axios.get("/api/cars")).data
  groupedCars.value = _.groupBy(carsList.value, x => x.teamId)
}

const response = reactive({
  message: '',
  variant: '',
  statusCode: 404,
  errorMessage: ''
})

async function createCar(car, modalRef) {
  if (car.teamId == null) {
    response.message = "Команда не выбрана!"
    response.variant = "warning"
    response.errorMessage = ''
    return
  }

  modalRef.value.close()

  try {
    let res = await axios.post('/api/cars', car)
    response.message = "Болид успешно добавлен!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при добавлении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }

  modalRef.value.resetForm()

  await update()
}

async function deleteCar(carId) {
  await axios.delete(`/api/cars/${carId}`)

  await update()
}

async function editCar(car) {
  console.log(car)

  try {
    let res = await axios.put(`/api/cars/${car.id}`, car)
    response.message = "Болид успешно изменён!"
    response.variant = "info"
    response.statusCode = res.status
    response.errorMessage = ''
  } catch (error) {
    response.message = "Ошибка при изменении!"
    response.variant = "danger"
    response.statusCode = error.status
    response.errorMessage = error.message
  }

  await update()
}
</script>

<template>
  <header>
    <b-container>
      <h1>Поиск</h1>
      <hr>

      <form class="row my-3 gx-3 gy-2 align-items-end">
        <div class="col-auto">
          <select v-model="selectedTeam" class="form-select">
            <option :value="undefined" selected>Все команды</option>
            <option v-for="team in teamsList" :value="team.id">{{ team.title }}</option>
          </select>
        </div>

        <div class="col-4">
          <input type="text" class="form-control" v-model="carTitle" placeholder="Название болида">
        </div>

        <div class="col-4">
          <input type="text" class="form-control" v-model="carInformation" placeholder="Описание болида">
        </div>
      </form>
    </b-container>
  </header>

  <main>
    <b-container>
      <hr>

      <TeamList  @create-car="createCar" @delete-car="deleteCar" @edit-car="editCar"
                 :teams="teamsList" :cars="groupedCars"
                :team-id="selectedTeam" :car-title="carTitle" :car-information="carInformation" />
    </b-container>
  </main>
</template>

<style scoped>
</style>