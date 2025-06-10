<script setup>
import { ref , onMounted} from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';

let schema = {
    action: yup.string().nonNullable().required(),
    crypto_code: yup.string().required(),
    client_id: yup.number().required(),
    crypto_amount: yup.number().min(0).nonNullable().required(),
    money: yup.number().positive().required(),
    datetime: yup.date().required(),
}

let newTransaction = ref({
    action:'',
    crypto_code: '',
    client_id: '',
    crypto_amount: '',
    money: '',
    datetime: ''
});




  //nuevo
 async function enviarDatosApi() {


  const response = await fetch('https://localhost:7294/api/Transactions',
  {
    method: 'POST',
    body: JSON.stringify(newTransaction.value),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElPost'
    }
  });

  if (response.ok) {
    alert('New Transaction add succesfully');
  } else {

    console.log('LCDSM')
    const errorData = await response.json();
    console.error('Error en la API:', errorData);
    alert('Error to add a new transaction');
  }
}


const clients = ref([]);
async function traerClientes() {
  let clientApiData= await fetch('https://localhost:7294/api/Client');
  clients.value = await clientApiData.json();
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

function actualizarFechaHora() {
  fechaHora.value = getFechaHoraActual();
  newTransaction.value.datetime = new Date().toISOString()
}

onMounted(() => {
  traerClientes();
  actualizarFechaHora();
  setInterval(actualizarFechaHora, 60000); // cada 60 seg
});

import { watch } from 'vue'
// Espera que cambien ambos valores y actualiza el total
watch(
  () => [newTransaction.value.crypto_code, newTransaction.value.crypto_amount],
  ([codigo, cantidad]) => {
    if (codigo && parseFloat(cantidad) > 0) {
      llamarCryptoApi(codigo, cantidad);
    } else {
      money.value = 0;
    }
  }
);



let money = ref(0)
const crypto = ref('')


async function llamarCryptoApi(codigo, cantidad) {
  try {

    const url = `https://criptoya.com/api/${codigo}/ars/${cantidad}`;

    const response = await fetch(url);
    const data = await response.json();
    crypto.value = data;
    money.value = data.ripio.ask;
    newTransaction.value.money = money.value;
  } catch (error) {
    console.error('Error llamando a la API de cripto:', error);
    money.value = 0;
  }
}

import TransactionNavBar from './TransactionNavBar.vue';
</script>

<template>
  <TransactionNavBar></TransactionNavBar>

  <div v-if="clients.length === 0">
    <p>Aún no hay clientes cargados.</p>
  </div>

  <div v-else>
    <h1>NEW TRANSACTION FORM</h1>
  <h3>La hora es: {{ fechaHora }}</h3>

   <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga">

    <label>
      Action:
    <Field as="select" name="action" v-model="newTransaction.action">
    <option disabled value="">CHOOSE AN ACTION</option>
    <option value="purchase">PURCHASE</option>
    <option value="sell">SELL</option>
    </Field>
    </label>
    <ErrorMessage name="action" />

    <br>

    <label>
   Crypto:
    <Field as="select" name="crypto_code" v-model="newTransaction.crypto_code">
    <option disabled value="">Seleccione una opción</option>
    <option value="BTC">Bitcoin</option>
    <option value="ETH">Ethereum</option>
    <option value="USDC">USDC</option>
    </Field>
      </label>
      <ErrorMessage name="crypto_code" />

    <br>

    <br>

    <label >
      CLIENT:
      <Field as="select" v-model="newTransaction.client_id" name="client_id" id="client_id">
        <option
            v-for="client in clients"
            :key="client.id"
            :value="client.id"
        >{{ client.name }}
      </option>
      </Field>
    </label>


    <ErrorMessage name="client_id"></ErrorMessage>

    <br>

    <label>
      CRYPTO AMOUNT:
      <Field
      v-model="newTransaction.crypto_amount"
       name="crypto_amount"
      type="number"
      step="any"
       id="crypto_amount"
      />
    <ErrorMessage name="crypto_amount" />
      </label>

      <br>
      <br>
      <br>
    <input type="submit" value="Guardar">

    <h1 v-if="money > 0">PRECIO ${{ money * newTransaction.crypto_amount }}</h1>
  </Form>

  </div>

</template>

<style>

</style>
