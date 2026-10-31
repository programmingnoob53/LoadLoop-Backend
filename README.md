# LoadLoop Backend

A backend system for **LoadLoop**, a freight exchange platform designed to connect businesses seeking transportation services with transport providers offering available capacity.

This repository contains the server-side implementation responsible for handling API requests, business operations, and database communication.

## Overview

LoadLoop aims to simplify the connection between shipping businesses and transport providers through a shared freight exchange platform.

The backend provides a RESTful API that communicates with backend services to process application requests and interact with the database.

## Technology Stack

- **Language:** C#
- **Framework:** ASP.NET Core
- **API Architecture:** REST
- **Service Communication:** Windows Communication Foundation (WCF)
- **Database:** Microsoft SQL Server
- **Data Transfer:** Data Transfer Objects (DTOs)

## Architecture

The application separates HTTP request handling from backend business operations.

```text
Client / Frontend
       |
       v
ASP.NET Core Web API
       |
       v
WCF Service Client
       |
       v
LoadLoop Service
       |
       v
Microsoft SQL Server
```

### Components

- **ASP.NET Core API:** Exposes HTTP endpoints and handles incoming client requests.
- **WCF Services:** Handle backend operations through service contracts.
- **DTOs:** Define the data exchanged between application components.
- **SQL Server:** Stores application data.

## Current Functionality

The backend currently includes operations for:

- User management
- Truck management
- Retrieving individual records
- Retrieving collections of records
- Registering records
- Deleting records

Additional functionality may be introduced as the project evolves.

## Getting Started

### Prerequisites

- Visual Studio
- .NET SDK compatible with the project
- Microsoft SQL Server
- SQL Server Management Studio (SSMS)

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/programmingnoob53/LoadLoop-Backend.git
   ```

2. Open the solution file (`.sln`) in Visual Studio.

3. Configure your SQL Server connection string using your local development configuration.

4. Ensure the WCF service is configured and accessible.

5. Build the solution.

6. Run the ASP.NET Core API project.

7. Access the available API endpoints through Swagger (if enabled).

## Configuration and Security

Database credentials and other sensitive configuration values should be supplied through local configuration or environment variables.

## Project Status

**Development:** Ongoing</text>

This project is being developed and improved incrementally. Architecture, validation, error handling, and additional backend functionality may evolve over time.

## License

No license has been specified yet.
