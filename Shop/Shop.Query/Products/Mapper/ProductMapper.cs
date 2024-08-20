using Microsoft.EntityFrameworkCore;
using Shop.Domain.ProuductAgg;
using Shop.Infrastructure.Persistent.EF;
using Shop.Query.Products.DTOs;

namespace Shop.Query.Products.Mapper;

public static class ProductMapper
{
    public static ProductDto? MapDto(this Product? product)
    {
        if (product == null)
        {
            return null;
        }
        return new ProductDto()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            Description = product.Description,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title,
            SeoData = product.SeoData,
            Specifications = product.Specifications.Select(s => new ProductSpecificationDto()
            {
                Value = s.Value,
                Key = s.Key,

            }).ToList(),
            Images = product.Images.Select(i => new ProductImageDto()
            {
                Id = i.Id,
                CreationDate = i.CreationDate,
                ProductId = i.ProductId,
                ImageName = i.ImageName,
                Sequence = i.Sequence,
            }).ToList(),


            Category = new ProductCategoryDto() { Id = product.CategoryId },
            SubCategory = new ProductCategoryDto() { Id = product.SubCategoryId },
            SecondarySubCategory = product.SubCategoryId != null ? new ProductCategoryDto() { Id = (long)product.SecondarySubCategoryId } : null,
        };
    }

    public static ProductFilterData MapListData(this Product product)
    {
        return new ProductFilterData()
        {
            Id = product.Id,
            CreationDate = product.CreationDate,
            ImageName = product.ImageName,
            Slug = product.Slug,
            Title = product.Title,
        };
    }
    public static async Task SetCategories(this ProductDto product, ShopContext _context)
    {
        var categories = await _context.Categories.Where(r => r.Id == product.Category.Id || r.Id == product.SubCategory.Id)
            .Select(s => new ProductCategoryDto()
            {
                Id = s.Id,
                Slug = s.Slug,
                ParentId = s.ParentId,
                SeoData = s.SeoData,
                Title = s.Title,
            }).ToListAsync();

        //var category = await _context.Categories.Where(f => f.Id == product.Category.Id).Select(s => new ProductCategoryDto()
        //{
        //    Id = s.Id,
        //    Slug = s.Slug,
        //    ParentId = s.ParentId,
        //    SeoData = s.SeoData,
        //    Title = s.Title,
        //}).FirstOrDefaultAsync();

        //var subCategory = await _context.Categories.Where(f => f.Id == product.SubCategory.Id).Select(s => new ProductCategoryDto()
        //{
        //    Id = s.Id,
        //    Slug = s.Slug,
        //    ParentId = s.ParentId,
        //    SeoData = s.SeoData,
        //    Title = s.Title,
        //}).FirstOrDefaultAsync();

        if (product.SecondarySubCategory != null)
        {
            var secondarySubCategory = await _context.Categories.Where(f => f.Id == product.SecondarySubCategory.Id).Select(s => new ProductCategoryDto()
            {
                Id = s.Id,
                Slug = s.Slug,
                ParentId = s.ParentId,
                SeoData = s.SeoData,
                Title = s.Title,
            }).FirstOrDefaultAsync();

            if (secondarySubCategory != null)
            {
                product.SecondarySubCategory = secondarySubCategory;
            }
        }

        product.Category = categories.First(r => r.Id == product.Category.Id);
        product.SubCategory = categories.First(r => r.Id == product.SubCategory.Id);
    }
}
