using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.MappingConfigurations;

public class ChargeStationConfiguration : IEntityTypeConfiguration<ChargeStation>
{
    public void Configure(EntityTypeBuilder<ChargeStation> builder)
    {
        builder.ToTable("ChargeStation");
        builder.HasKey(cs => cs.Id);
        builder.Property(cs => cs.Id).ValueGeneratedNever();

        builder.OwnsOne(e => e.Name, ba =>
        {
            ba.Property(p => p.Value).IsRequired().HasColumnName("Name").HasMaxLength(100);
        });

        builder.HasMany(cs => cs.Connectors)
            .WithOne(c => c.ChargeStation)
            .HasForeignKey(c => c.ChargeStationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.Group)
            .WithMany(g => g.ChargeStations)
            .HasForeignKey(cs => cs.GroupId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(cs => cs.GroupId).IsRequired();

        builder.HasIndex(cs => cs.GroupId).IsUnique(false);
    }
}