using Shop.Domain.CategoryAgg;
using Shop.Query.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Query.Categories.Mapper;

internal static class CategoryMapper
{
    public static CategoryDto MapToDto(this Category? category)
    {
        if (category == null)
        {
            return null;
        }
        return new CategoryDto()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            SeoData = category.SeoData,
            Childs = category.Childs.MapChildren(),
        };
    }

    public static List<CategoryDto> MapToDto(this List<Category> categories)
    {
        var model=new List<CategoryDto>();
        categories.ForEach(category =>
        {
            model.Add(new CategoryDto()
            {
                Id = category.Id,
                CreationDate = category.CreationDate,
                Title = category.Title,
                Slug = category.Slug,
                SeoData = category.SeoData,
                Childs = category.Childs.MapChildren(),
            });
        });
        return model;
    }
    public static List<ChildCategoryDto> MapChildren(this List<Category> children)
    {
        var model = new List<ChildCategoryDto>();
        children.ForEach(c =>
        {
            model.Add(new ChildCategoryDto()
            {
                Id = c.Id,
                CreationDate = c.CreationDate,
                Title = c.Title,
                Slug = c.Slug,
                SeoData = c.SeoData,
                ParentId = (long)c.ParentId,
                Childs = c.Childs.MapSecondaryChildren()
            });
        });
        return model;
    }
    private static List<SecondaryChildCategoryDto> MapSecondaryChildren(this List<Category> childrens)
    {
        var model = new List<SecondaryChildCategoryDto>();
        childrens.ForEach(c =>
        {
            model.Add(new SecondaryChildCategoryDto()
            {
                Id = c.Id,
                CreationDate = c.CreationDate,
                Title = c.Title,
                Slug = c.Slug,
                SeoData = c.SeoData,
                ParentId = (long)c.ParentId,
            });
        });
        return model;
    }
}
