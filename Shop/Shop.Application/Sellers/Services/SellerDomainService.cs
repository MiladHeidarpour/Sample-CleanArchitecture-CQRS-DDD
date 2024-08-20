using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repositories;
using Shop.Domain.SellerAgg.Services;

namespace Shop.Application.Sellers.Services;

public class SellerDomainService : ISellerDomainService
{
    private readonly ISellerRepository _repository;

    public SellerDomainService(ISellerRepository repository)
    {
        _repository = repository;
    }

    public bool IsValidSellerInformation(Seller seller)
    {
        var sellerExist = _repository.Exists(s => s.NationalCode == seller.NationalCode || s.UserId == seller.UserId);
        return !sellerExist;
        //if (sellerExist == false)
        //{
        //    return true;
        //}
        //return false;
    }

    public bool NationalCodeIsExist(string nationalCode)
    {
        return _repository.Exists(s => s.NationalCode == nationalCode);
    }
}
