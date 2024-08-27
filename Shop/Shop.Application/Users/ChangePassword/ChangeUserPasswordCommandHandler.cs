using Common.Application;
using Common.Application.SecurityUtil;
using Shop.Domain.UserAgg.Repositories;

namespace Shop.Application.Users.ChangePassword;

internal class ChangeUserPasswordCommandHandler : IBaseCommandHandler<ChangeUserPasswordCommand>
{
    private readonly IUserRepository _repository;

    public ChangeUserPasswordCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
        {
            return OperationResult.NotFound("کاربر یافت نشد");
        }
        var currentPasswordHash = Sha256Hasher.Hash(request.CurrentPassword);
        if (user.Password != currentPasswordHash)
        {
            return OperationResult.Error("کلمه عبور فعلی نامعتبر است");
        }
        var newPasswordHash = Sha256Hasher.Hash(request.Password);
        user.ChangePassword(newPasswordHash);
        await _repository.Save();
        return OperationResult.Success();
    }
}