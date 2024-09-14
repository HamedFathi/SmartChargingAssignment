using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Groups.Commands.Delete;

public class DeleteGroupCommandValidator : CommandValidator<DeleteGroupCommand, bool>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(e => e.Id.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}