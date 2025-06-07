<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>

    <div class="wrapper">
      <nav>
        <RouterLink to="/">Inicio</RouterLink>
        <RouterLink to="/clients">Clientes</RouterLink>
        <RouterLink to="/transactions">Transacciones</RouterLink>

      </nav>
    </div>
  </header>
  <body>
    <h2>este es la appa principoal qe comparte todo</h2>
  </body>

  <RouterView />
</template>

<style scoped>



</style>
