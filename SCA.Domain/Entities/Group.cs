using HamedStack.TheAggregateRoot;
using SCA.Domain.ValueObjects;

namespace SCA.Domain.Entities;

public class Group : Entity<Guid>
{
    public Name Name { get; set; }
    public CapacityInAmps CapacityInAmps { get; set; }

    private readonly List<ChargeStation> _chargeStations = new List<ChargeStation>();
    public IReadOnlyList<ChargeStation> ChargeStations => _chargeStations.AsReadOnly();

    private Group()
    {
    }
    public Group(Guid id, string name, int capacityInAmps)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (capacityInAmps <= 0) throw new ArgumentException("Capacity in Amps must be greater than zero.", nameof(capacityInAmps));

        Id = id;
        Name = new Name(name);
        CapacityInAmps = new CapacityInAmps(capacityInAmps);
    }

    public void AddChargeStation(ChargeStation chargeStation)
    {
        if (chargeStation == null) throw new ArgumentNullException(nameof(chargeStation));
        if (chargeStation.Group != null) throw new InvalidOperationException("ChargeStation is already assigned to a Group.");
        if (_chargeStations.Contains(chargeStation)) throw new InvalidOperationException("ChargeStation is already in this Group.");

        _chargeStations.Add(chargeStation);
        chargeStation.Group = this;

        ValidateCapacity();
    }

    public void RemoveChargeStation(ChargeStation chargeStation)
    {
        if (chargeStation == null) throw new ArgumentNullException(nameof(chargeStation));
        if (!_chargeStations.Contains(chargeStation)) throw new InvalidOperationException("ChargeStation not found in the Group.");

        _chargeStations.Remove(chargeStation);
        chargeStation.Group = null;

        // Since ChargeStation cannot exist without a Group, it's effectively deleted
        chargeStation.Delete();
    }
    
    public void UpdateCapacityInAmps(int newCapacity)
    {
        if (newCapacity <= 0) throw new ArgumentException("Capacity in Amps must be greater than zero.", nameof(newCapacity));
        CapacityInAmps = new CapacityInAmps(newCapacity);

        ValidateCapacity();
    }

    internal void ValidateCapacity()
    {
        var totalConnectorAmps = _chargeStations.Sum(cs => cs.Connectors.Sum(c => c.MaxCurrentInAmps));
        if (CapacityInAmps < totalConnectorAmps)
        {
            throw new InvalidOperationException("Group capacity cannot be less than the sum of MaxCurrentInAmps of all Connectors in the Group.");
        }
    }
}