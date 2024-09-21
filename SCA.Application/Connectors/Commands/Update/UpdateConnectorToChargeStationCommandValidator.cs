using FluentValidation;
using HamedStack.CQRS.FluentValidation;

namespace SCA.Application.Connectors.Commands.Update;

public class UpdateConnectorToChargeStationCommandValidator : CommandValidator<UpdateConnectorToChargeStationCommand, bool>
{
    public UpdateConnectorToChargeStationCommandValidator()
    {
        RuleFor(e => e.MaxCurrentAmps).GreaterThan(0);

        RuleFor(e => e.ChargeStationId).NotEmpty();
        RuleFor(e => e.ChargeStationVersion).NotEmpty();

        RuleFor(e => e.ConnectorId).NotEmpty();
        RuleFor(e => e.ConnectorVersion).NotEmpty();

        RuleFor(e => e.GroupId).NotEmpty();
        RuleFor(e => e.GroupVersion).NotEmpty();
    }
}