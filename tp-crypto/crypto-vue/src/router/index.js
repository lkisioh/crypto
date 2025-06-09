import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import ClientsView from '../views/ClientsView.vue'
import TransactionsView from '../views/TransactionsView.vue'

import NewClientView from '../components/modules/client/components/NewClientView.vue'
import ClientListView from '../components/modules/client/components/ClientListView.vue'

import NewTransactionView from '../components/modules/transaction/components/NewTransactionView.vue'
import TransactionListView from '../components/modules/transaction/components/TransactionListView.vue'

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
      path: '/transactions',
      name: 'transactions',
      component: TransactionsView
    },
    {
      path: '/clients/new',
      name: 'newclient',
      component: NewClientView
    },
    {
      path: '/clients/list',
      name: 'clientlist',
      component: ClientListView
    },
    {
      path: '/transactions/new',
      name: 'newtransaction',
      component: NewTransactionView
    },
    {
      path: '/transactions/list',
      name: 'transactionlist',
      component: TransactionListView
    }
  ],
})

export default router
