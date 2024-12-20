﻿using Common.Application;
using Common.CacheHelper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Products.AddImage;
using Shop.Application.Products.Create;
using Shop.Application.Products.Edit;
using Shop.Application.Products.RemoveImage;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Products.DTOs;
using Shop.Query.Products.GetByFilter;
using Shop.Query.Products.GetById;
using Shop.Query.Products.GetBySlug;
using Shop.Query.Products.GetForShop;

namespace Shop.Presentation.Facade.Products;

internal class ProductFacade : IProductFacade
{
    private readonly IMediator _mediator;
    private readonly IDistributedCache _distributedCache;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;
    public ProductFacade(IMediator mediator, IDistributedCache distributedCache, ISellerInventoryFacade sellerInventoryFacade)
    {
        _mediator = mediator;
        _distributedCache = distributedCache;
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    public async Task<OperationResult> CreateProduct(CreateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditProduct(EditProductCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            await _distributedCache.RemoveAsync(CacheKeys.Product(command.Slug));
        }
        return result;
    }

    public async Task<OperationResult> AddImage(AddProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _distributedCache.RemoveAsync(CacheKeys.Product(product.Slug));
            await _distributedCache.RemoveAsync(CacheKeys.SingleProduct(product.Slug));
        }
        return result;
    }

    public async Task<OperationResult> RemoveImage(RemoveProductImageCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.Status == OperationResultStatus.Success)
        {
            var product = await GetProductById(command.ProductId);
            await _distributedCache.RemoveAsync(CacheKeys.Product(product.Slug));
            await _distributedCache.RemoveAsync(CacheKeys.SingleProduct(product.Slug));
        }
        return result;
    }

    public async Task<ProductDto?> GetProductById(long productId)
    {
        return await _mediator.Send(new GetProductByIdQuery(productId));
    }

    public async Task<ProductDto?> GetProductBySlug(string slug)
    {
        return await _distributedCache.GetOrSet(CacheKeys.Product(slug), () =>
        {
            return _mediator.Send(new GetProductBySlugQuery(slug));
        });
    }

    public async Task<SingleProductDto?> GetProductBySlugForSinglePage(string slug)
    {
        return await _distributedCache.GetOrSet(CacheKeys.Product(slug), async () =>
        {
            var product = await _mediator.Send(new GetProductBySlugQuery(slug));
            if (product == null)
                return null;

            var inventories = await _sellerInventoryFacade.GetByProductId(product.Id);

            var model = new SingleProductDto()
            {
                ProductDto = product,
                InventoryDtos = inventories,
            };

            return model;
        });
    }

    public async Task<ProductFilterResult> GetProductsByFilter(ProductFilterParams filterParams)
    {
        return await _mediator.Send(new GetProductByFilterQuery(filterParams));
    }
    public async Task<ProductShopResult> GetProductsForShop(ProductShopFilterParam filterParams)
    {
        return await _mediator.Send(new GetProductsForShopQuery(filterParams));
    }
}
