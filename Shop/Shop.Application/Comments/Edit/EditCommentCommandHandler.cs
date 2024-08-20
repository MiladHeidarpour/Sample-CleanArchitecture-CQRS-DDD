using Common.Application;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg.Repositories;

namespace Shop.Application.Comments.Edit;

public class EditCommentCommandHandler : IBaseCommandHandler<EditCommentCommand>
{
    private readonly ICommentRepository _repository;

    public EditCommentCommandHandler(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetTracking(request.Id);
        if (comment == null || comment.UserId != request.UserId)
        {
            return OperationResult.NotFound();
        }
        comment.Edit(request.Text);
        await _repository.Save();
        return OperationResult.Success();
    }
}
