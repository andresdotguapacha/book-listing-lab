# Book Listing Web Application

A full-stack digital library app with a React frontend and a .NET backend API, allowing users to add, edit, filter, and manage books and authors. The UI has a retro green-on-black console vibe.

Take a look [PREVIEW](https://jam.dev/c/06e1b4e4-ec5e-400a-8917-67b800de5633)

---

## Table of Contents

- [Project Overview](#project-overview)
- [Features](#features)
- [Folder Structure](#folder-structure)
- [Prerequisites](#prerequisites)
- [Backend Setup (BookListingAPI)](#backend-setup-booklistingapi)
- [Frontend Setup (book-listing-web)](#frontend-setup-book-listing-web)
- [Running the Application](#running-the-application)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)
  
---

## Project Overview

This project is a simple digital library management system featuring:

- A .NET Core backend API with Entity Framework Core for data management.
- A React-based frontend that lets users view, filter, add, edit, and delete books with multiple authors.
- Custom filters including title search, date range, and number of authors.
- A retro console-themed UI with green text on black background.

---

## Features

- Add and edit books with multiple authors.
- Filter books by title, publication date range, and author count.
- Sort books by title, publication date, or number of authors.
- Responsive layout supporting desktop and mobile.
- Confirmation on delete operations.

---

## Folder Structure

- `BookListingAPI/` — Backend .NET Web API project.
- `book-listing-web/` — Frontend React app created with Create React App.

---

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Node.js (v18 or newer)](https://nodejs.org/)
- npm (comes with Node.js)
- SQL Server or LocalDB instance for backend database

---

## Backend Setup (BookListingAPI)

1.  Navigate to the backend folder:

    ```bash
    cd BookListingAPI
    ```

2.  Configure the connection string in `appsettings.json`:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookListingDB;Trusted_Connection=True;"
    }
    ```

3.  Restore NuGet Packages:

    ```bash
    dotnet restore
    ```

4.  Apply Entity Framework migrations and update the database:

    ```bash
    dotnet ef database update
    ```

5.  Run the backend API:

    ```bash
    dotnet run
    ```

---

## Frontend Setup (book-listing-web)

1.  Navigate to the frontend folder:

    ```bash
    cd book-listing-web
    ```

2.  Install dependencies:

    ```bash
    npm install
    ```

3.  Start the React development server:

    ```bash
    npm start
    ```

The frontend will run on `http://localhost:3000` by default and communicate with the backend API on port 5080.

---

## Running the Application

1.  Start the backend
2.  Start the frontend React app
3.  Open your browser at `http://localhost:3000` to access the app.

---

## Technologies Used

-   **Backend:** .NET 7, ASP.NET Core Web API, Entity Framework Core, SQL Server
-   **Frontend:** React 19, TypeScript, Axios, CSS (retro terminal theme)
-   **Tools:** Visual Studio / VS Code, npm, Entity Framework CLI

---

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests for bug fixes and improvements.

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

---
