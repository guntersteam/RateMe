using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class UserTrackConfiguration : IEntityTypeConfiguration<UserTrackEntity>
{
   public void Configure(EntityTypeBuilder<UserTrackEntity> builder)
   {
      builder.HasKey(ut => ut.UserTrackId);

      builder.HasOne(ut => ut.User)
         .WithMany(u => u.UserTracks)
         .HasForeignKey(ut => ut.UserId);

      builder.HasOne(ut => ut.Track)
         .WithMany(t => t.UserTracks)
         .HasForeignKey(ut => ut.TrackId);
   }
}