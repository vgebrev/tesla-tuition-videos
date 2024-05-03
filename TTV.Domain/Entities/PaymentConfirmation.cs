namespace TTV.Domain.Entities;
public class PaymentConfirmation : BaseEntity
{
    public DateTime? ReceivedOn { get; set; }
    public bool IsSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public Guid PaymentId { get; private set; }
    public Payment Payment { get; set; } = new();
}

public class PayfastPaymentConfirmation : PaymentConfirmation
{
    public string Payload { get; set; } = string.Empty;

    public int PayfastPaymentId { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;

    public decimal AmountGross { get; set; }
    public decimal AmountFee { get; set; }
    public decimal AmountNet { get; set; }
    public string Signature { get; set; } = string.Empty;
}
