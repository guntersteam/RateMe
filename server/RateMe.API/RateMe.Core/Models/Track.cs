namespace RateMe.Core.Models;

public class Track
{
   public const string GENRE_SEPARATOR = "||";
   public Guid TrackId { get; }
   public string TrackName { get; } = string.Empty;
   public string ArtistName { get; } = string.Empty;
   public TimeSpan Duration { get; } //TODO TimeSpan??
   public string TrackLogoUrl { get; }
   public double AvarageRating { get; } = default(double);
   public string Genre { get; } = string.Empty;

   public Track(Guid trackId, string trackName, string artistName, TimeSpan duration, string trackLogoUrl,
      double avarageRating, string genre)
   {
      TrackId = trackId;
      TrackName = trackName;
      ArtistName = artistName;
      Duration = duration;
      TrackLogoUrl = trackLogoUrl;
      AvarageRating = avarageRating;
      Genre = genre;
   }

   public static (Track, string Error) Create(Guid trackId, string trackName, string artistName, TimeSpan duration,
      string trackLogoUrl, double avarageRating, string genre)
   {
      var error = string.Empty;
      
      if (string.IsNullOrEmpty(trackName))
      {
         error = "Track name can't be empty";
      }

      if (genre.Split(GENRE_SEPARATOR).Length < 1)
      {
         error = "Track can't be without genre";
      }

      var track = new Track(trackId, trackName, artistName, duration, trackLogoUrl, avarageRating, genre);

      return (track, error);
      
   }
}