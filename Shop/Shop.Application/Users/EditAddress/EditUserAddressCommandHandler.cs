using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repositories;

namespace Shop.Application.Users.EditAddress;

public class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public EditUserAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound();
        }
        var userAddress = new UserAddress(request.Shire, request.City, request.NationalCode, request.PostalAddress, request.PhoneNumber, request.Name, request.Family, request.NationalCode);
        user.EditAddress(userAddress,request.Id);
        await _repository.Save();
        return OperationResult.Success();
    }
}
