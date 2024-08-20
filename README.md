# Notes Web Application

A web application that allows users to perform CRUD operations on notes with JWT authentication. Built using ASP.NET and a monolithic architecture.

## Features

- **User Authentication:** Secure user login and registration using JWT (JSON Web Tokens).
- **CRUD Operations:** Create, Read, Update, and Delete notes.
- **User-Specific Notes:** Each user can manage their own notes.
- **Responsive UI:** Intuitive and responsive design for a seamless user experience.

## Tech Stack

- **Backend:** ASP.NET Core
- **Authentication:** JWT (JSON Web Tokens)
- **Architecture:** Monolithic

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any compatible database

### Installation

1. Clone the repository:

    ```bash
    git clone (https://github.com/thedineshjoshi/Notes_Backend.git)
    cd notes-web-application
    ```

2. Restore the project dependencies:

    ```bash
    dotnet restore
    ```

3. Set up the database:

    - Update the connection string in `appsettings.json` to match your database configuration.
    - Run the database migrations:

    ```bash
    dotnet ef database update
    ```

4. Run the application:

    ```bash
    dotnet run
    ```

5. Navigate to `http://localhost:5000` in your browser to access the application.

## Usage

### User Registration

- Endpoint: `POST /api/auth/register`
- Body: `{ "username": "string", "password": "string" }`

### User Login

- Endpoint: `POST /api/auth/login`
- Body: `{ "username": "string", "password": "string" }`
- Returns: JWT token for authenticated requests

### CRUD Operations on Notes

- **Create Note:**
  - Endpoint: `POST /api/notes`
  - Body: `{ "title": "string", "content": "string" }`

- **Get All Notes:**
  - Endpoint: `GET /api/notes`

- **Update Note:**
  - Endpoint: `PUT /api/notes/{id}`
  - Body: `{ "title": "string", "content": "string" }`

- **Delete Note:**
  - Endpoint: `DELETE /api/notes/{id}`

## Configuration

- **JWT Secret:** Set the JWT secret key in `appsettings.json` under `JwtSettings`.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## Acknowledgments

- [ASP.NET Core](https://dotnet.microsoft.com/apps/aspnet) for the framework
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) for data access
- [JWT](https://jwt.io/) for authentication

