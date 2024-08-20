
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utilities;
using Common.Domain.ValueObjects;
using Shop.Domain.ProuductAgg.Services;

namespace Shop.Domain.ProuductAgg;

public class Product : AggregateRoot
{
    public string Title { get; private set; }
    public string ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long? SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ProductImage> Images { get; private set; }
    public List<ProductSpecification> Specifications { get; private set; }

    private Product()
    {
        
    }
    public Product(string title, string imageName, string description, long categoryId, long subCategoryId,
        long? secondarySubCategoryId, string slug, SeoData seoData, IProductDomainService productDomainService)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Gaurd(title, description, slug, productDomainService);
        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }
    public void Edit(string title,  string description, long categoryId,
        long subCategoryId, long secondarySubCategoryId, string slug, SeoData seoData,IProductDomainService productDomainService)
    {
        Gaurd(title, description, slug, productDomainService);
        Title = title;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }
    public void SetProductImage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName=imageName;
    }
    public void AddImage(ProductImage image)
    {
        image.ProductId = Id;
        Images.Add(image);
    }
    public string RemoveImage(long id)
    {
        var image = Images.FirstOrDefault(f => f.Id == id);
        if (image == null)
        {
            throw new NullOrEmptyDomainDataException("عکس یافت نشد");
        }
        Images.Remove(image);
        return image.ImageName;
    }
    public void SetSpecification(List<ProductSpecification> specifications)
    {
        specifications.ForEach(s=>s.ProductId = Id);
        Specifications = specifications;
    }

    private void Gaurd(string title, string description,string slug,IProductDomainService productDomainService)
    {
        if (slug!=Slug)
        {
            if (productDomainService.IsSlugExsit(slug.ToSlug())==true)
            {
                throw new SlugIsDuplicateException();
            }
        }
        NullOrEmptyDomainDataException.CheckString(title, nameof(title)); 
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));
    }
}
