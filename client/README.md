# Phonebook Application

A simple phonebook application built with Vue.js (frontend) and .NET (backend).

## Features

- Add new contacts with name, phone number, and optional email
- View all contacts in a list
- Search contacts by name or phone number
- Edit existing contacts
- Delete contacts
- Responsive design that works on mobile and desktop

## Technologies Used

- Frontend: Vue.js 3, Vue Router, Axios, Bootstrap 5
- Backend: .NET 6, Entity Framework Core, SQLite
- Testing: xUnit (backend), Vitest (frontend)

## Prerequisites

- Node.js (v16 or higher)
- .NET 6 SDK
- npm or yarn

## Setup Instructions

### Backend Setup

1. Navigate to the server directory:
   cd server

2. Restore dependencies:
   dotnet restore

3. Build the project:
   dotnet build

4. Run the application:
   dotnet run


### Frontend Setup

1. Navigate to the client directory:
   cd client

2. Install dependencies
   npm install

3. Run the development server:
   npm run dev