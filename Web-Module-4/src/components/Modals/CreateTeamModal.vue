<script setup>
import BModal from '@/components/Modals/BModal.vue'
import {reactive, ref} from "vue"

const emit = defineEmits(["create"])

const modalRef = ref(null)
const createForm = ref(null)

const team = reactive({
  title: '',
  url: ''
})

defineExpose({
  show () {
    modalRef.value.show()
  },
  close () {
    modalRef.value.close()
  },
  resetForm () {
    createForm.value.reset()
  }
})
</script>

<template>
  <BModal ref="modalRef">
    <div class="modal-dialog modal-sm">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Добавление команды</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <div class="modal-body">
          <form ref="createForm" @submit.prevent="$emit('create', team)" method="POST" class="row g-3 justify-content-evenly">
            <b-col>
              <input v-model="team.title" required type="text" class="form-control" placeholder="Наименование">
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