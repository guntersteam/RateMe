using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class PlayListConfiguration : IEntityTypeConfiguration<PlayListEntity>
{
   public void Configure(EntityTypeBuilder<PlayListEntity> builder)
   {
      builder.HasKey(p => p.PlayListId);

      builder.HasOne(p => p.User)
         .WithMany(u => u.PlayLists)
         .HasForeignKey(p => p.UserId);

      builder.HasMany(p => p.PlayListTracks)
         .WithOne(plt => plt.PlayList)
         .HasForeignKey(p => p.PlayListId);
   }
}