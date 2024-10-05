using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Api.ViewModels.Products;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Products;
using Shop.Query.Products.DTOs;

namespace Shop.Api.Controllers;


[PermissionChecker(Permission.CRUD_Product)]
public class ProductController : ApiController
{
    private readonly IProductFacade _productFacade;

    public ProductController(IProductFacade productFacade)
    {
        _productFacade = productFacade;
    }
    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<ProductFilterResult>> GetProductByFilter([FromQuery] ProductFilterParams filterParams)
    {
        var product = await _productFacade.GetProductsByFilter(filterParams);
        return QueryResult(product);
    }


    [AllowAnonymous]
    [HttpGet("Shop")]
    public async Task<ApiResult<ProductShopResult>> GetProductForShopFilter([FromQuery] ProductShopFilterParam filterParams)
    {
        var product = await _productFacade.GetProductsForShop(filterParams);
        return QueryResult(product);
    }

    [AllowAnonymous]
    [HttpGet("{productId}")]
    public async Task<ApiResult<ProductDto?>> GetProductById(long productId)
    {
        var product = await _productFacade.GetProductById(productId);
        return QueryResult(product);
    }


    [AllowAnonymous]
    [HttpGet("bySlug/{slug}")]
    public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
    {
        var product = await _productFacade.GetProductBySlug(slug);
        return QueryResult(product);
    }

    //[AllowAnonymous]
    //[HttpGet("{slug}")]
    //public async Task<ApiResult<ProductDto?>> GetProductBySlug(string slug)
    //{
    //    var product = await _productFacade.GetProductBySlug(slug);
    //    return QueryResult(product);
    //}

    [HttpPost]
    public async Task<ApiResult> CreateProduct([FromForm] CreateProductViewModel command)
    {
        var result = await _productFacade.CreateProduct(new CreateProductCommand()
        {
            SeoData = command.SeoData.Map(),
            CategoryId = command.CategoryId,
            Description = command.Description,
            ImageFile = command.ImageFile,
            SecondarySubCategoryId = command.SecondarySubCategoryId,
            Slug = command.Slug,
            Specifications = command.GetSpecification(),
            SubCategoryId = command.SubCategoryId,
            Title = command.Title
        });
        return CommandResult(result);
    }

    [HttpPost("Images/Add")]
    public async Task<ApiResult> AddProductImage([FromForm] AddProductImageCommand command)
    {
        var result = await _productFacade.AddImage(command);
        return CommandResult(result);
    }

    [HttpPut]
    public async Task<ApiResult> EditProduct([FromForm] EditProductViewModel command)
    {
        var result = await _productFacade.EditProduct(new EditProductCommand(command.ProductId,command.Title,command.ImageFile,command.Description,command.CategoryId
            ,command.SubCategoryId,command.SecondarySubCategoryId,command.Slug,command.SeoData.Map(),command.GetSpecification()));

        return CommandResult(result);
    }

    [HttpDelete("Images/Remove")]
    public async Task<ApiResult> RemoveProductImage(RemoveProductImageCommand command)
    {
        var result=await _productFacade.RemoveImage(command);
        return CommandResult(result);
    }
}
