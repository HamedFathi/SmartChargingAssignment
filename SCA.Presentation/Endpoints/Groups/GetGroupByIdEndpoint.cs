using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;
using SCA.Application.Groups.Queries.Get;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Presentation.Endpoints.Groups;

public class GetGroupByIdEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapGet("/group/{id:guid}", GetGroupByIdEndpointHandler);

    }

    private async Task<Result<GroupModel>> GetGroupByIdEndpointHandler([FromRoute] Guid id, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(new GetGroupByIdQuery(id));
        return output;
    }
}