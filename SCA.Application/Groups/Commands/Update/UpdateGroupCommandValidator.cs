using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Groups.Commands.Update;

public class UpdateGroupCommandValidator : CommandValidator<UpdateGroupCommand, bool>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty().Length(1, 100);
        RuleFor(e => e.Capacity).GreaterThan(0);
        RuleFor(e => e.Id.ToString()).NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}