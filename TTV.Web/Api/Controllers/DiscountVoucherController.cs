using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTV.Application.DataServices;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;

[Route("api/discount-voucher")]
[ApiController]
public class DiscountVoucherController : ControllerBase
{
    private readonly ILogger<DiscountVoucherController> logger;
    private readonly IDiscountVoucherDataService dataService;

    public DiscountVoucherController(ILogger<DiscountVoucherController> logger, IDiscountVoucherDataService dataService)
    {
        this.logger = logger;
        this.dataService = dataService;
    }

    [HttpPost]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<DiscountVoucherDto> IssueAsync([FromBody] DiscountVoucherIssueDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@DiscountVoucherIssueDto})", nameof(IssueAsync), dto);
        var voucher = await dataService.IssueVoucherAsync(dto.Amount, dto.ExpirationDate, dto.Note, cancellationToken);
        return voucher.ToDiscountVoucherDto();
    }

    [HttpGet]
    [Authorize(Policy = "CanIssueVouchers")]
    public async Task<IEnumerable<DiscountVoucherDto>> GetListAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}", nameof(GetListAsync));
        var vouchers = await dataService.GetListAsync(cancellationToken);
        return vouchers.ToEnumerableDiscountVoucherDto();
    }

    [HttpPut("{voucherCode}/order/{orderId}")]
    [Authorize]
    public async Task<ActionResult<ResultDto<AppliedDiscountDto?>>> ApplyDiscountVoucher([FromRoute] string voucherCode, [FromRoute] int orderId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({VoucherCode}, {OrderId})", nameof(ApplyDiscountVoucher), voucherCode, orderId);
        var result = await dataService.ApplyDiscountVoucherAsync(voucherCode, orderId, cancellationToken);
        var response = new ResultDto<AppliedDiscountDto?>(result.Value.ToAppliedDiscountDto(), result.IsSuccess, result.Message);
        if (!result.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }
}
