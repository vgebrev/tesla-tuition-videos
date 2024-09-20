using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.Exceptions;
using TTV.Application.Managers;
using TTV.Domain.DomainServices;
using TTV.Domain.Filters;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("discount-vouchers")]
[ApiController]
public class DiscountVouchersController(ILogger<DiscountVouchersController> logger, IDiscountVoucherManager discountVoucherManager, IUserIdentityService userIdentity) : ControllerBase
{
    private readonly ILogger<DiscountVouchersController> logger = logger;
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
    public async Task<PageDto<DiscountVoucherDto>> GetList([FromQuery] PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetList));
        if (pageFilter?.Take is null)
        {
            pageFilter = null;
        }
        var page = await discountVoucherManager.GetListAsync(pageFilter, cancellationToken);
        return new PageDto<DiscountVoucherDto>(
            page.Items.ToEnumerableDiscountVoucherDto(),
            page.PageInfo.ToPageInfoDto()
        );
    }

    [HttpGet("own")]
    [Authorize]
    public async Task<PageDto<DiscountVoucherDto>> GetOwnList([FromQuery] PageFilter? pageFilter = null, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetOwnList));
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        if (pageFilter?.Take is null)
        {
            pageFilter = null;
        }
        var page = await discountVoucherManager.GetClaimedByUserListAsync(userId, pageFilter, cancellationToken);
        return new PageDto<DiscountVoucherDto>(
            page.Items.ToEnumerableDiscountVoucherDto(),
            page.PageInfo.ToPageInfoDto()
        );
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
