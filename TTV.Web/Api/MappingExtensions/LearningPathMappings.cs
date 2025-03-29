using TTV.Domain.Entities;
using TTV.Web.Shared;

namespace TTV.Web.Api.MappingExtensions;

internal static class LearningPathMappings
{
    public static LearningPathDto ToLearningPathDto(this LearningPath learningPath) =>
        new()
        {
            Id = learningPath.Id,
            Curricula = [.. learningPath.Curricula.ToLookupDtoEnumerable()],
            Name = learningPath.Name,
            Description = learningPath.Description,
            Items = [.. learningPath.Items.Where(i => i.Parent == null).ToLearningPathItemDtoEnumerable()],
        };

    public static IEnumerable<LearningPathDto> ToLearningPathDtoEnumerable(this IEnumerable<LearningPath> learningPaths) =>
        learningPaths.Select(learningPath => learningPath.ToLearningPathDto());

    public static LearningPathItemDto ToLearningPathItemDto(this LearningPathItem learningPathItem) =>
        new()
        {
            Id = learningPathItem.Id,
            Curricula = [.. learningPathItem.Curricula.ToLookupDtoEnumerable()],
            Name = learningPathItem.Name,
            Description = learningPathItem.Description,
            Sequence = learningPathItem.Sequence,
            Items = [.. learningPathItem.Items.ToLearningPathItemDtoEnumerable()],
            LessonId = learningPathItem.Lesson?.Id
        };

    public static IEnumerable<LearningPathItemDto> ToLearningPathItemDtoEnumerable(this IEnumerable<LearningPathItem> learningPathItems) =>
        learningPathItems.Select(learningPathItem => learningPathItem.ToLearningPathItemDto());
}
