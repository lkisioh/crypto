<template>
  <div id="contenedor-general">
    <h1 id="titulo" class="caja">{{ datosNotebook.title }}</h1>

    <img class="caja" id="imagen-notebook" :src="datosNotebook.image_url" alt="" />

    <div class="caja" id="descripcion-producto">
      <p id="descripcion">
        {{ datosNotebook.description }}
      </p>

      <button id="agregar-favoritos">Agregar a favoritos</button>

      <p id="agregado-favoritos-texto"></p>

      <h2>Características</h2>
      <ul id="lista-tipos">
        <li
            v-for="(notebook, indice) in datosNotebook.features"
            :key="indice"
        >{{ notebook.name + ': ' + notebook.description }}</li>
      </ul>

      <a :href="datosNotebook.factory_url" id="link-sitio-web">Sitio web</a>
    </div>

    <div class="caja" id="opiniones">
      <h2>Opiniones</h2>
      <ul>
        <li>Buena notebook - 5/5</li>
        <li>Muy buena - 4/5</li>
        <li>Me llego rota - 1/5</li>
      </ul>
    </div>

    <div class="caja" id="formulario-opinion">
      <h2>Deja tu opinión</h2>

      <form action="#" method="post">
        <label>
          Comentario:
          <input type="text" name="comentario" />
        </label>

        <label>
          Calificación:
          <select name="calificacion">
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
          </select>
        </label>

        <label>
          <input type="submit" value="Enviar" />
        </label>
      </form>
    </div>
  </div>
</template>

<style>
</style>


<script setup>
import {ref} from 'vue'

let datosNotebook = ref({
  title: '',
  description: '',
  image_url: '',
  factory_url: '',
  features: [
    {
      name: '',
      description: ''
    },
    {
      name: '',
      description: ''
    }
  ]
});

async function cargarDatosApi() {
  let respuesta = await fetch('https://my-json-server.typicode.com/agustinruatta/fake_json_server_db/products/1');
  datosNotebook.value = await respuesta.json();
}

cargarDatosApi();
</script>

<template>

</template>
