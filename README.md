# ShopApp

ShopApp is an e-commerce platform backend API built with ASP.NET Core, PostgreSQL, and Entity Framework Core. It provides CRUD operations for managing products and interacts with a PostgreSQL database for data storage.

## Table of Contents

- [Introduction](#introduction)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Database Setup](#database-setup)
  - [Running Migrations](#running-migrations)
  - [Running the Application](#running-the-application)
- [Contributing](#contributing)

## Introduction

ShopApp API is a backend service for managing products. It provides endpoints to perform CRUD operations on products stored in a PostgreSQL database. The application is structured using ASP.NET Core MVC, with repository and service layers for separation of concerns.

## Technologies Used

- **ASP.NET Core**: Web framework
- **Entity Framework Core**: Object-Relational Mapping (ORM) for database interactions
- **PostgreSQL**: Relational database for data storage
- **C#**: Primary programming language

## Getting Started

To get started with the ShopApp API, follow these steps:

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [PostgreSQL](https://www.postgresql.org/download/) installed locally or accessible via a connection string.

### Installation

1. **Clone the repository** or download the source code.

2. **Navigate to the project directory**:

   ```bash
   cd ShopApp
# ShopApp Database Setup and Migrations

This section provides instructions on setting up the PostgreSQL database and running migrations for the ShopApp API.

## Prerequisites

- PostgreSQL installed locally or accessible via a connection string.
- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

## Database Setup

1. **Install PostgreSQL**:
   - If not already installed, download and install PostgreSQL from [here](https://www.postgresql.org/download/).

2. **Create Database**:
   - Open pgAdmin or any PostgreSQL client.
   - Create a new database named `ShopAppDb`.

3. **Configure Connection String**:
   - Open `appsettings.json` in your ShopApp project.
   - Update the `ConnectionStrings` section with your PostgreSQL connection details:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=5432;Database=ShopAppDb;User Id=your_username;Password=your_password;"
     }
     ```

   Replace `your_username` and `your_password` with your PostgreSQL username and password.

## Running Migrations

1. **Install Entity Framework Core tools** (if not already installed):

   ```bash
   dotnet tool install --global dotnet-ef

2. **Apply Migrations**:
   - Open a terminal or command prompt.
   - Navigate to your ShopApp project directory.

   
   ```bash
   cd path/to/your/ShopApp

   # Apply migrations to create the database schema
   dotnet ef database update
   ```

 ## Running the Application

1. **Build the project**:

   ```bash
   cd path/to/your/ShopApp

   dotnet build

2. **Run the application**:
   
   ```bash
   dotnet run

## Contributing

Contributions are welcome! Please follow these steps to contribute to the project
