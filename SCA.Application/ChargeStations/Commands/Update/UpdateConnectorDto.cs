namespace SCA.Application.ChargeStations.Commands.Update;

public class UpdateConnectorDto
{
    public UpdateConnectorDto(int id,int maxCurrentInAmps)
    {
        Id = id;
        MaxCurrentInAmps = maxCurrentInAmps;
    }
    public int Id { get; }

    public int MaxCurrentInAmps { get; }
}