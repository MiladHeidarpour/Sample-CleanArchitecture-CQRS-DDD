using Common.Application;
using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.Sellers.AddInventory;
using Shop.Application.Sellers.Create;
using Shop.Application.Sellers.Edit;
using Shop.Application.Sellers.EditInventory;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.Sellers;
using Shop.Presentation.Facade.Sellers.Inventories;
using Shop.Query.Sellers.DTOs;
using Shop.Query.Sellers.Inventories.GetListBySellerId;

namespace Shop.Api.Controllers;

public class SellerController : ApiController
{
    private readonly ISellerFacade _sellerFacade;
    private readonly ISellerInventoryFacade _sellerInventoryFacade;
    public SellerController(ISellerFacade sellerFacade, ISellerInventoryFacade sellerInventoryFacade)
    {
        _sellerFacade = sellerFacade;
        _sellerInventoryFacade = sellerInventoryFacade;
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpGet]
    public async Task<ApiResult<SellerFilterResult>> GetSellers(SellerFilterParams filterParams)
    {
        var result = await _sellerFacade.GetSellersByFilter(filterParams);
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<SellerDto?>> GetSellerById(long sellerId)
    {
        var result = await _sellerFacade.GetSellerById(sellerId);
        return QueryResult(result);
    }

    [Authorize]
    [HttpGet("Current")]
    public async Task<ApiResult<SellerDto?>> GetSeller()
    {
        var result = await _sellerFacade.GetSellerByUserId(User.GetUserId());
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPost]
    public async Task<ApiResult> CreateSeller(CreateSellerCommand command)
    {
        var result = await _sellerFacade.CreateSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Management)]
    [HttpPut]
    public async Task<ApiResult> EditSeller(EditSellerCommand command)
    {
        var result = await _sellerFacade.EditSeller(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Add_Inventory)]
    [HttpPost("Inventory")]
    public async Task<ApiResult> AddInventory(AddSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.AddInventory(command);
        return CommandResult(result);
    }

    [HttpPut("Inventory")]
    [PermissionChecker(Permission.Edit_Inventory)]
    public async Task<ApiResult> EditInventory(EditSellerInventoryCommand command)
    {
        var result = await _sellerInventoryFacade.EditInventory(command);
        return CommandResult(result);
    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("Inventory")]
    public async Task<ApiResult<List<InventoryDto>>> GetInventories()
    {
        var seller = await _sellerFacade.GetSellerById(User.GetUserId());

        if (seller == null)
        {
            return QueryResult(new List<InventoryDto>());
        }
        var result = await _sellerInventoryFacade.GetList(seller.Id);
        return QueryResult(result);
    }

    [PermissionChecker(Permission.Seller_Panel)]
    [HttpGet("Inventory/{invntoryId}")]
    public async Task<ApiResult<InventoryDto>> GetInventories(long invntoryId)
    {
        var seller = await _sellerFacade.GetSellerById(User.GetUserId());

        if (seller == null)
        {
            return QueryResult(new InventoryDto());
        }

        var result = await _sellerInventoryFacade.GetById(invntoryId);
        if (result == null || result.SellerId != seller.Id)
        {
            return QueryResult(new InventoryDto());
        }

        return QueryResult(result);
    }
}