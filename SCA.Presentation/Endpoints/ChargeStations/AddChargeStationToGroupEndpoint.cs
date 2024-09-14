using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.ChargeStations.Commands.Create;

namespace SCA.Presentation.Endpoints.ChargeStations;

public class AddChargeStationToGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/chargestation", AddChargeStationToGroupEndpointHandler);
    }

    private async Task<Result<Guid>> AddChargeStationToGroupEndpointHandler(AddChargeStationToGroupCommand addChargeStationToGroupCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(addChargeStationToGroupCommand);
        return output;
    }
}