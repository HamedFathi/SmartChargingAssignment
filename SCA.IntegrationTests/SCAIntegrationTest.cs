using FluentAssertions;
using SCA.Application.ChargeStations.Commands.Create;
using SCA.Application.Groups.Commands.Create;

namespace SCA.IntegrationTests;

public class SCAIntegrationTest : WebIntegrationTestBase
{
    private readonly IntegrationTestWebAppFactory _factory;

    public SCAIntegrationTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_Add_A_Group_In_Database_Successfully()
    {
        var command = new CreateGroupCommand("my-group", 10);

        var result = await Dispatcher.Send(command);

        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeEmpty("because the GUID should be valid and not empty");

    }

    [Fact]
    public async Task Should_Not_Add_Group_When_Max_Capacity_Exceeded()
    {
        var groupCommand = new CreateGroupCommand("my-group", 10);
        var groupResult = await Dispatcher.Send(groupCommand);

        var connectors = new List<AddConnectorDto>()
        {
            new AddConnectorDto(2),
            new AddConnectorDto(3),
            new AddConnectorDto(4),
            new AddConnectorDto(5),
        };

        var chargeStationCommand =
            new AddChargeStationToGroupCommand(groupResult.Value, "my-chargestation", connectors);

        Func<Task> act = async () => await Dispatcher.Send(chargeStationCommand);
        await act.Should().ThrowAsync<InvalidOperationException>("Group capacity cannot be less than the sum of MaxCurrentInAmps of all Connectors in the Group.");

    }
}