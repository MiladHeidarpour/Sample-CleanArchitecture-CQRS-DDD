namespace Shop.Domain.SellerAgg.Services;

public interface ISellerDomainService
{
    bool IsValidSellerInformation(Seller seller);
    bool NationalCodeIsExist(string nationalCode);
}
