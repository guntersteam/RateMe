namespace RateMe.Persistence.Entities;

public class TrackEntity
{
   public Guid TrackId { get; set; }
   public string TrackName { get; set; } = string.Empty;
   public string ArtistName { get; set; } = string.Empty;
   public TimeSpan Duration { get; set; } 
   public string TrackLogoUrl { get; set; } = string.Empty;
   public double AvarageRating { get; set; } = default(double);
   public string Genre { get; set; } = string.Empty;
   public List<ReviewEntity> Reviews { get; set; } = [];
   public List<UserTrackEntity> UserTracks { get; set; } = [];
   public List<PlayListTrackEntity> PlayListTracks { get; set; } = [];
}