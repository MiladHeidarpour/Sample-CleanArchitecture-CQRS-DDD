using Shop.Domain.ProuductAgg.Repositories;
using Shop.Domain.ProuductAgg.Services;

namespace Shop.Application.Products.Services;

public class ProductDomainService : IProductDomainService
{
    private readonly IProductRepository _repoitory;

    public ProductDomainService(IProductRepository repoitory)
    {
        _repoitory = repoitory;
    }

    public bool IsSlugExsit(string slug)
    {
        return _repoitory.Exists(s => s.Slug == slug);
    }
}
