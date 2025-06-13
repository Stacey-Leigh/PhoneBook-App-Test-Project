<template>
  <div class="app-container">
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary mb-4">
      <div class="container">
        <router-link to="/" class="navbar-brand">Phonebook App</router-link>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
          <ul class="navbar-nav">
            <li class="nav-item">
              <router-link to="/" class="nav-link">Contacts</router-link>
            </li>
            <li class="nav-item">
              <router-link to="/add" class="nav-link">Add Contact</router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'App'
  }
</script>

<style>
  @import 'bootstrap/dist/css/bootstrap.css';

  .app-container {
    min-height: 100vh;
    background-color: #f8f9fa;
  }

  .navbar {
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  }

  .container {
    max-width: 800px;
  }

  @media (max-width: 576px) {
    .container {
      padding: 0 15px;
    }
  }
</style>
