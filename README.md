# Clean Architecture Template for .NET 8

A comprehensive Clean Architecture template built with .NET 8, following
Domain-Driven Design (DDD) principles and implementing CQRS pattern with
MediatR.

## Architecture Overview

This template implements Clean Architecture with clear separation of
concerns across multiple layers:

| Project                    | Purpose                             | Key Components                                 | Dependencies                |
| -------------------------- | ----------------------------------- | ---------------------------------------------- | --------------------------- |
| **Project.API**            | Web API hosting and configuration   | `Program.cs`, Middleware, Service registration | Presentation, Application   |
| **Project.Presentation**   | HTTP endpoints and request handling | Carter endpoints, Controllers                  | Application, Contracts      |
| **Project.Application**    | Business logic orchestration        | MediatR Handlers, Pipeline Behaviors           | Domain, Contracts           |
| **Project.Domain**         | Core business logic and rules       | Entities, Value Objects, Domain Events         | None                        |
| **Project.Persistence**    | Data access and storage             | `AppDbContext`, Repositories, Migrations       | Domain, Contracts           |
| **Project.Infrastructure** | Cross-cutting concerns              | Domain Event Dispatching, External Services    | Domain, Application         |
| **Project.Contracts**      | Shared data contracts               | DTOs, Commands, Queries, Messages              | None (shared across layers) |

## Key Notes

This template demonstrates modern architectural best practices and design patterns:

- **Domain-Driven Design (DDD):** Separation of domain, application, infrastructure, and presentation.
- **CQRS (Command Query Responsibility Segregation):** Separation of write operations (Commands) and read operations (Queries).
- **Mediator Pattern (via MediatR):** Implements CQRS by dispatching requests to their corresponding handlers, ensuring loose coupling between controllers and business logic.
- **Repository Pattern:** Abstraction for data access in persistence layer.
- **Unit of Work:** Coordinated transaction management across multiple repositories.
- **Domain Events:** Decoupled communication of business events.
- **Cross-Cutting Concerns:** Centralized handling of logging, validation, exception handling.

## Project Structure

### Core Projects

#### \[Project\].Domain

- **Purpose**: Contains the core business logic and domain entities\
- **Key Components**:
  - `Entities/`: Domain entities and aggregate roots\
  - `ValueObjects/`: Immutable value objects\
  - `DomainEvents/`: Domain events for decoupled communication\
  - `Abstractions/`: Base classes and interfaces\
  - `Enums/`: Domain-specific enumerations\
  - `Exceptions/`: Domain-specific exceptions

#### \[Project\].Application

- **Purpose**: Contains application logic and use cases\
- **Key Components**:
  - `UseCases/`: Command and query handlers\
  - `Behaviors/`: MediatR pipeline behaviors (Transaction, Domain
    Events)\
  - `Interfaces/`: Application service contracts\
  - `DependencyInjection/`: Service registration

#### \[Project\].API

- **Purpose**: Web API layer with endpoints and middleware\
- **Key Components**:
  - `DependencyInjection/`: API-specific service configuration\
  - `Middlewares/`: Custom middleware (Exception handling)\
  - `Program.cs`: Application entry point\
  - `appsettings.json`: Configuration files

#### \[Project\].Persistence

- **Purpose**: Data access layer using Entity Framework Core\
- **Key Components**:
  - `AppDbContext.cs`: Main database context\
  - `Configurations/`: EF Core entity configurations\
  - `Migrations/`: Database migration files\

#### \[Project\].Infrastructure

- **Purpose**: Cross-cutting concerns and external integrations\
- **Key Components**:
  - `DomainEvents/`: Domain event dispatching and handling\
  - `DependencyInjection/`: Infrastructure service registration

#### \[Project\].Contracts

- **Purpose**: Shared contracts and DTOs\
- **Key Components**:
  - `Abstractions/`: Base interfaces for CQRS\
  - `DTOs/`: Data transfer objects\
  - `Messages/`: Application messages and constants\
  - `Exceptions/`: Shared exception types\
  - `Settings/`: Configuration settings

#### \[Project\].Presentation

- **Purpose**: Presentation layer components\
- **Key Components**:
  - Controllers and endpoints\
  - View models and presentation logic

---

## How to Use

1.  **Clone the template**

    ```bash
    git clone https://github.com/Bentanik/CleanArchDotNetTemplate
    ```

2.  **Install the template into .NET CLI**\
    Inside the project root (where .template.config/template.json is located):

    ```bash
    dotnet new install ./
    ```

3.  **Create a new project from the template**

    ```bash
    dotnet new cleanarch-cqrs-ddd -n MyApp
    ```

    Here:

    - `cleanarch-cqrs-ddd` is the shortName defined in `template.json`\
    - `-n MyApp` specifies the name of the new solution
