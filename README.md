# MovieCatalogAPI – Movie Ticket Booking System (Backend)

This is my first major backend project, built with ASP.NET Core Web API, applying Clean Architecture principles and real-world backend design patterns. It’s a fully structured backend focused on learning and building maintainable, scalable APIs.

## Key Highlights / Learning Points

- Implemented Clean Architecture with Domain, Application, Infrastructure, and API layers.
- Built Repository & Service Layers for clean separation of business logic and data access.
- Used DTOs and manual mapping for safe API input/output handling.
- Implemented JWT Authentication and Role-based Authorization (Admin/User).
- Learned to structure relational data with Entity Framework Core and SQL Server.
- Hands-on experience with seat booking logic, movie likes, favorites, and reviews.
- Applied Dependency Injection and middleware for request handling.
- Practiced Code-First Migrations for evolving database schemas.
- Prepared APIs ready for future frontend integration.

**Note:** All credentials (database connection, JWT secret) are local and for learning purposes only. No production data is included.

## Tech Stack
- ASP.NET Core 9 (Web API)
- Entity Framework Core (Code-First Migrations)
- SQL Server
- JWT Authentication & Role-based Authorization

## Features

### Admin Features
- Manage Movies (add, update, delete, list)
- Manage Cinemas & Showtimes

### User Features
- Register, Login & Manage Profile
- Browse Movies and Showtimes
- Like & Favorite movies
- Write Reviews on movies
- Book Tickets with seat selection
