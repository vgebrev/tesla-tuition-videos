using System.ComponentModel.DataAnnotations;

namespace TTV.Web.Auth.Pages.Account.PasswordReset;

public class InputModel
{
    [Display(Name = "E-mail")]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Username { get; set; }
    public string ReturnUrl { get; set; }
    public string Button { get; set; }
}
