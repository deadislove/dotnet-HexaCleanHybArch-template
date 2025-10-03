# HexaCleanHybArch.Template

![Visitors](https://img.shields.io/badge/visitors-17_total-brightgreen)
![Clones](https://img.shields.io/badge/clones-16_total_12_unique-blue) <!--CLONE-BADGE-->

A **Modular Hexagonal-Clean Hybrid Architecture** template built with **.NET 9**, designed for scalable, maintainable, and plug-in-style backend systems.

<a href='https://ko-fi.com/F1F82YR41' target='_blank'><img height='36' style='border:0px;height:36px;' src='https://storage.ko-fi.com/cdn/kofi6.png?v=6' border='0' alt='Buy Me a Coffee at ko-fi.com' /></a>

---

## ❓ Why This Architecture

Modern applications require flexibility, feature modularity, and clear separation of concerns. This template combines:

- **Hexagonal Architecture** for inbound/outbound decoupling
- **Clean Architecture** for layer separation and testability
- **Plugin-based modularity** for scalable and independently evolvable features

> Ideal for: Domain-rich enterprise apps, modular monoliths, service-oriented backends.

---

## 🧩 Architecture Overview

This architecture blends concepts from **Hexagonal Architecture**, **Clean Architecture**, and **Plugin-based Modular Monoliths**, with the following principles:

### 🔑 Key Concepts

- **Feature-Oriented Modules**  
  Each domain feature (e.g., `User`, `Auth`) is built as an independent module containing its own:
  - Domain Layer
  - Application Layer
  - Infrastructure Layer

- **Adapters as Boundaries**  
  Instead of treating `interface` as the sole boundary, this pattern treats **entire adapter modules** as the boundary units, following Hexagonal principles. These modules expose `Register()` methods to inject themselves at runtime.

- **Dynamic Adapter Registration**  
  A lightweight plugin mechanism loads all adapters implementing `IAdapterModule` from their assemblies at runtime, allowing high modularity and minimal API-core coupling.

- **Decentralized Port Interfaces**  
  Unlike traditional Hexagonal where all `ports` are defined in a central `Core`, this pattern allows each adapter to define its own ports/interfaces to avoid high coupling and make module ownership clearer.

## 🧱 Layered Structure

Each module follows the Clean Architecture layering **within itself**:

```bash
[ Api ]
    ↓
[ Adapter Module ]
  ├── Application
  ├── Domain
  └── Infra
    ↓
[ External Dependency ]
```

> Core remains free of direct adapter dependencies.

## 🔌 Modules

Examples of pluggable modules:
- `HexaCleanHybArch.Template.Adapters.User` – Handles user profile logic
- `HexaCleanHybArch.Template.Adapters.Auth` – Handles authentication & token flow

Each implements `IAdapterModule`, and is auto-registered via `AdapterModuleLoader`.

---

## ⚙️ Customize the Template
To start your own project based on this template:

```bash
dotnet new install .
dotnet new hexa-clean -n {new_project_name}
```

---

## 🚀 How to Run

Using the DockerFile:

```bash
## Build image
docker build -t {image_name} .

## Run Docker file(http: 8080 -> 8081; https: 8081 ->8082)
docker run -d -p 8081:8080 -p 8082:8081 --name {container_name} {image_name}
```

Or using Docker Compose:

```bash
# For SQLite (default)
docker-compose -f docker-compose.sqlite.yml up --build

# For PostgreSQL
docker-compose -f docker-compose.postgres.yml up --build

# For MSSQL
docker-compose -f docker-compose.mssql.yml up --build
```

## 🧪 Testing Strategy

This project separates test types clearly:

- tests/Unit/

  Unit tests focused on Core logic and internal rules
e.g., UserServiceTests.cs, AuthServiceTest.cs

- tests/Integration/

  API-level tests validating end-to-end flow
e.g., AuthApiIntegrationTests.cs, SharedApiFactoryCollection.cs

Test Tools Used:

- xUnit
- Moq
- FluentAssertions
- Microsoft.AspNetCore.Mvc.Testing

See: [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0&pivots=xunit)

## 📦 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/)
- [Docker](https://www.docker.com/) (required for database containers)

## 🗂️ Project Structure (Simplified)

```bash
src/
├── Api                        // Entry API + Middleware
├── Core                       // Core business logic
├── Adapters/
│   ├── Auth                   // Auth module
│   └── User                   // User module
├── Config                     // Database / DI factory
├── Shared                     // Common exceptions, DTOs
tests/
├── Unit/                      // Pure unit tests
└── Integration/               // API-level tests
```

## 📈 Architecture Diagram

The following diagram illustrates the modular, adapter-boundary-centric architecture you're implementing:

📎 [Download Architecture Diagram (PNG)](sandbox:/mnt/data/A_diagram_illustrates_a_modular_software_architect.png)

### Diagram Highlights:
- Each Adapter module contains its own Clean Architecture layers (Application / Domain / Infra)
- The API project composes multiple adapters via their `IAdapterModule` implementations
- The adapter boundary acts as the external interface to the Core
- The Core knows nothing about the Adapters — only ports/interfaces as abstractions

This encourages strong modularity, dynamic plug-in style composition, and separation of concerns per feature.

## 💬 Stay in touch

- Author - [Da-Wei Lin](https://www.linkedin.com/in/da-wei-lin-689a35107/)
- Website - [David Weblog](https://davidskyspace.com/)
- [MIT LICENSE](https://github.com/deadislove/dotnet-HexaCleanHybArch-template/blob/main/LICENSE)

## 🏗️ Future Enhancements

- ✅ MediatR integration
- ✅ FluentValidation auto-loading
- ⏳ Swagger modular plugin support
- ⏳ gRPC and EventBus adapters

## Reference

- [Integration tests in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0&pivots=xunit)
