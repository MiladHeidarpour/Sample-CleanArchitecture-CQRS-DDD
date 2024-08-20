using Common.Application;
using Shop.Domain.CategoryAgg.Repositories;

namespace Shop.Application.Categories.Remove;

internal class RemoveCategoryCommandHandler : IBaseCommandHandler<RemoveCategoryCommand>
{
    private readonly ICategoryRepository _repository;

    public RemoveCategoryCommandHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var result=await _repository.DeleteCategory(request.CategoryId);
        if (result==true)
        {
            return OperationResult.Success();
        }
        return OperationResult.Error("امکان حذف این دسته بندی وجود ندارد");
    }
}
