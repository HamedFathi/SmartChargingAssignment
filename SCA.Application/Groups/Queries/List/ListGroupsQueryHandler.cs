using FluentValidation;
using HamedStack.CQRS;
using HamedStack.Paging;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Application.Groups.Queries.Models;
using SCA.Domain.Entities;

namespace SCA.Application.Groups.Queries.List;

public class ListGroupsQueryHandler : IQueryHandler<ListGroupsQuery, IEnumerable<GroupModel>>
{
    private readonly IRepository<Group> _repository;
    private readonly IValidator<ListGroupsQuery> _validator;

    public ListGroupsQueryHandler(IRepository<Group> repository, IValidator<ListGroupsQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<IEnumerable<GroupModel>>> Handle(ListGroupsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<IEnumerable<GroupModel>>();
        }

        var result = await _repository.ToListAsync(cancellationToken);

        if (request is { PageIndex: not null, PageSize: not null })
            result = result.ToPaged(request.PageIndex.Value, request.PageSize.Value).ToList();

        var output = GroupMapper.MapToGroupModels(result);
        return Result<IEnumerable<GroupModel>>.Success(output);
    }
}