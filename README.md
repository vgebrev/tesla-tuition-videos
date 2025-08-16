# Tesla Tuition Videos Development Environment Setup

This document outlines the trickier bits when setting up a working development environment.

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

Download [cloudflared](https://developers.cloudflare.com/cloudflare-one/connections/connect-networks/downloads/#windows) (in `C:\Tools`) to create a tunnel for the API and Front-end that Payfast can call:

      :API
      C:\Tools>cloudflared --url https://localhost:5002

      :Svelte front-end (or https://localhost:5003 for Blazor)
      C:\Tools>clourflared --url https://localhost:5173

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

* In `Web\Svelte\.evn`:
      
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
```

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
    "VideosPath": "{C:\\Path\\To\\Videos}",
    "DocumentsPath": "{C:\\Path\\To\\Documents}",
    "DiscountVoucherPepper": "{Discount Voucher Encryption Pepper GUID}",
    "IsTestEnvironment": true,
    "EmailSettings": {
      "SmtpHost": "{smtp.your.mail.service.com}",
      "SmtpPort": 587,
      "EnableSsl": true,
      "SmtpUsername": "{SmtpUsername}",
      "SmtpPassword": "{SmtpPassword}",
      "DefaultFrom": "support@teslatuitionvideos.co.za"
    },
    "NotificationSettings": {
      "DisabledNotificationTypes": [ "OrderConfirmation" ]
    }
  }
}
```

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
      "SmtpHost": "{smtp.your.mail.service.com}",
      "SmtpPort": 587,
      "EnableSsl": true,
      "SmtpUsername": "{SmtpUsername}",
      "SmtpPassword": "{SmtpPassword}",
      "DefaultFrom": "support@teslatuitionvideos.co.za"
    },
    "NotificationSettings": {
      "DisabledNotificationTypes": []
    }
  },
  "Authentication": {
    "Google": {
      "ClientId": "{Google client ID}",
      "ClientSecret": "{Google client secret}",
      "Enabled": true
    },
    "Facebook": {
      "AppID": "{Facebook app ID}",
      "AppSecret": "{Facebook app secret}",
      "Enabled": false
    }
  }
}
```
---

## Environment Variables for the Svelte Front-end

The Svelte front-end uses an `.env` file (at the root of `TTV.Web\Svelte`) to manage environment-specific settings. This file should not be commited to the repository. 

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
- Never commit `secrets.json` or `.env` to version control.
- Use environment variables for production secrets.
- Rotate credentials regularly.
- Restrict access to `.NET Secret Manager`.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.