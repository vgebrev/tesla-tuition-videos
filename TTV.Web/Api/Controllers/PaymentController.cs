using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using TTV.Application.Managers;
using TTV.Domain.Entities;
using TTV.Web.Api.MappingExtensions;
using TTV.Web.Shared;

namespace TTV.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController(ILogger<PaymentController> logger, IPaymentManager paymentManager) : ControllerBase
{
    private readonly ILogger<PaymentController> logger = logger;
    private readonly IPaymentManager paymentManager = paymentManager;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ResultDto<PaymentDto?>>> InitiateOrderPaymentAsync([FromBody] PaymentInitiateDto initiatePaymentDto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@PaymentInitiateDto})", nameof(InitiateOrderPaymentAsync), initiatePaymentDto);
        var result = await paymentManager.InitiateOrderPaymentAsync(initiatePaymentDto.OrderId, (PaymentType)(int)initiatePaymentDto.PaymentMethod, cancellationToken);
        var response = new ResultDto<PaymentDto?>(result.Value.ToPaymentDto(), result.IsSuccess, result.Message);
        if (!response.IsSuccess)
        {
            return UnprocessableEntity(response);
        }
        return Ok(response);
    }

    [HttpPost("confirm-payfast")]
    [AllowAnonymous]
    public async Task<ActionResult> ConfirmPaymentPayfastAsync([FromForm] Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("{MethodName}({@Data})", nameof(ConfirmPaymentPayfastAsync), confirmationData);
        try
        {
            var paymentId = Guid.Parse(confirmationData["m_payment_id"]);
            confirmationData.Add("pf_host", new Uri(Request.Headers[HeaderNames.Referer].ToString()).Host);
            await paymentManager.ConfirmPaymentAsync(paymentId, PaymentType.Payfast, confirmationData, cancellationToken);
        }
        catch (Exception ex)
        {
            // The only source of errors at this point is a malicious actor or a bug on our side.
            // Nothing Payfast can do in either case, so we log the error and return Ok() to stop Payfast from retrying the request.
            logger.LogError(ex, "Could not confirm Payfast payment");
        }
        return Ok();
    }
}
