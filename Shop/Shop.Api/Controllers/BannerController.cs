using Common.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure.Security;
using Shop.Application.SiteEntities.Banners.Create;
using Shop.Application.SiteEntities.Banners.Edit;
using Shop.Domain.RoleAgg.Enums;
using Shop.Presentation.Facade.SiteEntities.Banners;
using Shop.Query.SiteEntities.DTOs;

namespace Shop.Api.Controllers;

[PermissionChecker(Permission.CRUD_Banner)]
public class BannerController : ApiController
{
    private readonly IBannerFacade _bannerFacade;

    public BannerController(IBannerFacade bannerFacade)
    {
        _bannerFacade = bannerFacade;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ApiResult<List<BannerDto>>> GetList()
    {
        var result = await _bannerFacade.GetBanners();
        return QueryResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ApiResult<BannerDto?>> GetById(long id)
    {
        var result = await _bannerFacade.GetBannerById(id);
        return QueryResult(result);
    }

    [HttpPost]
    public async Task<ApiResult> Create(CreateBannerCommand command)
    {
        var result = await _bannerFacade.CreateBanner(command);
        return CommandResult(result);
    }
    [HttpPut]
    public async Task<ApiResult> Edit(EditBannerCommand command)
    {
        var result = await _bannerFacade.EditBanner(command);
        return CommandResult(result);
    }
}
