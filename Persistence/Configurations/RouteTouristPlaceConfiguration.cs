using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal sealed class RouteTouristPlaceConfiguration : IEntityTypeConfiguration<RouteTouristPlace>
{
    public void Configure(EntityTypeBuilder<RouteTouristPlace> builder)
    {
        builder.ToTable(nameof(RouteTouristPlace));

        builder.HasKey(rtp => rtp.Id);

        builder.Property(rtp => rtp.Id).ValueGeneratedOnAdd();

        builder.Property(rtp => rtp.Sequence).IsRequired();

        builder.Property(rtp => rtp.VisitDate).IsRequired();
        
        builder.HasOne(rtp => rtp.Routes)
            .WithMany()
            .HasForeignKey(rtp => rtp.RouteId);

        builder.HasOne(rtp => rtp.TouristPlaces)
            .WithMany()
            .HasForeignKey(rtp => rtp.TouristPlaceId);
    }
}
