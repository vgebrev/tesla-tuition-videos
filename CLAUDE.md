# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Tesla Tuition Videos (TTV) is a comprehensive e-learning platform built with .NET 10 and SvelteKit, providing video lessons primarily for chemistry and physics education. The system implements a clean architecture pattern with Domain-Driven Design principles.

## Development Commands

### .NET Backend
```bash
# Build and test
dotnet build TTV.sln            # Build entire solution
dotnet test                     # Run xUnit tests using FakeItEasy and Shouldly

# Database operations (see "Database Setup" below — order matters)
dotnet run --project TTV.Web/Auth -- /seed   # Identity/Duende schema, then exits
dotnet run --project TTV.DatabaseDeploy      # App schema + lesson catalogue, via EF Core 10

# EF tooling — a single root manifest pins dotnet-ef 10.0.0 for the whole repo
dotnet tool restore
dotnet ef migrations add <Name> --project <proj> --startup-project <proj>
```

### SvelteKit Frontend
```bash
cd TTV.Web/Svelte

# Development — port 5173 is mandatory, not just the Vite default (see below)
npm run dev -- --port 5173 --strictPort
npm run dev -- --open          # Start dev server and open browser

# Building
npm run build                   # Production build
npm run build:uat               # UAT environment build
npm run build:prod             # Production environment build
npm run preview                 # Preview production build

# Quality Assurance
npm run lint                    # Run prettier and eslint checks
npm run format                  # Format code with prettier
npm run check                   # svelte-kit sync + svelte-check against jsconfig.json
npm run check:watch             # Run type checking in watch mode
npm run test                    # Run vitest (watch mode; use `npx vitest run` for one-shot)
```

`npm run check` is **not** clean — it currently reports ~29 pre-existing errors, mostly SvelteKit
typed-route mismatches. Treat that as the baseline rather than a regression you introduced.

## Architecture Overview

### Backend (.NET 10)
The backend follows **Clean Architecture** with clear separation of concerns:

- **TTV.Domain**: Core domain entities, value objects, and business rules
- **TTV.Application**: Business logic, use cases, managers, and application services
- **TTV.Infrastructure**: External concerns (database, email, payments, file storage, AI tutor integration)
- **TTV.Web.Api**: REST API controllers with mapping extensions
- **TTV.Web.Auth**: Duende IdentityServer 7 authentication service with OIDC/OAuth2
- **TTV.Web.Shared**: Shared DTOs between API and clients
- **TTV.DatabaseDeploy**: EF Core migrations and database deployment
- **TTV.Tests**: xUnit tests with FakeItEasy mocking and Shouldly assertions

`TTV.Web/Blazor.Deprecated` still targets `net8.0` but is **not** in `TTV.sln`; don't build it.

Key patterns:
- **Repository pattern** for data access
- **Unit of Work** for transaction management
- **Manager classes** for coordinating business operations
- **Result pattern** for consistent error handling
- **Background job processing** for async operations

### Frontend (SvelteKit)
- **SvelteKit 2** SPA (`adapter-static` with `fallback: index.html`, `ssr = false`)
- **JavaScript with JSDoc types**, not TypeScript — there are no `.ts` sources, only `src/app.d.ts`
  and `src/lib/types.d.ts`. Type checking is `svelte-check --tsconfig ./jsconfig.json` with
  `allowJs`/`checkJs`/`strict`
- **Svelte 5 runtime, legacy (non-runes) component syntax throughout** — the codebase uses
  `export let`, `$:` and `<slot>` everywhere and zero runes. Do not introduce `$state`/`$props`
  into existing components; mixing the two modes in one file is a compile error
- **Component-based architecture** with feature-based organization
- **OIDC authentication** via the deprecated `oidc-client@1.11.5` — *not* `oidc-client-ts`, which
  has a different API. A second vendored copy at `static/lib/oidc-client/` serves silent renew
- **Centralized API layer** with automatic token handling
- **Svelte stores** for state management (no external state library)
- **Bootstrap 5 is not an npm dependency** — it's vendored in `static/lib/` (actually Bootswatch
  5.2.3 "vapor", plus Bootstrap Icons 1.10.5 and jQuery slim) and loaded via `<link>`/`<script>`
  tags in `src/app.html`. Nothing to install or upgrade through npm

### Key Technologies
- **.NET 10** with Entity Framework Core 10
- **Duende IdentityServer 7** for OAuth2/OIDC authentication
- **SQL Server** database (runs in Docker locally — see `docker-compose.yml`)
- **SvelteKit** with Vite 7 for frontend
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
- **SystemSettings**: File paths (videos, documents), email SMTP, payment gateway credentials.
  The full shape is `TTV.Application/SystemSettings.cs`; a worked example is in README.md
- **SystemSettings:DiscountVoucherPepper**: Encryption key for voucher generation — nested under
  `SystemSettings`, not a top-level key
- **SystemSettings:OllamaAiTutorSettings:Uri**: must be a valid absolute URI even when
  `IsEnabled` is `false`, or resolving `IAiTutor` throws `UriFormatException`

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

See README.md for the full Ubuntu quickstart. Key points:

### Ports
| Service | URL | Source |
|---|---|---|
| Auth (Duende) | `https://localhost:5001` | `TTV.Web/Auth/Properties/launchSettings.json` (HTTPS only) |
| API | `https://localhost:5002` | `TTV.Web/Api/Properties/launchSettings.json`, profile `https` |
| Svelte | `https://localhost:5173` | Vite default |

