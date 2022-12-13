<script setup>
import BModal from '@/components/Modals/BModal.vue'
import {ref} from "vue"

const modalRef = ref(null)
const emit = defineEmits(["logout"])

defineExpose({
  show () {
    modalRef.value.show()
  }
})
</script>

<template>
  <BModal ref="modalRef">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Выйти?</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          Вы уверены что хотите выйти?
        </div>
        <div class="modal-footer">
          <b-button type="button" variant="danger" data-bs-dismiss="modal" @click="$emit('logout')">Да</b-button>
          <b-button type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
        </div>
      </div>
    </div>
  </BModal>
</template>

<style scoped>
</style>