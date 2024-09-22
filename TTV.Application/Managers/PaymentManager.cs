using Microsoft.Extensions.Logging;
using TTV.Application.Exceptions;
using TTV.Application.Payments;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.Managers;
public class PaymentManager(ILogger<PaymentManager> logger, IUserIdentityService userIdentity, IUnitOfWorkFactory unitOfWorkFactory, IPaymentProcessorFactory paymentProcessorFactory) : IPaymentManager
{
    private readonly ILogger<PaymentManager> logger = logger;
    private readonly IUserIdentityService userIdentity = userIdentity;
    private readonly IUnitOfWorkFactory unitOfWorkFactory = unitOfWorkFactory;
    private readonly IPaymentProcessorFactory paymentProcessorFactory = paymentProcessorFactory;

    public async Task<Result> ConfirmPaymentAsync(Guid paymentId, PaymentType paymentType, Dictionary<string, string> confirmationData, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        try
        {
            var payment = await unitOfWork.PaymentRepository.GetByIdAsync(paymentId, cancellationToken) ?? throw new PaymentNotFoundException(paymentId);

            var paymentProcessor = paymentProcessorFactory.CreatePaymentProcessor(paymentType);
            var result = await paymentProcessor.ConfirmAsync(payment, confirmationData, cancellationToken);

            await unitOfWork.EndAsync(cancellationToken);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error with payment confirmation");
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }

    }

    public async Task<Result<Payment>> InitiateOrderPaymentAsync(int orderId, PaymentType paymentType, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        try
        {
            Order order;

            if (userIdentity.IsAdmin)
            {
                order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, cancellationToken) ?? throw new OrderNotFoundException(orderId);
            }
            else 
            {
                var userId = userIdentity.UserId ?? throw new UnauthenticatedException();
                order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userId, cancellationToken) ?? throw new OrderNotFoundException(orderId);
            }

            var paymentProcessor = paymentProcessorFactory.CreatePaymentProcessor(paymentType);
            var result = await paymentProcessor.InitiateAsync(order, cancellationToken);
            if (result.IsSuccess)
            {
                unitOfWork.PaymentRepository.Add(result.Value);
                await unitOfWork.EndAsync(cancellationToken);
                return result;
            }
            else
            {
                throw new PaymentInitiateException(orderId, paymentType, result.Message);
            }
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }
}
