using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Comments.DTOs;
using Shop.Query.Comments.Mapper;

namespace Shop.Query.Comments.GetByFilter;

internal class GetCommentByFilterQueryHandler : IQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _context;

    public GetCommentByFilterQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var filterParams = request.FilterParams;
        var result = _context.Comments.OrderByDescending(c => c.CreationDate).AsQueryable();
        if (filterParams.CommentStatus != null)
        {
            result = result.Where(r => r.Status == filterParams.CommentStatus);
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

        var model = new CommentFilterResult()
        {
            Data = await result.Skip(skip).Take(filterParams.Take).Select(comment =>CommentMapper.MapDto(comment)).ToListAsync(cancellationToken),
            FilterParams = filterParams,
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;

    }
}