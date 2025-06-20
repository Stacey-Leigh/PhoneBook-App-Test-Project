import { createRouter, createWebHistory } from 'vue-router'
import ContactList from '../views/ContactList.vue'
import AddContact from '../views/AddContact.vue'
import EditContact from '../views/EditContact.vue'

const routes = [
  {
    path: '/',
    name: 'ContactList',
    component: ContactList
  },
  {
    path: '/add',
    name: 'AddContact',
    component: AddContact
  },
  {
    path: '/edit/:id',
    name: 'EditContact',
    component: EditContact,
    props: true
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

export default router
