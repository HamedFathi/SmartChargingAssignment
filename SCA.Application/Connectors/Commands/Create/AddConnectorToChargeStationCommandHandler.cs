using FluentValidation;
using HamedStack.CQRS;
using HamedStack.TheRepository;
using HamedStack.TheResult;
using HamedStack.TheResult.FluentValidation;
using SCA.Domain.Entities;

namespace SCA.Application.Connectors.Commands.Create;

public class AddConnectorToChargeStationCommandHandler : ICommandHandler<AddConnectorToChargeStationCommand, int>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddConnectorToChargeStationCommand> _validator;

    public AddConnectorToChargeStationCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<AddConnectorToChargeStationCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<int>> Handle(AddConnectorToChargeStationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<int>();
        }

        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);

        var chargeStation = group?.ChargeStations.SingleOrDefault(x => x.Id == request.ChargeStationId);

        var newId = GetValidId(chargeStation?.Connectors!);
        chargeStation?.AddConnector(new Connector(newId, request.MaxCurrentAmps));

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(newId);
    }

    private int GetValidId(IEnumerable<Connector> connectors)
    {
        var id = 1;
        var allIds = connectors.Select(x => x.Id).ToList();
        while (true)
        {
            var freeId = !allIds.Contains(id);
            if (freeId)
            {
                return id;
            }

            id++;
        }
    }
}