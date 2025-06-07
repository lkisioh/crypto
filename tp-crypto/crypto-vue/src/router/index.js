import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ClientsView from '../views/ClientsView.vue'
import ClientTransactionsView from '../views/ClientTransactionsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    }
    ,
    {
      path: '/clients',
      name: 'clients',
      component: ClientsView
    },
    {
      path: '/Transactions',
      name: 'transactions',
      component: ClientTransactionsView
    }
  ],
})

export default router
