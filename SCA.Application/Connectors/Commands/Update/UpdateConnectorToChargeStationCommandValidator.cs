using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Connectors.Commands.Update;

public class UpdateConnectorToChargeStationCommandValidator : CommandValidator<UpdateConnectorToChargeStationCommand, bool>
{
    public UpdateConnectorToChargeStationCommandValidator()
    {
        RuleFor(e => e.MaxCurrentAmps).GreaterThan(0);
        RuleFor(e => e.ChargeStationId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
        RuleFor(e => e.ConnectorId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));

    }
}