using System.ComponentModel.DataAnnotations;

namespace TTV.Web.Shared;
public enum PaymentMethod
{
    [Display(Name = "Manual Bank Transfer")]
    BankTransfer = 1,

    [Display(Name = "Payfast")]
    Payfast = 2,

    [Display(Name = "PayPal")]
    PayPal = 3
}
