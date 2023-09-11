using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;
public enum NotificationType
{
    [Display(Name = "Order Confirmation")]
    OrderConfirmation = 1,

    [Display(Name = "Order Complete")]
    OrderComplete = 2,

    [Display(Name = "Order Cancelled")]
    OrderCancelled = 3,

    [Display(Name = "Password Reset")]
    PasswordReset = 4,
}
