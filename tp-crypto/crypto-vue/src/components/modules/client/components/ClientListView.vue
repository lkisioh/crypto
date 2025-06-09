<template>
  <ClientNavBar></ClientNavBar>

<H1>LISTA DE CLIENTES</H1>

  <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md">

  <tr class="bg-gray-100 text-left">
    <th class="py-2 px-4 border-b">Id</th>
    <th class="py-2 px-4 border-b">NOMBRE</th>
    <th class="py-2 px-4 border-b">Email</th>
    <th class="py-2 px-4 border-b">edit</th>
    <th class="py-2 px-4 border-b">mod</th>
    <th class="py-2 px-4 border-b">eliminar</th>
  </tr>

  <tbody>
    <tr v-for="client in clients"
            :key="client.id" class="hover:bg-gray-50">
            <td class="py-2 px-4 border-b"> {{ client.id }} </td>
            <td class="py-2 px-4 border-b"> {{ client.name }} </td>
            <td class="py-2 px-4 border-b"> {{  client.email}} </td>
            <td class="py-2 px-4 border-b"><button>edit</button></td>
            <td class="py-2 px-4 border-b"><button>mod</button></td>
            <td class="py-2 px-4 border-b"><button>eliminar</button></td>
    </tr>
  </tbody>

  </table>

</template>

<script setup>
import ClientNavBar from './ClientNavBar.vue';
import { ref } from 'vue';

const clients = ref([]);
async function cargarDatosApi() {
  let respuesta = await fetch('https://localhost:7294/api/Client');
  clients.value = await respuesta.json();
}
cargarDatosApi();
</script>
<style scoped></style>
