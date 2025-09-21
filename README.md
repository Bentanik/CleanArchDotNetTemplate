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

## 🚀 Features

### ✅ Implemented Features

- **Clean Architecture**: Clear separation of concerns across layers
- **CQRS Pattern**: Command Query Responsibility Segregation with MediatR
- **Domain-Driven Design**: Rich domain models with aggregates and value objects
- **Entity Framework Core**: Database access with migrations
- **Swagger/OpenAPI**: API documentation
- **Exception Handling**: Global exception handling middleware
- **Dependency Injection**: Comprehensive DI setup
- **Domain Events**: Decoupled event handling
- **Transaction Management**: Automatic transaction handling
- **API Versioning**: Built-in API versioning support
- **Validation**: FluentValidation integration

### 🛠️ Technology Stack

- **.NET 8**: Latest LTS version
- **Entity Framework Core 8**: ORM for data access
- **MediatR**: CQRS implementation
- **FluentValidation**: Input validation
- **Swashbuckle**: Swagger/OpenAPI documentation
- **Carter**: Minimal API framework
- **AutoMapper**: Object mapping (if needed)

## 🏃‍♂️ Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or SQLite for development)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd CleanArchTemplateNET
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Update connection string**

   - Edit `src/[Project].API/appsettings.json`
   - Update the connection string to point to your database

4. **Run database migrations**

   ```bash
   dotnet ef database update --project src/[Project].Persistence --startup-project src/[Project].API
   ```

5. **Run the application**

   ```bash
   dotnet run --project src/[Project].API
   ```

6. **Access Swagger UI**
   - Navigate to `https://localhost:7000/swagger` (or your configured port)

## 📝 Usage Examples

### Creating a New Use Case

1. **Create Command/Query in Contracts**

   ```csharp
   public record CreateExampleCommand(string Text) : ICommand<Guid>;
   ```

2. **Create Handler in Application**

   ```csharp
   public class CreateExampleHandler : IRequestHandler<CreateExampleCommand, Result<Guid>>
   {
       // Implementation
   }
   ```

3. **Create Endpoint in API**
   ```csharp
   public class ExampleEndpoints : ICarterModule
   {
       public void AddRoutes(IEndpointRouteBuilder app)
       {
           app.MapPost("/examples", async (CreateExampleCommand command, IMediator mediator) =>
           {
               var result = await mediator.Send(command);
               return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
           });
       }
   }
   ```

### Adding a New Entity

1. **Create Entity in Domain**

   ```csharp
   public class MyEntity : BaseEntity<Guid>
   {
       // Properties and methods
   }
   ```

2. **Add to DbContext in Persistence**

   ```csharp
   public DbSet<MyEntity> MyEntities { get; set; }
   ```

3. **Create Configuration**
   ```csharp
   public class MyEntityConfiguration : IEntityTypeConfiguration<MyEntity>
   {
       public void Configure(EntityTypeBuilder<MyEntity> builder)
       {
           // Configuration
       }
   }
   ```

## 🧪 Testing

The template is structured to support various testing strategies:

- **Unit Tests**: Test individual components in isolation
- **Integration Tests**: Test layer interactions
- **End-to-End Tests**: Test complete user scenarios

## 📚 Best Practices

### Domain Layer

- Keep business logic in domain entities
- Use value objects for complex types
- Implement domain events for side effects
- Avoid dependencies on external layers

### Application Layer

- Keep use cases focused and single-purpose
- Use MediatR for CQRS implementation
- Implement behaviors for cross-cutting concerns
- Keep application services thin

### Infrastructure Layer

- Implement interfaces defined in Application layer
- Keep external dependencies isolated
- Use configuration for external service settings

## 🔧 Configuration

### Database Settings

Configure your database connection in `appsettings.json`:

```json
{
  "DatabaseSettings": {
    "ConnectionString": "Server=localhost;Database=MyDatabase;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

### Swagger Configuration

Swagger is automatically configured with API versioning support. Access the documentation at `/swagger` endpoint.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- Domain-Driven Design concepts by Eric Evans
- CQRS pattern implementation with MediatR
- .NET community for excellent tooling and libraries

---

**Note**: This is a template project. Replace `[Project]` placeholders with your actual project name and customize the domain entities and use cases according to your business requirements.
