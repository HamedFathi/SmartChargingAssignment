using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using SCA.Application.Groups.Commands.Create;

namespace SCA.Presentation.Endpoints.Groups;

public class CreateGroupEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/group", CreateGroupEndpointHandler);

    }

    private async Task<Result<Guid>> CreateGroupEndpointHandler(CreateGroupCommand createGroupCommand, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(createGroupCommand);
        return output;
    }
}