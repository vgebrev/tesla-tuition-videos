using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;

public enum OrderStatus
{
    [Display(Name = "New")]
    New = 0,

    [Display(Name = "Processing")]
    Processing = 1,

    [Display(Name = "Completed")]
    Completed = 2,

    [Display(Name = "Cancelled")]
    Cancelled = 3
}
