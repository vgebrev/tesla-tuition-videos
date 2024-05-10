namespace TTV.Application.Exceptions;
public class OrderNotFoundException(int orderId) : ApplicationException("Order not found")
{
    public int OrderId { get; } = orderId;
}
