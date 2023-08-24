using TTV.Application.Exceptions;
using TTV.Domain;
using TTV.Domain.DomainServices;
using TTV.Domain.Entities;

namespace TTV.Application.DataServices;

public class DiscountVoucherDataService : IDiscountVoucherDataService
{
    private readonly IDiscountVoucherCodeGenerator codeGenerator;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;
    private readonly IUserIdentityService userIdentity;

    public DiscountVoucherDataService(IDiscountVoucherCodeGenerator codeGenerator, IUnitOfWorkFactory unitOfWorkFactory, IUserIdentityService userIdentity)
    {
        this.codeGenerator = codeGenerator;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.userIdentity = userIdentity;
    }

    public async Task<DiscountVoucher> IssueVoucherAsync(decimal amount, DateOnly? expirationDate = null, string? note = null, CancellationToken cancellationToken = default)
    {
        var issuerUserId = userIdentity.UserId ?? throw new UnauthenticatedException();
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        var issuedBy = await unitOfWork.UserRepository.GetByIdAsync(issuerUserId, cancellationToken) ?? throw new UserDoesntExistException(issuerUserId);
        var voucher = new DiscountVoucher
        {
            Amount = amount,
            ExpirationDate = expirationDate,
            Note = note,
            IssuedBy = issuedBy,
            IssuedAt = DateTime.Now
        };
        voucher.Code = codeGenerator.Generate(voucher);
        unitOfWork.DiscountVoucherRepository.Add(voucher);
        await unitOfWork.EndAsync(cancellationToken);
        return voucher;
    }

    public async Task<IEnumerable<DiscountVoucher>> GetListAsync(CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        return await unitOfWork.DiscountVoucherRepository.GetListAsync(cancellationToken);
    }

    public async Task<Result<OrderDiscountVoucher?>> ApplyDiscountVoucherAsync(string voucherCode, int orderId, CancellationToken cancellationToken = default)
    {
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        try
        {
            var userId = userIdentity.UserId ?? throw new UnauthenticatedException();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId, cancellationToken) ?? throw new UserDoesntExistException(userId);
            var order = await unitOfWork.OrderRepository.GetByIdAsync(orderId, userId, cancellationToken) ?? throw new ApplyVoucherException("Order not found");
            var voucher = await unitOfWork.DiscountVoucherRepository.GetByCodeAsync(voucherCode, cancellationToken) ?? throw new ApplyVoucherException("Invalid voucher code");

            var result = order.ApplyDiscountVoucher(voucher);
            if (result.IsSuccess)
            {
                await unitOfWork.EndAsync(cancellationToken);
            }
            else
            {
                await unitOfWork.CancelAsync(cancellationToken);
            }
            return result;
        }
        catch (ApplicationException ex) when (ex is ApplyVoucherException
                                              || ex is UnauthenticatedException
                                              || ex is UserDoesntExistException)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            return new Result<OrderDiscountVoucher?>(null, false, ex.Message);
        }
        catch (Exception)
        {
            await unitOfWork.CancelAsync(cancellationToken);
            throw;
        }
    }
}
