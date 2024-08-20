using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Categories.DTOs;
using Shop.Query.Categories.Mapper;

namespace Shop.Query.Categories.GetList;

internal class GetCategoryListQueryHandler : IQueryHandler<GetCategoryListQuery, List<CategoryDto>>
{
    private readonly ShopContext _context;

    public GetCategoryListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
    {
        //return await _context.Categories.Select(r => r.MapToDto()).OrderByDescending(b=>b.Id).ToListAsync(cancellationToken);
        var model=await _context.Categories.Where(f=>f.ParentId==null).Include(c=>c.Childs).ThenInclude(c=>c.Childs).OrderByDescending(c => c.Id).ToListAsync();
        return model.MapToDto();
    }
}