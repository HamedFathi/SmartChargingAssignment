using HamedStack.CQRS;

namespace SCA.Application.ChargeStations.Commands.Create;

public class AddChargeStationToGroupCommand : ICommand<Guid>
{
    public Guid GroupId { get; }
    public string ChargeStationName { get; }
    public IEnumerable<AddConnectorDto> Connectors  { get;}  
    public AddChargeStationToGroupCommand(Guid groupId, string chargeStationName, IEnumerable<AddConnectorDto> connectors)
    {
        GroupId = groupId;
        ChargeStationName = chargeStationName;
        Connectors = connectors;
    }
}