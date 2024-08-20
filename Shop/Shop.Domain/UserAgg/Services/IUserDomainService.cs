namespace Shop.Domain.UserAgg.Services;

public interface IUserDomainService
{
    bool IsEmailExist(string email);
    bool IsPhoneNumberExist(string phoneNumber);
}
