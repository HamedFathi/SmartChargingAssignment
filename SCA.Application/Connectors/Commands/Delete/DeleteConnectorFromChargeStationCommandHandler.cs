using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.Connectors.Commands.Delete;

public class DeleteConnectorFromChargeStationCommandHandler : ICommandHandler<DeleteConnectorFromChargeStationCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<DeleteConnectorFromChargeStationCommand> _validator;

    public DeleteConnectorFromChargeStationCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<DeleteConnectorFromChargeStationCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<bool>> Handle(DeleteConnectorFromChargeStationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);

        var chargeStation = group?.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);

        var connector = chargeStation?.Connectors.SingleOrDefault(x => x.Id == request.ConnectorId);

        if (connector != null) 
            chargeStation?.RemoveConnector(connector);

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}