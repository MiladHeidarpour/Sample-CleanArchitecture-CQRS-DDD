using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Query.SiteEntities.Banners.GetByList;

internal class GetBannerByListQueryHandler : IQueryHandler<GetBannerByListQuery, List<BannerDto>>
{
    private readonly ShopContext _context;

    public GetBannerByListQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<List<BannerDto>> Handle(GetBannerByListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners.OrderByDescending(b => b.Id).Select(banner => new BannerDto()
        {
            Id = banner.Id,
            CreationDate = banner.CreationDate,
            ImageName = banner.ImageName,
            Link = banner.Link,
            Position = banner.Position,
        }).ToListAsync(cancellationToken);
    }
}