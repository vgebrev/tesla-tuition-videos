# Tesla Tuition Videos Development Environment Setup

This document outlines the trickier bits when setting up a working development environment.

---

## Quickstart (Ubuntu)

Tested on Ubuntu 24.04. Prerequisites: **.NET 10 SDK**, **Node 22.12+ or 24.x**, **Docker**, and a
user in the `docker` group.

### 1. Backing services

SQL Server has no native Ubuntu 24.04 package in the 2022 line, so it runs in Docker along with
[Mailpit](https://mailpit.axllent.org/), which catches outbound dev email instead of relaying it.

```bash
echo "MSSQL_SA_PASSWORD=<a strong password>" > .env   # gitignored
chmod 600 .env
docker compose up -d
```

SQL Server rejects weak passwords: 8+ characters using three of upper / lower / digit / symbol.
Mailpit's web UI is at <http://localhost:8025>.

### 2. Certificates

Both front-end and back-end run over HTTPS, and OIDC redirects mean a click-through certificate
exception is not good enough — the CA must actually be trusted.

```bash
sudo apt install -y mkcert libnss3-tools
mkcert -install

# Vite reads these two paths literally (vite.config.js), so keep the filenames.
cd TTV.Web/Svelte
mkcert -key-file cert/key.pem -cert-file cert/cert.pem localhost 127.0.0.1 ::1
cd ../..
```

The backend has no Kestrel HTTPS configuration at all; `https://localhost:5001` and `:5002` are
served with the ASP.NET Core development certificate. Two separate consumers must trust it — the
API process (it fetches the OIDC discovery document from the Auth service) and your browser:

```bash
dotnet dev-certs https --trust
dotnet dev-certs https --export-path /tmp/aspnet-dev.crt --format PEM --no-password

# System CA bundle — for the API -> Auth server-to-server call
sudo cp /tmp/aspnet-dev.crt /usr/local/share/ca-certificates/aspnet-dev-localhost.crt
sudo update-ca-certificates

# NSS store — for Chrome / Firefox. Note the trust flag: "P,," (trusted peer),
# NOT "C,," (trusted CA). The dev certificate is a self-signed *leaf* with
# `CA:FALSE`, so trusting it as a CA makes NSS try to use it as its own issuer
# and fail with "Issuer certificate is invalid" — which Chrome surfaces as the
# unhelpful ERR_CERT_INVALID. mkcert's CA does use "C,,", because it is a CA.
certutil -d sql:$HOME/.pki/nssdb -A -t "P,," -n aspnet-dev-localhost -i /tmp/aspnet-dev.crt
```

Restart the browser completely after touching the NSS store — it is read at startup.

> **This will break roughly once a year.** Peer trust pins Chrome to that exact certificate, so
> regenerating the dev certificate (on expiry, or after `dotnet dev-certs https --clean`) invalidates
> it and the `certutil` step must be repeated. Verify with
> `certutil -d sql:$HOME/.pki/nssdb -V -n <nickname> -u V`, which should print
> `certificate is valid`.
>
> To avoid the maintenance, point Kestrel at an mkcert-issued certificate instead — mkcert's CA is
> trusted as a CA, so re-issuing a leaf needs no trust-store changes. No repo changes required,
> just two environment variables per run configuration:
> `ASPNETCORE_Kestrel__Certificates__Default__Path` and `…__KeyPath`.

### 3. Secrets, content directories and `.env`

Populate `secrets.json` for the three projects (see
[User Secrets Configuration](#user-secrets-configuration-for-net-solution) below), using POSIX
paths for `VideosPath` / `DocumentsPath`, and create those directories. The subdirectory names come
from the `RelativePath` values in the seed data:

```bash
mkdir -p ~/ttv-content/Videos/LimitedAccess/Thumbnails
mkdir -p ~/ttv-content/Documents/{ExercisePdfs,Reference}

cp TTV.Web/Svelte/.env.example TTV.Web/Svelte/.env
```

> Media filenames are derived from lesson titles and ext4 is case-sensitive where NTFS was not.
> Content copied from a Windows machine must match the seeded titles exactly.

### 4. Database

**Order matters, and it is the reverse of what you might expect.** The `TTV_Create` migration
declares foreign keys against `[user].[AspNetUsers]`, which is created by the Auth project's
ASP.NET Identity migration — so Auth must be seeded first. Both steps create the database if it
does not exist, so there is nothing to set up by hand.

```bash
dotnet tool restore                 # dotnet-ef 10.0.0; run from the repo root
dotnet build TTV.sln

# 1. Identity + Duende schema, roles, and the seed users
dotnet run --project TTV.Web/Auth -- /seed

# 2. Application schema and the lesson catalogue
dotnet run --project TTV.DatabaseDeploy
```

To target a different database for a one-off run, override the connection string without touching
your secrets:

```bash
ConnectionStrings__DataContext="Server=localhost,1433;Database=TTV_scratch;..." \
  dotnet run --project TTV.DatabaseDeploy
```

Seeded logins are `admin@example.com` and `demo@example.com`, both with password
`SecurePassword123!` (defined in `TTV.Web/Auth/appsettings.Development.json`).

### 5. Run

```bash
dotnet run --project TTV.Web/Auth                          # https://localhost:5001
dotnet run --project TTV.Web/Api --launch-profile https    # https://localhost:5002
cd TTV.Web/Svelte && npm ci && npm run dev -- --port 5173 --strictPort
```

Port **5173 is mandatory**, not merely the Vite default — the seeded IdentityServer client's
redirect URIs and the `Cors:AllowedOrigins` entries in both `appsettings.Development.json` files
hard-code it. Never use the API's `IIS Express` launch profile (Windows-only) or its plain `http`
profile (`UseHttpsRedirection` has no HTTPS endpoint to redirect to there).

### Optional extras

```bash
# sqlcmd on the host, for the scripts in TTV.DatabaseDeploy/Scripts/
curl -sSL https://packages.microsoft.com/keys/microsoft.asc | sudo tee /etc/apt/trusted.gpg.d/microsoft.asc > /dev/null
curl -sSL https://packages.microsoft.com/config/ubuntu/24.04/prod.list | sudo tee /etc/apt/sources.list.d/mssql-release.list
sudo apt update && sudo ACCEPT_EULA=Y apt install -y mssql-tools18 unixodbc-dev
# then add /opt/mssql-tools18/bin to PATH

# Ollama, for the AI tutor endpoint
curl -fsSL https://ollama.com/install.sh | sh
ollama pull llama3.1 && ollama pull nomic-embed-text
```

---

## Payfast Sandbox Integration Setup in Development

Onsite Payments Reference: https://developers.payfast.co.za/docs#onsite_payments

### Payfast Endpoints

* In TTV.Web.Api `secrets.json`, `PayfastSettings`:

      "PayfastUrl": "https://sandbox.payfast.co.za/onsite/process",
      "PayfastValidationUrl": "https://sandbox.payfast.co.za/eng/query/validate",


* In `TTV.Web\Svelte\src\app.html` (or `TTV.Web\Blazor\wwwwroot\index.html`):

      <script src="https://sandbox.payfast.co.za/onsite/engine.js"></script>


### Create Cloudflare Tunnels

Install [cloudflared](https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/downloads/) to create a tunnel for the API and Front-end that Payfast can call:

      curl -fsSL https://github.com/cloudflare/cloudflared/releases/latest/download/cloudflared-linux-amd64.deb -o /tmp/cloudflared.deb
      sudo dpkg -i /tmp/cloudflared.deb

Then, in two separate terminals:

      # API
      cloudflared --url https://localhost:5002

      # Svelte front-end
      cloudflared --url https://localhost:5173

This will give 2 temporal public URLs that look like `https://garbage-sofa-scored-ah.trycloudflare.com`

### Configure Apps with Tunnel URLs

* In TTV.Web.API:
    * `secrets.json`, `PayfastSettings`:

          "ReturnUrl": "{front-end-tunnel-url}/order-complete",
          "CancelUrl": "{front-end-tunnel-url}/payment-cancel",
          "NotifyUrl": "{api-tunnel-url}/payments/confirm-payfast",
        

    * `appsettings.Development.json`: Add the front-end tunnel URL to `Cors:AllowedOrigins`

* In TTV.Web.Auth:
    * `appsettings.Development.json`: Add the front-end tunnel URL to `Cors:AllowedOrigins`
    * Update TTV.Database.Deploy `Scripts\UpdateReverseProxyRedirectUris.sql`:

          declare @Uri nvarchar(64) = '{front-end-tunnel-url}'

        * Double check the `[Id]` fields actually match the records (if any) in the `[auth_cfg].[ClientRedirectUris]` and `[auth_cfg].[ClientPostLogoutRedirectUris]` tables.
        * Run the script against the Dev database `TTV`.

* In `TTV.Web/Svelte/.env`:
      
      PUBLIC_API_BASEURL="{api-tunnel-url}"

* **OBSOLETE** For the TTV.Web.Blazor front-end (superceded by the Svelte front-end), `wwwroot/appsettings.Development.json`, `Local`:

      "ApiRootUri": "{api-tunnel-url}",
      "PostLogoutRedirectUri": "{front-end-tunnel-url}/authentication/logout-callback",
      "RedirectUri": "{front-end-tunnel-url}/authentication/login-callback",

The following should now work:
* Access the front-end through the front-end tunnel URL.
* Authenticate users and redirect back to that URL.
* Front-end calls the API through the API tunnel URL.
* API initiates payments with Payfast (Sandbox).
* Front-end triggers Payfast (Sandbox) onsite modal.
* Payfast (Sandbox) confirms payments with the API.
* Payfast (Sandbox) redirects back to front-end when payments are completed/cancelled.

---

## User Secrets Configuration for .NET Solution

This section provides an overview of how to configure and manage user secrets (`secrets.json`) for the TTV .NET solution. User secrets store sensitive information securely and should not be committed to version control.

### Overview
Each relevant project within the solution has its own `secrets.json` file containing configuration settings required for its functionality, such as database connections, IdentityServer third-party providers authentication credentials, file paths, email settings, and payment gateway credentials. These settings are accessed at runtime via `.NET Secret Manager`.

---

### SystemSettings.cs Definition
The TTV.Application `SystemSettings.cs` file defines the structure of system-wide settings used across the solution. Below is the definition along with descriptions of each setting:

```csharp
using TTV.Domain.Entities;

namespace TTV.Application;

public record SystemSettings
{
    public string VideosPath { get; init; } = string.Empty; // Path where video files are stored
    public string DocumentsPath { get; init; } = string.Empty; // Path where document files are stored
    public string DiscountVoucherPepper { get; init; } = string.Empty; // GUID Value. Encryption pepper for discount voucher hash generation
    public bool IsTestEnvironment { get; set; } = true; // Indicates whether the environment is a test setup
    public EmailSettings EmailSettings { get; init; } = new(); // Email configuration settings
    public NotificationSettings NotificationSettings { get; init; } = new(); // Notification configuration settings
    public PayfastSettings PayfastSettings { get; init; } = new(); // Payfast payment configuration settings
    public OllamaAiTutorSettings OllamaAiTutorSettings { get; init; } = new(); // Ollama AI tutor configuration settings
}

public record EmailSettings
{
    public string SmtpHost { get; init; } = string.Empty; // SMTP server address
    public int? SmtpPort { get; init; } // SMTP port (587 for TLS, 465 for SSL, 25 for non-secure)
    public bool? EnableSsl { get; init; } // Enables SSL/TLS encryption
    public string? SmtpUsername { get; init; } // Username for SMTP authentication
    public string? SmtpPassword { get; init; } // Password for SMTP authentication
    public string DefaultFrom { get; init; } = string.Empty; // Default sender email address
    public string? DefaultTo { get; init; } // Default recipient email address
}

public record PayfastSettings
{
    public string MerchantId { get; init; } = string.Empty; // Payfast merchant ID
    public string MerchantKey { get; init; } = string.Empty; // Payfast merchant key
    public string? Passphrase { get; init; } // Security passphrase for Payfast transactions
    public string PayfastUrl { get; init; } = string.Empty; // Payfast payment processing URL
    public string PayfastValidationUrl { get; init; } = string.Empty; // Payfast payment validation URL
    public string ReturnUrl { get; init; } = string.Empty; // UI callback URL for successful transactions
    public string CancelUrl { get; init; } = string.Empty; // UI callback URL for canceled transactions
    public string NotifyUrl { get; init; } = string.Empty; // API callback endpoint for Payfast payment notifications
    public string[] AllowedHosts { get; init; } = []; // Allowed Payfast hosts for secure transactions
}

public record NotificationSettings
{
    public NotificationType[] DisabledNotificationTypes { get; init; } = []; // Notification types that are disabled
}

public record OllamaAiTutorSettings
{
    public bool IsEnabled { get; init; } // Whether the AI tutor endpoint is active
    public string Uri { get; init; } = string.Empty; // Ollama server URI, e.g. http://localhost:11434
    public string Model { get; init; } = string.Empty; // Chat model name
    public string EmbeddingsModel { get; init; } = string.Empty; // Embeddings model name
    public float QuestionRelevanceThreshold { get; init; } // Minimum cosine similarity for a question to be answered
}
```

> `Uri` must be a valid absolute URI **even when `IsEnabled` is `false`**. `OllamaAiTutor`
> constructs its `OllamaApiClient` in a field initialiser, before the enabled check, so an empty
> string throws `UriFormatException` when `IAiTutor` is resolved.

---

### Secrets Structure
The following sections outline the secrets configuration for each relevant project in the solution.

#### TTV.Database.Deploy Secrets

```json
{
  "ConnectionStrings": {
    "DataContext": "{Connection string to the TTV database for EF to use}"
  }
}
```

#### TTV.Web.Api Secrets

```json
{
  "ConnectionStrings": {
    "DataContext": "{Connection string to the TTV database for EF to use}"
  },
  "SystemSettings": {
    "VideosPath": "/home/{user}/ttv-content/Videos",
    "DocumentsPath": "/home/{user}/ttv-content/Documents",
    "DiscountVoucherPepper": "{Discount Voucher Encryption Pepper GUID}",
    "IsTestEnvironment": true,
    "EmailSettings": {
      "SmtpHost": "localhost",
      "SmtpPort": 1025,
      "EnableSsl": false,
      "SmtpUsername": null,
      "SmtpPassword": null,
      "DefaultFrom": "support@teslatuitionvideos.co.za",
      "DefaultTo": "dev@localhost"
    },
    "NotificationSettings": {
      "DisabledNotificationTypes": [ "OrderConfirmation" ]
    },
    "PayfastSettings": {
      "MerchantId": "10000100",
      "MerchantKey": "46f0cd694581a",
      "Passphrase": null,
      "PayfastUrl": "https://sandbox.payfast.co.za/onsite/process",
      "PayfastValidationUrl": "https://sandbox.payfast.co.za/eng/query/validate",
      "ReturnUrl": "https://localhost:5173/order-complete",
      "CancelUrl": "https://localhost:5173/payment-cancel",
      "NotifyUrl": "https://localhost:5002/payments/confirm-payfast",
      "AllowedHosts": [ "sandbox.payfast.co.za", "www.payfast.co.za" ]
    },
    "OllamaAiTutorSettings": {
      "IsEnabled": true,
      "Uri": "http://localhost:11434",
      "Model": "llama3.1",
      "EmbeddingsModel": "nomic-embed-text",
      "QuestionRelevanceThreshold": 0.5
    }
  }
}
```

The `EmailSettings` above point at the Mailpit container from `docker-compose.yml`. `DefaultTo` is
an override sink — `EmailSender` sends to `DefaultTo ?? to`, so setting it guarantees development
mail can never reach a real recipient.

#### TTV.Web.Auth Secrets

```json
{
  "ConnectionStrings": {
    "IdentityServer": "{Connection string to the TTV database for IdentityServer to use}",
    "DataContext": "{Connection string to the TTV database for EF to use}"
  },
  "SystemSettings": {
    "IsTestEnvironment": true,
    "EmailSettings": {
      "SmtpHost": "localhost",
      "SmtpPort": 1025,
      "EnableSsl": false,
      "SmtpUsername": null,
      "SmtpPassword": null,
      "DefaultFrom": "support@teslatuitionvideos.co.za",
      "DefaultTo": "dev@localhost"
    },
    "NotificationSettings": {
      "DisabledNotificationTypes": []
    }
  },
  "Authentication": {
    "Google": {
      "ClientId": "{Google client ID}",
      "ClientSecret": "{Google client secret}",
      "Enabled": false
    },
    "Facebook": {
      "AppId": "{Facebook app ID}",
      "AppSecret": "{Facebook app secret}",
      "Enabled": false
    }
  }
}
```

Both social providers can stay disabled for local development — the seeded users authenticate with
username and password. Note that `Enabled` is compared as the **string** `"True"`, so use JSON
booleans here rather than quoted strings.
---

## Environment Variables for the Svelte Front-end

The Svelte front-end uses an `.env` file (at the root of `TTV.Web/Svelte`) to manage environment-specific settings. This file should not be committed to the repository — copy `.env.example` to `.env` and edit it. Note that `npm run build:uat` and `npm run build:prod` use Vite modes `uat` and `prod`, so they read `.env.uat` and `.env.prod` respectively; neither is committed.

### Environment File Structure
The following section shows the structure of an example development `.env` file and descriptions of each variable.

```bash
PUBLIC_OIDC_AUTHORITY="https://localhost:5001" # OpenID Connect authority for authentication
PUBLIC_API_BASEURL="https://localhost:5002" # Base URL for the API backend
PUBLIC_ANNOUNCEMENT_ACTIVE="true" # Whether an announcement banner is active
PUBLIC_ANNOUNCEMENT_ID="unique-announcement-id" # Unique identifier for the active announcement
PUBLIC_ANNOUNCEMENT_TITLE="Tesla Tuition Videos" # Title of the announcement
PUBLIC_ANNOUNCEMENT_MESSAGE="Announcement message" # Announcement text displayed to users
PUBLIC_ANNOUNCEMENT_ENDDATE="2024-12-31T23:59:59" # Expiration date for the announcement
PUBLIC_ISTESTENVIRONMENT="true" # Whether the Test Environment banner is displayed
PUBLIC_BANKACCOUNT_BANKNAME="ACME Bank" # Bank name for Manual Bank payment info
PUBLIC_BANKACCOUNT_ACCOUNTNUMBER="1234567890" # Bank account number for Manual Bank payment info
PUBLIC_BANKACCOUNT_BRANCHCODE="123456" # Branch code for Manual Bank payment info
```

---

### Security Considerations
- Never commit `secrets.json`, `.env`, or `TTV.Web/Svelte/cert/*.pem` to version control.
- Use environment variables for production secrets.
- Rotate credentials regularly.
- Restrict access to `.NET Secret Manager`.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.