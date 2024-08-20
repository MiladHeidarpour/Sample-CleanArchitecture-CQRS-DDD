using Common.Application;
using MediatR;
using Shop.Application.Users.AddAddress;
using Shop.Application.Users.DeleteAddress;
using Shop.Application.Users.EditAddress;
using Shop.Domain.UserAgg;
using Shop.Query.Users.Addresses.GetById;
using Shop.Query.Users.Addresses.GetList;
using Shop.Query.Users.DTOs.Addresses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Shop.Presentation.Facade.Users.Addresses;

internal class UserAddressFacade : IUserAddressFacade
{
    private readonly IMediator _mediator;

    public UserAddressFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> AddAddress(AddUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteAddress(DeleteUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditAddress(EditUserAddressCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<AddressDto> GetById(long UserAddressId)
    {
        return await _mediator.Send(new GetUserAddressByIdQuery(UserAddressId));
    }

    public async Task<List<AddressDto>> GetList(long userId)
    {
        return await _mediator.Send(new GetUserAddressesListQuery(userId));
    }
}