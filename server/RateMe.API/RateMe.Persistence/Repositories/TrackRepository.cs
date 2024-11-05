using System.Globalization;
using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class TrackRepository : ITrackRepository
{
   private readonly RateMeDbContext _context;

   public TrackRepository(RateMeDbContext context)
   {
      _context = context;
   }

   public async Task<Guid> Create(Track track)
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

   public async Task<List<Track?>> Get()
   {
      var trackEntities = await _context.Tracks
         .AsNoTracking()
         .Select(t => Track.Create(t.TrackId,t.TrackName,t.ArtistName,t.Duration,t.TrackLogoUrl,t.AvarageRating,t.Genre).Track)
         .ToListAsync();

      return trackEntities;
   }

   public async Task<Guid> Delete(Guid id)
   {
      await _context.Tracks
         .Where(t => t.TrackId == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<Track> GetById(Guid id)
   {
       var tracks = await _context.Tracks
         .AsNoTracking()
         .Select(t => Track.Create(t.TrackId,t.TrackName,t.ArtistName,t.Duration,t.TrackLogoUrl,t.AvarageRating,t.Genre).Track)
         .FirstOrDefaultAsync(t => t.TrackId == id);

       return tracks;
   }

   public async Task<List<Track>> GetByPage(int page, int size)
   {
      return await _context.Tracks
         .AsNoTracking()
         .Select(t => Track.Create(t.TrackId,t.TrackName,t.ArtistName,t.Duration,t.TrackLogoUrl,t.AvarageRating,t.Genre).Track)
         .Skip((page - 1) * size)
         .Take(size)
         .ToListAsync();
   }
}