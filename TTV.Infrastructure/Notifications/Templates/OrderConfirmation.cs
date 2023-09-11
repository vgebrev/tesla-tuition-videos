namespace TTV.Infrastructure.Notifications.Templates;

public record OrderItem(int Id, string Description, decimal Amount);
public record OrderConfirmation(int Id, string CustomerName, decimal OrderTotal, OrderItem[] Items);
