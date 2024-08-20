using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Shop.Application._Utilities;
using Shop.Domain.ProuductAgg;
using Shop.Domain.ProuductAgg.Repositories;
using Shop.Domain.ProuductAgg.Services;

namespace Shop.Application.Products.AddImage;

internal class AddProductImageCommandHandler : IBaseCommandHandler<AddProductImageCommand>
{
    private readonly IProductRepository _repository;
    private readonly IFileService _fileService;

    public AddProductImageCommandHandler(IProductRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProductId);
        if (product == null)
        {
            return OperationResult.NotFound();
        }
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductGalleryImage);

        product.AddImage(new ProductImage(imageName, request.Sequence));
        await _repository.Save();
        return OperationResult.Success();
    }
}
