using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.Groups.Commands.Update;

namespace SCA.Presentation.Endpoints.Groups;

public class UpdateGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPut("/group", UpdateGroupEndpointHandler);

    }

    private async Task<Result<bool>> UpdateGroupEndpointHandler(UpdateGroupCommand updateGroupCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(updateGroupCommand);
        return output;
    }
}