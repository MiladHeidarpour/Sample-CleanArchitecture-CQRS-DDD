using Common.Application;

namespace Shop.Application.Sellers.Create;

public class CreateSellerCommand : IBaseCommand
{
    public long UserId { get; set; }
    public string ShopName { get; set; }
    public string NationalCode { get; set; }
    public CreateSellerCommand(long userId, string shopName, string nationalCode)
    {
        UserId = userId;
        ShopName = shopName;
        NationalCode = nationalCode;
    }
}
