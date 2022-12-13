<script setup>
  const props = defineProps ({
    car: Object
  })
</script>

<template>
  <p><b>{{ props.car.title }}</b> - {{ props.car.information }}</p>
</template>

<style scoped>
</style>