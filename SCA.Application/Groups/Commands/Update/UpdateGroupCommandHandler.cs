using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;
using SCA.Domain.ValueObjects;

namespace SCA.Application.Groups.Commands.Update;

internal class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateGroupCommand> _validator;

    public UpdateGroupCommandHandler(
        IRepository<Group> repository, 
        IUnitOfWork unitOfWork,
        IValidator<UpdateGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        var group = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (group is null)
        {
            return Result<bool>.NotFound(false, "Group is not found.");
        }
        if (group.Version != request.Version)
        {
            return Result<bool>.Conflict(false, "Group was already updated. Please fetch the group again and repeat.");
        }

        if (request.Capacity is { } newCapacity)
        {
            group.UpdateCapacityInAmps(newCapacity);
        }

        if (request.Name is { } newName)
        {
            group.Name = new Name(newName);
        }

        group.Version = Guid.NewGuid(); // NOTE: better do that in EF interceptor
        await _repository.UpdateAsync(group, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}