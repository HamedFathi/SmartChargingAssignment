using HamedStack.CQRS;

namespace SCA.Application.Connectors.Commands.Delete;

public class DeleteConnectorFromChargeStationCommand : ICommand<bool>
{
    public Guid GroupId { get; }
    public Guid ChargeStationId { get; }
    public int ConnectorId { get; }

    public DeleteConnectorFromChargeStationCommand(Guid groupId, Guid chargeStationId, int connectorId)
    {
        GroupId = groupId;
        ConnectorId = connectorId;
        ChargeStationId = chargeStationId;
    }
}