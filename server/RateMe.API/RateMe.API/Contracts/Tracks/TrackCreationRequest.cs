namespace RateMe.API.Contracts.Tracks;

public record TrackCreationRequest(
   string TrackName,
   string ArtistName,
   TimeSpan Duration,
   string TrackLogoUrl,
   string Genre);