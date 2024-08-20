using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.Mapper;

namespace Shop.Query.Sellers.GetByUserId;

internal class GetSellerByUserIdQueryHandler : IQueryHandler<GetSellerByUserIdQuery, SellerDto?>
{
    private readonly ShopContext _context;

    public GetSellerByUserIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<SellerDto?> Handle(GetSellerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var seller = await _context.Sellers.FirstOrDefaultAsync(f => f.UserId == request.UserId,cancellationToken);
        return seller.MapDto();
    }
}
