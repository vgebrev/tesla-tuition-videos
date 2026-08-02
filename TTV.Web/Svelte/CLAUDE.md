# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a SvelteKit e-learning platform for Tesla Tuition Videos (TTV), an educational service providing video lessons primarily for chemistry and science subjects. The application supports user authentication, course management, shopping cart functionality, payment processing, and administrative features.

## Development Commands

```bash
# Development — port 5173 is mandatory (seeded OIDC redirect URIs + backend CORS)
npm run dev -- --port 5173 --strictPort
npm run dev -- --open    # Start dev server and open browser

# Building
npm run build            # Production build
npm run build:uat        # UAT environment build
npm run build:prod       # Production environment build
npm run preview          # Preview production build

# Quality Assurance
npm run lint             # Run prettier and eslint checks
npm run format           # Format code with prettier
npm run check            # svelte-kit sync + svelte-check against jsconfig.json
npm run check:watch      # Run type checking in watch mode
npm run test             # Run vitest (watch mode; `npx vitest run` for one-shot)
```

`npm run check` reports ~29 pre-existing errors — that's the baseline, not a regression.

## Architecture Overview

### Core Structure

- **SvelteKit 2** with static adapter (SPA: `ssr = false`, `fallback: index.html`)
- **Svelte 5 runtime, legacy (non-runes) syntax throughout** — `export let`, `$:` and `<slot>`
  everywhere, zero runes. Don't introduce `$state`/`$props`/`$derived` into existing components
- **JavaScript with JSDoc types**, not TypeScript — no `.ts` sources, only `.d.ts` declarations
- **Component-based architecture** with shared components in `src/components/`
- **Route-based pages** in `src/routes/` following SvelteKit conventions
- **Centralized API layer** via `src/lib/api.js` with authentication handling
- **Type definitions** in `src/lib/types.d.ts` covering all domain entities

### Authentication

- Uses **OIDC/OAuth2** via the deprecated `oidc-client@1.11.5` — _not_ `oidc-client-ts`, which has
  a different API. A vendored copy at `static/lib/oidc-client/` backs silent renew
- Managed through `src/lib/auth.js` with reactive stores
- Automatic token handling in API requests
- Silent token renewal and callback handling

### State Management

- **Svelte stores** for component state (no external state library)
- Store patterns defined in component-specific `.js` files (e.g., `learning-path.js`)
- Authentication state centralized in `authStore`

### Key Architectural Patterns

#### Component Organization

- **Feature-based folders**: Components grouped by functionality (`Admin/`, `LearningPath/`
  (singular), `Checkout/`, `AiTutor/`, `ShoppingCart/`, `NavMenu/`, etc.)
- **Common components**: Reusable UI elements in `components/common/`
- **Layout components**: Navigation and structure in `NavMenu/`, `AppFooter.svelte`

#### API Integration

- Centralized `api.js` provides HTTP methods (get, post, put, delete)
- Automatic Bearer token injection for authenticated requests
- Base URL configuration via environment variables

#### Type System

- JSDoc annotations over plain JavaScript, checked with `checkJs` + `strict` via `jsconfig.json`
- Ambient `.d.ts` declarations for all domain entities in `src/lib/types.d.ts`
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
- **vite.config.js**: HTTPS dev server with local certificates, plus the vitest config
- **eslint.config.js**: ESLint 9 flat config with Svelte and Prettier integration
- **jsconfig.json**: `checkJs`/`strict`; extends `.svelte-kit/tsconfig.json`, which only exists
  after `svelte-kit sync`
- **.npmrc**: `engine-strict=true`, so transitive engine constraints are hard install errors.
  Node 22.12+ or 24.x is required

### Vendored libraries

`static/lib/` holds libraries loaded by `<link>`/`<script>` tags in `src/app.html` rather than
bundled: Bootswatch 5.2.3 "vapor" (under a `bootstrap/` folder name), Bootstrap Icons 1.10.5,
jQuery slim, and a copy of `oidc-client` for the silent-renew iframe. `bootstrap` is declared as a
global in both `jsconfig.json` and `eslint.config.js`. None of these are npm dependencies, so they
can't be upgraded with npm.

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

The development server runs with HTTPS using local certificates in the `cert/` directory. This is
required for OIDC authentication flows.

`cert/` is gitignored — generate the pair per machine, keeping the exact filenames, since
`vite.config.js` reads `./cert/key.pem` and `./cert/cert.pem` literally:

```bash
mkcert -key-file cert/key.pem -cert-file cert/cert.pem localhost 127.0.0.1 ::1
```

Two consequences worth knowing:

- The paths resolve against the **process CWD**, so npm scripts must be run from
  `TTV.Web/Svelte/`.
- The `readFileSync` calls live in the top-level exported config, so they run for _every_ Vite
  invocation. Missing certs break `npm run build` and `vitest` too, not just `npm run dev`.

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
