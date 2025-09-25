# Clean Architecture Template for .NET 8

A comprehensive Clean Architecture template built with .NET 8, following Domain-Driven Design (DDD) principles and implementing CQRS pattern with MediatR.

## 🏗️ Architecture Overview

This template implements Clean Architecture with clear separation of concerns across multiple layers:

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                       │
│  ┌─────────────────┐  ┌─────────────────────────────────┐  │
│  │   [Project].API │  │    [Project].Presentation      │  │
│  │   (Web API)     │  │    (Controllers/Endpoints)     │  │
│  └─────────────────┘  └─────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                        │
│  ┌─────────────────────────────────────────────────────────┐│
│  │              [Project].Application                     ││
│  │  (Use Cases, CQRS, MediatR, Behaviors)                ││
│  └─────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Domain Layer                             │
│  ┌─────────────────────────────────────────────────────────┐│
│  │                [Project].Domain                        ││
│  │  (Entities, Value Objects, Domain Events, Business)    ││
│  └─────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                  Infrastructure Layer                       │
│  ┌─────────────────┐  ┌─────────────────────────────────┐  │
│  │[Project].Infra- │  │    [Project].Persistence       │  │
│  │structure        │  │    (EF Core, DbContext)        │  │
│  └─────────────────┘  └─────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Contracts Layer                          │
│  ┌─────────────────────────────────────────────────────────┐│
│  │                [Project].Contracts                     ││
│  │  (DTOs, Commands, Queries, Exceptions, Messages)       ││
│  └─────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
```

## 📁 Project Structure

### Core Projects

#### 🎯 [Project].Domain

- **Purpose**: Contains the core business logic and domain entities
- **Key Components**:
  - `Entities/`: Domain entities and aggregate roots
  - `ValueObjects/`: Immutable value objects
  - `DomainEvents/`: Domain events for decoupled communication
  - `Abstractions/`: Base classes and interfaces
  - `Enums/`: Domain-specific enumerations
  - `Exceptions/`: Domain-specific exceptions

#### 🔧 [Project].Application

- **Purpose**: Contains application logic and use cases
- **Key Components**:
  - `UseCases/`: Command and query handlers
  - `Behaviors/`: MediatR pipeline behaviors (Transaction, Domain Events)
  - `Interfaces/`: Application service contracts
  - `DependencyInjection/`: Service registration

#### 🌐 [Project].API

- **Purpose**: Web API layer with endpoints and middleware
- **Key Components**:
  - `DependencyInjection/`: API-specific service configuration
  - `Middlewares/`: Custom middleware (Exception handling)
  - `Program.cs`: Application entry point
  - `appsettings.json`: Configuration files

#### 🗄️ [Project].Persistence

- **Purpose**: Data access layer using Entity Framework Core
- **Key Components**:
  - `AppDbContext.cs`: Main database context
  - `Configurations/`: EF Core entity configurations
  - `Migrations/`: Database migration files
  - `Interceptors/`: EF Core interceptors

#### 🔌 [Project].Infrastructure

- **Purpose**: Cross-cutting concerns and external integrations
- **Key Components**:
  - `DomainEvents/`: Domain event dispatching and handling
  - `DependencyInjection/`: Infrastructure service registration

#### 📋 [Project].Contracts

- **Purpose**: Shared contracts and DTOs
- **Key Components**:
  - `Abstractions/`: Base interfaces for CQRS
  - `DTOs/`: Data transfer objects
  - `Messages/`: Application messages and constants
  - `Exceptions/`: Shared exception types
  - `Settings/`: Configuration settings

#### 🎨 [Project].Presentation

- **Purpose**: Presentation layer components
- **Key Components**:
  - Controllers and endpoints
  - View models and presentation logic
