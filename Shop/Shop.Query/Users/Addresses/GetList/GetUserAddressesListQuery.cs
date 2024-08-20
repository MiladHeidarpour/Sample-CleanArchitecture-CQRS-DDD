
using Common.Query;
using Shop.Query.Users.DTOs.Addresses;

namespace Shop.Query.Users.Addresses.GetList;

public record GetUserAddressesListQuery(long UserId) : IQuery<List<AddressDto>>;
