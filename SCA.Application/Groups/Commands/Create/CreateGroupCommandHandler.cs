using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using SCA.Domain.Entities;
using FluentValidation;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.ValueObjects;

namespace SCA.Application.Groups.Commands.Create;

public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand,Guid>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateGroupCommand> _validator;

    public CreateGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<CreateGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<Guid>();
        }

        var id = Guid.NewGuid();
        var group = new Group(id, new Name(request.Name), new CapacityInAmps(request.Capacity));

        await _repository.AddAsync(group, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(id);
    }
}