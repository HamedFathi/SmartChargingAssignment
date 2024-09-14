using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.Groups.Commands.Delete;

internal class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteGroupCommand> _validator;

    public DeleteGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork,
        IValidator<DeleteGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}