using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Infrastructure.Videos;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/order")]
[ApiController]
[Authorize]
public class OrderController(ILogger<OrderController> logger, IOrderManager orderManager, IUserIdentityService userIdentity, IVideoPathCache videoPathCache) : ControllerBase
{
    private readonly ILogger<OrderController> logger = logger;
    private readonly IOrderManager orderManager = orderManager;
    private readonly IUserIdentityService userIdentity = userIdentity;
    private readonly IVideoPathCache videoPathCache = videoPathCache;

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateNewOrder([FromBody] OrderCreateDto createOrderDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@CreateOrderDto})", nameof(CreateNewOrder), createOrderDto);
        var order = await orderManager.CreateNewOrderAsync(createOrderDto.LessonsIds, cancellationToken);
        return CreatedAtAction(nameof(GetOrder).Replace("Async", ""), new { orderId = order.Id }, order.ToOrderDto());
    }

    [HttpGet("own")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOwnList(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetOwnList));
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        var orders = await orderManager.GetPlacedByUserListAsync(userId, cancellationToken);
        return Ok(orders.ToEnumerableOrderDto());
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> GetOrder([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(GetOrder), orderId);
        var order = await orderManager.GetOrderAsync(orderId, cancellationToken);
        if (order == null)
        {
            return NotFound(null);
        }
        return Ok(order.ToOrderDto());
    }

    [HttpPut("{orderId}/complete")]
    public async Task<ActionResult<ResultDto<OrderDto>>> CompleteOrder([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(CompleteOrder), orderId);
        var result = await orderManager.CompleteOrderAsync(orderId, cancellationToken);
        var response = new ResultDto<OrderDto>(result.Value.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        foreach (var lesson in response.Value.Lessons)
        {
            videoPathCache.TryRemove(lesson.Id, userIdentity.Email);
        }
        return Ok(response);
    }

    [HttpDelete("{orderId}")]
    public async Task<ActionResult<ResultDto<OrderDto>>> CancelOrder([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(CancelOrder), orderId);
        var result = await orderManager.CancelOrderAsync(orderId, cancellationToken);
        var response = new ResultDto<OrderDto>(result.Value.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }
}
