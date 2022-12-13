<script setup>
import TeamList from '@/components/TeamList.vue'
import {onBeforeMount, reactive, ref, watch} from "vue"
import axios from "axios"
import _ from "lodash"
import {useRoute} from "vue-router"

const route = useRoute()
const teamId = ref()

watch(route, () => {
  teamId.value = route.query.teamId
})

const teamsList = ref([])
const carsList = ref([])
const groupedCars = ref({})

onBeforeMount(async () => {
  teamId.value = route.query.teamId
  await fetchData()
})

async function fetchData() {
  teamsList.value = (await axios.get("/api/teams")).data
  carsList.value = (await axios.get("/api/cars")).data
  groupedCars.value = _.groupBy(carsList.value, x => x.teamId)
}

async function update() {
  await fetchData()
  //showAlert()
}

//const dismissSec = ref(5)
//const dismissCountDown = ref(0)
//
//function countDownChanged(dismissCountDown) {
//  dismissCountDown.value = dismissCountDown
//}
//
//function showAlert() {
//  dismissCountDown.value = dismissSec.value
//}

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
    let res = await axios.post('/api/cars', car, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })

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
  <main>
    <b-container>
      <h1>Чемпионские болиды</h1>
      <hr>

      <!--
      <b-alert
          :show="dismissCountDown"
          dismissible
          variant="success"
          @dismissed="dismissCountDown=0"
          @dismiss-count-down="countDownChanged">
        <p>Болид удалён!</p>
      </b-alert>
      -->

      <TeamList @create-car="createCar" @delete-car="deleteCar" @edit-car="editCar"
                :teams="teamsList" :cars="groupedCars" :team-id="teamId"/>
    </b-container>
  </main>
</template>
