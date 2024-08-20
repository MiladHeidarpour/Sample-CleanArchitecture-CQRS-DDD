using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Orders.DTOs;
using Shop.Query.Orders.Mapper;

namespace Shop.Query.Orders.GetByFilter;

internal class GetOrderByFilterQueryHandler : IQueryHandler<GetOrderByFilterQuery, OrderFilterResult>
{
    private readonly ShopContext _context;
    public GetOrderByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<OrderFilterResult> Handle(GetOrderByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Orders.OrderByDescending(d => d.Id).AsQueryable();

        if (filterParams.Status != null)
        {
            result = result.Where(r => r.Status == filterParams.Status);
        }
        if (filterParams.UserId != null)
        {
            result = result.Where(r => r.UserId == filterParams.UserId);
        }
        if (filterParams.StartDate != null)
        {
            result = result.Where(r => r.CreationDate.Date >= filterParams.StartDate.Value.Date);
        }
        if (filterParams.EndDate != null)
        {
            result = result.Where(r => r.CreationDate.Date <= filterParams.EndDate.Value.Date);
        }
        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new OrderFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(order => order.MapFilterData(_context)).ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}