Port 5173 is **load-bearing**: the seeded IdentityServer client's redirect URIs live in
`auth_cfg.ClientRedirectUris`, and `Cors:AllowedOrigins` in *both* `appsettings.Development.json`
files hard-code it. Use `--strictPort` so a clash fails loudly. Never use the API's `IIS Express`
profile (Windows-only) or its plain `http` profile (`UseHttpsRedirection` has no HTTPS endpoint
bound there).

### HTTPS Development
Both frontend and backend run with HTTPS certificates — required for OIDC flows:
- **Backend**: there is no Kestrel HTTPS configuration anywhere; TLS comes entirely from
  `dotnet dev-certs https`. It must be trusted in *two* places — the system CA bundle (so the API
  can fetch the OIDC discovery document from the Auth service) and the NSS store (so Chrome and
  Firefox accept the redirect to `:5001`). In NSS it needs trust flag **`P,,` (peer), not `C,,`
  (CA)** — it is a self-signed leaf with `CA:FALSE`, and trusting it as a CA yields
  `ERR_CERT_INVALID` in Chrome. Expect to redo this when the certificate expires (currently
  2027-08-02); see README.md for the commands and a lower-maintenance alternative
- **Frontend**: `TTV.Web/Svelte/cert/{cert,key}.pem`, read literally by `vite.config.js`. These are
  gitignored and generated per machine with mkcert. Note the `readFileSync` calls sit in the
  top-level exported config, so **missing certs break `npm run build` and `vitest`, not just
  `npm run dev`**

### Local infrastructure
`docker-compose.yml` at the repo root provides SQL Server (`:1433`) and Mailpit
(`:1025` SMTP, `:8025` UI). The SA password comes from a gitignored `.env`.

### Data Protection is platform- and path-sensitive
Data Protection is left at framework defaults (`XmlEncryptor: Not Configured`,
`XmlRepository: Not Configured`), which behaves differently per OS:

- **Windows** — keys land in `%LOCALAPPDATA%\ASP.NET\DataProtection-Keys`, encrypted with DPAPI
- **Linux** — keys land in `~/.aspnet/DataProtection-Keys`, **unencrypted at rest**

The key ring is isolated by `ApplicationDiscriminator`, which defaults to the **content root path** —
so it differs between operating systems (`C:\…\TTV.Web\Auth\` vs `/home/…/TTV.Web/Auth/`), and even
between two clones of this repo in different directories.

This matters because Duende protects data with it: `KeyManagement.DataProtectKeys` and
`PersistentGrants.DataProtectData` are both `true`, so the RS256 signing key in `auth_ops.Keys` and
every persisted grant are encrypted with the local key ring. **Restoring a UAT or production
database into a different environment will therefore fail to unprotect them** — IdentityServer
rotates in a fresh signing key and existing grants become unreadable, logging everyone out. A
locally seeded database is unaffected. To share a database across environments, set an explicit
`SetApplicationName()` and a shared key repository.

### Database Setup
The dependency order is the reverse of what the project layout suggests, because the `TTV_Create`
migration declares foreign keys against `[user].[AspNetUsers]`, which the Auth project's Identity
migration creates. Both steps create the database if it is absent:

1. Configure `secrets.json` for TTV.DatabaseDeploy, TTV.Web.Api and TTV.Web.Auth
2. `dotnet run --project TTV.Web/Auth -- /seed` — Identity + Duende schema, the `ttv_web_frontend`
   client, the `admin` role, and users `admin@example.com` / `demo@example.com`
   (password `SecurePassword123!`)
3. `dotnet run --project TTV.DatabaseDeploy` — application schema and lesson catalogue

Seeding in TTV.DatabaseDeploy is **unconditional**, not gated on environment. It is idempotent but
destructive: `SampleData` deletes rows that aren't present in its source arrays.

### Payment Integration
For Payfast sandbox testing:
1. Use Cloudflare tunnels to expose local endpoints
2. Configure tunnel URLs in secrets.json for API callbacks
3. Update CORS settings in both API and Auth services

## Testing Strategy

### Backend Tests
- **xUnit** test framework with **FakeItEasy** for mocking
- **Shouldly** for readable test assertions (replaced FluentAssertions in commit `9f24328`)
- Focus on business logic in managers and domain services
- Payment signature validation and voucher generation testing
- All tests are pure unit tests — no database or network dependency

### Frontend Tests
- **Vitest** is wired up but effectively unused: the only test is `src/index.test.js` asserting
  `1 + 2 === 3`. There is no DOM environment, no setup file and no testing-library, so component
  testing would need that scaffolding built first
- No Playwright / e2e suite exists

## Key Development Guidelines

### Code Quality
- **TreatWarningsAsErrors** enabled for all .NET projects. This includes NuGet audit warnings
  (NU1903), so a newly disclosed advisory against a *transitive* package will break the build;
  the fix is an explicit `PackageReference` pinning a patched version
- **Nullable reference types** enabled throughout
- **Consistent error handling** using Result pattern
- **`checkJs` + `strict`** over JSDoc-annotated JavaScript in the SvelteKit frontend

### Security Considerations
- OIDC/OAuth2 for authentication with automatic token renewal
- Role-based authorization (Admin vs User)
- Secure file streaming with ownership verification
- Payment webhook validation with signature checking
- Never commit secrets, environment files, or `TTV.Web/Svelte/cert/*.pem` to the repository

### Component Organization (SvelteKit)
- Feature-based folders: `Admin/`, `LearningPath/` (singular), `Checkout/`, `AiTutor/`,
  `ShoppingCart/`, `NavMenu/`, and so on
- Common reusable components in `components/common/`
- Use `$components` alias instead of relative imports
- Follow established store patterns for state management