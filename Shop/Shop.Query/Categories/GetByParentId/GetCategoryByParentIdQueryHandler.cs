
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.Mapper;

namespace Shop.Query.Categories.GetByParentId;

internal class GetCategoryByParentIdQueryHandler : IQueryHandler<GetCategoryByParentIdQuery, List<ChildCategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryByParentIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<ChildCategoryDto>> Handle(GetCategoryByParentIdQuery request, CancellationToken cancellationToken)
    {
        var model= await _context.Categories.Include(c => c.Childs).Where(f=>f.ParentId==request.ParentId).ToListAsync(cancellationToken);
        return model.MapChildren();
    }
}