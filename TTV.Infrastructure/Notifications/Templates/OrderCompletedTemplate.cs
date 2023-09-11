namespace TTV.Infrastructure.Notifications.Templates;
public record OrderCompletedTemplate(int Id, string CustomerName, decimal OrderTotal, decimal PaymentsTotal, OrderItem[] Items, OrderItem[] Payments);
