<script setup>
import { ref , onMounted} from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';
import { useRoute} from 'vue-router';

let schema = {
    action: yup.string().nonNullable().required(),
    crypto_code: yup.string().required(),
    client_id: yup.number().required(),
    crypto_amount: yup.number().min(0).nonNullable().required(),
    money: yup.number().positive().required(),
    datetime: yup.date().required(),
}

let editTransaction = ref({
  id:'',
    action:'',
    crypto_code: '',
    client_id: '',
    crypto_amount: '',
    money: '',
    datetime: ''
});

  let route = useRoute()
  let id = route.params.id

  let transaction = ref(undefined)

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
     editTransaction.value=  await response.json();
     transaction.value = 1
     console.log('Search succesfully');

  } else {
    alert('Error to find transaction');
  }}

  BuscarDatosApi();

 async function EditarDatosApi() {

  let response = await fetch('https://localhost:7294/api/Transactions/'+id,
  {
    method: 'PATCH',
    body: JSON.stringify(editTransaction.value),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElEditTransaction'
    }
  });

  if (response.ok) {
    alert('Transaction edited succesfully');
  } else {

    console.log('LCDSM')
    const errorData = await response.json();
    console.error('Error en la API:', errorData);
    alert('Error editing transaction');
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
  editTransaction.value.datetime = new Date().toISOString()
}

onMounted(() => {
  traerClientes();
  actualizarFechaHora();
  setInterval(actualizarFechaHora, 60000); // cada 60 seg
});

import { watch } from 'vue'
// Espera que cambien ambos valores y actualiza el total
watch(
  () => [editTransaction.value.crypto_code, editTransaction.value.crypto_amount],
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
    editTransaction.value.money = money.value * editTransaction.value.crypto_amount;
  } catch (error) {
    console.error('Error llamando a la API de cripto:', error);
    money.value = 0;
  }
}

import TransactionNavBar from './TransactionNavBar.vue';
</script>

<template>
  <TransactionNavBar></TransactionNavBar>

  <div v-if="transaction=== undefined">
    <p>CARGANDO...</p>
  </div>

  <div v-else>
    <h1>EDIT TRANSACTION</h1>
    <h2>ID {{ editTransaction.id }}</h2>
    <h3>La hora es: {{ fechaHora }}</h3>
    <div class="form-container">

      <Form :validation-schema="schema" @submit="EditarDatosApi" id="formulario-carga" class="form-box label ">

        <label>
      Action:
    <Field as="select" name="action" v-model="editTransaction.action" class="input-field">
    <option disabled value="">CHOOSE AN ACTION</option>
    <option value="purchase">PURCHASE</option>
    <option value="sell">SELL</option>
    </Field>
    </label>
    <ErrorMessage name="action" class="error-message"/>

    <br>

    <label>
   Crypto:
    <Field as="select" name="crypto_code" v-model="editTransaction.crypto_code" class="input-field">
    <option disabled value="">Seleccione una opción</option>
    <option value="BTC">Bitcoin</option>
    <option value="ETH">Ethereum</option>
    <option value="USDC">USDC</option>
    </Field>
      </label>
      <ErrorMessage name="crypto_code" class="error-message"/>

    <br>

    <br>

    <label >
      CLIENT:
      <Field as="select" v-model="editTransaction.client_id" name="client_id" id="client_id" class="input-field">
        <option
            v-for="client in clients"
            :key="client.id"
            :value="client.id"
        >{{ client.name }}
      </option>
      </Field>
    </label>


    <ErrorMessage name="client_id" class="error-message"></ErrorMessage>

    <br>

    <label>
      CRYPTO AMOUNT:
      <Field
      v-model="editTransaction.crypto_amount"
       name="crypto_amount"
      type="number"
      step="any"
       id="crypto_amount"
      class="input-field"/>
    <ErrorMessage name="crypto_amount" class="error-message"/>
      </label>

      <br>
      <br>
      <br>

        <Field type="hidden" name="action" v-model="editTransaction.action" id="action"/>
        <Field type="hidden" name="crypto_code" v-model="editTransaction.crypto_code" id="crypto_code"/>
        <Field type="hidden" name="client_id"  v-model="editTransaction.client_id" id="client_id"/>
        <Field type="hidden" name="crypto_amount" v-model="editTransaction.crypto_amount"  id="crypto_amount"/>
        <Field type="hidden" name="money" v-model="editTransaction.money"  id="money"/>
        <Field type="hidden" name="datetime" v-model="editTransaction.datetime"  id="datetime"/>

      <h1 v-if="editTransaction.money > 0">PRECIO ${{ editTransaction.money }}</h1>
    <input type="submit" value="Editar" class="submit-button">

  </Form>

  </div>
  </div>


</template>

<style scoped>
.form-container {
  max-width: 500px;
  margin: 50px auto;
  padding: 2rem;
  background-color: #ffffff;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  margin-bottom: 2rem;
  font-size: 1.8rem;
  color: #333;
}

.form-box label {
  display: block;
  margin-bottom: 1.5rem;
  font-weight: 600;
  color: #444;
}

.input-field {
  display: block;
  width: 100%;
  padding: 0.6rem;
  margin-top: 0.3rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  font-size: 1rem;
}

.input-field:focus {
  border-color: #007BFF;
  outline: none;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.2);
}

.error-message {
  color: #e74c3c;
  font-size: 0.9rem;
  margin-top: 0.3rem;
}

.submit-button {
  background-color: #007BFF;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  font-size: 1rem;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  display: block;
  margin: 0 auto;
}

.submit-button:hover {
  background-color: #0056b3;
}
</style>
