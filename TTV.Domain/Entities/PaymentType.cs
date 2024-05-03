using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;
public enum PaymentType
{
    [Display(Name = "Manual Bank Transfer")]
    ManualBankTransfer = 1,

    [Display(Name = "Payfast")]
    Payfast = 2,

    [Display(Name = "PayPal")]
    PayPal = 3
}
