using Common.Application;
using Shop.Domain.UserAgg;
using Shop.Domain.UserAgg.Repositories;

namespace Shop.Application.Users.ChargeWallet;

internal class ChargeuserWalletCommandHandler : IBaseCommandHandler<ChargeuserWalletCommand>
{
    private readonly IUserRepository _repository;

    public ChargeuserWalletCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(ChargeuserWalletCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user==null)
        {
            return OperationResult.NotFound();
        }
        var wallet = new Wallet(request.Price, request.Description, request.IsFinally, request.Type);
        user.ChargeWallet(wallet);
        await _repository.Save();
        return OperationResult.Success();
    }
}
