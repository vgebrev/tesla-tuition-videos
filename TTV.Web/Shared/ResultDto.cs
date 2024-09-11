namespace TTV.Web.Shared;

public record ResultDto(bool IsSuccess, string? Message = null);


public record ResultDto<TValue>(TValue Value, bool IsSuccess, string? Message, PageInfoDto? PageInfo = null)
    : ResultDto(IsSuccess, Message);

