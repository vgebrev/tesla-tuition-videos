using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.DataServices;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> logger;
    private readonly IOrderDataService orderDataService;

    public OrderController(ILogger<OrderController> logger, IOrderDataService orderDataService)
    {
        this.logger = logger;
        this.orderDataService = orderDataService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<OrderDto>> CreateNewOrderAsync([FromBody] OrderCreateDto createOrderDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@CreateOrderDto})", nameof(CreateNewOrderAsync), createOrderDto);
        var order = await orderDataService.CreateNewOrderAsync(createOrderDto.LessonsIds, cancellationToken);
        return CreatedAtAction(nameof(GetOrderAsync).Replace("Async", ""), new { orderId = order.Id }, order.ToOrderDto());
    }

    [HttpGet("{orderId}")]
    [Authorize]
    public async Task<ActionResult<OrderDto>> GetOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(GetOrderAsync), orderId);
        var order = await orderDataService.GetOrderAsync(orderId, cancellationToken);
        if (order == null)
        {
            return NotFound(null);
        }
        return Ok(order.ToOrderDto());
    }
}
