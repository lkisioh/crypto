
<script setup>
import {ref} from 'vue';
import {Form, Field, ErrorMessage} from 'vee-validate';
import * as yup from 'yup';

let schema = {
  title: yup.string().min(1),
  description: yup.string().min(1),
  image_url: yup.string().url().min(1),
  factory_url: yup.string().url().min(1)
}

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

async function enviarDatosApi() {
  //No hace falta hacer event.preventDefault() porque vee-validate lo hace solo

  let response = await fetch('https://crudcrud.com/api/3dfc4bff57134489abd590c1bfc6bccc/notebooks', {
    method: 'POST',
    body: JSON.stringify(datosNotebook.value),
    headers: {
      'Content-Type': 'application/json'
    }
  });

  //Verifico si fue exitosa la respuesta
  if (response.ok) {
    alert('Notebook guardada correctamente');
  } else {
    alert('Error al guardar el notebook');
  }
}
</script>

<template>
   <Form :validation-schema="schema" @submit="enviarDatosApi" id="formulario-carga">
    <label>
      Título:
      <Field v-model="datosNotebook.title" type="text" name="title" id="title"/>
    </label>
    <ErrorMessage name="title"></ErrorMessage>

    <br>

    <label>
      Descripción:
      <Field v-model="datosNotebook.description" type="text" name="description" id="description"/>
    </label>
    <ErrorMessage name="description"></ErrorMessage>

    <br>

    <label>
      URL imagen:
      <Field v-model="datosNotebook.image_url" type="text" name="image_url" id="image"/>
    </label>
    <ErrorMessage name="image_url"></ErrorMessage>

    <br>

    <label>
      URL sitio web:
      <Field v-model="datosNotebook.factory_url" type="text" name="factory_url" id="site"/>
    </label>
    <ErrorMessage name="factory_url"></ErrorMessage>

    <br>

    <label>
      Característica 1:
      <Field v-model="datosNotebook.features[0].name" type="text" name="nameFeatureA" id="nameFeatureA"
             placeholder="Nombre"/>
      <Field v-model="datosNotebook.features[0].description" type="text" name="descriptionFeatureA"
             id="descriptionFeatureA" placeholder="Descripción"/>
    </label>

    <br>

    <label>
      Característica 2:
      <Field v-model="datosNotebook.features[1].name" type="text" name="nameFeatureB" id="nameFeatureB"
             placeholder="Nombre"/>
      <Field v-model="datosNotebook.features[1].description" type="text" name="descriptionFeatureB"
             id="descriptionFeatureB" placeholder="Descripción"/>
    </label>

    <br>

    <input type="submit" value="Guardar">
  </Form>
</template>

<style>

</style>
