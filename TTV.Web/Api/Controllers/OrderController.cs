using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController(ILogger<OrderController> logger, IOrderManager orderManager, IUserIdentityService userIdentity) : ControllerBase
{
    private readonly ILogger<OrderController> logger = logger;
    private readonly IOrderManager orderManager = orderManager;
    private readonly IUserIdentityService userIdentity = userIdentity;

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateNewOrderAsync([FromBody] OrderCreateDto createOrderDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@CreateOrderDto})", nameof(CreateNewOrderAsync), createOrderDto);
        var order = await orderManager.CreateNewOrderAsync(createOrderDto.LessonsIds, cancellationToken);
        return CreatedAtAction(nameof(GetOrderAsync).Replace("Async", ""), new { orderId = order.Id }, order.ToOrderDto());
    }

    [HttpGet("own")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOwnListAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetOwnListAsync));
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        var orders = await orderManager.GetPlacedByUserListAsync(userId, cancellationToken);
        return Ok(orders.ToEnumerableOrderDto());
    }

    [HttpGet("{orderId}")]
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

    [HttpPut("{orderId}/complete")]
    public async Task<ActionResult<ResultDto<OrderDto>>> CompleteOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(CompleteOrderAsync), orderId);
        var result = await orderManager.CompleteOrderAsync(orderId, cancellationToken);
        var response = new ResultDto<OrderDto>(result.Value.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }

    [HttpDelete("{orderId}")]
    public async Task<ActionResult<ResultDto<OrderDto>>> CancelOrderAsync([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(CancelOrderAsync), orderId);
        var result = await orderManager.CancelOrderAsync(orderId, cancellationToken);
        var response = new ResultDto<OrderDto>(result.Value.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }
}
