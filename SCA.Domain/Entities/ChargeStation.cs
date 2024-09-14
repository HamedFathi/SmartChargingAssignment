using HamedStack.TheAggregateRoot;
using SCA.Domain.ValueObjects;

namespace SCA.Domain.Entities;

public class ChargeStation : Entity<Guid>
{
    public Name Name { get; set; }

    public Guid GroupId { get; set; }
    public Group? Group { get; set; } = null!;

    private readonly List<Connector> _connectors = new();
    public IReadOnlyList<Connector> Connectors => _connectors.AsReadOnly();

    private ChargeStation()
    {
    }
    public ChargeStation(Guid id, string name, IEnumerable<Connector> connectors)
    {
        if (id == Guid.Empty) throw new ArgumentException("Id cannot be empty.", nameof(id));
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (connectors == null) throw new ArgumentNullException(nameof(connectors));

        Id = id;
        Name = new Name(name);

        var connectorList = connectors.ToList();
        if (connectorList.Count is < 1 or > 5)
        {
            throw new ArgumentException("ChargeStation must have at least 1 but not more than 5 Connectors.", nameof(connectors));
        }

        var connectorIds = new HashSet<int>();
        foreach (var connector in connectorList)
        {
            if (connectorIds.Contains(connector.Id))
            {
                throw new ArgumentException($"Duplicate Connector Id {connector.Id} in ChargeStation.", nameof(connectors));
            }
            if (connector.Id is < 1 or > 5)
            {
                throw new ArgumentException("Connector Id must be between 1 and 5.", nameof(connectors));
            }
            connectorIds.Add(connector.Id);
            connector.ChargeStation = this;
        }
        _connectors.AddRange(connectorList);
    }

    public void AddConnector(Connector connector)
    {
        if (connector == null) throw new ArgumentNullException(nameof(connector));
        if (_connectors.Count >= 5) throw new InvalidOperationException("Cannot have more than 5 Connectors.");
        if (_connectors.Any(c => c.Id == connector.Id))
        {
            throw new InvalidOperationException($"Connector with Id {connector.Id} already exists in this ChargeStation.");
        }
        if (connector.Id is < 1 or > 5)
        {
            throw new ArgumentException("Connector Id must be between 1 and 5.", nameof(connector));
        }

        _connectors.Add(connector);
        connector.ChargeStation = this;

        Group?.ValidateCapacity();
    }

    public void RemoveConnector(Connector connector)
    {
        if (connector == null) throw new ArgumentNullException(nameof(connector));
        if (!_connectors.Contains(connector))
        {
            throw new InvalidOperationException("Connector not found in the ChargeStation.");
        }

        _connectors.Remove(connector);
        connector.ChargeStation = null;

        // Since Connector cannot exist without a ChargeStation, it's effectively deleted

        Group?.ValidateCapacity();
    }

    public void UpdateConnector(Connector connector)
    {
        var currentConnector = _connectors.Single(c => c.Id == connector.Id);
        RemoveConnector(currentConnector);
        AddConnector(connector);
    }

    public void UpdateName(string newName)
    {
        Name = new Name(newName);
    }
    internal void Delete()
    {
        foreach (var connector in _connectors)
        {
            connector.ChargeStation = null;
            // Connector is effectively deleted
        }
        _connectors.Clear();
        // ChargeStation is effectively deleted
    }
}
