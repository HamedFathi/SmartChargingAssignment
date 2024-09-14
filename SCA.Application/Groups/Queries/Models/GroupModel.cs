namespace SCA.Application.Groups.Queries.Models;

public class GroupModel
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public IList<ChargeStationModel> ChargeStations { get; set; } = new List<ChargeStationModel>();
}