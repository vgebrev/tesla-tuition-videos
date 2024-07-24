using System.ComponentModel.DataAnnotations;

namespace TTV.Web.Auth.Pages.Account.Register
{
    public class InputModel
    {
        [Display(Name = "E-mail")]
        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Username { get; set; }

        [Display(Name = "Confirm E-mail")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [Compare(nameof(Username))]
        public string ConfirmUsername { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Surname")]
        public string Surname { get; set; }
        public string ReturnUrl { get; set; }

        public string Button { get; set; }
    }
}
