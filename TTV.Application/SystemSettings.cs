namespace TTV.Application;

public record SystemSettings
{
    public string VideosPath { get; init; } = string.Empty;
    public string DocumentsPath { get; init; } = string.Empty;
    public string DiscountVoucherPepper { get; init; } = string.Empty;
    public bool IssueDiscountVoucherOnFirstOrder { get; init; }
    public EmailSettings EmailSettings { get; init; } = new();
    public PayfastSettings PayfastSettings { get; init; } = new();
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

public record PayfastSettings
{
    public string MerchantId { get; init; } = string.Empty;
    public string MerchantKey { get; init; } = string.Empty;
    public string? Passphrase { get; init; }
    public string PayfastUrl { get; init; } = string.Empty;
    public string PayfastValidationUrl { get; init; } = string.Empty;
    public string ReturnUrl { get; init; } = string.Empty;
    public string CancelUrl { get; init; } = string.Empty;
    public string NotifyUrl { get; init; } = string.Empty;
    public string[] AllowedHosts { get; init; } = [];
}