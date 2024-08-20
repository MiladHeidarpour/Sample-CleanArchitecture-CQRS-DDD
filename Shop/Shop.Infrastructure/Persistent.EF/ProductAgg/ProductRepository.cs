using Shop.Domain.ProuductAgg;
using Shop.Domain.ProuductAgg.Repositories;
using Shop.Infrastructure._Utilities;

namespace Shop.Infrastructure.Persistent.EF.ProductAgg;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ShopContext context) : base(context)
    {
    }
}
