<script setup>
import useAuthenticationStore from "@/store/authenticationStore"
const props = defineProps({
  car: Object,
})

const authStore = useAuthenticationStore()

const emit = defineEmits(['delete-car-click', 'edit-car-click'])

function onDeleteClick(car) {
  emit("delete-car-click", car)
}

function onEditClick(car) {
  emit("edit-car-click", car)
}
</script>

<template>
  <div>
    <b-card
        no-body
        tag="article"
        style="width: 320px"
        border-variant="grey"
        class="m-2 text-center">

      <b-card-img
          :src="props.car.imageUrl"
          alt="Image"
          width="320"
          height="175"
          style="object-fit: cover;"
          top>
      </b-card-img>

      <b-card-body>
        <b-button
            class="d-flex flex-column flex-grow-1 mb-2"
            :to="`car/${props.car.id}`"
            variant="primary">
          {{ props.car.title }}
        </b-button>

        <div class="d-flex justify-content-around mb-2">
          <b-button :to="`car/${props.car.id}#image`" variant="outline-danger">Картинка</b-button>
          <b-button :to="`car/${props.car.id}#information`" variant="outline-danger">Описание</b-button>
        </div>
        <div v-if="authStore.canAction('Editor')" class="d-flex justify-content-end">
          <b-button class="fas fa-pen me-2" variant="outline-primary" @click="onEditClick({...props.car})"/>
          <b-button class="fas fa-trash" variant="outline-danger" @click="onDeleteClick(props.car)"/>
        </div>
      </b-card-body>
    </b-card>
  </div>
</template>

<style scoped>
</style>