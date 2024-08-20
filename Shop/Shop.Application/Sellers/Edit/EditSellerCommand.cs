using Common.Application;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Enums;

namespace Shop.Application.Sellers.Edit;

public record EditSellerCommand(long Id,string ShopName,string NationalCode,SellerStatus Status):IBaseCommand;
