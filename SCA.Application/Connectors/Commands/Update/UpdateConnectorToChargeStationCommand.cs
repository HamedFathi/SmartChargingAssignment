using HamedStack.CQRS;

namespace SCA.Application.Connectors.Commands.Update;

public class UpdateConnectorToChargeStationCommand : ICommand<bool>
{
    public Guid GroupId { get; }
    public Guid ChargeStationId { get; }
    public int ConnectorId { get; }

    public int MaxCurrentAmps { get; }
    public UpdateConnectorToChargeStationCommand(Guid groupId, Guid chargeStationId, int connectorId, int maxCurrentAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        MaxCurrentAmps = maxCurrentAmps;
        ConnectorId = connectorId;
    }
}