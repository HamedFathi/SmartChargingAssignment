using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.ChargeStations.Commands.Update;

public class UpdateChargeStationToGroupCommandHandler : ICommandHandler<UpdateChargeStationToGroupCommand, bool>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateChargeStationToGroupCommand> _validator;

    public UpdateChargeStationToGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<UpdateChargeStationToGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<bool>> Handle(UpdateChargeStationToGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<bool>();
        }

        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);

        var chargeStation = group?.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);

        chargeStation?.UpdateName(request.ChargeStationName);

        foreach (var connector in request.Connectors)
        {
            chargeStation?.UpdateConnector(new Connector(connector.Id, connector.MaxCurrentInAmps));
        }

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(result > 0);
    }
}