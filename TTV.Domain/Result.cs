namespace TTV.Domain;

public record Result(bool IsSuccess, string? ResultReason = null);


public record Result<TValue> (TValue Value, bool IsSuccess, string? ResultReason) 
    : Result(IsSuccess, ResultReason);
