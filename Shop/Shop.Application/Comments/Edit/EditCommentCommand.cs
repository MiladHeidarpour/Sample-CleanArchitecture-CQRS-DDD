using Common.Application;

namespace Shop.Application.Comments.Edit;

public record EditCommentCommand(long Id,string Text,long UserId):IBaseCommand;
