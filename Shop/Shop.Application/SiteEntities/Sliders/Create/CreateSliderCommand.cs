using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.SiteEntities.Sliders.Create;

public class CreateSliderCommand:IBaseCommand
{
    public string Title { get; private set; }
    public string Link { get; private set; }
    public IFormFile ImageFile { get; private set; }

    public CreateSliderCommand(string title, string link, IFormFile imageFile)
    {
        Title = title;
        Link = link;
        ImageFile = imageFile;
    }
}
