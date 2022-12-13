<script setup>
import BModal from '@/components/Modals/BModal.vue'
import { ref } from "vue"

const car = ref(Object)

const emit = defineEmits(["delete"])

const modalRef = ref(null)

defineExpose({
  show () {
    modalRef.value.show()
  },
  handCar (object) {
    car.value = object
  },
})
</script>

<template>
  <BModal ref="modalRef">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">Удалить {{ car.title }}?</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          Отменить это действие будет невозможно
        </div>
        <div class="modal-footer">
          <b-button type="button" variant="danger" data-bs-dismiss="modal" @click="$emit('delete', car.id)">Удалить</b-button>
          <b-button type="button" variant="outline-secondary" data-bs-dismiss="modal">Отмена</b-button>
        </div>
      </div>
    </div>
  </BModal>
</template>

<style scoped>
</style>