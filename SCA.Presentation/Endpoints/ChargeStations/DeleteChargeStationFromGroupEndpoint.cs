using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;
using SCA.Application.ChargeStations.Commands.Delete;

namespace SCA.Presentation.Endpoints.ChargeStations;

public class DeleteChargeStationFromGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete("/chargestation/{groupId:guid}/{chargeStationId:guid}", DeleteChargeStationFromGroupEndpointHandler);
    }

    private async Task<Result<bool>> DeleteChargeStationFromGroupEndpointHandler([FromRoute] Guid groupId, [FromRoute] Guid chargeStationId, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(new DeleteChargeStationFromGroupCommand(groupId, chargeStationId));
        return output;
    }
}