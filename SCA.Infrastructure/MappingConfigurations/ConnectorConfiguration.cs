using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.MappingConfigurations;

public class ConnectorConfiguration : IEntityTypeConfiguration<Connector>
{
    public void Configure(EntityTypeBuilder<Connector> builder)
    {
        builder.ToTable("Connector");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.OwnsOne(e => e.MaxCurrentInAmps, ba =>
        {
            ba.Property(p => p.Value).IsRequired().HasColumnName("MaxCurrent"); ;
        });

        builder.HasIndex(c => new { c.Id, c.ChargeStationId })
            .IsUnique();

        builder.HasOne(c => c.ChargeStation)
            .WithMany(cs => cs.Connectors)
            .HasForeignKey(c => c.ChargeStationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(c => c.ChargeStationId);


    }
}