<script setup>
  const props = defineProps ({
    image: String
  })
</script>

<template>
  <div class="d-flex justify-content-center p-2">
    <img :src="props.image" alt=""/>
  </div>
</template>

<style scoped>
img {
  max-width: 700px;
  border-radius: 16px;
}
</style>