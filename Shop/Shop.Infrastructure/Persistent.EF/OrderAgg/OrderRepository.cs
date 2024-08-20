using Shop.Infrastructure._Utilities;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Infrastructure.Persistent.EF.OrderAgg;

internal class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ShopContext context) : base(context)
    {
    }

    public Task<Order> GetCurrentUserOrder(long userId)
    {
        throw new NotImplementedException();
    }
}
