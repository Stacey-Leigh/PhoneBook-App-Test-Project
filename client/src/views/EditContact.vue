<template>
  <div>
    <h1 class="mb-4">Edit Contact</h1>

    <form @submit.prevent="submitForm" v-if="contact">
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
          Update Contact
        </button>
      </div>

      <div v-if="error" class="alert alert-danger mt-3">
        {{ error }}
      </div>
    </form>

    <div v-if="loading" class="text-center my-4">
      <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden">Loading...</span>
      </div>
    </div>

    <div v-if="notFound" class="alert alert-danger">
      Contact not found.
    </div>
  </div>
</template>

<script>
import api from '@/services/api'

export default {
  name: 'EditContact',
  props: ['id'],
  data() {
    return {
      contact: null,
      loading: false,
      error: null,
      notFound: false
    }
  },
  created() {
    this.fetchContact()
  },
  methods: {
    async fetchContact() {
      this.loading = true
      this.error = null
      try {
        const response = await api.getContact(this.id)
        this.contact = response.data
      } catch (err) {
        if (err.response && err.response.status === 404) {
          this.notFound = true
        } else {
          this.error = 'Failed to load contact. Please try again later.'
        }
        console.error(err)
      } finally {
        this.loading = false
      }
    },
    async submitForm() {
      this.loading = true
      this.error = null

      try {
        await api.updateContact(this.id, this.contact)
        this.$router.push('/')
      } catch (err) {
        if (err.response && err.response.data) {
          this.error = err.response.data
        } else {
          this.error = 'Failed to update contact. Please try again later.'
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
