namespace RateMe.Core.Models;

public class Review
{
   public Guid ReviewId { get; }
   public Guid UserId { get; }
   public Guid TrackId { get; }
   public string Comment { get; } = string.Empty;
   public int Rating { get; }
   
   public Review(Guid reviewId, Guid userId, Guid trackId, int rating)
   {
      ReviewId = reviewId;
      UserId = userId;
      TrackId = trackId;
      Rating = rating;
   }
   
   //TODO Create
}