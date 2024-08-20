using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Users.Register;

public record RegisterUserCommand(PhoneNumber PhoneNumber, string Password) : IBaseCommand;
