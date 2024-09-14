using HamedStack.CQRS;

namespace SCA.Application.Connectors.Commands.Create;

public class AddConnectorToChargeStationCommand : ICommand<int>
{
    public Guid GroupId { get; }
    public Guid ChargeStationId { get; }
    public int MaxCurrentAmps { get; }
    public AddConnectorToChargeStationCommand(Guid groupId, Guid chargeStationId, int maxCurrentAmps)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        MaxCurrentAmps = maxCurrentAmps;
    }
}