using Common.Application;
using Shop.Domain.CategoryAgg.Repositories;

namespace Shop.Application.Comments.Create;

public record CreateCommentCommand(long UserId,string Text,long ProductId):IBaseCommand;

