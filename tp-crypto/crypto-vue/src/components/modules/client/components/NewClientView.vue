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
  <div class="form-container">

    <h1>NEW CLIENT FORM</h1>

   <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga" class="form-box">
    <label>
      Name:
    <Field v-model="newClient.name" type="text" name="name" id="name" class="input-field"/>
    </label>
    <ErrorMessage name="name" class="error-message"></ErrorMessage>

    <br>

    <label>
      Email:
      <Field v-model="newClient.email" type="text" name="email" id="email" class="input-field"/>
    </label>
    <ErrorMessage name="email" class="error-message"></ErrorMessage>

    <br>

    <input type="submit" value="Guardar" class="submit-button">
  </Form>

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
