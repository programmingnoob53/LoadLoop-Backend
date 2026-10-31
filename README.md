# LoadLoop Backend

A backend for **LoadLoop**, a freight exchange platform that connects businesses needing transport with transport providers offering available capacity.

This repository contains the ASP.NET Core Web API that handles requests, applies business rules and talks to the database.

## Technology Stack

- **Language:** C#
- **Framework:** ASP.NET Core Web API (minimal API endpoints)
- **Data access:** Entity Framework Core (database-first)
- **Database:** Microsoft SQL Server (LocalDB for development)
- **Data transfer:** DTOs with validation attributes

## Architecture

```text
Client / Frontend
       |
       v
Endpoints (Extensions/)
       |
       v
Services (business rules)
       |
       v
EF Core DbContext
       |
       v
SQL Server
```

- **Endpoints:** map HTTP routes, call services, return the right status codes.
- **Services:** hold business rules (for example, only transport providers can own trucks).
- **DTOs:** define what goes in and out of the API, so entities and password hashes never leave the server.
- **EF Core:** maps tables to C# classes, scaffolded from the existing database.

## Current Functionality

- Users: register, list, get by id, update, delete
- Trucks: in progress

## Getting Started

### Prerequisites

- .NET SDK 10
- Visual Studio (or any editor)
- SQL Server or SQL Server LocalDB

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/programmingnoob53/LoadLoop-Backend.git
   ```
2. Create the database by running `LoadLoop.Api/Sql Scripts/Create Database.sql` in SSMS or Visual Studio.
3. Set the connection string in `appsettings.json` (or user secrets):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LoadLoop;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
4. Run the API:
   ```bash
   dotnet run --project LoadLoop.Api
   ```
5. Open Swagger UI to try the endpoints.

## Roles

- `A`: admin (created manually, never through public registration)
- `T`: transport provider (can own trucks)
- `U`: normal user (the default for registration)

## Security Notes

- Passwords are stored as hashes, never in plain text.
- Authentication and authorization (JWT) are not implemented yet, so don't expose this API publicly.
- Never commit database files or credentials.

## Project Status

In development. Planned next: trucks, authentication, and tests.

## License

No license has been specified yet.
