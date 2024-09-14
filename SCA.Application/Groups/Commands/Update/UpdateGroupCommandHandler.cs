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

    public UpdateGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork,
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

        var group = new Group(request.Id, new Name(request.Name), new CapacityInAmps(request.Capacity));
        await _repository.UpdateAsync(group, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}