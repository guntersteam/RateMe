using RateMe.Application.Interfaces.Services;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;

namespace RateMe.Application.Services;

public class TrackService : ITrackService
{
   private readonly ITrackRepository _trackRepository;

   public TrackService(ITrackRepository trackRepository)
   {
      _trackRepository = trackRepository;
   }

   public async Task CreateTrack(string TrackName, string ArtistName, TimeSpan Duration, string TrackLogoUrl, string Genre,
      double AverageRating = 0)
   {
      var trackResult = Track.Create(Guid.NewGuid(), TrackName, ArtistName, Duration, TrackLogoUrl, AverageRating,
         Genre);

      if (!string.IsNullOrEmpty(trackResult.Error))
      {
         //TODO: change error handling
         throw new Exception("An error occured while creating track model");
      }

      await _trackRepository.Create(trackResult.Track);

   }
}