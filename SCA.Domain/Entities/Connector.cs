using HamedStack.TheAggregateRoot;
using SCA.Domain.ValueObjects;

namespace SCA.Domain.Entities;

public class Connector : Entity<int>
{
    public MaxCurrentInAmps MaxCurrentInAmps { get; private set; }

    public Guid ChargeStationId { get; set; }
    public ChargeStation? ChargeStation { get; set; }

    private Connector()
    {

    }
    public Connector(int id, int maxCurrentInAmps)
    {
        if (id is < 1 or > 5) throw new ArgumentException("Connector Id must be between 1 and 5.", nameof(id));
        if (maxCurrentInAmps <= 0) throw new ArgumentException("Max current in Amps must be greater than zero.", nameof(maxCurrentInAmps));

        Id = id;
        MaxCurrentInAmps = new MaxCurrentInAmps(maxCurrentInAmps);
    }

    public void UpdateMaxCurrentInAmps(int newMaxCurrentInAmps)
    {
        if (newMaxCurrentInAmps <= 0) throw new ArgumentException("Max current in Amps must be greater than zero.", nameof(newMaxCurrentInAmps));
        MaxCurrentInAmps = new MaxCurrentInAmps(newMaxCurrentInAmps);

        ChargeStation?.Group?.ValidateCapacity();
    }
}