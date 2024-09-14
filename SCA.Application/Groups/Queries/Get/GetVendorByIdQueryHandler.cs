using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Application.Groups.Queries.Models;
using SCA.Domain.Entities;

namespace SCA.Application.Groups.Queries.Get;

public class GetVendorByIdQueryHandler : IQueryHandler<GetGroupByIdQuery, GroupModel>
{
    private readonly IRepository<Group> _repository;
    private readonly IValidator<GetGroupByIdQuery> _validator;

    public GetVendorByIdQueryHandler(IRepository<Group> repository, IValidator<GetGroupByIdQuery> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<Result<GroupModel>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<GroupModel>();
        }

        var result = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (result == null)
        {
            return Result<GroupModel>.NotFound(null, "No record found.");
        }

        var output = GroupMapper.MapToGroupModel(result);
        return Result<GroupModel>.Success(output);
    }

}