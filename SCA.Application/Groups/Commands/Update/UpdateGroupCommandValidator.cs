using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Groups.Commands.Update;

public class UpdateGroupCommandValidator : CommandValidator<UpdateGroupCommand, bool>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();

        RuleFor(e => e.Version)
            .NotEmpty();

        RuleFor(e => e.Name)
            .NotEmpty()
            .Length(1, 100)
            .When(e => e.Name is not null);

        RuleFor(e => e.Capacity)
            .GreaterThan(0)
            .When(e => e.Capacity is not null);

        RuleFor(e => e)
            .Must(e => e.Capacity is not null || e.Name is not null)
            .WithMessage(e => $"'{nameof(e.Capacity)}' or '{nameof(e.Name)}' must be provided");
    }
}