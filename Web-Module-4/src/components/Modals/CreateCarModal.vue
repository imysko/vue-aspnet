<script setup>
import BModal from '@/components/Modals/BModal.vue'
import {reactive, ref} from "vue"

const props = defineProps({
  teams: Array
})

const emit = defineEmits(["create"])

const modalRef = ref(null)
const createForm = ref(null)

const car = reactive({
  title: '',
  information: '',
  image: File,
  description: '',
  teamId: null,
})

defineExpose({
  show () {
    modalRef.value.show()
  },
  close () {
    modalRef.value.close()
  },
  handTeamId (id) {
    car.teamId = id
  },
  resetForm () {
    createForm.value.reset()
  }
})

function uploadImage(e) {
  car.image = e.target.files[0]
}
</script>

<template>
  <BModal ref="modalRef">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Добавление болида</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <div class="modal-body">
          <form ref="createForm" @submit.prevent="$emit('create', car)" method="POST" class="row g-3 justify-content-evenly">
            <b-col sm="3">
              <input v-model="car.title" required type="text" class="form-control" placeholder="Наименование">
            </b-col>

            <b-col sm="5">
              <input v-model="car.description" type="text" class="form-control" placeholder="Краткое описание">
            </b-col>

            <b-col sm="auto">
              <select v-model="car.teamId" required class="form-select">
                <option :value="null" disabled>Команда</option>
                <option v-for="team in props.teams" :value="team.id">{{ team.title }}</option>
              </select>
            </b-col>

            <b-col sm="12">
              <input ref="image" class="form-control" type="file" @change="uploadImage">
            </b-col>

            <b-col sm="12">
              <textarea v-model="car.information" placeholder="Полное описание..." class="form-control" rows="5"></textarea>
            </b-col>

            <b-col sm="12" class="text-end">
              <b-button type="submit" variant="outline-primary">Добавить</b-button>
              <b-button class="ms-2" type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
            </b-col>
          </form>
        </div>
      </div>
    </div>
  </BModal>
</template>

<style scoped>
</style>