<script setup>
import { ref, onMounted } from "vue"
import { Modal } from "bootstrap"

const modalRef = ref()
let modalInstance = null

onMounted(() => {
  modalInstance = new Modal(modalRef.value)
})

function show() {
  modalInstance.show()
}

function close() {
  modalInstance.hide()
}

defineExpose({
  show,
  close
})

</script>

<template>
  <!-- Modal -->
  <div class="modal fade" ref="modalRef" tabindex="-1" aria-labelledby="templateModal" aria-hidden="true">
    <slot></slot>
  </div>
</template>


<style scoped>
</style>