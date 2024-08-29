using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class PlayListTrackConfiguration : IEntityTypeConfiguration<PlayListTrackEntity>
{
   public void Configure(EntityTypeBuilder<PlayListTrackEntity> builder)
   {
      builder.HasKey(plt => plt.PlayListTrackId);

      builder.HasOne(plt => plt.PlayList)
         .WithMany(p => p.PlayListTracks)
         .HasForeignKey(plt => plt.PlayListId);

      builder.HasOne(plt => plt.Track)
         .WithMany(t => t.PlayListTracks)
         .HasForeignKey(plt => plt.TrackId);
   }
}