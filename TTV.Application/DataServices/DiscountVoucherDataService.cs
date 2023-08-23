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
        var issuerUserId = userIdentity.UserId ?? throw new InvalidOperationException("Issuing user id is not set");
        using var unitOfWork = await unitOfWorkFactory.CreateAsync(cancellationToken);
        await unitOfWork.StartAsync(cancellationToken);
        var issuedBy = await unitOfWork.UserRepository.GetByIdAsync(issuerUserId, cancellationToken) ?? throw new InvalidOperationException("Issuing user doesn't exist");
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
}
