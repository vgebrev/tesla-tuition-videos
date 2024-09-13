using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application;
using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;
using TTV.Domain.Filters;
using TTV.Infrastructure.Videos;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("orders")]
[ApiController]
[Authorize]
public class OrdersController(ILogger<OrdersController> logger, IAdminOrderComplete adminOrderComplete, IOrderManager orderManager, IUserIdentityService userIdentity, IVideoPathCache videoPathCache) : ControllerBase
{
    private readonly ILogger<OrdersController> logger = logger;
    private readonly IAdminOrderComplete adminOrderComplete = adminOrderComplete;
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

    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetList([FromQuery]OrderListFilter filter, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@OrderListFilter})", nameof(GetList), filter);
        var orders = await orderManager.GetListAsync(filter, cancellationToken);
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

    [HttpPut("{orderId}/complete-admin")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<ResultDto<OrderDto>>> CompleteOrderAdmin([FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({OrderId})", nameof(CompleteOrderAdmin), orderId);
        var result = await adminOrderComplete.CompleteOrderAsync(orderId, cancellationToken);
        var response = new ResultDto<OrderDto>(result.Value.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        foreach (var lesson in response.Value.Lessons)
        {
            videoPathCache.TryRemove(lesson.Id, response.Value.PlacedBy.Email);
        }
        return Ok(response);
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

    [HttpGetAttribute("statuses")]
    public ActionResult<IEnumerable<LookupDto>> GetStatuses()
    {
        logger.LogInformation("{MethodName}", nameof(GetStatuses));
        var statuses = Enum.GetValues<OrderStatus>().Select(EnumMappings.ToLookupDto);
        return Ok(statuses.ToArray());
    }
}
