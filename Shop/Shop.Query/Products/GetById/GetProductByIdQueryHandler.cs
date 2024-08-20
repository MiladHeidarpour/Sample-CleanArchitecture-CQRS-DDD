using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.Mapper;

namespace Shop.Query.Products.GetById;

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly ShopContext _context;

    public GetProductByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FirstOrDefaultAsync(f => f.Id == request.ProductId, cancellationToken);
        var model = product.MapDto();
        if (model==null)
        {
            return null;
        }
        await model.SetCategories(_context);
        return model;
    }
}
