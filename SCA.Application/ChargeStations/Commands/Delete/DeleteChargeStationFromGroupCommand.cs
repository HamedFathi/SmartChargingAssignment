using HamedStack.CQRS;

namespace SCA.Application.ChargeStations.Commands.Delete;

public class DeleteChargeStationFromGroupCommand : ICommand<bool>
{
    public Guid GroupId { get; }
    public Guid ChargeStationId { get; }

    public DeleteChargeStationFromGroupCommand(Guid groupId, Guid chargeStationId)
    {
        GroupId = groupId;
        ChargeStationId = chargeStationId;
    }
}