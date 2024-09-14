using HamedStack.CQRS;

namespace SCA.Application.ChargeStations.Commands.Update;

public class UpdateChargeStationToGroupCommand : ICommand<bool>
{
    public Guid GroupId { get; }
    public Guid ChargeStationId { get; }
    public string ChargeStationName { get; }
    public IEnumerable<UpdateConnectorDto> Connectors { get; }

    public UpdateChargeStationToGroupCommand(Guid groupId, Guid chargeStationId, string chargeStationName, IEnumerable<UpdateConnectorDto> connectors)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
        ChargeStationName = chargeStationName;
        Connectors = connectors;
    }
}