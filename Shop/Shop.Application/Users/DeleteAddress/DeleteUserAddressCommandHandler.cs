using Common.Application;
using Shop.Domain.UserAgg.Repositories;
using Shop.Domain.UserAgg.Services;

namespace Shop.Application.Users.DeleteAddress;

internal class DeleteUserAddressCommandHandler : IBaseCommandHandler<DeleteUserAddressCommand>
{
    private readonly IUserRepository _repository;
    private readonly IUserDomainService _domainService;

    public DeleteUserAddressCommandHandler(IUserRepository repository, IUserDomainService domainService)
    {
        _repository = repository;
        _domainService = domainService;
    }
    public async Task<OperationResult> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        user.DeleteAddress(request.AddressId);
        await _repository.Save();
        return OperationResult.Success();
    }
}
