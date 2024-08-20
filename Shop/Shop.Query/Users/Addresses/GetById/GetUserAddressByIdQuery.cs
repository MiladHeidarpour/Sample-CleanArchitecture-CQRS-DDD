
using Common.Query;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs.Addresses;

namespace Shop.Query.Users.Addresses.GetById;

public record GetUserAddressByIdQuery(long AddressId) : IQuery<AddressDto?>;
