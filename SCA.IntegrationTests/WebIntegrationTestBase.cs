using HamedStack.CQRS;
using Microsoft.Extensions.DependencyInjection;
using SCA.Infrastructure;

namespace SCA.IntegrationTests;

public abstract class WebIntegrationTestBase : IClassFixture<IntegrationTestWebAppFactory>
{
    public HttpClient HttpClient { get; }
    protected ICommandQueryDispatcher Dispatcher { get; }
    protected SmartChargingAssignmentContext DbContext { get; }

    protected WebIntegrationTestBase(IntegrationTestWebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        Dispatcher = scope.ServiceProvider.GetRequiredService<ICommandQueryDispatcher>();
        DbContext = scope.ServiceProvider.GetRequiredService<SmartChargingAssignmentContext>();
        HttpClient = factory.CreateClient();
    }
}