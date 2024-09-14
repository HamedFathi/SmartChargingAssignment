using HamedStack.CQRS;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Application.Groups.Queries.List;

public class ListGroupsQuery(int? pageIndex, int? pageSize) : IQuery<IEnumerable<GroupModel>>
{
    public int? PageIndex { get; set; } = pageIndex;
    public int? PageSize { get; set; } = pageSize;
}