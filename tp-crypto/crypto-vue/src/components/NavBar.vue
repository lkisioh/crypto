<template>
  <div class="navBar">
    <ul>
      <li>
        <RouterLink to="/" class="no-subrayado">Menu</RouterLink>
      </li>
      <li>
        <RouterLink to="/clients" class="no-subrayado">Clients</RouterLink>
      </li>
      <li>
        <RouterLink to="/transactions" class="no-subrayado">Transactions</RouterLink>
      </li>
      <li>
        <RouterLink to="/shop" class="no-subrayado">Market</RouterLink>
      </li>

    </ul>

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
