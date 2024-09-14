using HamedStack.CQRS;
using HamedStack.TheResult;
using FluentValidation;
using HamedStack.TheRepository;
using SCA.Domain.Entities;
using HamedStack.TheResult.FluentValidation;

namespace SCA.Application.ChargeStations.Commands.Create;

public class AddChargeStationToGroupCommandHandler : ICommandHandler<AddChargeStationToGroupCommand, Guid>
{
    private readonly IRepository<Group> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddChargeStationToGroupCommand> _validator;

    public AddChargeStationToGroupCommandHandler(IRepository<Group> repository, IUnitOfWork unitOfWork, IValidator<AddChargeStationToGroupCommand> validator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    public async Task<Result<Guid>> Handle(AddChargeStationToGroupCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToResult<Guid>();
        }

        var connectorsCount = request.Connectors.Count();

        if (connectorsCount is < 1 or > 5)
        {
            return Result<Guid>.ValidationError(Guid.Empty, "Connectors must be between 1 and 5.");
        }
        
        var group = await _repository.GetByIdAsync(request.GroupId, cancellationToken);

        var id = Guid.NewGuid();
        group?.AddChargeStation(new ChargeStation(id, request.ChargeStationName, CreateConnectors(request)));

        await _repository.UpdateAsync(group!, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(id);
    }

    private static IEnumerable<Connector> CreateConnectors(AddChargeStationToGroupCommand request)
    {
        var connectors = new List<Connector>();

        var id = 1;
        foreach (var connector in request.Connectors)
        {
            connectors.Add(new Connector(id,connector.MaxCurrentInAmps) );
            id++;
        }

        return connectors;
    }
}