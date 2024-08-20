using Shop.Domain.SellerAgg;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Mapper;

public static class SellerMapper
{
    public static SellerDto MapDto(this Seller seller)
    {
        if (seller == null)
        {
            return null;
        }
        return new SellerDto()
        {
            Id = seller.Id,
            CreationDate = seller.CreationDate,
            NationalCode = seller.NationalCode,
            Status = seller.Status,
            ShopName = seller.ShopName,
            UserId = seller.UserId,
        };
    }
}
