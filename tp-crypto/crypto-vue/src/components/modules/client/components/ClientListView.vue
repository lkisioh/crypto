<template>
  <ClientNavBar></ClientNavBar>

   <div class="tableContainer max-w-5xl mx-auto mt-10 px-4">
    <h1>Lista de Clientes</h1>

    <div class="overflow-x-auto">
      <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md overflow-hidden">
        <thead class="bg-gray-100 text-gray-700">
          <tr>
            <th class="py-3 px-4 text-left">ID</th>
            <th class="py-3 px-4 text-left">Nombre</th>
            <th class="py-3 px-4 text-left">Email</th>
            <th class="py-3 px-4 text-left">Editar</th>
            <th class="py-3 px-4 text-left">Modificar</th>
            <th class="py-3 px-4 text-left">Eliminar</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="client in clients" :key="client.id" class="hover:bg-gray-50">
            <td class="py-3 px-4 border-t">{{ client.id }}</td>
            <td class="py-3 px-4 border-t">{{ client.name }}</td>
            <td class="py-3 px-4 border-t">{{ client.email }}</td>
            <td class="py-3 px-4 border-t">
              <button class="text-blue-600 hover:underline">Editar</button>
            </td>
            <td class="py-3 px-4 border-t">
              <button class="text-green-600 hover:underline">Modificar</button>
            </td>
            <td class="py-3 px-4 border-t">
              <button class="text-red-600 hover:underline">Eliminar</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

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

<style scoped>
h1{
  color: chocolate;
}
.tableContainer{
   max-width: 80vw;
  margin: auto;
  padding: 2rem;
  background-color: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  align-items: center;
}
</style>
