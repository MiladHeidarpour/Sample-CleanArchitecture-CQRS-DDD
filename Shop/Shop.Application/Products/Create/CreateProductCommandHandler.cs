using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Domain.Utilities;
using Shop.Application._Utilities;
using Shop.Domain.ProuductAgg;
using Shop.Domain.ProuductAgg.Repositories;
using Shop.Domain.ProuductAgg.Services;

namespace Shop.Application.Products.Create;

internal class CreateProductCommandHandler : IBaseCommandHandler<CreateProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _domainService;
    private readonly IFileService _fileService;

    public CreateProductCommandHandler(IProductRepository repository, IProductDomainService domainService, IFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);

        var product = new Product(request.Title, imageName, request.Description, request.CategoryId, request.SubCategoryId,
            request.SecondarySubCategoryId, request.Slug, request.SeoData, _domainService);

         _repository.Add(product);

        var specifications = new List<ProductSpecification>();
        request.Specifications.ToList().ForEach(specification =>
        {
            specifications.Add(new ProductSpecification(specification.Key, specification.Value));
        });
        product.SetSpecification(specifications);
        await _repository.Save();
        return OperationResult.Success();
    }
}
