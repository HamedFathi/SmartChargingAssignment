using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.Connectors.Commands.Update;

namespace SCA.Presentation.Endpoints.Connectors
{
    public class UpdateConnectorToChargeStationEndpoint : IMinimalApiEndpoint
    {
        public void HandleEndpoint(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapPut("/connector", UpdateConnectorToChargeStationEndpointHandler);
        }

        private async Task<Result<bool>> UpdateConnectorToChargeStationEndpointHandler(
            UpdateConnectorToChargeStationCommand updateConnectorToChargeStationCommand,
            ICommandQueryDispatcher dispatcher)
        {
            var output = await dispatcher.Send(updateConnectorToChargeStationCommand);
            return output;
        }
    }
}
