using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.Mapper;

namespace Shop.Query.Products.GetByFilter;

internal class GetProductByFilterQueryHandler : IQueryHandler<GetProductByFilterQuery, ProductFilterResult>
{
    private readonly ShopContext _context;

    public GetProductByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductFilterResult> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Products.OrderByDescending(f => f.Id).AsQueryable();
        if (!string.IsNullOrWhiteSpace(filterParams.Slug))
        {
            result = result.Where(r => r.Slug == filterParams.Slug);
        }
        if (!string.IsNullOrWhiteSpace(filterParams.Title))
        {
            result = result.Where(r => r.Slug.Contains(filterParams.Title));
        }
        if (filterParams.Id != null)
        {
            result = result.Where(r => r.Id == filterParams.Id);
        }
        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new ProductFilterResult()
        {
            Data =await result.Skip(skip).Take(filterParams.Take).Select(s => s.MapListData()).ToListAsync(),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}