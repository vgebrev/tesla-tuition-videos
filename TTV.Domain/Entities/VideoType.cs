using System.ComponentModel.DataAnnotations;

namespace TTV.Domain.Entities;

public enum VideoType
{
    [Display(Name = "Full Lesson")]
    FullLesson = 1,

    [Display(Name = "Intro")]
    Intro = 2,
}
