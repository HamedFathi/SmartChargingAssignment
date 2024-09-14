using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;
using SCA.Application.Groups.Commands.Delete;

namespace SCA.Presentation.Endpoints.Groups;

public class DeleteGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapDelete("/group/{id:guid}", DeleteGroupEndpointHandler);

    }

    private async Task<Result<bool>> DeleteGroupEndpointHandler([FromRoute] Guid id, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(new DeleteGroupCommand(id));
        return output;
    }
}