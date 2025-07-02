<script setup>
  import { ref } from 'vue';
  import { useRoute } from 'vue-router';

  let route = useRoute()
  let id = route.params.id

import ClientNavBar from './ClientNavBar.vue';

let client = ref(undefined);

  async function BuscarDatosApi() {

  let response = await fetch('https://localhost:7294/api/Client/'+id,
  {
    method: 'GET',
    body: JSON.stringify(),
    headers: {
      'Content-Type': 'application/json',
      'Authorization': 'Bearer HaciendoElPost'
    }
  });

  //Verifico si fue exitosa la respuesta
  if (response.ok) {
     client.value=  await response.json();
    console.log('Search succesfully');

  } else {
    alert('Error to find client');
  }}

  BuscarDatosApi();


</script>

<template>
  <ClientNavBar></ClientNavBar>
  <div v-if= "client === undefined"> CARGANDO...</div>
  <div v-else>
    <div class="form-container">

    <h1>CLIENT</h1>

    <h2>Name</h2>
    <h3>{{ client.name }}</h3>

    <h2>Email</h2>
    <h3>{{ client.email }}</h3>
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
