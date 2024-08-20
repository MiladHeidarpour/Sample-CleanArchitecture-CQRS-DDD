using Common.Domain.Repository;

namespace Shop.Domain.OrderAgg.Repositories;

public interface IOrderRepository:IBaseRepository<Order>
{
    Task<Order> GetCurrentUserOrder(long userId);
}
