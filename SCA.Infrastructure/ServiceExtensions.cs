using HamedStack.TheAggregateRoot.Events;
using HamedStack.TheRepository;
using Microsoft.Extensions.DependencyInjection;
using HamedStack.TheRepository.EntityFrameworkCore;
using SCA.Domain.Entities;
using SCA.Infrastructure.Repositories;

namespace SCA.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContextBase
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<TDbContext>();
        services.AddScoped<DbContextBase>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped(typeof(IRepository<Group>), typeof(GroupRepository));
        services.AddScoped(typeof(IRepository<ChargeStation>), typeof(ChargeStationRepository));
        services.AddScoped(typeof(IRepository<Connector>), typeof(ConnectorRepository));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}