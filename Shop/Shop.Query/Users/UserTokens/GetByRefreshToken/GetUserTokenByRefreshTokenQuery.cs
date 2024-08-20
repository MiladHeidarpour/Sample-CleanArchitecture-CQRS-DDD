using Common.Query;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Users.DTOs.UserTokens;

namespace Shop.Query.Users.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;
