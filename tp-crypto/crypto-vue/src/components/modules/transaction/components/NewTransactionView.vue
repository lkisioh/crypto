<script setup>
import { ref , onMounted} from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';

let schema = {
   action: yup.string().nonNullable().required(),
    crypto_code: yup.string().nonNullable().required(),
    client_id: yup.number().integer().nonNullable().required(),
    crypto_amount: yup.number().min(0).nonNullable().required(),
    money: yup.number().min(0).nonNullable().required(),
    datetime: yup.date().nonNullable().required(),
}

let newTransaction = ref({
    action:'',
    crypto_code: '',
    client_id: '',
    crypto_amount: '',
    money: '',
    datetime: ''
});

async function enviarDatosApi() {

  let response = await fetch('https://localhost:7294/api/Transactions',
  {
    method: 'POST',
    body: JSON.stringify(newTransaction.value),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElPost'
    }
  });

  //Verifico si fue exitosa la respuesta
  if (response.ok) {
    alert('New Transaction add succesfully');
  } else {
    alert('Error to add a new transaction');
  }}


const clients = ref([]);
async function traerClientes() {
  let clientApiData= await fetch('https://localhost:7294/api/Client');
  clients.value = await clientApiData.json();
}
traerClientes();

if(clients.value===null){
  alert('Aún no hay clientes cargados')
}

const fechaHora = ref('');

function getFechaHoraActual() {
  const x = new Date();
  const anio = x.getFullYear();
  const mes = String(x.getMonth() + 1).padStart(2, '0');
  const dia = String(x.getDate()).padStart(2, '0');
  const horas = String(x.getHours()).padStart(2, '0');
  const minutos = String(x.getMinutes()).padStart(2, '0');
  return `${anio}-${mes}-${dia} ${horas}:${minutos}`;
}

onMounted(() => {
  fechaHora.value = getFechaHoraActual();
});

</script>

<template>

  <h1>NEW TRANSACTION FORM</h1>
  <h3>La hora es: {{ fechaHora }}</h3>

   <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga">

    <label>
      Action:
      <Field v-model="newTransaction.action" type="text" name="action" id="action"/>
    </label>
    <ErrorMessage name="crypto_code"></ErrorMessage>
    <br>

    <label>
      Crypto:
      <Field v-model="newTransaction.crypto_code" type="text" name="crypto_code" id="crypto_code"/>
    </label>
    <ErrorMessage name="crypto_code"></ErrorMessage>

    <br>

    <br>

    <label>
      CLIENT:
      <select>
        <option
            v-for="client in clients"
            :key="client.id"
        >{{ client.name }}
      </option>
      </select>
    </label>
    <!-- DATOS GUARDADOS AL ELEGIR UN CLIENTE -->
     <Field v-model="newTransaction.client_id" type="hidden" name="client_id" id="client_id"/>
     <Field v-model="newTransaction.client_data.id" type="hidden" name="client_data.id" id="client_data.id"/>
     <Field v-model="newTransaction.client_data.name" type="hidden" name="client_data.name" id="client_data.name"/>
     <Field v-model="newTransaction.client_data.email" type="hidden" name="client_data.email" id="client_data.email"/>

    <ErrorMessage name="client_id"></ErrorMessage>

    <br>

    <label>
      CRYPTO AMOUNT:
      <Field v-model="newTransaction.crypto_amount" type="number" name="crypto_amount" id="crypto_amount"/>
    </label>
    <ErrorMessage name="crypto_amount"></ErrorMessage>

    <Field v-model="newTransaction.datetime" type="hidden" name="datetime" id="datetime"/>

    <input type="submit" value="Guardar">
  </Form>
</template>

<style>

</style>
