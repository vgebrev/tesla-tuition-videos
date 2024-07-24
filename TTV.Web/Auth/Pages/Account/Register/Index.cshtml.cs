using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTV.Web.Auth.Models;

namespace TTV.Web.Auth.Pages.Account.Register
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class Index(UserManager<ApplicationUser> userManager) : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager = userManager;

        [BindProperty]
        public InputModel Input { get; set; }

        public ViewModel View { get; set; }

        public IActionResult OnGet(string returnUrl)
        {
            Input = new InputModel
            {
                ReturnUrl = returnUrl
            };

            View = new ViewModel
            {
                IsRegistrationSuccessful = false
            };
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            View = new();

            if (Input.Button != "register")
            {
                return Page();
            }

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(Input.Username);
                if (user != null)
                {
                    ModelState.AddModelError($"{nameof(Input)}.{nameof(Input.Username)}", "E-mail is already in use.");
                    return Page();
                }

                user = new ApplicationUser()
                {
                    UserName = Input.Username,
                    Email = Input.Username,
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, Input.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }

                result = await userManager.AddClaimsAsync(user,
                [
                    new(JwtClaimTypes.Name, $"{Input.FirstName} {Input.Surname}"),
                    new(JwtClaimTypes.GivenName, Input.FirstName ?? Input.Username),
                ]);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
            }

            View.IsRegistrationSuccessful = ModelState.IsValid;
            return Page();
        }
    }
}
