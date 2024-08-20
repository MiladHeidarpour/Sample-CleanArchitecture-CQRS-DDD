
using Common.Query;
using Shop.Query.Users.DTOs.UserTokens;

namespace Shop.Query.Users.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;
