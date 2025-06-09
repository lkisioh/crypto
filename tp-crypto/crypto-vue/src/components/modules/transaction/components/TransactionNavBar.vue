<template>
  <div class="navBar">
    <ul>
      <li>
        <RouterLink to="/transactions/new" class="no-subrayado">New Transaction</RouterLink>
      </li>
      <li>
        <RouterLink to="/transactions/list" class="no-subrayado">List Transaction</RouterLink>
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
