import axios from 'axios'

const apiClient = axios.create({
  baseURL: 'http://localhost:5069/api',
  withCredentials: false,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json'
  }
})

export default {
  getContacts() {
    return apiClient.get('/contacts')
  },
  searchContacts(query) {
    return apiClient.get(`/contacts/search?query=${query}`)
  },
  getContact(id) {
    return apiClient.get(`/contacts/${id}`)
  },
  createContact(contact) {
    return apiClient.post('/contacts', contact)
  },
  updateContact(id, contact) {
    return apiClient.put(`/contacts/${id}`, contact)
  },
  deleteContact(id) {
    return apiClient.delete(`/contacts/${id}`)
  }
}
