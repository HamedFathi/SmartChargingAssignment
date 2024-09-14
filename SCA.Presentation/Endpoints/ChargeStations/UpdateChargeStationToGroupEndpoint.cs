using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.ChargeStations.Commands.Update;

namespace SCA.Presentation.Endpoints.ChargeStations;

public class UpdateChargeStationToGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut("/chargestation", UpdateChargeStationToGroupEndpointHandler);
    }

    private async Task<Result<bool>> UpdateChargeStationToGroupEndpointHandler(UpdateChargeStationToGroupCommand updateChargeStationToGroupCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(updateChargeStationToGroupCommand);
        return output;
    }
}