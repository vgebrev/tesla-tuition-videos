using System.Linq.Expressions;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Domain.Specifications
{
    public class LessonSearchSpecification : ISpecification<Lesson>
    {
        private readonly List<Expression<Func<Lesson, bool>>> filters = new();
        public LessonSearchSpecification(string? searchText, int[]? searchTagsIds)
        {
            
            if (!string.IsNullOrEmpty(searchText))
            {
                filters.Add((Lesson) => Lesson.Title.Contains(searchText) || Lesson.Description.Contains(searchText) || Lesson.Tags.Any(tag => tag.Name.Contains(searchText)));
            }
            if (searchTagsIds != null && searchTagsIds.Length > 0)
            {
                filters.Add((Lesson) => Lesson.Tags.Any(tag => searchTagsIds.Contains(tag.Id)));
            }

            if (filters.Any())
            {
                Filters = filters.ToArray();
            }
        }

        public Expression<Func<Lesson, bool>>[]? Filters { get; set; }
        public Func<IQueryable<Lesson>, IOrderedQueryable<Lesson>> Sort { get; set; } = default!;
        public string IncludeProperties { get; set; } = $"{nameof(Lesson.Tags)}.{nameof(Tag.Category)}";

    }
}
