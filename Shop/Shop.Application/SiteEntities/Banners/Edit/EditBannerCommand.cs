using Common.Application;
using Microsoft.AspNetCore.Http;
using Shop.Domain.SiteEntities.Enums;

namespace Shop.Application.SiteEntities.Banners.Edit;

public class EditBannerCommand : IBaseCommand
{
    public long Id { get;private set; }
    public string Link { get; private set; }
    public IFormFile? ImageFile { get; private set; }
    public BannerPosition Position { get; private set; }

    public EditBannerCommand(long id,string link, IFormFile? imageFile, BannerPosition position)
    {
        Id = id;
        Link = link;
        ImageFile = imageFile;
        Position = position;
    }
}
