namespace TTV.Application.Exceptions;
public class OrderNotFoundException : ApplicationException
{
    public OrderNotFoundException(int orderId)
        : base("Order not found")
    {
        OrderId = orderId;
    }

    public int OrderId { get; }
}
