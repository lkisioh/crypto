<script setup>
import { ref } from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';

import ClientNavBar from './ClientNavBar.vue';

let schema = {
  name: yup.string().required().min(1),
  email: yup.string().email().required().min(1),
}

let newClient = ref({
  name: '',
  email: ''
});

async function enviarDatosApi() {

  let response = await fetch('https://localhost:7294/api/Client',
  {
    method: 'POST',
    body: JSON.stringify(newClient.value),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElPost'
    }
  });

  //Verifico si fue exitosa la respuesta
  if (response.ok) {
    alert('New client add succesfully');
  } else {
    alert('Error to add a new client');
  }}

</script>

<template>
  <ClientNavBar></ClientNavBar>

  <h1>NEW CLIENT FORM</h1>

   <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga">
    <label>
      Name:
    <Field v-model="newClient.name" type="text" name="name" id="name"/>
    </label>
    <ErrorMessage name="name"></ErrorMessage>

    <br>

    <label>
      Email:
      <Field v-model="newClient.email" type="text" name="email" id="email"/>
    </label>
    <ErrorMessage name="email"></ErrorMessage>

    <br>

    <input type="submit" value="Guardar">
  </Form>
</template>

<style>

</style>
