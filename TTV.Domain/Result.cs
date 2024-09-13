namespace TTV.Domain;

public record Result(bool IsSuccess, string? Message = null);


public record Result<TValue> (TValue Value, bool IsSuccess, string? Message, PageInfo? PageInfo = null)
    : Result(IsSuccess, Message);
