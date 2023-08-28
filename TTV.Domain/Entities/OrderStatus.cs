using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;

public enum OrderStatus
{
    [Display(Name = "New")]
    New = 1,

    [Display(Name = "Awaiting Payment")]
    AwaitingPayment = 2,

    [Display(Name = "Completed")]
    Completed = 3,

    [Display(Name = "Cancelled")]
    Cancelled = 4
}
