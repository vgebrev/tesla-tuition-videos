using TTV.Web.Shared;

namespace TTV.Web.Blazor.Pages.ShoppintCart.Store.Actions;

public record LocalStorePersistRequest
{
    public LessonDto[] Lessons { get; init; } = [];
}

public record LocalStorePersistResponse { }

public record LocalStorePersistError
{
    public string ErrorMessage { get; init; } = string.Empty;
}

public record LocalStoreLoadRequest { }

public record LocalStoreLoadResponse
{
    public LessonDto[] Lessons { get; init; } = [];
}

public record LocalStoreLoadError
{
    public string ErrorMessage { get; init; } = string.Empty;
}

