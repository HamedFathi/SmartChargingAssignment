namespace SCA.Application.Groups.Queries.Models;

public class ChargeStationModel
{
    public string Name { get; set; } = null!;
    public ICollection<ConnectorModel> Connectors { get; set; } = new List<ConnectorModel>();
}