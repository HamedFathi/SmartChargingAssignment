using HamedStack.CQRS;

namespace SCA.Application.Connectors.Commands.Update;

public class UpdateConnectorToChargeStationCommand : ICommand<bool>
{
    public required Guid GroupId { get; init; }
    public required Guid GroupVersion { get; init; }

    public required Guid ChargeStationId { get; init; }
    public required Guid ChargeStationVersion { get; init; }

    public required int ConnectorId { get; init; }
    public required Guid ConnectorVersion { get; init; }

    public required int MaxCurrentAmps { get; init; }
}