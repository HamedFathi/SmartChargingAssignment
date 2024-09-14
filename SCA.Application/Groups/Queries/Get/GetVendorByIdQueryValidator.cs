using FluentValidation;
using HamedStack.CQRS.FluentValidation;
using SCA.Application.Groups.Queries.Models;

namespace SCA.Application.Groups.Queries.Get;

public class GetVendorByIdQueryValidator : QueryValidator<GetGroupByIdQuery, GroupModel>
{
    public GetVendorByIdQueryValidator()
    {
        RuleFor(e => e.Id.ToString()).NotEmpty().Must(guid => Guid.TryParse(guid, out _));
    }
}