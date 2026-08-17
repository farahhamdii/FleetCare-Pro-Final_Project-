#  FleetCare Pro

### Vehicle Fleet & Maintenance Management System

FleetCare Pro is a production-style **ASP.NET Core MVC** application designed to manage vehicle fleets, maintenance operations, service centers, service categories, service records, and user access.

The project was built with a strong focus on **Clean Architecture, separation of concerns, maintainability, security, scalability, and real-world backend development practices**.

---

## 📌 Table of Contents

* [Overview](#-overview)
* [Business Problem](#-business-problem)
* [Main Features](#-main-features)
* [User Roles](#-user-roles)
* [Architecture](#-architecture)
* [Project Structure](#-project-structure)
* [Domain Model](#-domain-model)
* [Application Layer](#-application-layer)
* [Infrastructure Layer](#-infrastructure-layer)
* [Web Layer](#-web-layer)
* [Authentication & Authorization](#-authentication--authorization)
* [Validation](#-validation)
* [Data Access](#-data-access)
* [Design Decisions](#-design-decisions)
* [Technologies](#-technologies)
* [Getting Started](#-getting-started)
* [Database Setup](#-database-setup)
* [Testing](#-testing)
* [Future Improvements](#-future-improvements)
* [Learning Outcomes](#-learning-outcomes)
* [Author](#-author)

---

# 📖 Overview

FleetCare Pro is a vehicle maintenance management system built to simulate a real-world fleet management application.

The system allows organizations to manage:

* Vehicles
* Drivers
* Fleet managers
* Service categories
* Service centers
* Vendor relationships
* Maintenance records
* Service line items
* Vehicle status
* Maintenance history
* Audit information
* User roles and permissions

The application follows a layered architecture based on **Clean Architecture principles**, keeping business logic independent from the UI and infrastructure concerns.

---

# 🎯 Business Problem

Managing a fleet manually can make it difficult to track:

* Vehicle maintenance history
* Service costs
* Service centers
* Vehicle mileage
* Maintenance status
* Assigned drivers
* Maintenance categories
* User responsibilities
* Administrative actions

FleetCare Pro provides a centralized system where fleet operations can be managed through a structured and secure application.

### Example Workflow

```text
Vehicle
   ↓
Maintenance Required
   ↓
Service Record Created
   ↓
Service Center Selected
   ↓
Service Categories / Line Items Added
   ↓
Maintenance Completed
   ↓
Vehicle Status Updated
   ↓
Maintenance History Stored
```

---

# ✨ Main Features

## 🚘 Vehicle Management

The system provides complete vehicle management functionality including:

* Create vehicle
* Edit vehicle
* View vehicle details
* Delete vehicle
* Track vehicle status
* Track mileage
* Store VIN
* Store license plate
* Store make and model
* Store production year
* Store purchase price
* Upload vehicle image
* Assign vehicles to drivers
* View vehicle maintenance history

---

## 🔧 Service Record Management

Maintenance operations can be tracked through service records.

Each service record can contain:

* Service date
* Current mileage
* Service center
* Vehicle
* Total cost
* Notes
* Status
* Invoice/document path
* Created by user
* Service line items

This allows the application to maintain a complete maintenance history for every vehicle.

---

## 🛠️ Service Categories

Service categories allow maintenance operations to be classified.

Examples:

```text
Oil Change
Brake Service
Tire Replacement
Engine Repair
Electrical Service
Periodic Maintenance
```

Each service record can contain multiple service line items based on the performed maintenance.

---

## 🏢 Service Centers

FleetCare Pro supports managing service centers used for vehicle maintenance.

The system can associate service records with service centers and maintain the relationship between vehicles, maintenance operations, and service providers.

---

## 🔗 Vendor Services

The system supports relationships between service centers/vendors and service categories.

This allows the application to represent which services are available through different vendors.

---

## 👤 User Management

The application uses **ASP.NET Core Identity** for user management.

Users can have different roles and permissions depending on their responsibilities.

Supported roles include:

* `Admin`
* `FleetManager`
* `Driver`

---

## 📊 Dashboard

The application provides a centralized dashboard designed to give administrators and fleet managers a quick overview of fleet operations.

The dashboard can be extended to include:

* Total vehicles
* Vehicles under maintenance
* Active vehicles
* Service records
* Maintenance costs
* Service center statistics
* Recent maintenance activities

---

# 👥 User Roles

## 🔴 Admin

The administrator has the highest level of access.

Typical responsibilities:

* Manage users
* Manage roles
* Manage fleet data
* Manage service categories
* Manage service centers
* Review audit information
* Access administrative functionality

---

## 🟡 Fleet Manager

The Fleet Manager is responsible for fleet operations.

Typical responsibilities:

* Manage vehicles
* Track vehicle status
* Create maintenance records
* Manage service operations
* View maintenance history
* Monitor fleet activities

---

## 🟢 Driver

Drivers have restricted access according to their assigned responsibilities.

Typical functionality can include:

* View assigned vehicles
* View vehicle information
* View maintenance history
* Access permitted vehicle-related operations

---

# 🏗️ Architecture

FleetCare Pro follows **Clean Architecture principles**.

The solution is divided into independent projects:

```text
FleetCarePro
│
├── FleetCarePro.Domain
│
├── FleetCarePro.Application
│
├── FleetCarePro.Infrastructure
│
├── FleetCarePro.Web
│
└── FleetCarePro.Tests
```

The main dependency direction is:

```text
                 ┌─────────────────┐
                 │   FleetCarePro  │
                 │      Web        │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │   Application   │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │     Domain      │
                 └─────────────────┘

                 ┌─────────────────┐
                 │ Infrastructure  │
                 └────────┬────────┘
                          │
                          ▼
                 ┌─────────────────┐
                 │   Application   │
                 └─────────────────┘
```

The goal is to keep the business rules independent from:

* UI
* Database
* EF Core
* External services
* Infrastructure implementation details

---

# 📂 Project Structure

## FleetCarePro.Domain

The Domain layer contains the core business entities and domain concepts.

```text
FleetCarePro.Domain
│
├── Entities
│   ├── Vehicle
│   ├── ServiceCategory
│   ├── ServiceCenter
│   ├── VendorService
│   ├── ServiceRecord
│   ├── ServiceLineItem
│   ├── AuditLog
│   └── ApplicationUser
│
└── Enums
    ├── VehicleStatus
    └── ServiceRecordStatus
```

### Responsibility

The Domain layer should not depend on:

* ASP.NET Core MVC
* EF Core
* SQL Server
* UI concerns
* Infrastructure implementations

This makes the business model easier to test and maintain.

---

# 🧠 Application Layer

The Application layer contains application-specific business logic and abstractions.

Main responsibilities include:

* Application services
* DTOs
* Interfaces
* Mapping
* Business workflows
* Communication between Web and Infrastructure

Example structure:

```text
FleetCarePro.Application
│
├── DTOs
│   ├── Vehicle
│   ├── ServiceRecord
│   ├── ServiceCategory
│   └── ServiceCenter
│
├── Interfaces
│   ├── Services
│   └── Infrastructure
│
├── Services
│   ├── VehicleService
│   └── ServiceRecordService
│
├── Mapping
│   └── MappingProfile
│
└── DependencyInjection
```

---

# 🏭 Infrastructure Layer

The Infrastructure layer contains implementation details related to persistence and external infrastructure.

Responsibilities include:

* Entity Framework Core
* DbContext
* Repositories
* Database configuration
* Entity relationships
* Data access implementation

Example:

```text
FleetCarePro.Infrastructure
│
├── Persistence
│   └── AppDbContext
│
├── Repositories
│   ├── VehicleRepository
│   ├── ServiceRecordRepository
│   ├── ServiceCenterRepository
│   └── ...
│
└── DependencyInjection
```

---

# 🌐 Web Layer

The Web project is the presentation layer.

It contains:

* MVC Controllers
* Razor Views
* ViewModels
* Middleware
* Authentication configuration
* Authorization policies
* UI components
* Application startup configuration

Example:

```text
FleetCarePro.Web
│
├── Controllers
│
├── ViewModels
│
├── Views
│
├── Middleware
│
├── wwwroot
│
├── Program.cs
└── appsettings.json
```

The controllers communicate with the Application layer instead of directly accessing the database.

---

# 🧪 Tests

The solution contains a dedicated testing project:

```text
FleetCarePro.Tests
```

The goal is to isolate and verify important application behavior such as:

* Service logic
* Business rules
* Validation
* Error scenarios
* Data-related behavior

The test layer is separated from the production projects to keep testing concerns isolated.

---

# 🗃️ Domain Model

The main entities include:

### Vehicle

Represents a fleet vehicle.

Important information includes:

* Vehicle ID
* VIN
* License Plate
* Make
* Model
* Year
* Purchase Price
* Mileage
* Status
* Image
* Assigned Driver

---

### ServiceRecord

Represents a maintenance operation performed on a vehicle.

Important information includes:

* Vehicle
* Service Center
* Service Date
* Current Mileage
* Total Cost
* Status
* Notes
* Invoice Document
* Created By
* Service Line Items

---

### ServiceLineItem

Represents an individual service performed as part of a service record.

Example:

```text
Service Record #1001

├── Oil Change
├── Brake Inspection
└── Tire Replacement
```

---

### ServiceCategory

Represents the type/category of maintenance service.

---

### ServiceCenter

Represents a maintenance/service provider.

---

### VendorService

Represents the relationship between vendors/service centers and available service categories.

---

### AuditLog

Stores information about important system actions and changes.

This provides the foundation for tracking:

* Who performed an action
* What action was performed
* When it happened
* What data was affected

---

### ApplicationUser

Extends ASP.NET Core Identity's user model with application-specific information such as:

* Full Name
* Employee ID
* Assigned Vehicles
* Created Service Records
* Audit Logs

---

# 🔐 Authentication & Authorization

FleetCare Pro uses **ASP.NET Core Identity** for authentication and user management.

The application supports role-based and policy-based authorization.

Configured roles include:

```text
Admin
FleetManager
Driver
```

Authorization policies include role-specific policies such as:

```text
AdminOnly
FleetManagerOnly
DriverOnly
```

The application also configures authentication behavior for:

* Login
* Access denied
* User authentication
* Role-based access

### Why ASP.NET Core Identity?

Identity provides a secure and extensible foundation for:

* Password hashing
* User management
* Roles
* Claims
* Authentication cookies
* Account security features

---

# 🛡️ Validation

The application includes custom validation rules for domain-specific data.

Examples include:

### VIN Validation

A custom validation attribute is used to validate vehicle VIN values.

```text
ValidVINAttribute
```

### Vehicle Image Validation

Vehicle image uploads are validated using:

```text
VehicleImageValidationAttribute
```

These validations help prevent invalid data from entering the application.

---

# 🗄️ Data Access

The project uses:

* SQL Server
* Entity Framework Core
* Repository pattern

The data access flow is:

```text
Controller
     ↓
Application Service
     ↓
Repository Interface
     ↓
Repository Implementation
     ↓
EF Core DbContext
     ↓
SQL Server
```

This separation keeps database-specific logic outside the presentation layer.

---

# 🔄 Application Flow

A typical request follows this structure:

```text
HTTP Request
     ↓
MVC Controller
     ↓
ViewModel Validation
     ↓
Application Service
     ↓
Repository
     ↓
EF Core
     ↓
SQL Server
     ↓
Entity
     ↓
DTO
     ↓
ViewModel
     ↓
Razor View
     ↓
HTTP Response
```

---

# 🧩 DTOs vs ViewModels vs Entities

The project intentionally separates these models.

### Entity

Used for the domain and persistence model.

```text
Vehicle
ServiceRecord
ServiceCenter
```

### DTO

Used to transfer application data between layers.

```text
VehicleDTO
ServiceRecordDTO
```

### ViewModel

Used specifically by the MVC UI.

```text
VehicleViewModel
ServiceRecordViewModel
```

This prevents the presentation layer from being tightly coupled to the database/domain model.

---

# 🗺️ Mapping

The project uses **AutoMapper** to reduce repetitive mapping code between:

```text
Entity
   ↓
DTO
   ↓
ViewModel
```

Mapping configuration is centralized inside the Application layer.

---

# 🧱 Repository Pattern

Repositories provide an abstraction over data access.

For example:

```text
IVehicleRepository
        ↓
VehicleRepository
```

The Application layer depends on abstractions rather than directly depending on EF Core implementations.

This improves:

* Separation of concerns
* Testability
* Maintainability
* Flexibility

---

# 💡 Important Architecture Decisions

## Why Clean Architecture?

Clean Architecture was selected to separate:

```text
Business Logic
    from
Infrastructure
    from
Presentation
```

This makes the application easier to:

* Maintain
* Test
* Extend
* Refactor
* Convert to another presentation technology

---

## Why Repository Pattern?

Repositories isolate EF Core and database access from application services.

This keeps services focused on application/business behavior rather than SQL/data-access implementation.

---

## Why no Unit of Work?

A dedicated Unit of Work abstraction was intentionally avoided because it was not necessary for every operation in the current application.

Instead, transaction boundaries can be introduced at the appropriate application/service level when a business operation requires multiple changes to be committed atomically.

This avoids introducing abstractions only for the sake of following a pattern.

---

## Why MVC Authentication Uses Cookies?

FleetCare Pro is currently an ASP.NET Core MVC web application.

Cookie-based authentication is a natural fit for server-rendered MVC applications.

JWT is more appropriate when exposing the backend as an API consumed by:

* React
* Angular
* Flutter
* Mobile applications
* External clients

The current architecture is designed so that an API presentation layer can be added without rewriting the Domain and Application layers.

---

# ⚡ Performance Considerations

The architecture is designed with future optimization in mind.

Potential optimization strategies include:

* `AsNoTracking()` for read-only EF Core queries
* Pagination for large collections
* Filtering at database level
* Efficient projections
* Avoiding unnecessary `Include()` calls
* Proper database indexes
* Query optimization
* Caching where appropriate

---

# 🔒 Security Considerations

The application is designed around several security principles:

* ASP.NET Core Identity
* Role-based authorization
* Policy-based authorization
* Server-side validation
* Model validation
* Restricted access to administrative functionality
* Controlled file uploads
* Separation between domain and presentation models
* No direct database access from controllers

For production deployment, additional security hardening can include:

* Secure secrets management
* HTTPS enforcement
* Rate limiting
* Security headers
* Account lockout configuration
* Email confirmation
* Password reset
* Logging and monitoring
* Production-safe error handling

---

# 🛠️ Technologies

## Backend

* C#
* ASP.NET Core MVC
* .NET
* Entity Framework Core
* ASP.NET Core Identity

## Database

* Microsoft SQL Server

## Architecture & Patterns

* Clean Architecture
* Layered Architecture
* Repository Pattern
* Dependency Injection
* Service Layer
* DTO Pattern
* ViewModel Pattern
* Separation of Concerns

## Libraries / Tools

* AutoMapper
* Bootstrap
* Razor Views
* xUnit / testing infrastructure

## Development

* Visual Studio
* Git
* GitHub
* SQL Server / SQL Server Management Studio

---

# 📋 Requirements

Before running the project, make sure you have:

* .NET SDK installed
* SQL Server
* SQL Server Management Studio or another SQL client
* Visual Studio or VS Code
* Git

---

# 🚀 Getting Started

## 1. Clone the Repository

```bash
git clone https://github.com/farahhamdii/FleetCare-Pro-Final_Project-.git
```

Navigate into the project:

```bash
cd FleetCare-Pro-Final_Project-
```

---

## 2. Configure the Database Connection

Open:

```text
FleetCarePro.Web/appsettings.json
```

Configure the SQL Server connection string.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=FleetCareProDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Do not commit production credentials or secrets to GitHub.

---

## 3. Apply EF Core Migrations

From the solution directory:

```bash
dotnet ef database update
```

If the startup project needs to be specified:

```bash
dotnet ef database update \
  --project FleetCarePro.Infrastructure \
  --startup-project FleetCarePro.Web
```

---

## 4. Build the Solution

```bash
dotnet build
```

---

## 5. Run the Application

```bash
dotnet run --project FleetCarePro.Web
```

Or run the project through Visual Studio.

---

# 🧪 Running Tests

Run all tests using:

```bash
dotnet test
```

To run tests with detailed output:

```bash
dotnet test --verbosity normal
```

---

# 🔄 Future API Version

The current project is implemented as an ASP.NET Core MVC application.

However, the architecture allows a Web API layer to be introduced without rewriting the core business logic.

A future structure could be:

```text
FleetCarePro
│
├── FleetCarePro.Domain
├── FleetCarePro.Application
├── FleetCarePro.Infrastructure
│
├── FleetCarePro.Web
│      └── ASP.NET Core MVC
│
├── FleetCarePro.API
│      └── ASP.NET Core Web API
│
└── FleetCarePro.Tests
```

Both presentation layers can use the same:

```text
Domain
   +
Application
   +
Infrastructure
```

For an API version, the technology stack could include:

* ASP.NET Core Web API
* JWT Bearer Authentication
* Role & Policy Authorization
* RESTful APIs
* Swagger / OpenAPI
* FluentValidation
* AutoMapper
* EF Core
* SQL Server
* Serilog
* xUnit
* Docker
* GitHub Actions

---

# 📈 Production Roadmap

The project can be further hardened toward production with:

### Security

* [ ] Complete authentication flow
* [ ] Refresh tokens for API
* [ ] Claims-based authorization
* [ ] Password policy hardening
* [ ] Account lockout
* [ ] Email confirmation
* [ ] Secure file upload handling
* [ ] Rate limiting

### Reliability

* [ ] Global exception handling
* [ ] Structured logging
* [ ] Health checks
* [ ] Transaction management
* [ ] Consistent error responses

### Performance

* [ ] Pagination
* [ ] Server-side filtering
* [ ] Search optimization
* [ ] `AsNoTracking()` where appropriate
* [ ] Query projections
* [ ] Database indexes
* [ ] Caching

### Observability

* [ ] Serilog
* [ ] Application monitoring
* [ ] Request logging
* [ ] Audit logging improvements
* [ ] Error tracking

### DevOps

* [ ] Docker
* [ ] CI/CD pipeline
* [ ] GitHub Actions
* [ ] Environment-specific configuration
* [ ] Production secrets management
* [ ] Cloud deployment

---

# 📚 Learning Outcomes

This project was designed as a practical ASP.NET Core backend project covering both fundamental and advanced concepts.

Through FleetCare Pro, the following concepts were practiced:

### C# / OOP

* Classes
* Interfaces
* Encapsulation
* Inheritance
* Abstraction
* Enums
* SOLID principles

### ASP.NET Core

* MVC
* Dependency Injection
* Middleware
* Configuration
* Model Binding
* Model Validation
* Authorization
* Authentication

### Entity Framework Core

* Code First
* Migrations
* Relationships
* Navigation Properties
* LINQ
* Includes
* Query optimization
* Repository abstraction

### Architecture

* Clean Architecture
* Separation of Concerns
* Dependency Inversion
* Service Layer
* Repository Pattern
* DTOs
* ViewModels

### Security

* ASP.NET Core Identity
* Roles
* Policies
* Access Control
* Validation

### Software Engineering

* Git
* GitHub
* Testing
* Maintainable project structure
* Production-oriented design

---

# 🧭 Architecture Philosophy

The main goal of FleetCare Pro is not simply to implement CRUD operations.

The project focuses on building software that can evolve.

The architecture intentionally separates:

```text
                    Presentation
                         │
                         ▼
                  Application Logic
                         │
                         ▼
                       Domain
                         ▲
                         │
                  Infrastructure
```

This allows the application to evolve from:

```text
ASP.NET Core MVC
```

into:

```text
ASP.NET Core MVC
        +
ASP.NET Core Web API
        +
React / Flutter / Mobile Clients
```

without duplicating the core business logic.

---

# 📊 Project Status

### Current Status

🟢 Core architecture implemented

🟢 Domain entities implemented

🟢 Application services implemented

🟢 Repository layer implemented

🟢 Entity Framework Core integration

🟢 ASP.NET Core Identity integration

🟢 Role-based authorization foundation

🟢 MVC controllers and views

🟢 DTOs and ViewModels

🟢 AutoMapper integration

🟢 Custom validation

🟢 Service record management

🟢 Vehicle management

🟢 Service center management

🟢 Service category management

🟡 Advanced production hardening in progress

---

# 👩‍💻 Author

**Farah Hamdy**

Computer Science / Artificial Intelligence Student
ASP.NET Core Backend Developer

### Technical Focus

* C#
* ASP.NET Core
* ASP.NET Core Web API
* MVC
* Entity Framework Core
* SQL Server
* Clean Architecture
* REST APIs
* Authentication & Authorization
* Backend Development

---

# ⭐ Project Goal

FleetCare Pro is more than a CRUD application.

It was built as a practical exercise in designing a maintainable backend system using real-world architectural principles.

The long-term goal is to evolve the system toward a production-ready backend with:

```text
Clean Architecture
        +
Secure Authentication
        +
Robust Authorization
        +
Testing
        +
Performance Optimization
        +
Logging & Monitoring
        +
CI/CD
        +
Web API
        +
Modern Frontend / Mobile Client
```

---

## 📄 License

This project is intended for educational and portfolio purposes.
