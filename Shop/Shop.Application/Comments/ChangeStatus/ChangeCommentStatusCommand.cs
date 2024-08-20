using Common.Application;
using Common.Application.Validations;
using FluentValidation;
using Shop.Domain.CategoryAgg.Services;
using Shop.Domain.CommentAgg.Enums;

namespace Shop.Application.Comments.ChangeStatus;

public record ChangeCommentStatusCommand(long Id,CommentStatus Status):IBaseCommand;
