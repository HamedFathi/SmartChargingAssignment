using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SCA.Domain.Entities;

namespace SCA.Infrastructure.MappingConfigurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Group");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Id).ValueGeneratedNever();

        builder.OwnsOne(e => e.Name, ba =>
        {
            ba.Property(p => p.Value).IsRequired().HasMaxLength(100).HasColumnName("Name");
        });

        builder.OwnsOne(e => e.CapacityInAmps, ba =>
        {
            ba.Property(p => p.Value).IsRequired().HasColumnName("Capacity");
        });

        builder.HasMany(g => g.ChargeStations)
            .WithOne(cs => cs.Group)
            .HasForeignKey(cs => cs.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}