using HamedStack.AspNetCore.Endpoint;
using HamedStack.CQRS;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;
using SCA.Application.Groups.Queries.List;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Presentation.Endpoints.Groups;

public class ListGroupsQueryEndpoint : IMinimalApiEndpoint
{
    public void HandleEndpoint(IEndpointRouteBuilder endpoint)
    {
        endpoint.MapPost("/group/list", ListGroupsQueryEndpointHandler);
    }

    private static async Task<Result<IEnumerable<GroupModel>>> ListGroupsQueryEndpointHandler([FromBody] ListGroupsQuery listGroupsQuery, ICommandQueryDispatcher dispatcher)
    {
        var output = await dispatcher.Send(listGroupsQuery);
        return output;
    }
}