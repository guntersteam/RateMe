namespace RateMe.Application.Interfaces.Services;

public interface ITrackService
{
   Task CreateTrack(string TrackName, string ArtistName, TimeSpan Duration, string TrackLogoUrl, string Genre,
      double AverageRating = 0);
}