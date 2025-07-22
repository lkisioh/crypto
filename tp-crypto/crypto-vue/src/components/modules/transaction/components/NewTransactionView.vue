<script setup>
import { ref , onMounted} from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';

let schema = {
    action: yup.string().nonNullable().required(),
    crypto_code: yup.string().required(),
    client_id: yup.number().required(),
    crypto_amount: yup.number().min(0).nonNullable().required(),

}

let newTransaction = ref({
    action:'',
    crypto_code: '',
    client_id: '',
    crypto_amount: '',
});
  //nuevo
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



onMounted(() => {
  traerClientes();

});

import TransactionNavBar from './TransactionNavBar.vue';
</script>

<template>
  <TransactionNavBar></TransactionNavBar>

  <div v-if="clients.length === 0">
    <p>Aún no hay clientes cargados.</p>
  </div>

  <div v-else>
    <h1>NEW TRANSACTION FORM</h1>

    <div class="form-container">
      <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga" class="form-box label ">

    <label>
      Action:
    <Field as="select" name="action" v-model="newTransaction.action" class="input-field">
    <option disabled value="">CHOOSE AN ACTION</option>
    <option value="purchase">PURCHASE</option>
    <option value="sell">SELL</option>
    </Field>
    </label>
    <ErrorMessage name="action" class="error-message"/>

    <br>

    <label>
   Crypto:
    <Field as="select" name="crypto_code" v-model="newTransaction.crypto_code" class="input-field">
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
      <Field as="select" v-model="newTransaction.client_id" name="client_id" id="client_id" class="input-field">
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
      v-model="newTransaction.crypto_amount"
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

        <Field type="hidden" name="action" v-model="newTransaction.action" id="action"/>
        <Field type="hidden" name="crypto_code" v-model="newTransaction.crypto_code" id="crypto_code"/>
        <Field type="hidden" name="client_id"  v-model="newTransaction.client_id" id="client_id"/>
        <Field type="hidden" name="crypto_amount" v-model="newTransaction.crypto_amount"  id="crypto_amount"/>
        <Field type="hidden" name="money" v-model="newTransaction.money"  id="money"/>
        <Field type="hidden" name="datetime" v-model="newTransaction.datetime"  id="datetime"/>

      <h1 v-if="newTransaction.money > 0">PRECIO ${{ newTransaction.money }}</h1>
    <input type="submit" value="Guardar" class="submit-button">

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
