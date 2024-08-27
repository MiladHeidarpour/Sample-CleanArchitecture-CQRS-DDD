using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.OrderAgg.Enums;
using Shop.Infrastructure.Persistent.Dapper;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.Mapper;

namespace Shop.Query.Orders.GetCurrent;

public class GetCurrentUserOrderQueryHandler : IQueryHandler<GetCurrentUserOrderQuery, OrderDto?>
{
    private readonly ShopContext _context;
    private readonly DapperContext _dapperContext;

    public GetCurrentUserOrderQueryHandler(ShopContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
    }

    public async Task<OrderDto?> Handle(GetCurrentUserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Status == OrderStatus.Pending,cancellationToken);

        if (order == null)
        {
            return null;
        }

        var orderDto = order.MapToDto();

        orderDto.UserFullName = await _context.Users.Where(f => f.Id == orderDto.UserId).Select(s => $"{s.Name} {s.Family}").FirstAsync(cancellationToken);
        orderDto.Items = await orderDto.GetOrderItems(_dapperContext);
        return orderDto;
    }
}