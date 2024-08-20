using Common.Application;
using FluentValidation;

namespace Shop.Application.Products.RemoveImage;

public record RemoveProductImageCommand(long ProductId,long ImageId):IBaseCommand;
