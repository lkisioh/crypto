<template>
<TransactionNavBar></TransactionNavBar>
<H1>LISTA DE TRANSACCIONES</H1>

<table class="min-w-full bg-white border border-gray-200 shadow-md rounded-md">

  <tr class="bg-gray-100 text-left">
    <th class="py-2 px-4 border-b">Id</th>
    <th class="py-2 px-4 border-b">action</th>
    <th class="py-2 px-4 border-b">crypto code</th>
    <th class="py-2 px-4 border-b">client id</th>
    <th class="py-2 px-4 border-b">cantidad</th>
    <th class="py-2 px-4 border-b">dinero</th>
    <th class="py-2 px-4 border-b">datetime</th>

  </tr>

  <tbody>
    <tr v-for="transaction in transactions"
            :key="transaction.id" class="hover:bg-gray-50">
            <td class="py-2 px-4 border-b"> {{ transaction.id }} </td>
            <td class="py-2 px-4 border-b"> {{ transaction.action }} </td>
            <td class="py-2 px-4 border-b"> {{  transaction.crypto_code}} </td>
            <td class="py-2 px-4 border-b"> {{  transaction.client_id}} </td>
            <td class="py-2 px-4 border-b"> {{  transaction.crypto_amount}} </td>
            <td class="py-2 px-4 border-b"> {{  transaction.money}} </td>
            <td class="py-2 px-4 border-b"> {{  transaction.datetime}} </td>
            <td class="py-2 px-4 border-b"><button>edit</button></td>
            <td class="py-2 px-4 border-b"><button>mod</button></td>
            <td class="py-2 px-4 border-b"><button>eliminar</button></td>
    </tr>
  </tbody>

  </table>

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

</style>
