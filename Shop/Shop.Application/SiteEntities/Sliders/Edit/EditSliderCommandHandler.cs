using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Microsoft.AspNetCore.Http;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Sliders.Edit;

internal class EditSliderCommandHandler : IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly IFileService _fileService;

    public EditSliderCommandHandler(ISliderRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.Id);

        if (slider == null)
        {
            return OperationResult.NotFound();
        }
        var imageName=slider.ImageName;
        var oldImage = slider.ImageName;
        if (request.ImageFile!=null)
        {
            imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
        }
        slider.Edit(request.Title, request.Link, imageName);
        await _repository.Save();
        DeleteOldImage(request.ImageFile, oldImage);
        return OperationResult.Success();
    }
    private void DeleteOldImage(IFormFile? Imagefile,string oldImage)
    {
        if (Imagefile != null)
        {
            _fileService.DeleteFile(Directories.SliderImages,oldImage);
        }
    }
}
