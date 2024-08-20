
using Common.Query;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetById;

public record GetUserByIdQuery(long UserId) : IQuery<UserDto?>;
