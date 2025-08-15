# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a SvelteKit e-learning platform for Tesla Tuition Videos (TTV), an educational service providing video lessons primarily for chemistry and science subjects. The application supports user authentication, course management, shopping cart functionality, payment processing, and administrative features.

## Development Commands

```bash
# Development
npm run dev              # Start development server with HTTPS
npm run dev -- --open    # Start dev server and open browser

# Building
npm run build            # Production build
npm run build:uat        # UAT environment build
npm run build:prod       # Production environment build
npm run preview          # Preview production build

# Quality Assurance
npm run lint             # Run prettier and eslint checks
npm run format           # Format code with prettier
npm run check            # Run svelte-check for type checking
npm run check:watch      # Run type checking in watch mode
npm run test             # Run vitest tests
```

## Architecture Overview

### Core Structure

- **SvelteKit** framework with static adapter for deployment
- **Component-based architecture** with shared components in `src/components/`
- **Route-based pages** in `src/routes/` following SvelteKit conventions
- **Centralized API layer** via `src/lib/api.js` with authentication handling
- **Type definitions** in `src/lib/types.d.ts` covering all domain entities

### Authentication

- Uses **OIDC/OAuth2** via `oidc-client` library
- Managed through `src/lib/auth.js` with reactive stores
- Automatic token handling in API requests
- Silent token renewal and callback handling

### State Management

- **Svelte stores** for component state (no external state library)
- Store patterns defined in component-specific `.js` files (e.g., `learning-paths.js`)
- Authentication state centralized in `authStore`

### Key Architectural Patterns

#### Component Organization

- **Feature-based folders**: Components grouped by functionality (Admin/, LearningPaths/, Checkout/, etc.)
- **Common components**: Reusable UI elements in `components/common/`
- **Layout components**: Navigation and structure in `NavMenu/`, `AppFooter.svelte`

#### API Integration

- Centralized `api.js` provides HTTP methods (get, post, put, delete)
- Automatic Bearer token injection for authenticated requests
- Base URL configuration via environment variables

#### Type System

- Comprehensive TypeScript definitions for all domain entities
- Store state types for predictable state management
- External library type declarations (oidc-client, payfast)

## Configuration

### Environment Variables

The app uses public environment variables prefixed with `PUBLIC_`:

- `PUBLIC_API_BASEURL` - Backend API base URL
- `PUBLIC_OIDC_AUTHORITY` - Identity provider authority
- `PUBLIC_BANKACCOUNT_*` - Bank transfer payment details
- `PUBLIC_ANNOUNCEMENT_*` - Site-wide announcements
- `PUBLIC_ISTESTENVIRONMENT` - Test environment flag

### Key Configuration Files

- **svelte.config.js**: Adapter configuration, aliases (`$components` → `src/components`)
- **vite.config.js**: HTTPS dev server with local certificates
- **eslint.config.js**: ESLint with Svelte and Prettier integration

## Domain Model

### Core Entities

- **Lesson**: Video content with pricing, tags, and ownership
- **Order**: Purchase transactions with payment tracking
- **User**: OIDC-authenticated users with roles
- **LearningPath**: Structured curriculum with hierarchical items
- **DiscountVoucher**: Promotional codes with balance tracking

### Business Logic

- **Shopping cart**: Lesson selection with price calculation
- **Payment processing**: Multiple methods (Bank transfer, Payfast, PayPal)
- **Content access**: Ownership-based lesson access control
- **Administrative features**: Order management, discount voucher issuance

## Development Notes

### HTTPS Development

The development server runs with HTTPS using local certificates in `cert/` directory. This is required for OIDC authentication flows.

### Component Aliases

Use `$components` alias instead of relative imports:

```javascript
import LessonCard from '$components/common/LessonCard.svelte';
```

### API Error Handling

All API responses follow the `Result<T>` or `ResultOf<T>` pattern with `isSuccess` and `message` fields for consistent error handling.

### State Management Pattern

Components typically follow this pattern:

1. Import component-specific store (e.g., `learningPathsStore`)
2. Use reactive statements for side effects
3. Call store actions for data mutations
4. Handle loading and error states consistently
