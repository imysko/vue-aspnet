<script setup>
import CarCard from '@/components/CarCard.vue'
import {computed} from "vue"

const props = defineProps({
  cars: Array,
  carTitle: String,
  carInformation: String,
})

const emit = defineEmits(['delete-car-click', 'edit-car-click'])

const filterCars = computed(() => {
  return props.cars?.filter(el => (el['title'] != null && el['title'].toLowerCase().includes(props.carTitle.toLowerCase()))
      && (el['information'] != null && el['information'].toLowerCase().includes(props.carInformation.toLowerCase())))
})

function onDeleteClick(car) {
  emit("delete-car-click", car)
}

function onEditClick(car) {
  emit("edit-car-click", car)
}
</script>

<template>
  <b-card-group>
    <CarCard @delete-car-click="onDeleteClick" @edit-car-click="onEditClick" v-for="car in filterCars" :car="car"/>
  </b-card-group>
</template>

<style scoped>
</style>