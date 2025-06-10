<template>
<TransactionNavBar></TransactionNavBar>

<div class="tableContainer">
  <H1>LISTA DE TRANSACCIONES</H1>

  <table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md">

  <tr class="bg-gray-100 text-center">
    <th class="py-3 px-5 border-b">ID</th>
    <th class="py-3 px-5 border-b">Action</th>
    <th class="py-3 px-5 border-b">Crypto code</th>
    <th class="py-3 px-5 border-b">Client id</th>
    <th class="py-3 px-5 border-b">Cantidad</th>
    <th class="py-3 px-5 border-b">Dinero</th>
    <th class="py-3 px-5 border-b">Fecha</th>

  </tr>

  <tbody>
    <tr v-for="transaction in transactions"
            :key="transaction.id" class="hover:bg-gray-50">
            <td class="py-3 px-5 border-b"> {{ transaction.id }} </td>
            <td class="py-3 px-5 border-b"> {{ transaction.action }} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_code}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.client_id}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_amount}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.money}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.datetime}} </td>

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


</template>

<script setup>
import TransactionNavBar from './TransactionNavBar.vue';
import { ref } from 'vue';

const transactions = ref([]);

async function cargarDatosApi() {
  let respuesta = await fetch('https://localhost:7294/api/Transactions');
  transactions.value = await respuesta.json();
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
