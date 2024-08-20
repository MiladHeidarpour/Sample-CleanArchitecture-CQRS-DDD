
using Shop.Domain.CommentAgg;
using Shop.Query.Comments.DTOs;

namespace Shop.Query.Comments.Mapper;

internal static class CommentMapper
{
    public static CommentDto? MapToDto(this Comment? comment)
    {
        if (comment == null)
        {
            return null;
        }
        return new CommentDto()
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            Status = comment.Status,
            UserId = comment.UserId,
            ProductId = comment.ProductId,
            Text = comment.Text,
        };
    }

    public static CommentDto MapDto(this Comment comment)
    {
        if (comment == null)
        {
            return null;
        }
        return new CommentDto()
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            Status = comment.Status,
            UserId = comment.UserId,
            ProductId = comment.ProductId,
            Text = comment.Text,
        };
    }
}
