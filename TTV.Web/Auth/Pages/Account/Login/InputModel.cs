// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace TTV.Web.Auth.Pages.Login;

public class InputModel
{
    [Required]
    [Display(Name = "E-mail")]
    public string Username { get; set; }
        
    [Required]
    public string Password { get; set; }

    public bool RememberLogin { get; set; } = true;
        
    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}