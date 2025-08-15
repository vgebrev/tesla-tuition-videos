# Gemini Code Assistant Documentation

This document provides guidance for using Gemini Code Assistant with the TTV codebase.

## Technology Stack

The TTV solution is built on the .NET platform, utilizing the following key technologies:

- **Backend:** C# with ASP.NET Core for web APIs and authentication.
- **Frontend:** Svelte for the client-side UI. The legacy Blazor front-end is no longer in use or developed but kept for reference.
- **Database:** Microsoft SQL Server with Entity Framework Core for data access.
- **Authentication:** Duende IdentityServer for OpenID Connect and OAuth 2.0.
- **Testing:** xUnit, FakeItEasy, and FluentAssertions for unit and integration testing.

## Architecture

The solution follows a clean architecture pattern, separating concerns into the following projects:

- **TTV.Domain:** Contains the core business logic and entities.
- **TTV.Application:** Implements the application logic and use cases.
- **TTV.Infrastructure:** Provides implementations for external concerns like data access, file storage, and other services.
- **TTV.Web:** Contains the web-related projects, including the API, authentication, and Blazor UI.

## Design Patterns

The codebase makes use of several design patterns, including:

- **Repository Pattern:** The `TTV.Domain` project defines repository interfaces for data access, which are implemented in the `TTV.Infrastructure` project.
- **Unit of Work Pattern:** The `IUnitOfWork` interface in `TTV.Domain` is used to manage transactions and coordinate changes to multiple repositories.
- **Dependency Injection:** The solution uses the built-in dependency injection container in ASP.NET Core to manage dependencies between components.
- **Options Pattern:** The `SystemSettings` class in `TTV.Application` is configured using the options pattern to provide strongly-typed access to configuration settings.

## Getting Started

To get started with the project, you will need to have the following installed:

- .NET 9 SDK
- Microsoft SQL Server
- An IDE like Visual Studio or VS Code

Once you have the prerequisites installed, you can open the `TTV.sln` file in your IDE and run the `TTV.DatabaseDeploy` project to create and seed the database. Then, you can run the `TTV.Web.Api` and `TTV.Web.Auth` projects to start the backend services, and navigate to the `TTV.Web\Svelte` directory to launch the frontend application.

## How to Contribute

When contributing to the codebase, please adhere to the following guidelines:

- Follow the existing coding style and conventions.
- Write unit tests for new features and bug fixes.
- Ensure that all tests pass before submitting a pull request.
- Update the documentation as needed.
