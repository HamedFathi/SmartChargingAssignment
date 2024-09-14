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

        var chargeStation = group?.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);

        var connector = chargeStation?.Connectors.SingleOrDefault(x => x.Id == request.ConnectorId);

        connector?.UpdateMaxCurrentInAmps(request.MaxCurrentAmps);

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result>0);
    }
}