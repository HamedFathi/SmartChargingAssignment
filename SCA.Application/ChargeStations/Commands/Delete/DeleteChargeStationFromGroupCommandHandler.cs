using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.ChargeStations.Commands.Delete;

public class DeleteChargeStationFromGroupCommandHandler : ICommandHandler<DeleteChargeStationFromGroupCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteChargeStationFromGroupCommand> _validator;

    public DeleteChargeStationFromGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<DeleteChargeStationFromGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<bool>> Handle(DeleteChargeStationFromGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);
        var chargeStation = group?.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);

        if (chargeStation != null) 
            group?.RemoveChargeStation(chargeStation);

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}