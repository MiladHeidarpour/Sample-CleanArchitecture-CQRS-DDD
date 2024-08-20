
using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Sliders;

internal class GetSliderByListQueryHandler : IQueryHandler<GetSliderByListQuery, List<SliderDto>>
{
    private readonly ShopContext _context;

    public GetSliderByListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<SliderDto>> Handle(GetSliderByListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Sliders.OrderByDescending(b => b.Id).Select(slider => new SliderDto()
        {
            Id = slider.Id,
            CreationDate = slider.CreationDate,
            Link = slider.Link,
            ImageName = slider.ImageName,
            Title = slider.Title,
        }).ToListAsync(cancellationToken);
    }
}
