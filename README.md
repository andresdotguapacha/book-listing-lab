# Book Listing Web Application

A full-stack digital library app with a React frontend and a .NET backend API, allowing users to add, edit, filter, and manage books and authors. The UI has a retro green-on-black console vibe.

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
- [Contact](#contact)

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

1. Navigate to the backend folder:

   ```bash
   cd BookListingAPI
   ```

Configure the connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookListingDB;Trusted_Connection=True;"
}

Restore Nugget Packages:
dotnet restore

Apply Entity Framework migrations and update the database:

bash
Copy
Edit
dotnet ef database update


Run the backend API on port 5080:

bash
Copy
Edit
dotnet run --urls "http://localhost:5080"
Frontend Setup (book-listing-web)
Navigate to the frontend folder:

bash
Copy
Edit
cd book-listing-web
Install dependencies:

bash
Copy
Edit
npm install
Start the React development server:

bash
Copy
Edit
npm start
The frontend will run on http://localhost:3000 by default and communicate with the backend API on port 5080.

Running the Application
Start the backend API (see Backend Setup).

Start the frontend React app (see Frontend Setup).

Open your browser at http://localhost:3000 to access the app.

Technologies Used
Backend: .NET 7, ASP.NET Core Web API, Entity Framework Core, SQL Server

Frontend: React 19, TypeScript, Axios, CSS (retro terminal theme)

Tools: Visual Studio / VS Code, npm, Entity Framework CLI

Contributing
Contributions are welcome! Feel free to open issues or submit pull requests for bug fixes and improvements.

License
This project is licensed under the MIT License. See the LICENSE file for details.