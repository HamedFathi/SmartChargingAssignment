using System.Net;
using FluentAssertions;
using SCA.Application.ChargeStations.Commands.Create;
using SCA.Application.Groups.Commands.Create;
using SCA.Domain.Exceptions;

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
            new(2),
            new(3),
            new(4),
            new(5),
        };

        var chargeStationCommand =
            new AddChargeStationToGroupCommand(groupResult.Value, "my-chargestation", connectors);

        Func<Task> act = async () => await Dispatcher.Send(chargeStationCommand);
        await act.Should().ThrowAsync<GroupCapacityException>("Group capacity cannot be less than the sum of MaxCurrentInAmps of all Connectors in the Group.");

    }

    [Fact]
    public async Task Should_Delete_Group_Successfully()
    {
        var groupCommand = new CreateGroupCommand("my-group", 10);
        var groupResult = await Dispatcher.Send(groupCommand);

        var response = await HttpClient.DeleteAsync($"/group/{groupResult.Value}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}