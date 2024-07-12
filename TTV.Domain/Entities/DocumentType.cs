using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;
public enum DocumentType
{
    [Display(Name = "Exercise PDF")]
    ExercisePdf = 1,

    [Display(Name = "Reference")]
    Reference = 2
}
