using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Groups.Commands.Create;

public class CreateGroupCommandValidator : CommandValidator<CreateGroupCommand,Guid>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().Length(1, 100);
        RuleFor(e => e.Capacity).GreaterThan(0);
    }
}