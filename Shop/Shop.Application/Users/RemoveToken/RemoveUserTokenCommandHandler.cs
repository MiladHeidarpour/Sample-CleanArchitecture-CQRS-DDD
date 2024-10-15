using Common.Application;
using Shop.Domain.UserAgg.Repositories;

namespace Shop.Application.Users.RemoveToken;

internal class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand, string>
{
    private readonly IUserRepository _repository;

    public RemoveUserTokenCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<string>> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult<string>.NotFound();
        }
        var token = user.RemoveToken(request.TokenId);
        await _repository.Save();
        return OperationResult<string>.Success(token);
    }
}
