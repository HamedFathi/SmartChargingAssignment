using HamedStack.CQRS.FluentValidation;
using FluentValidation;

namespace SCA.Application.Connectors.Commands.Delete;

public class DeleteConnectorFromChargeStationCommandValidator : CommandValidator<DeleteConnectorFromChargeStationCommand, bool>
{
    public DeleteConnectorFromChargeStationCommandValidator()
    {
        RuleFor(e => e.ConnectorId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
        RuleFor(e => e.ChargeStationId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}