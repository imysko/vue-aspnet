<script setup>
import CarImage from "@/components/CarImage.vue"
import CarDescription from "@/components/CarDescription.vue"

import {ref, onBeforeMount, watch} from "vue"
import { useRoute } from 'vue-router'
import axios from "axios"

const route = ref(useRoute())
const car = ref(Object);

const id = ref(route.value.params.id)
const type = ref('')

watch(route.value, () => {
  type.value = route.value.hash
})

onBeforeMount(async () => {
  car.value = (await axios.get(`/api/cars/${id.value}`)).data
  type.value = route.value.hash
});
</script>

<template>
  <b-container>
    <h2 class="mt-4 ms-4">{{ car.title }}</h2>

    <div class="my-3">
      <router-link to="#image" class="m-3">
        <b-button v-if="type === '#image'" variant="primary" active>Картинка</b-button>
        <b-button v-else variant="light">Картинка</b-button>
      </router-link>
      <router-link to="#information">
        <b-button v-if="type === '#information'" variant="primary" active>Описание</b-button>
        <b-button v-else variant="light">Описание</b-button>
      </router-link>
    </div>
    <div class="d-flex flex-column border border-1 rounded-2 p-3 justify-content-center">
      <div v-if="type === ''">Нажмите на кнопку чтобы что-то узнать</div>
      <CarImage v-else-if="type === '#image'" :image="car.imageUrl" />
      <CarDescription v-else-if="type === '#information'" :car="car" />
    </div>
  </b-container>
</template>

<style scoped>

</style>