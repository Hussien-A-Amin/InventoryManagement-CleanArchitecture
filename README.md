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


