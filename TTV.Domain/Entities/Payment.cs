using TTV.Domain.DomainServices;

namespace TTV.Domain.Entities;
public class Payment : BaseEntity<Guid>
{
    public string? ExternalIdentifier { get; set; }
    public PaymentType Type { get; set; }
    public PaymentStatus Status { get; set; }
    public string? StatusReason { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? FinalisedOn { get; set; }
    public User CreatedBy { get; set; } = new();
    public User? ProcessedBy { get; set; }
    public Order Order { get; set; } = new();
    public PaymentConfirmation? PaymentConfirmation { get; set; }

    public Result Cancel(string reason)
    {
        if (Status != PaymentStatus.Pending)
        {
            return new Result(false, $"Payments with status {Status.ToDisplayString()} cannot be cancelled.");
        }

        Status = PaymentStatus.Cancelled;
        StatusReason = reason;
        return new Result(true);
    }

    public Result Confirm(PayfastPaymentConfirmation confirmation)
    {
        if (confirmation.IsSuccessful)
        {
            Status = PaymentStatus.Paid;
            StatusReason = "Successful confirmation received";
        }
        else
        {
            Status = PaymentStatus.Failed;
            StatusReason = confirmation.FailureReason;
        }

        FinalisedOn = DateTime.Now;
        PaymentConfirmation = confirmation;

        return new Result(confirmation.IsSuccessful, StatusReason);
    }
}
