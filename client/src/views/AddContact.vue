<template>
  <div>
    <h1 class="mb-4">Add New Contact</h1>

    <form @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="name" class="form-label">Name *</label>
        <input type="text"
               class="form-control"
               id="name"
               v-model="contact.name"
               required />
      </div>

      <div class="mb-3">
        <label for="phoneNumber" class="form-label">Phone Number *</label>
        <input type="tel"
               class="form-control"
               id="phoneNumber"
               v-model="contact.phoneNumber"
               required />
      </div>

      <div class="mb-3">
        <label for="emailAddress" class="form-label">Email Address</label>
        <input type="email"
               class="form-control"
               id="emailAddress"
               v-model="contact.emailAddress" />
      </div>

      <div class="d-flex justify-content-between">
        <router-link to="/" class="btn btn-outline-secondary">
          Cancel
        </router-link>
        <button type="submit" class="btn btn-primary" :disabled="loading">
          <span v-if="loading" class="spinner-border spinner-border-sm"></span>
          Save Contact
        </button>
      </div>

      <div v-if="error" class="alert alert-danger mt-3">
        {{ error }}
      </div>
    </form>
  </div>
</template>

<script>
import api from '@/services/api'

export default {
  name: 'AddContact',
  data() {
    return {
      contact: {
        name: '',
        phoneNumber: '',
        emailAddress: ''
      },
      loading: false,
      error: null
    }
  },
  methods: {
    async submitForm() {
      this.loading = true
      this.error = null

      try {
        await api.createContact(this.contact)
        this.$router.push('/')
      } catch (err) {
        if (err.response && err.response.data) {
          this.error = err.response.data
        } else {
          this.error = 'Failed to add contact. Please try again later.'
        }
        console.error(err)
      } finally {
        this.loading = false
      }
    }
  }
}
</script>

<style scoped>
  form {
    max-width: 500px;
    margin: 0 auto;
  }
</style>
