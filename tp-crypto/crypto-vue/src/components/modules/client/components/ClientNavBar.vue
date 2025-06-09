<template>
  <div class="navBar">
    <ul>
      <li>
        <RouterLink to="/clients/new" class="no-subrayado">New Client</RouterLink>
      </li>
      <li>
        <RouterLink to="/clients/list" class="no-subrayado">List Clients</RouterLink>
      </li>
    </ul>
  </div>
  <div>
    <RouterView></RouterView>
  </div>
</template>

<script setup>
import { RouterLink} from 'vue-router'

</script>

<style>

.navBar{
 display: flex;
 justify-content: center;
 align-items: center;
 padding: 10 px 20 px;
 top: 0;
 right:0;
 left:0;
 background-color: black;
 color: aqua;
}
li{
  display: inline;
  margin-right: 10px;
  margin-left: 15px;
  margin-right: 15px;
}
.no-subrayado{
  text-decoration: none
}

</style>
