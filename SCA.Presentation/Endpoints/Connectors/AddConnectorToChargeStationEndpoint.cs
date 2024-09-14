using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.Connectors.Commands.Create;

namespace SCA.Presentation.Endpoints.Connectors
{
    public class AddConnectorToChargeStationEndpoint : IMinimalApiEndpoint
    {
        public void HandleEndpoint(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapPost("/connector", AddConnectorToChargeStationEndpointHandler);
        }

        private async Task<Result<int>> AddConnectorToChargeStationEndpointHandler(AddConnectorToChargeStationCommand addConnectorToChargeStationCommand, ICommandQueryDispatcher dispatcher)
        {
            var output = await dispatcher.Send(addConnectorToChargeStationCommand);
            return output;
        }
    }
}
