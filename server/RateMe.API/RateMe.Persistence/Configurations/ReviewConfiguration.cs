using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
   public void Configure(EntityTypeBuilder<ReviewEntity> builder)
   {
      builder.HasKey(r => r.ReviewId);

      builder.HasOne(r => r.User)
         .WithMany(u => u.Reviews);

      builder.HasOne(r => r.Track)
         .WithMany(t => t.Reviews);
   }
}