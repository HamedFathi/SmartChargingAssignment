using HamedStack.CQRS.FluentValidation;
using FluentValidation;

namespace SCA.Application.Connectors.Commands.Create;

public class AddConnectorToChargeStationCommandValidator : CommandValidator<AddConnectorToChargeStationCommand, int>
{
    public AddConnectorToChargeStationCommandValidator()
    {
        RuleFor(e => e.MaxCurrentAmps).GreaterThan(0);
        RuleFor(e => e.ChargeStationId.ToString()).NotNull().NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}