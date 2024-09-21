using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.Connectors.Commands.Update;

public class UpdateConnectorToChargeStationCommandHandler : ICommandHandler<UpdateConnectorToChargeStationCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateConnectorToChargeStationCommand> _validator;

    public UpdateConnectorToChargeStationCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<UpdateConnectorToChargeStationCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<bool>> Handle(UpdateConnectorToChargeStationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);
        if (group is null)
        {
            return Result<bool>.NotFound(false, "Group is not found.");
        }
        if (group.Version != request.GroupVersion)
        {
            return Result<bool>.Conflict(false, "Group was already updated. Please fetch the group again and repeat.");
        }

        var chargeStation = group.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);
        if (chargeStation is null)
        {
            return Result<bool>.NotFound(false, "ChargeStation is not found.");
        }
        if (chargeStation.Version != request.ChargeStationVersion)
        {
            return Result<bool>.Conflict(false, "ChargeStation was already updated. Please fetch the chargeStation again and repeat.");
        }

        var connector = chargeStation.Connectors.SingleOrDefault(x => x.Id == request.ConnectorId);
        if (connector is null)
        {
            return Result<bool>.NotFound(false, "Connector is not found.");
        }
        if (connector.Version != request.ConnectorVersion)
        {
            return Result<bool>.Conflict(false, "Connector was already updated. Please fetch the connector again and repeat.");
        }

        connector.UpdateMaxCurrentInAmps(request.MaxCurrentAmps);

        // NOTE: Updated max current on connector also affect parent group, as capacity depends on connectors
        // better to bump chargeStation too, so that the list of connectors is re-fetched.
        group.Version = Guid.NewGuid();
        chargeStation.Version = Guid.NewGuid();

        connector.Version = Guid.NewGuid(); // NOTE: better do that in EF interceptor
        await _repository.UpdateAsync(group, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result>0);
    }
}