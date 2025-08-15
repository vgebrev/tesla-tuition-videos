# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Tesla Tuition Videos (TTV) is a comprehensive e-learning platform built with .NET 9 and SvelteKit, providing video lessons primarily for chemistry and physics education. The system implements a clean architecture pattern with Domain-Driven Design principles.

## Development Commands

### .NET Backend
```bash
# Build and test
dotnet build                    # Build entire solution
dotnet test                     # Run xUnit tests using FakeItEasy and FluentAssertions

# Database operations
cd TTV.DatabaseDeploy
dotnet run                      # Deploy/migrate database using EF Core 9
```

### SvelteKit Frontend
```bash
cd TTV.Web/Svelte

# Development
npm run dev                     # Start HTTPS development server (port 5173)
npm run dev -- --open          # Start dev server and open browser

# Building
npm run build                   # Production build
npm run build:uat               # UAT environment build
npm run build:prod             # Production environment build
npm run preview                 # Preview production build

# Quality Assurance
npm run lint                    # Run prettier and eslint checks
npm run format                  # Format code with prettier
npm run check                   # Run svelte-check for TypeScript checking
npm run check:watch             # Run type checking in watch mode
npm run test                    # Run vitest tests
```

## Architecture Overview

### Backend (.NET 9)
The backend follows **Clean Architecture** with clear separation of concerns:

- **TTV.Domain**: Core domain entities, value objects, and business rules
- **TTV.Application**: Business logic, use cases, managers, and application services
- **TTV.Infrastructure**: External concerns (database, email, payments, file storage, AI tutor integration)
- **TTV.Web.Api**: REST API controllers with mapping extensions
- **TTV.Web.Auth**: IdentityServer-based authentication service with OIDC/OAuth2
- **TTV.Web.Shared**: Shared DTOs between API and clients
- **TTV.DatabaseDeploy**: EF Core migrations and database deployment
- **TTV.Tests**: xUnit tests with FakeItEasy mocking and FluentAssertions

Key patterns:
- **Repository pattern** for data access
- **Unit of Work** for transaction management
- **Manager classes** for coordinating business operations
- **Result pattern** for consistent error handling
- **Background job processing** for async operations

### Frontend (SvelteKit)
- **SvelteKit** with TypeScript and static adapter for deployment
- **Component-based architecture** with feature-based organization
- **OIDC authentication** via oidc-client library
- **Centralized API layer** with automatic token handling
- **Svelte stores** for state management (no external state library)
- **Bootstrap 5** for UI components with custom styling

### Key Technologies
- **.NET 9** with Entity Framework Core 9
- **IdentityServer** for OAuth2/OIDC authentication
- **SQL Server** database
- **SvelteKit** with Vite for frontend
- **Multiple payment processors**: Payfast, PayPal, manual bank transfer
- **Email notifications** via SMTP
- **AI tutor integration** via Ollama

## Domain Model

### Core Business Entities
- **Lesson**: Video content with pricing, duration, tags, and access control
- **Order**: Purchase transactions with voucher support and payment tracking
- **User**: OIDC-authenticated users with admin roles
- **LearningPath**: Hierarchical curriculum structure with nested items
- **DiscountVoucher**: Promotional codes with balance tracking and expiry
- **Payment**: Multi-provider payment processing with confirmation workflow
- **Video/Document**: Content management with file streaming

### Business Flows
- **Shopping Cart**: Lesson selection → Order creation → Payment processing → Content access
- **Authentication**: OIDC flow → Token management → Role-based authorization
- **Content Delivery**: Ownership verification → Secure file streaming
- **Admin Operations**: User management, voucher issuance, order fulfillment

## Configuration Requirements

### .NET Projects
All backend projects require `secrets.json` configuration via .NET Secret Manager:

#### TTV.Web.Api Secrets
- **ConnectionStrings:DataContext**: EF Core database connection
- **SystemSettings**: File paths (videos, documents), email SMTP, payment gateway credentials
- **DiscountVoucherPepper**: Encryption key for voucher generation

#### TTV.Web.Auth Secrets
- **ConnectionStrings**: Both IdentityServer and DataContext connections
- **Authentication**: Google/Facebook OAuth provider credentials
- **SystemSettings**: Email configuration for user notifications

### SvelteKit Frontend (.env)
- **PUBLIC_OIDC_AUTHORITY**: Identity server URL (https://localhost:5001)
- **PUBLIC_API_BASEURL**: Backend API URL (https://localhost:5002)
- **PUBLIC_BANKACCOUNT_***: Manual payment bank details
- **PUBLIC_ANNOUNCEMENT_***: Site-wide announcement configuration
- **PUBLIC_ISTESTENVIRONMENT**: Test environment banner flag

## Development Environment Setup

### HTTPS Development
Both frontend and backend run with HTTPS certificates:
- Backend: Uses .NET development certificates
- Frontend: Local certificates in `TTV.Web/Svelte/cert/` directory
- Required for OIDC authentication flows

### Database Setup
1. Configure connection string in `TTV.DatabaseDeploy/secrets.json`
2. Run `dotnet run` in TTV.DatabaseDeploy to apply EF Core migrations
3. Sample data is seeded automatically in development environment

### Payment Integration
For Payfast sandbox testing:
1. Use Cloudflare tunnels to expose local endpoints
2. Configure tunnel URLs in secrets.json for API callbacks
3. Update CORS settings in both API and Auth services

## Testing Strategy

### Backend Tests
- **xUnit** test framework with **FakeItEasy** for mocking
- **FluentAssertions** for readable test assertions
- Focus on business logic in managers and domain services
- Payment signature validation and voucher generation testing

### Frontend Tests
- **Vitest** for unit testing Svelte components
- Component interaction and API integration tests
- Authentication flow testing

## Key Development Guidelines

### Code Quality
- **TreatWarningsAsErrors** enabled for all .NET projects
- **Nullable reference types** enabled throughout
- **Consistent error handling** using Result pattern
- **TypeScript** strict mode in SvelteKit frontend

### Security Considerations
- OIDC/OAuth2 for authentication with automatic token renewal
- Role-based authorization (Admin vs User)
- Secure file streaming with ownership verification
- Payment webhook validation with signature checking
- Never commit secrets or environment files to repository

### Component Organization (SvelteKit)
- Feature-based folders: `Admin/`, `LearningPaths/`, `Checkout/`
- Common reusable components in `components/common/`
- Use `$components` alias instead of relative imports
- Follow established store patterns for state management