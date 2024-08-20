using Common.Application;
using Common.Domain.Exceptions;

namespace Shop.Application.Users.RemoveToken;

public record RemoveUserTokenCommand(long UserId, long TokenId) : IBaseCommand;
