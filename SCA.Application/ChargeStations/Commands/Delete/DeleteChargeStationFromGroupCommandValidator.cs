using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.ChargeStations.Commands.Delete;

public class DeleteChargeStationFromGroupCommandValidator : CommandValidator<DeleteChargeStationFromGroupCommand, bool>
{
    public DeleteChargeStationFromGroupCommandValidator()
    {
        RuleFor(e => e.GroupId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
        RuleFor(e => e.ChargeStationId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}