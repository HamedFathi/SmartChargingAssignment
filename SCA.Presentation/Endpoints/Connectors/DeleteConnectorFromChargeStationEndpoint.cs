using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;
using SCA.Application.Connectors.Commands.Delete;

namespace SCA.Presentation.Endpoints.Connectors
{
    public class DeleteConnectorFromChargeStationEndpoint : IMinimalApiEndpoint
    {
        public void HandleEndpoint(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapDelete("/connector/{groupId:guid}/{chargeStationId:guid}/{connectorId:int}",
                DeleteChargeStationFromGroupEndpointHandler);
        }

        private async Task<Result<bool>> DeleteChargeStationFromGroupEndpointHandler([FromRoute] Guid groupId,
            [FromRoute] Guid chargeStationId, [FromRoute] int connectorId, ICommandQueryDispatcher dispatcher)
        {
            var output =
                await dispatcher.Send(
                    new DeleteConnectorFromChargeStationCommand(groupId, chargeStationId, connectorId));
            return output;
        }
    }
}