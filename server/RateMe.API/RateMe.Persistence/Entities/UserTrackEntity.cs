using System.ComponentModel.DataAnnotations.Schema;

namespace RateMe.Persistence.Entities;

public class UserTrackEntity
{
   public Guid UserTrackId { get; set; }
   public Guid UserId { get; set; }
   public UserEntity User { get; set; }
   public Guid TrackId { get; set; }
   public TrackEntity Track { get; set; }
   

}