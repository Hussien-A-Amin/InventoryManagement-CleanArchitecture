# Inventory Management | Clean Architecture v2

**A .NET Backend Engineering Showcase focused on Clean Architecture, secure APIs, software quality, and production-oriented observability.**

[![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-2563EB)](#architecture)
[![API](https://img.shields.io/badge/API-REST-009688)](#api-overview)
[![Status](https://img.shields.io/badge/Status-In%20Development-orange)](#project-status)

## Overview

Inventory Management is a portfolio project designed to demonstrate practical backend engineering with **ASP.NET Core Web API**.

Rather than focusing on a large number of business features, this project emphasizes the engineering practices behind building maintainable, secure, testable, and observable backend applications.

The business domain is intentionally simple: customers, products, and orders. This keeps the focus on architecture, implementation quality, reliability, and the integration of backend technologies.

This repository is an independent portfolio demonstration derived from experience building a commercial inventory management product. It is not intended to provide the complete commercial application.

## Engineering Objectives

- Apply Clean Architecture and separation of concerns.
- Build RESTful APIs with clear contracts and consistent responses.
- Implement authentication and authorization using ASP.NET Core Identity and JWT.
- Handle application errors consistently and predictably.
- Design maintainable data access with Entity Framework Core.
- Validate behavior through automated testing.
- Implement structured logging and distributed observability.
- Explore caching, automation, and deployment practices where they provide measurable value.

## Technology Stack

The following table describes the intended technology stack. Implementation status should be updated as each capability is completed.

| Technology | Purpose |
|---|---|
| C# / .NET | Application development |
| ASP.NET Core Web API | HTTP endpoints and API infrastructure |
| Entity Framework Core | Data access and ORM |
| SQL Server | Relational persistence |
| ASP.NET Core Identity | User and role management |
| JWT Bearer Authentication | Token-based API authentication |
| Redis | Caching where appropriate |
| Serilog | Structured application logging |
| OpenTelemetry | Application telemetry and instrumentation |
| Grafana | Telemetry visualization and monitoring |
| xUnit | Automated testing |
| Git / GitHub | Version control and collaboration |
| GitHub Actions | Continuous integration and automation |
| Angular | Planned web client, developed separately |
| WPF / .NET MAUI | Existing or future client applications, maintained separately |

Technologies are introduced incrementally. A technology listed here is part of the intended stack, not necessarily an already completed feature.

## Architecture

The application is intended to follow Clean Architecture principles, keeping business rules independent of infrastructure concerns.

```text
InventoryManagement.sln
│
├── src/
│   ├── InventoryManagement.Domain/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Common/
│   │
│   ├── InventoryManagement.Application/
│   │   ├── Features/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Validators/
│   │   └── Common/
│   │
│   ├── InventoryManagement.Infrastructure/
│   │   ├── Persistence/
│   │   ├── Identity/
│   │   ├── Repositories/
│   │   └── Services/
│   │
│   └── InventoryManagement.API/
│       ├── Controllers/
│       ├── Middleware/
│       ├── Extensions/
│       └── Configuration/
│
└── tests/
    ├── InventoryManagement.UnitTests/
    └── InventoryManagement.IntegrationTests/
```

The exact project structure may evolve as implementation decisions are finalized.

### Layer Responsibilities

**Domain**

Contains core business entities, domain rules, and concepts that should not depend on frameworks or infrastructure.

**Application**

Coordinates application use cases, defines service contracts, and contains application-level validation and DTOs.

**Infrastructure**

Implements persistence, identity-related infrastructure, repositories where justified, and external integrations.

**API**

Provides HTTP endpoints, request handling, authentication configuration, middleware, and dependency injection composition.

### Dependency Direction

The dependency rule is the central architectural principle:

- The Domain layer does not depend on outer layers.
- The Application layer depends on the Domain layer.
- Infrastructure implements interfaces defined by inner layers where appropriate.
- The API composes the application and infrastructure components.

The objective is to keep business rules independent of the database, HTTP framework, and external services.

## Core Domain

The initial domain is intentionally limited to three main concepts.

### Customer

Represents a customer who can be associated with orders.

Potential responsibilities:

- Customer information and validation.
- Customer-related use cases.
- Access control over customer-specific resources where applicable.

### Product

Represents an item that can be offered for purchase.

Potential responsibilities:

- Product information and validation.
- Product listing and retrieval.
- Consistent handling of missing or invalid product data.

### Order

Represents an order associated with a customer and one or more products.

Potential responsibilities:

- Order creation and retrieval.
- Validation of referenced customers and products.
- Explicit handling of invalid order requests.

The portfolio version intentionally excludes the complete commercial inventory and warehouse workflows.

## Authentication and Authorization

Authentication and authorization are treated as separate security concerns.

### Authentication

The intended implementation uses ASP.NET Core Identity for identity management and JWT Bearer authentication for protecting API endpoints.

Areas covered by the implementation may include:

- User registration and login.
- Password validation and secure password storage through Identity.
- Token generation and validation.
- Authentication middleware and protected endpoints.
- Appropriate handling of invalid, missing, or expired tokens.

### Authorization

Authorization determines what an authenticated user is permitted to do.

Depending on the implemented requirements, this may include:

- Role-based authorization.
- Policy-based authorization.
- Resource-level access checks.
- Protection against unauthorized access to customer and order data.

Security decisions are enforced on the server, not entrusted to the client application.

## API Design

The API follows REST-oriented design principles, with explicit request and response contracts.

Illustrative endpoints:

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/products` | Retrieve products |
| GET | `/api/products/{id}` | Retrieve a product |
| POST | `/api/products` | Create a product |
| GET | `/api/customers/{id}` | Retrieve a customer |
| POST | `/api/orders` | Create an order |
| GET | `/api/orders/{id}` | Retrieve an order |

These routes are examples of the intended API surface. They should be adjusted to match the actual controllers and authorization rules.

### API Design Principles

- Use appropriate HTTP methods and status codes.
- Validate incoming requests.
- Separate API contracts from persistence entities.
- Avoid exposing internal implementation details.
- Return consistent error responses.
- Document endpoints with OpenAPI/Swagger where configured.
- Apply authorization rules to protected operations.

## Error Handling and Validation

Errors should be handled consistently rather than through repetitive controller-level exception handling.

The intended approach includes:

- Centralized exception handling middleware or the framework's exception-handling facilities.
- Request validation with clear validation errors.
- Appropriate HTTP status codes.
- Consistent error response structures.
- Safe client-facing messages that do not expose stack traces or secrets.
- Structured server-side logs for troubleshooting.
- Explicit handling of expected business failures.

The objective is to make failures predictable for API consumers and diagnosable for developers.

## Data Access and Persistence

Entity Framework Core provides the data access layer, with SQL Server as the primary relational database.

Engineering considerations include:

- Clear entity configurations.
- Database migrations and schema evolution.
- Appropriate relationships and constraints.
- Asynchronous database operations.
- Query efficiency and pagination where needed.
- Avoiding unnecessary database round trips.
- Appropriate transaction boundaries.
- Keeping persistence concerns outside the Domain layer.

Repository and Unit of Work abstractions should be used where they provide a meaningful architectural benefit, rather than adding abstractions automatically around every EF Core operation.

## Testing Strategy

Automated tests are intended to verify both individual components and important application workflows.

### Unit Tests

Focus on isolated behavior, including:

- Domain rules.
- Application use cases.
- Validation behavior.
- Expected failures and edge cases.

### Integration Tests

Focus on interactions between components, including:

- API request and response behavior.
- Authentication and authorization.
- Persistence behavior.
- Validation and error responses.
- Important order-related workflows.

Tests should verify observable behavior rather than implementation details.

## Logging and Observability

Logging and observability help explain what an application is doing and diagnose failures.

### Structured Logging

Serilog is intended to provide structured logs with useful contextual properties, such as request identifiers, operation names, and exception details.

Sensitive information, credentials, tokens, and unnecessary personal data must not be logged.

### OpenTelemetry

OpenTelemetry provides vendor-neutral instrumentation and collection of telemetry signals.

Depending on the implementation, this can include:

- Traces for incoming HTTP requests and downstream operations.
- Metrics for request volume, latency, and failures.
- Instrumentation for supported database and HTTP operations.
- Export of telemetry to a compatible backend.

### Grafana

Grafana can be used to visualize collected telemetry through dashboards.

**OpenTelemetry and Grafana have different responsibilities:** OpenTelemetry provides instrumentation and telemetry pipelines, while Grafana visualizes and explores data from configured data sources. Grafana alone does not automatically collect every telemetry signal.

The observability setup should be documented with its actual exporters, receivers, data sources, and dashboards.

## Caching

Redis may be introduced for selected read-heavy operations when caching is justified.

The implementation should document:

- Which data is cached and why.
- Cache key design and expiration policies.
- Cache invalidation behavior.
- How stale data is handled.
- What happens when Redis is unavailable.

Caching is not applied indiscriminately. Database correctness and application behavior should remain well-defined even when cached data is unavailable.

## Continuous Integration and Delivery

GitHub Actions is intended to automate repeatable quality checks.

A CI workflow may include:

1. Restore dependencies.
2. Build the solution.
3. Run automated tests.
4. Publish test results.
5. Build deployment artifacts when appropriate.

Deployment automation can be added separately once the hosting environment and deployment strategy are selected.

The objective is to make changes easier to verify and reduce reliance on manual checks.

## Client Applications

The backend is designed to expose an API that can be consumed independently by different clients.

Potential clients include:

- **Angular:** the primary web client planned for the portfolio.
- **WPF:** an existing desktop client that may be adapted to consume the API.
- **.NET MAUI:** a potential mobile client.

These clients are separate presentation applications. They should communicate with the backend through documented API contracts rather than accessing its database directly.

Client implementations are developed incrementally and may live in separate repositories.

## Project Structure and Development Approach

The project evolves in stages to keep the engineering work understandable and verifiable.

| Stage | Focus |
|---|---|
| 1 | Establish the API and Clean Architecture boundaries |
| 2 | Implement core customer, product, and order workflows |
| 3 | Configure Identity, JWT, and authorization |
| 4 | Standardize validation and error handling |
| 5 | Add unit and integration tests |
| 6 | Add structured logging and OpenTelemetry |
| 7 | Configure Grafana dashboards and telemetry collection |
| 8 | Introduce Redis where justified |
| 9 | Add GitHub Actions workflows |
| 10 | Deploy a safe demonstration environment |
| 11 | Develop the Angular client separately |

The sequence is a development plan, not a claim that every stage has already been completed.

## Running the Project

Prerequisites will depend on the implemented configuration. The expected development environment includes:

- A compatible .NET SDK.
- SQL Server or a supported SQL Server development instance.
- Git.
- An IDE such as Visual Studio or Visual Studio Code.

### 1. Clone the Repository

```bash
git clone <YOUR_PUBLIC_REPOSITORY_URL>
cd <YOUR_REPOSITORY_DIRECTORY>
```

### 2. Configure the Environment

Configure the database connection string and any required authentication or infrastructure settings using local development configuration or environment variables.

Do not commit real credentials, production connection strings, JWT signing secrets, or API keys.

### 3. Apply Database Migrations

If EF Core migrations are configured:

```bash
dotnet ef database update
```

Run the command with the appropriate project and startup-project options if the migrations and API are in separate projects.

### 4. Build the Solution

```bash
dotnet restore
dotnet build
```

### 5. Run the API

```bash
dotnet run --project src/InventoryManagement.API
```

Update the path to match the actual repository structure.

### 6. Explore the API

Open the configured Swagger/OpenAPI endpoint in the development environment, if Swagger is enabled.

Exact URLs, configuration requirements, and database initialization instructions should be added once the project setup is finalized.

## Configuration and Security

Use environment-specific configuration for local development, testing, and deployment.

Important considerations:

- Keep secrets out of source control.
- Use HTTPS for deployed environments.
- Configure allowed origins explicitly when browser clients are used.
- Apply authorization to protected operations.
- Use safe error responses.
- Avoid logging sensitive values.
- Use synthetic data in the public demonstration.
- Restrict or disable destructive demo operations when appropriate.

The public deployment is intended for evaluation and learning, not as a production service for real customers.

## Technical Decisions

This project aims to document the reasoning behind important engineering choices.

Examples of decisions worth documenting as they are implemented:

- Why Clean Architecture is appropriate for this project's scope.
- Where interfaces provide useful boundaries.
- How authentication differs from authorization.
- Why centralized error handling improves consistency.
- Which automated tests provide the most confidence.
- Which telemetry signals are useful for diagnosing API failures.
- When Redis improves performance and when it adds unnecessary complexity.
- How CI checks protect code quality.

Each decision should describe the problem, alternatives considered, chosen approach, and trade-offs. This is more informative than simply listing technologies.

## Project Status

This repository is developed incrementally as a backend engineering portfolio.

- [ ] Finalize the architecture and project boundaries.
- [ ] Implement the initial customer, product, and order API.
- [ ] Complete authentication and authorization.
- [ ] Standardize error handling and validation.
- [ ] Add automated tests.
- [ ] Configure logging and observability.
- [ ] Add caching where appropriate.
- [ ] Configure CI automation.
- [ ] Publish a safe demo and API documentation.
- [ ] Document implementation decisions and known limitations.

Update this checklist as each item is completed.

## Portfolio Scope

This repository is intended to demonstrate backend engineering skills to recruiters, engineering managers, and technical interviewers.

It is not the full commercial inventory management product and does not aim to reproduce all of its business workflows.

The focus is on:

- Architecture and maintainability.
- Secure API development.
- Data access and correctness.
- Error handling and testing.
- Observability and diagnostics.
- Automation and deployment practices.
- Clear technical documentation.

## About the Developer

**Hussein Ahmed Amin**  
.NET Backend Developer

Focused on C#, ASP.NET Core Web API, SQL Server, backend architecture, API security, and reliable application development.

- LinkedIn: [Hussein Ahmed Amin](https://www.linkedin.com/in/hussien-ahmed-amin-a7b23731/)
- GitHub: [Hussien-A-Amin](https://github.com/Hussien-A-Amin)
- Portfolio: [kafegy.com](https://kafegy.com/)

## License

The repository's license should be selected explicitly before publication.

If this project is intended only for portfolio evaluation, choose a license that matches that intention and your rights to the underlying code. Do not assume that a public GitHub repository automatically grants permission for commercial reuse.

---

**Built as a practical exploration of .NET backend engineering, with an emphasis on engineering decisions, code quality, and continuous improvement.**
