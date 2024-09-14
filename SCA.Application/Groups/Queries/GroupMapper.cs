using SCA.Application.Groups.Queries.Models;
using SCA.Domain.Entities;

namespace SCA.Application.Groups.Queries;

public static class GroupMapper
{
    public static IEnumerable<GroupModel> MapToGroupModels(IEnumerable<Group> groups)
    {
        if (groups == null) throw new ArgumentNullException(nameof(groups));

        return groups.Select(MapToGroupModel).ToList();
    }

    public static GroupModel MapToGroupModel(Group group)
    {
        if (group == null) throw new ArgumentNullException(nameof(group));

        return new GroupModel
        {
            Name = group.Name.Value,
            Capacity = group.CapacityInAmps.Value,
            ChargeStations = group.ChargeStations.Select(MapToChargeStationModel).ToList()
        };
    }

    public static ChargeStationModel MapToChargeStationModel(ChargeStation chargeStation)
    {
        if (chargeStation == null) throw new ArgumentNullException(nameof(chargeStation));

        return new ChargeStationModel
        {
            Name = chargeStation.Name.Value,
            Connectors = chargeStation.Connectors.Select(MapToConnectorModel).ToList()
        };
    }

    public static ConnectorModel MapToConnectorModel(Connector connector)
    {
        if (connector == null) throw new ArgumentNullException(nameof(connector));

        return new ConnectorModel
        {
            MaxCurrent = connector.MaxCurrentInAmps.Value
        };
    }
}