using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
namespace Shop.Domain.UserAgg;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Family { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string AvatarName { get; private set; }
    public bool IsActive { get; private set; }
    public Gender Gender { get; private set; }
    public List<UserRole> Roles { get; }
    public List<Wallet> Wallets { get; }
    public List<UserAddress> Addresses { get; }
    public List<UserToken> Tokens { get; }

    public User(string name, string family, string phoneNumber, string email, string password, Gender gender, IUserDomainService domainService)
    {
        Gaurd(phoneNumber, email, domainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Password = password;
        Gender = gender;
        AvatarName = "avatar.png";
        IsActive = true;
        Roles = new();
        Wallets = new();
        Addresses = new();
        Tokens = new();
    }

    private User()
    {

    }
    public void Edit(string name, string family, string phoneNumber, string email, Gender gender, IUserDomainService domainService)
    {
        Gaurd(phoneNumber, email, domainService);
        Name = name;
        Family = family;
        PhoneNumber = phoneNumber;
        Email = email;
        Gender = gender;
    }

    public void ChangePassword(string newPassword)
    {
        NullOrEmptyDomainDataException.CheckString(newPassword, nameof(newPassword));
        Password = newPassword;
    }

    public void SetAvatar(string imageName)
    {
        if (string.IsNullOrWhiteSpace(imageName))
        {
            imageName = "avatar.png";
        }
        AvatarName = imageName;
    }
    public static User RegisterUser(string phoneNumber, string password, IUserDomainService domainService)
    {
        return new User("", "", phoneNumber, null, password, Gender.None, domainService);
    }

    // Addresses

    public void AddAddress(UserAddress address)
    {
        address.UserId = Id;
        Addresses.Add(address);
    }
    public void EditAddress(UserAddress address, long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("AddressNotFound");
        }
        oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress, address.PhoneNumber, address.Name, address.Family, address.NationalCode);
    }
    public void DeleteAddress(long addressId)
    {
        var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
        if (oldAddress == null)
        {
            throw new NullOrEmptyDomainDataException("AddressNotFound");
        }
        Addresses.Remove(oldAddress);
    }

    // Wallets

    public void ChargeWallet(Wallet wallet)
    {
        wallet.UserId = Id;
        Wallets.Add(wallet);
    }

    // UserRoles

    public void SetRoles(List<UserRole> roles)
    {
        roles.ForEach(f => f.UserId = Id);
        Roles.Clear();
        Roles.AddRange(roles);
    }

    //UserToken
    public void AddToken(string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
    {
        var activetokenCount = Tokens.Count(c => c.RefreshTokenExpireDate > DateTime.Now);
        if (activetokenCount == 3)
        {
            throw new InvalidDomainDataException("امکان استفاده همزمان از 4 دستگاه وجود ندارد");
        }
        var token = new UserToken(hashJwtToken, hashRefreshToken, tokenExpireDate, refreshTokenExpireDate, device);
        token.UserId = Id;
        Tokens.Add(token);
    }

    public void RemoveToken(long tokenId)
    {
        var token = Tokens.FirstOrDefault(c => c.Id == tokenId);
        if (token == null)
        {
            new InvalidDomainDataException("Invalid TokenId");
        }
        Tokens.Remove(token);
    }

    //Gaurd
    public void Gaurd(string phoneNumber, string email, IUserDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));
        if (phoneNumber.Length != 11)//length<11 || length>11
        {
            throw new InvalidDomainDataException("شماره موبایل نامعتبر است");
        }

        if (!string.IsNullOrWhiteSpace(email))
            if (email.IsValidEmail() == false)
            {
                throw new InvalidDomainDataException("ایمیل نامعتبر است");
            }

        if (phoneNumber != PhoneNumber)
        {
            if (domainService.IsPhoneNumberExist(phoneNumber) == true)
            {
                throw new InvalidDomainDataException("شماره موبایل تکراری است");
            }
        }

        if (email != Email)
        {
            if (domainService.IsEmailExist(email) == true)
            {
                throw new InvalidDomainDataException("ایمیل تکراری است");
            }
        }
    }
}
