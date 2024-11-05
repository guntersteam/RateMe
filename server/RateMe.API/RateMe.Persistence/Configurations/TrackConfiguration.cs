using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<TrackEntity>
{
   public void Configure(EntityTypeBuilder<TrackEntity> builder)
   {
      builder.HasKey(t => t.TrackId);

      builder.HasMany(t => t.Reviews)
         .WithOne(r => r.Track)
         .HasForeignKey(r => r.TrackId);

      builder.HasMany(t => t.UserTracks)
         .WithOne(ut => ut.Track)
         .HasForeignKey(ut => ut.TrackId);
   }
}