using HamedStack.CQRS;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Application.Groups.Queries.Get;

public class GetGroupByIdQuery(Guid id) : IQuery<GroupModel>
{
    public Guid Id { get; } = id;
}