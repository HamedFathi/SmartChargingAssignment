using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.ChargeStations.Commands.Create;

public class AddChargeStationToGroupCommandValidator : CommandValidator<AddChargeStationToGroupCommand, Guid>
{
    public AddChargeStationToGroupCommandValidator()
    {
        RuleFor(e => e.ChargeStationName).NotNull().NotEmpty().Length(1, 100);
        RuleFor(e => e.GroupId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}