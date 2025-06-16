<template>
  <div>
    <div class="d-flex justify-content-between align-items-center mb-4">
      <h1>Contacts</h1>
      <router-link to="/add"
                   class="btn btn-primary"
                   data-test="add-contact-btn">
        Add Contact
      </router-link>
    </div>

    <div class="mb-3">
      <input type="text"
             class="form-control"
             placeholder="Search by name or phone number"
             v-model="searchQuery"
             @input="searchContacts"
             data-test="search-input" />
    </div>

    <div v-if="error"
         class="alert alert-danger"
         data-test="error-message">
      {{ error }}
    </div>

    <div v-else-if="contacts.length === 0" class="alert alert-info">
      No contacts found.
    </div>

    <div v-else class="list-group">
      <div v-for="contact in contacts"
           :key="contact.id"
           class="list-group-item list-group-item-action"
           data-test="contact-item">
        <div class="d-flex justify-content-between align-items-center">
          <div>
            <h5 class="mb-1">{{ contact.name }}</h5>
            <p class="mb-1">{{ contact.phoneNumber }}</p>
            <small v-if="contact.emailAddress">{{ contact.emailAddress }}</small>
          </div>
          <div>
            <router-link :to="{ name: 'EditContact', params: { id: contact.id } }"
                         class="btn btn-sm btn-outline-primary me-2 mb-2 mb-md-0"
                         data-test="edit-btn">
              Edit
            </router-link>
            <button @click="deleteContact(contact.id)"
                    class="btn btn-sm btn-outline-danger"
                    data-test="delete-btn">
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import api from '@/services/api'

  export default {
    name: 'ContactList',
    data() {
      return {
        contacts: [],
        error: null, 
        searchQuery: ''
      }
    },
    created() {
      this.fetchContacts()
    },
    methods: {
      async fetchContacts() {
        try {
          const response = await api.getContacts()
          this.contacts = response.data
        } catch (err) {
          this.error = 'Failed to load contacts. Please try again later.'
          console.error(err)
        }
      },
      async searchContacts() {
        if (this.searchQuery.trim() === '') {
          this.fetchContacts()
          return
        }

        try {
          const response = await api.searchContacts(this.searchQuery)
          this.contacts = response.data
        } catch (err) {
          this.error = 'Failed to search contacts. Please try again later.'
          console.error(err)
        }
      },
      async deleteContact(id) {
        if (confirm('Are you sure you want to delete this contact?')) {
          try {
            await api.deleteContact(id)
            this.contacts = this.contacts.filter(contact => contact.id !== id)
          } catch (err) {
            alert('Failed to delete contact. Please try again later.')
            console.error(err)
          }
        }
      }
    }
  }
</script>

<style scoped>
  .list-group-item {
    margin-bottom: 10px;
    border-radius: 5px;
  }

  @media (max-width: 576px) {
    .d-flex {
      flex-direction: column;
      gap: 10px;
    }

    .btn {
      width: 100%;
    }
  }
</style>
