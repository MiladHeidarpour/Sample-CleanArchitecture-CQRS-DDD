using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;

namespace Shop.Query.Orders.Mapper;

public static class OrderMapper
{
    public static OrderDto MapToDto(this Order order)
    {
        return new OrderDto()
        {
            CreationDate = order.CreationDate,
            Id = order.Id,
            Status = order.Status,
            Address = order.Address,
            Discount = order.Discount,
            Items = new(),
            LastUpdate = order.LastUpdate,
            ShippingMethod = order.ShippingMethod,
            UserFullName = "",
            UserId = order.UserId,
        };
    }

    public static async Task<List<OrderItemDto>> GetOrderItems(this OrderDto orderDto, DapperContext _dapperContext)
    {
        var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT s.ShopName , o.OrderId , o.InventoryId , o.Count , o.Price , p.Title as ProductTitle , p.Slug as ProductSlug , p.ImageName as ProductImageName
        FROM {_dapperContext.OrderItems} o  
        Inner Join {_dapperContext.Inventories} i on o.InventoryId=i.Id
        Inner Join{_dapperContext.Products} p on i.ProductId=p.Id;
        Inner Join{_dapperContext.Sellers} s on i.SellerId=s.Id 
        where o.OrderId=@orderId";

        var result = await connection.QueryAsync<OrderItemDto>(sql, new { orderId = orderDto.Id });
        return result.ToList();
    }

    public static OrderFilterData MapFilterData(this Order order, ShopContext conetxt)
    {
        var userFullName = conetxt.Users.Where(r => r.Id == order.UserId).Select(r => $"{r.Name} {r.Family}").First();

        return new OrderFilterData()
        {
            Status = order.Status,
            Id = order.Id,
            CreationDate = order.CreationDate,
            City = order.Address?.City,
            ShippingType = order.ShippingMethod?.ShippingType,
            Shire = order.Address?.Shire,
            TotalItemCount = order.ItemCount,
            TotalPrice = order.TotalPrice,
            UserFullName = userFullName,
            UserId = order.UserId,
        };
    }
}
