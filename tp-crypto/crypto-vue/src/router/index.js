import { createRouter, createWebHistory } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import ClientsView from '../views/ClientsView.vue'
import TransactionsView from '../views/TransactionsView.vue'

import NewClientView from '../components/modules/client/components/NewClientView.vue'
import ClientListView from '../components/modules/client/components/ClientListView.vue'
import ClientView from '@/components/modules/client/components/ClientView.vue'
import ClientEditView from '@/components/modules/client/components/ClientEditView.vue'

import NewTransactionView from '../components/modules/transaction/components/NewTransactionView.vue'
import TransactionListView from '../components/modules/transaction/components/TransactionListView.vue'
import TransactionView from '@/components/modules/transaction/components/TransactionView.vue'
import TransactionEditView from '@/components/modules/transaction/components/TransactionEditView.vue'

import ShopView from '@/views/ShopView.vue'

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
    },
    {
      path: '/shop',
      name: 'shop',
      component: ShopView
    },
    {
      path: '/client/:id',
      name: 'client',
      component: ClientView
    },
    {
      path: '/client/edit/:id',
      name: 'clientEdit',
      component: ClientEditView
    },
    {
      path: '/transaction/:id',
      name: 'transaction',
      component: TransactionView
    },
    {
      path: '/transaction/edit/:id',
      name: 'transactionEdit',
      component: TransactionEditView
    }
  ],
})

export default router
