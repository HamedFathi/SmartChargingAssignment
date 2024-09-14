namespace SCA.Application.ChargeStations.Commands.Create;

public class AddConnectorDto
{
    public AddConnectorDto(int maxCurrentInAmps)
    {
        MaxCurrentInAmps = maxCurrentInAmps;
    }

    public int MaxCurrentInAmps { get; }
}