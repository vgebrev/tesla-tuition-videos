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
public class DiscountVoucherController : ControllerBase
{
    private readonly ILogger<DiscountVoucherController> logger;
    private readonly IDiscountVoucherManager discountVoucherManager;
    private readonly IUserIdentityService userIdentity;

    public DiscountVoucherController(ILogger<DiscountVoucherController> logger, IDiscountVoucherManager discountVoucherManager, IUserIdentityService userIdentity)
    {
        this.logger = logger;
        this.discountVoucherManager = discountVoucherManager;
        this.userIdentity = userIdentity;
    }

    [HttpPost]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<DiscountVoucherDto> IssueAsync([FromBody] DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@DiscountVoucherIssueDto})", nameof(IssueAsync), dto);
        var voucher = await discountVoucherManager.IssueVoucherAsync(dto.Amount, dto.ExpirationDate, dto.Note, cancellationToken);
        return voucher.ToDiscountVoucherDto();
    }

    [HttpGet]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<IEnumerable<DiscountVoucherDto>> GetListAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetListAsync));
        var vouchers = await discountVoucherManager.GetListAsync(cancellationToken);
        return vouchers.ToEnumerableDiscountVoucherDto();
    }

    [HttpGet("own")]
    [Authorize]
    public async Task<IEnumerable<DiscountVoucherDto>> GetOwnListAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetListAsync));
        var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
        var vouchers = await discountVoucherManager.GetClaimedByUserListAsync(userId, cancellationToken);
        return vouchers.ToEnumerableDiscountVoucherDto();
    }

    [HttpPut("{voucherCode}/order/{orderId}")]
    [Authorize]
    public async Task<ActionResult<ResultDto<AppliedDiscountDto?>>> ApplyDiscountVoucher([FromRoute] string voucherCode, [FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({VoucherCode}, {OrderId})", nameof(ApplyDiscountVoucher), voucherCode, orderId);
        var result = await discountVoucherManager.ApplyDiscountVoucherAsync(voucherCode, orderId, cancellationToken);
        var response = new ResultDto<AppliedDiscountDto?>(result.Value.ToAppliedDiscountDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }
}
