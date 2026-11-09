# LoadLoop Backend

A backend for **LoadLoop**, a freight exchange platform that connects businesses needing transport with transport providers offering available capacity.

This repository contains the ASP.NET Core Web API that handles requests, applies business rules and talks to the database.

## Technology Stack

- **Language:** C#
- **Framework:** ASP.NET Core Web API (minimal API endpoints)
- **Data access:** Entity Framework Core (database-first)
- **Database:** Microsoft SQL Server OR SQL Server in Docker for linux 
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
- Trucks: register, list, get by id, get by user id, update, delete, get list per user id

## Getting Started

### Prerequisites

- .NET SDK 10
- Visual Studio, VS Code or Rider
- SQL Server (LocalDB on Windows) or Docker (Linux)

### Windows setup

1. Clone the repository:
   ```bash
   git clone https://github.com/programmingnoob53/LoadLoop-Backend.git
   ```
2. Create the database by running `database/schema.sql` in SSMS or Visual Studio.
3. Check the connection string in `appsettings.json` (or set it as a user secret):
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LoadLoop;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
4. Run the API:
   ```bash
   dotnet run --project LoadLoop.Api
   ```
5. Open `/swagger` on the URL printed in the console.

### Linux setup (Arch)

1. Install the tools:
   ```bash
   sudo pacman -S docker
   sudo systemctl enable --now docker
   sudo usermod -aG docker $USER   # log out and back in afterwards
   ```
2. Clone the repository:
   ```bash
   git clone https://github.com/programmingnoob53/LoadLoop-Backend.git
   cd LoadLoop-Backend
   ```
3. Start SQL Server in a container. The password needs 8+ characters with three of: uppercase, lowercase, digits, symbols. Avoid `!` and `$`, which the shell can mangle.
   ```bash
   docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=<Password>' -p 1433:1433 --name sql --restart unless-stopped -v sqldata:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2022-latest
   ```
   Wait about 15 seconds, then check that `docker ps` shows `Up`.
4. Create the database from the schema script:
   ```bash
   docker cp Database/schema.sql sql:/tmp/schema.sql
   docker exec -it sql /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P '<Password>' -C -i /tmp/schema.sql
   ```
5. Set the connection string as a user secret:
   ```bash
   cd LoadLoop.Api
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost,1433;Database=LoadLoop;User Id=sa;Password=<Password>;TrustServerCertificate=True;"
   ```
6. Run the API:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```
   Open `/swagger` on the `http://localhost:<port>` URL printed in the console.

After a reboot, start the database with `docker start sql` if it isn't already running.

### Testing trucks

Public registration always creates role `U`. To test truck endpoints, promote a user:
```sql
UPDATE USER_TABLE SET USER_ROLE = 'T' WHERE USER_ID = 1;
```
## Roles

- `A`: admin (created manually, never through public registration)
- `T`: transport provider (can own trucks)
- `U`: normal user (the default for registration)

## Security Notes

- Passwords are stored as hashes, never in plain text.
- Authentication and authorization (JWT) are not implemented yet, so don't expose this API publicly.
- Never commit database files or credentials.

## Project Status

In development.

Planned next:
- JWT authentication and authorization.
- Unit and integration tests.
- Improved API documentation.

## License

No license has been specified yet.
