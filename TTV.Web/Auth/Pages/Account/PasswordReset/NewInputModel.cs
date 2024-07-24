using System.ComponentModel.DataAnnotations;

namespace TTV.Web.Auth.Pages.Account.PasswordReset;

public class NewInputModel
{
    [Display(Name = "E-mail")]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }

    [Display(Name = "New Password")]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Confirm New Password")]
    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }

    public string Button { get; set; }
    public string Code { get; set; }
    public string UserId { get; set; }
}
