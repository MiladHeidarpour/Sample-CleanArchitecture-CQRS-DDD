using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.Mapper;

namespace Shop.Query.Products.GetBySlug;

internal class GetProductBySlugQueryHandler : IQueryHandler<GetProductBySlugQuery, ProductDto?>
{
    private readonly ShopContext _context;

    public GetProductBySlugQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductBySlugQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(f => f.Slug == request.Slug, cancellationToken);
        var model = product.MapDto();
        if (model == null)
        {
            return null;
        }
        await model.SetCategories(_context);
        return model;
    }
}