using FluentValidation;
using HamedStack.CQRS.FluentValidation;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Application.Groups.Queries.List;

public class ListGroupsQueryValidator : QueryValidator<ListGroupsQuery, IEnumerable<GroupModel>>
{
    public ListGroupsQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .When(x => x.PageIndex != null);
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .When(x => x.PageSize != null);

    }
}