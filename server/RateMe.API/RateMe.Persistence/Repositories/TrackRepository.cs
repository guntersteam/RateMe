using System.Globalization;
using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class TrackRepository : IRepository<TrackEntity>
{
   private readonly RateMeDbContext _context;

   public TrackRepository(RateMeDbContext context)
   {
      _context = context;
   }

   public async Task<Guid> Create(TrackEntity track)
   {
      var trackEntity = new TrackEntity
      {
         TrackId = track.TrackId,
         ArtistName = track.ArtistName,
         AvarageRating = track.AvarageRating,
         Duration = track.Duration,
         Genre = track.Genre,
         TrackLogoUrl = track.TrackLogoUrl,
         TrackName = track.TrackName
      };

      await _context.AddAsync(trackEntity);
      await _context.SaveChangesAsync();

      return trackEntity.TrackId;
   }

   public async Task Update(Guid trackId, string trackName, string artistName, TimeSpan duration,
      string trackLogoUrl, double averageRating, string genre)
   {
      await _context.Tracks
         .Where(t => t.TrackId == trackId)
         .ExecuteUpdateAsync(s => s
               .SetProperty(t => t.TrackName, trackName)
               .SetProperty(t => t.ArtistName, artistName)
               .SetProperty(t => t.Duration, duration)
               .SetProperty(t => t.TrackLogoUrl, trackLogoUrl)
               .SetProperty(t => t.AvarageRating, averageRating)
               .SetProperty(t => t.Genre, genre));
      
      
   }

   public async Task<List<TrackEntity?>> Get()
   {
      var trackEntities = await _context.Tracks.AsNoTracking().ToListAsync();

      return trackEntities;
   }

   public async Task<Guid> Delete(Guid id)
   {
      await _context.Tracks
         .Where(t => t.TrackId == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<TrackEntity?> GetById(Guid id)
   {
      return await _context.Tracks
         .AsNoTracking()
         .FirstOrDefaultAsync(t => t.TrackId == id);
   }

   public async Task<List<TrackEntity>> GetByPage(int page, int size)
   {
      return await _context.Tracks
         .AsNoTracking()
         .Skip((page - 1) * size)
         .Take(size)
         .ToListAsync();
   }
}