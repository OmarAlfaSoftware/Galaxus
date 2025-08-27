# Galaxus Integration Solution Overview

## Solution Structure

This solution is designed for integrating with the Galaxus marketplace using .NET 9. It follows a modular, layered architecture to separate concerns and facilitate maintainability. The main projects are:

- **GalaxusIntegration.Api**: Exposes HTTP endpoints, handles authentication, and provides Swagger documentation for internal use.
- **GalaxusIntegration.Application**: Contains application logic, use cases, DTOs, and service abstractions.
- **GalaxusIntegration.Core**: Holds domain entities, business rules, and core validation logic.
- **GalaxusIntegration.Infrastructure**: Implements data access, SFTP/FTP, file processing, and external API integrations.
- **GalaxusIntegration.Tests**: Contains unit and integration tests for all layers.

## Project-by-Project Package Explanation

### GalaxusIntegration.Api
- **FluentValidation.AspNetCore**: Integrates FluentValidation with ASP.NET Core for model validation.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Adds JWT authentication for secure API access.
- **Microsoft.AspNetCore.OpenApi**: Enables OpenAPI/Swagger support for API documentation.
- **Microsoft.EntityFrameworkCore.Tools**: Provides EF Core CLI tooling for migrations and scaffolding.
- **Serilog.AspNetCore**: Adds Serilog logging to ASP.NET Core for structured logging.
- **Serilog.Sinks.PostgreSQL**: Allows logging directly to PostgreSQL for audit trails.
- **Swashbuckle.AspNetCore.Swagger/SwaggerGen/SwaggerUI**: Generates and serves Swagger UI and OpenAPI docs for the API.

### GalaxusIntegration.Application
- **AutoMapper**: Simplifies mapping between domain models and DTOs.
- **FluentValidation**: Provides a fluent interface for validating business rules and data.
- **MediatR**: Implements the mediator pattern for decoupled request/response and event handling.
- **Microsoft.Extensions.Logging.Abstractions**: Enables logging abstractions for dependency injection and testing.

### GalaxusIntegration.Core
- **FluentValidation**: Used for core business rule validation.
- **MediatR**: Supports domain events and notifications within the core domain.

### GalaxusIntegration.Infrastructure
- **Npgsql.EntityFrameworkCore.PostgreSQL**: EF Core provider for PostgreSQL, enabling database access and migrations.

### GalaxusIntegration.Tests
- **AutoFixture**: Auto-generates test data for unit tests.
- **coverlet.collector**: Collects code coverage data during test runs.
- **FluentAssertions**: Provides expressive assertions for unit tests.
- **Microsoft.EntityFrameworkCore.InMemory**: In-memory EF Core provider for fast, isolated tests.
- **Microsoft.NET.Test.Sdk**: Required for running .NET test projects.
- **Moq**: Mocking framework for creating test doubles.
- **xunit/xunit.runner.visualstudio**: Test framework and runner for .NET.

## Why These Packages?

- **Validation**: FluentValidation ensures all data (especially for Galaxus file requirements) is correct before processing or export.
- **Mapping**: AutoMapper reduces boilerplate when converting between internal models and Galaxus formats.
- **CQRS/Mediator**: MediatR enables clean separation of commands, queries, and events, which is ideal for integration workflows.
- **Logging**: Serilog (with PostgreSQL sink) provides robust, queryable audit trails for all integration activity.
- **Database**: Npgsql.EntityFrameworkCore.PostgreSQL is required for PostgreSQL, the recommended DB for this solution.
- **Testing**: xUnit, Moq, AutoFixture, FluentAssertions, and EFCore.InMemory ensure the solution is reliable and maintainable.
- **API & Documentation**: Swashbuckle and OpenAPI packages provide interactive documentation and testing for internal APIs.
- **Security**: JWT Bearer authentication secures the API endpoints.

## Summary

This solution is ready for robust, maintainable Galaxus integration, following best practices for .NET, validation, logging, and testability. Each package is chosen to address a specific requirement from the Galaxus integration guide or to support modern .NET development patterns.
