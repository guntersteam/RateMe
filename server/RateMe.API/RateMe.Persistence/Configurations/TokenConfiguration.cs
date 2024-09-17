using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Configurations;

public class TokenConfiguration : IEntityTypeConfiguration<TokenEntity>
{
   public void Configure(EntityTypeBuilder<TokenEntity> builder)
   {
      builder.HasKey(t => t.TokenId);

      builder.HasOne(t => t.User)
         .WithOne(u => u.Token)
         .HasForeignKey<TokenEntity>(t => t.UserId);
   }
}