using Dapper;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg;

internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
{
    private readonly DapperContext _dapperContext;
    public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }
    //public async Task<InventoryResult?> GetInventoryById(long id)
    //{
    //    return await Context.Inventories.Where(r => r.Id==id)
    //        .Select(i=>new InventoryResult()
    //        {
    //            Count = i.Count,
    //            Id = i.Id,
    //            Price = i.Price,
    //            ProductId = i.ProductId,
    //            SellerId = i.SellerId
    //        }).FirstOrDefaultAsync();
    //}
    public async Task<InventoryResult?> GetInventoryById(long id)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $"SELECt * from {_dapperContext.Inventories} where Id=@InventoryId";
        return await connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { InventoryId = id });
    }
}