using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTV.Web.Auth.Models;

namespace TTV.Web.Auth.Pages.Account.PasswordReset;

[SecurityHeaders]
[AllowAnonymous]
public class New(UserManager<ApplicationUser> userManager, IClientStore clientStore, Config config) : PageModel
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IClientStore clientStore = clientStore;
    private readonly Config config = config;

    [BindProperty]
    public NewInputModel Input { get; set; }
    public NewViewModel View { get; set; }

    public IActionResult OnGet(string userId, string code)
    {
        Input = new NewInputModel() { Code = code, UserId = userId };
        View = new NewViewModel() { IsSuccessful = false };

        return Page();
    }

    private async Task GetClientDetails()
    {

       var client = await clientStore.FindClientByIdAsync(config.GetClients().First().ClientId);
        var redirectUri = new Uri(client.RedirectUris.Last());
        View.ClientUri = new Uri(redirectUri.GetLeftPart(UriPartial.Authority)).ToString();
        View.ClientName = client.ClientName;
    }

    public async Task<IActionResult> OnPost()
    {
        View = new();

        if (!ModelState.IsValid || Input.Button != "reset")
        {
            return Page();
        }

        var user = await userManager.FindByNameAsync(Input.Username);
        if (user == null || user.Id != new Guid(Input.UserId))
        {
            ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Username)}", "E-mail is invalid for this password reset link");
            return Page();
        }

        var result = await userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
        if (result.Succeeded)
        {
            View.IsSuccessful = true;
            await GetClientDetails();
        }
        else
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Password)}", error.Description);
            }
        }
        return Page();
    }
}
