namespace TTV.Domain.Entities;

public class Lesson : BaseEntity
{
    public Lesson()
    {
        Videos = new HashSet<Video>();
        Tags = new HashSet<Tag>();
        OwnedBy = new HashSet<User>();
        Prices = new HashSet<Price>();
    }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public LessonType LessonType { get; set; }
    public virtual ICollection<Video> Videos { get; set; }
    public virtual ICollection<Tag> Tags { get; set; }
    public virtual ICollection<User> OwnedBy { get; set; }
    public virtual ICollection<Price> Prices { get; set; }
    public Price CurrentPrice => Prices.OrderByDescending(price => price.EffectiveDate)
        .First(price => DateOnly.FromDateTime(DateTime.Today) >= price.EffectiveDate);
}
