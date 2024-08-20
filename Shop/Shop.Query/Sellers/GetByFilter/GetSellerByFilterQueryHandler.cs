using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.Mapper;

namespace Shop.Query.Sellers.GetByFilter;

internal class GetSellerByFilterQueryHandler : IQueryHandler<GetSellerByFilterQuery, SellerFilterResult>
{
    private readonly ShopContext _context;

    public GetSellerByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerFilterResult> Handle(GetSellerByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Sellers.OrderByDescending(b => b.Id).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filterParams.NationalCode))
        {
            result = result.Where(r => r.NationalCode.Contains(filterParams.NationalCode));
        }
        if (!string.IsNullOrWhiteSpace(filterParams.ShopName))
        {
            result = result.Where(r => r.ShopName.Contains(filterParams.ShopName));
        }
        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new SellerFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(seller => seller.MapDto()).ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result,filterParams.Take,filterParams.PageId);
        return model;
    }
}
