using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.ChargeStations.Commands.Update;

public class UpdateChargeStationToGroupCommandValidator : CommandValidator<UpdateChargeStationToGroupCommand, bool>
{
    public UpdateChargeStationToGroupCommandValidator()
    {
        RuleFor(e => e.ChargeStationName).NotNull().NotEmpty().Length(1, 100);
        RuleFor(e => e.GroupId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
        RuleFor(e => e.ChargeStationId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));

    }
}