namespace TTV.Application;

public record SystemSettings
{
    public string VideosPath { get; init; } = string.Empty;
    public string DiscountVoucherPepper { get; init; } = string.Empty;
    public bool IssueDiscountVoucherOnFirstOrder { get; init; }
    public EmailSettings EmailSettings { get; init; } = new();
}

public record EmailSettings
{
    public string SmtpHost { get; init; } = string.Empty;
    public int? SmtpPort { get; init; }
    public bool? EnableSsl { get; init; }
    public string? SmtpUsername { get; init; }
    public string? SmtpPassword { get; init; }
    public string DefaultFrom { get; init; } = string.Empty;
    public string? DefaultTo { get; init; }
}