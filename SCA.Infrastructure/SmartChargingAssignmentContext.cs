using Microsoft.EntityFrameworkCore;
using SCA.Domain.Entities;
using SCA.Infrastructure.MappingConfigurations;
using HamedStack.TheRepository.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
// ReSharper disable ConvertToPrimaryConstructor

namespace SCA.Infrastructure;

public class SmartChargingAssignmentContext : DbContextBase
{
    public SmartChargingAssignmentContext(DbContextOptions<SmartChargingAssignmentContext> options, ILogger<DbContextBase> logger) : base(options, logger)
    {
    }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<ChargeStation> ChargeStations { get; set; }
    public virtual DbSet<Connector> Connectors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GroupConfiguration());
        modelBuilder.ApplyConfiguration(new ChargeStationConfiguration());
        modelBuilder.ApplyConfiguration(new ConnectorConfiguration());

        base.OnModelCreating(modelBuilder);
    }

}