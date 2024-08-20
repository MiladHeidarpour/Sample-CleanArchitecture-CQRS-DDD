using Common.Query;
using Dapper;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Query.Sellers.DTOs;

namespace Shop.Query.Sellers.Inventories.GetById;

internal class GetSellerInventoryByIdQueryHandler : IQueryHandler<GetSellerInventoryByIdQuery, InventoryDto?>
{
    private readonly DapperContext _dapperContext;

    public GetSellerInventoryByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<InventoryDto?> Handle(GetSellerInventoryByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = @$"SELECT Top(1) i.Id,SellerId,ProductId,Count,Price,i.CreationDate,DiscountPercentage,s.ShopName,p.Title as ProductTitle,p.ImageName as ProductImage
             FROM {_dapperContext.Inventories} i inner Join {_dapperContext.Sellers} s on i.SellerId=s.Id 
             inner Join {_dapperContext.Products} p on i.ProductId=p.Id WHERE i.Id=@id";

        var inventory = await connection.QueryFirstOrDefaultAsync<InventoryDto?>(sql, new { id = request.InventoryId });
        if (inventory == null)
        {
            return null;
        }
        return inventory;
    }
}
