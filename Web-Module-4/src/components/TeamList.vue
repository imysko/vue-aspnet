<script setup>
import CarList from "@/components/CarList.vue"
import CreateCarModal from "@/components/Modals/CreateCarModal.vue"
import DeleteModal from "@/components/Modals/DeleteModal.vue"
import EditCarModal from "@/components/Modals/EditCarModal.vue"
import { computed, ref} from "vue"
import _ from "lodash"
import useAuthenticationStore from "@/store/authenticationStore"

const props = defineProps({
  teams: Array,
  cars: Object,
  teamId: {
    type: Number,
    default: -1
  },
  carTitle: {
    type: String,
    default: ''
  },
  carInformation: {
    type: String,
    default: ''
  },
})

const authStore = useAuthenticationStore()

const emit = defineEmits(['create-car', 'delete-car', 'edit-car'])

const createModalRef = ref()
const deleteModalRef = ref()
const editModalRef = ref()

const filteredTeams = computed(() => {
  if (parseInt(props.teamId) !== -1) {
    return props.teams.filter(el => el['id'] === parseInt(props.teamId))
  }
  else {
    return props.teams.filter(team => _.filter(props.cars[team.id],
        car => (car['title'] != null && car['title'].toLowerCase().includes(props.carTitle.toLowerCase())
            && (car['information'] != null && car['information'].toLowerCase().includes(props.carInformation.toLowerCase())))
      ).length
    )
  }
})

function onCreateClick(teamID) {
  createModalRef.value.show()
  createModalRef.value.handTeamId(teamID)
}

function createCar(car) {
  emit("create-car", car, createModalRef)
}

function onDeleteClick(car) {
  deleteModalRef.value.show()
  deleteModalRef.value.handCar(car)
}

function deleteCar(carId) {
  emit("delete-car", carId)
}

function onEditClick(car) {
  editModalRef.value.show()
  editModalRef.value.handCar(car)
}

function editCar(car) {
  emit("edit-car", car)
}
</script>

<template>
  <CreateCarModal @create="createCar" ref="createModalRef" :teams="props.teams"/>
  <DeleteModal @delete="deleteCar" ref="deleteModalRef"/>
  <EditCarModal @edit="editCar" ref="editModalRef" :teams="props.teams"/>

  <div v-for="team in filteredTeams">
    <div class="d-flex mt-4 ms-4 align-items-center">
      <h2>{{ team.title }}</h2>
      <b-button v-if="authStore.canAction('User')" class="fas fa-plus m-2 p-2" variant="outline-primary" @click="onCreateClick(team.id)"/>
    </div>

    <CarList @delete-car-click="onDeleteClick" @edit-car-click="onEditClick" :cars="props.cars[team.id]"
             :car-title="props.carTitle" :car-information="carInformation"/>
  </div>
</template>

<style scoped>

</style>