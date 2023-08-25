using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Managers;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> logger;
    private readonly IOrderManager orderManager;

    public OrderController(ILogger<OrderController> logger, IOrderManager orderManager)
    {
        this.logger = logger;
        this.orderManager = orderManager;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<OrderDto>> CreateNewOrderAsync([FromBody] OrderCreateDto createOrderDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@CreateOrderDto})", nameof(CreateNewOrderAsync), createOrderDto);
        var order = await orderManager.CreateNewOrderAsync(createOrderDto.LessonsIds, cancellationToken);
        return CreatedAtAction(nameof(GetOrderAsync).Replace("Async", ""), new { orderId = order.Id }, order.ToOrderDto());
    }

    [HttpGet("{orderId}")]
    [Authorize]
    public async Task<ActionResult<OrderDto>> GetOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(GetOrderAsync), orderId);
        var order = await orderManager.GetOrderAsync(orderId, cancellationToken);
        if (order == null)
        {
            return NotFound(null);
        }
        return Ok(order.ToOrderDto());
    }
}
