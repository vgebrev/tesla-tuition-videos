namespace TTV.Domain.DomainServices;

public interface IUnitOfWorkFactory
{
    public Task<IUnitOfWork> CreateAsync();
}
