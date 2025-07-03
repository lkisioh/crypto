<script setup>
import { ref } from 'vue';
import TransactionNavBar from './TransactionNavBar.vue';
import { useRoute,useRouter } from 'vue-router';


const router = useRouter()
let route = useRoute()
let id = route.params.id
let transaction = ref(undefined);

async function BuscarDatosApi() {
  let response = await fetch('https://localhost:7294/api/Transactions/'+id,
  {
    method: 'GET',
    body: JSON.stringify(),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElGetTransaction'
    }
  });
  //Verifico si fue exitosa la respuesta
  if (response.ok) {
     transaction.value=  await response.json();
     console.log('Search succesfully');

  } else {
    alert('Error to find transaction');
  }}

BuscarDatosApi();

  async function  deleteTransaction(id) {

  let response = await fetch('https://localhost:7294/api/Transactions/'+id,
  {
    method: 'DELETE',
    body: JSON.stringify(),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElDeleteTransaction'
    }
  });

  if (response.ok) {
    alert('Transaction deleted succesfully');
    router.push('/transactions/list')
  } else {
    alert('Error deleting transaction');
  }}


</script>


<template>
<TransactionNavBar></TransactionNavBar>

<div v-if= "transaction === undefined"> CARGANDO...</div>
<div v-else class="tableContainer">

  <H1>TRANSACTION VIEW</H1>

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
    <tr>
            <td class="py-3 px-5 border-b"> {{ transaction.id }} </td>
            <td class="py-3 px-5 border-b"> {{ transaction.action }} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_code}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.client_id}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.crypto_amount}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.money}} </td>
            <td class="py-3 px-5 border-b"> {{  transaction.datetime}} </td>

            <td class="py-3 px-4 border-t">
              <button class="text-green-600 hover:underline"><RouterLink :to="'/transaction/edit/' + transaction.id">Modificar</RouterLink></button>
            </td>
            <td class="py-3 px-4 border-t">
              <button @click="deleteTransaction(transaction.id)" class="text-red-600 hover:underline">Eliminar</button>
            </td>
    </tr>
  </tbody>

  </table>

</div>


</template>


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
