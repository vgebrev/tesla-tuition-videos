## Payfast Sandbox Integration Setup in Development

Onsite Payments Reference: https://developers.payfast.co.za/docs#onsite_payments

### Payfast Endpoints

* In TTV.Web.Api `secrets.json`, `PayfastSettings`:

      "PayfastUrl": "https://sandbox.payfast.co.za/onsite/process",
      "PayfastValidationUrl": "https://sandbox.payfast.co.za/eng/query/validate",


* In `Web\Svelte\src\app.html` (or TTV.Web.Blazor `wwwwroot\index.html`):

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