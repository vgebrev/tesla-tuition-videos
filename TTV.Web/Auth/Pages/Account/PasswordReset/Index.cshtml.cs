using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TTV.Application.BackgroundJobs;
using TTV.Domain.DomainServices.BackgroundJobs;
using TTV.Web.Auth.Models;

namespace TTV.Web.Auth.Pages.Account.PasswordReset;

[SecurityHeaders]
[AllowAnonymous]
public class Index(UserManager<ApplicationUser> userManager, IBackgroundJobQueue backgroundJob) : PageModel
{
    private readonly UserManager<ApplicationUser> userManager = userManager;
    private readonly IBackgroundJobQueue backgroundJob = backgroundJob;

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
            IsSuccessful = false
        };
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        View = new ViewModel
        {
            IsSuccessful = ModelState.IsValid
        };

        if (Input.Button != "reset")
        {
            return Page();
        }

        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(Input.Username);
            if (user != null)
            {
                string code = await userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page("/Account/PasswordReset/New", null, new { userId = user.Id, code }, Request.Scheme);
                backgroundJob.Enqueue<CreatePasswordResetNotification>(
                    new CreatePasswordResetNotification.JobData(user.Id, callbackUrl));
            }
        }
        
        return Page();
    }
}