using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/discount-voucher")]
[ApiController]
public class DiscountVoucherController(ILogger<DiscountVoucherController> logger, IDiscountVoucherManager discountVoucherManager, IUserIdentityService userIdentity) : ControllerBase
{
    private readonly ILogger<DiscountVoucherController> logger = logger;
    private readonly IDiscountVoucherManager discountVoucherManager = discountVoucherManager;
    private readonly IUserIdentityService userIdentity = userIdentity;

    [HttpPost]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<DiscountVoucherDto> Issue([FromBody] DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@DiscountVoucherIssueDto})", nameof(Issue), dto);
        var voucher = await discountVoucherManager.IssueVoucherAsync(dto.Amount, dto.ExpirationDate, dto.Note, cancellationToken);
        return voucher.ToDiscountVoucherDto();
    }

    [HttpGet]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<IEnumerable<DiscountVoucherDto>> GetList(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetList));
        var vouchers = await discountVoucherManager.GetListAsync(cancellationToken);
        return vouchers.ToEnumerableDiscountVoucherDto();
    }

    [HttpGet("own")]
    [Authorize]
    public async Task<IEnumerable<DiscountVoucherDto>> GetOwnList(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetOwnList));
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        var vouchers = await discountVoucherManager.GetClaimedByUserListAsync(userId, cancellationToken);
        return vouchers.ToEnumerableDiscountVoucherDto();
    }

    [HttpPut("{voucherCode}/order/{orderId}")]
    [Authorize]
    public async Task<ActionResult<ResultDto<OrderDto?>>> ApplyDiscountVoucher([FromRoute] string voucherCode, [FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({VoucherCode}, {OrderId})", nameof(ApplyDiscountVoucher), voucherCode, orderId);
        var result = await discountVoucherManager.ApplyDiscountVoucherAsync(voucherCode, orderId, cancellationToken);
        var response = new ResultDto<OrderDto?>(result.Value?.Order.ToOrderDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }
}
