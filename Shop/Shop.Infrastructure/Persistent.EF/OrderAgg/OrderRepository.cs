using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure._Utilities;
using Shop.Domain.OrderAgg;
using Shop.Domain.OrderAgg.Enums;
using Shop.Domain.OrderAgg.Repositories;

namespace Shop.Infrastructure.Persistent.EF.OrderAgg;

internal class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ShopContext context) : base(context)
    {
    }

    public async Task<Order> GetCurrentUserOrder(long userId)
    {
        return await Context.Orders.AsTracking().FirstOrDefaultAsync(f => f.UserId == userId
                                                                          && f.Status == OrderStatus.Pending);
    }
}
