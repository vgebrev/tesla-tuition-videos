namespace TTV.Web.Blazor;

public record LocalConfig
{
    public string Authority { get; init; } = string.Empty;
    public string ApiRootUri { get; init; } = string.Empty;
    public string ClientId { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = string.Empty;
    public string PostLogoutRedirectUri { get; init; } = string.Empty;
    public string ResponseType { get; init; } = string.Empty;
    public string[] ApiScopes { get; init; } = [];
    public BankAccountConfig BankAccount { get; init; } = new();
    public ContactInfoConfig ContactInfo { get; init; } = new();
    public bool IsTestEnvironment { get; init; } = false;
}

public record BankAccountConfig
{
    public string AccountNumber { get; init; } = string.Empty;
    public string BankName { get; init; } = string.Empty;
    public string BranchCode { get; init; } = string.Empty;
    public string? Branch { get; init; }
}

public record ContactInfoConfig
{
    public string PhoneNumber { get; init; } = string.Empty;
    public string SupportEmail { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;

    public string WhatsAppLink => $"https://wa.me/{PhoneNumber.Replace(" ", "").Replace("+", "")}";
    public string TelLink => $"tel:{PhoneNumber.Replace(" ", "")}";
    public string MailtoLink => $"mailto:{SupportEmail}";
}