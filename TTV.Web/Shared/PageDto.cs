namespace TTV.Web.Shared;
public record PageDto<TItems>(IEnumerable<TItems> Items, PageInfoDto? PageInfo = null);
