using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTV.Web.Auth.Models;

namespace TTV.Web.Auth.Pages.Account.PasswordReset;

[SecurityHeaders]
[AllowAnonymous]
public class New(UserManager<ApplicationUser> userManager) : PageModel
{
    private readonly UserManager<ApplicationUser> userManager = userManager;

    [BindProperty]
    public NewInputModel Input { get; set; }
    public NewViewModel View { get; set; }

    public IActionResult OnGet(string userId, string code)
    {
        Input = new NewInputModel() { Code = code, UserId = userId };
        View = new NewViewModel() { IsSuccessful = false };

        return Page();
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
