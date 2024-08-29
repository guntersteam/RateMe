using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
   public void Configure(EntityTypeBuilder<UserEntity> builder)
   {
      builder.HasKey(u => u.Id);

      builder.HasMany(u => u.UserTracks)
         .WithOne(ut => ut.User)
         .HasForeignKey(ut => ut.UserId);

      builder.HasMany(u => u.Reviews)
         .WithOne(r => r.User)
         .HasForeignKey(r => r.UserId);

      builder.HasMany(u => u.PlayLists)
         .WithOne(p => p.User)
         .HasForeignKey(p => p.UserId);
   }
}