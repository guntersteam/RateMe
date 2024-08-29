using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RateMe.Core.Models;
using RateMe.Persistence.Configurations;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence;

public class RateMeDbContext(DbContextOptions<RateMeDbContext> options) 
   : DbContext(options)
{

   public DbSet<UserEntity> Users { get; set; }
   public DbSet<TrackEntity> Tracks { get; set; }
   public DbSet<ReviewEntity> Reviews { get; set; }
   public DbSet<UserTrackEntity> UserTracks { get; set; }
   public DbSet<PlayListEntity> PlayLists { get; set; }
   public DbSet<PlayListTrackEntity> PlayListTracks { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.ApplyConfiguration(new ReviewConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new TrackConfiguration());
      modelBuilder.ApplyConfiguration(new PlayListConfiguration());
      modelBuilder.ApplyConfiguration(new UserTrackConfiguration());
      modelBuilder.ApplyConfiguration(new PlayListTrackConfiguration());
   }
}

