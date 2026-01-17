# AI-Powered-Task-Management-System

This project is a Task Management System built with .NET, designed to demonstrate how to structure a modular monolith using Clean Architecture and Domain-Driven Design (DDD) principles.
The goal of this system is not just to manage tasks, but to showcase real-world architectural patterns such as bounded contexts, domain policies, outbox pattern, event-driven communication, and fine-grained authorization.

# Architecture Overview:

The solution follows a Modular Monolith approach:
- Each module is independently structured
- No direct dependencies between modules
- Communication happens via integration events
- Modules can later be extracted into microservices if needed

Modules:
- Identity Module
    - User management
    - Roles & permissions
    - Integration with Keycloak
    - Central authorization source

- Tasking Module
    - Task lifecycle management
    - Domain rules and invariants
    - Business authorization policies
    - Emits domain and integration events

- Notification Module
    - Email notifications
    - Subscribes to integration events via RabbitMQ

- Shared Module
    - Cross-cutting concerns (Results, CQRS, Security, Messaging, Seeding, DDD Abstractions)
 
# Clean Architecture Layers (per module)
Each module strictly follows Clean Architecture:

Domain
 ├─ Aggregates
 ├─ Entities
 ├─ Value Objects
 ├─ Domain Events
 └─ Business Rules

Application
 ├─ Commands & Queries (CQRS)
 ├─ MediatR handlers
 ├─ Authorization policies
 ├─ DTOs
 └─ Interfaces

Infrastructure
 ├─ EF Core
 ├─ Repositories
 ├─ External integrations
 ├─ RabbitMQ & CAP
 └─ Database migrations & seeding

API
 ├─ Controllers
 └─ HTTP mapping only
 
No layer depends inward on infrastructure or frameworks.


# CQRS & MediatR

- Commands and queries are clearly separated
- All application use cases are handled via MediatR
- No business logic in controllers
- Pipeline is clean, testable, and extensible


# Authorization Model (Real-World, Not Attributes)

Authorization is business-driven, not attribute-driven.
  - Permissions are explicit business concepts
  - Policies live in the Application layer
  - No [Authorize(Policy = "...")]
  - No leaking HTTP concerns into domain logic

Example:
  - CreateTaskPolicy
  - AssignTaskPolicy
  - UpdateTaskPolicy

Policies receive:
  - UserContext (permissions)
  - Domain entities (Task, Status, etc.)

They return business results, which are translated to HTTP responses at the API boundary.


# Identity & Permissions

- Authentication handled via Keycloak
- Authorization handled internally
- Permissions are:
    - Code-defined
    - Module-owned
    - Seeded automatically
- Roles aggregate permissions
- Users inherit permissions through roles

This allows strong consistency while keeping modules decoupled.


# Event-Driven Communication

- Domain Events for internal consistency
- Integration Events for cross-module communication
- Implemented using:
    - CAP
    - RabbitMQ
    - Outbox Pattern

This guarantees:
- No tight coupling
- No distributed transactions
- Safe eventual consistency


# Seeding & Migrations

- Each module owns its own migrations
- Each module can provide data seeders
- Centralized startup executes:
    - Migrations
    - Seeders
- Idempotent and safe to re-run


# Dockerized Environment

The entire system is containerized using Docker:
  - PostgreSQL
  - RabbitMQ
  - Keycloak
  - papercut For local email

This ensures:
  - Consistent local development
  - Easy onboarding
  - Production-like environment


# Tech Stack

- .NET
- Entity Framework Core
- PostgreSQL
- Docker
- CAP (Outbox + Messaging)
- Mediator
- RabbitMQ
- Keycloak
- CQRS
- Domain-Driven Design
- Clean Architecture


# Why This Project Matters

This is not a CRUD demo.

This project demonstrates:
  - How to apply DDD without over-engineering
  - How to build modular systems without microservices pain
  - How to design real authorization models
  - How to use messaging safely and reliably
  - How to keep business logic framework-agnostic

It reflects real backend engineering decisions used in scalable systems
