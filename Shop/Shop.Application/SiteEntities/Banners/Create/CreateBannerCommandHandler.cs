using Shop.Domain.SiteEntities;
using Common.Application;
using Common.Application.FileUtil.Interfaces;
using MediatR;
using Shop.Application._Utilities;
using Shop.Domain.SiteEntities.Repositories;

namespace Shop.Application.SiteEntities.Banners.Create;

internal class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly IFileService _fileService;

    public CreateBannerCommandHandler(IBannerRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    async Task<OperationResult> IRequestHandler<CreateBannerCommand, OperationResult>.Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
        var banner = new Banner(request.Link, imageName, request.Position);
        _repository.Add(banner);
        await _repository.Save();
        return OperationResult.Success();
    }
}
