using System.ComponentModel.DataAnnotations.Schema;
using RateMe.Core.Models;

namespace RateMe.Persistence.Entities;

public class ReviewEntity
{
   public Guid ReviewId { get; set; }
   public Guid UserId { get; set; }
   public UserEntity? User { get; set; }
   public Guid TrackId { get; set; }
   public TrackEntity? Track { get; set; } 
   public string Comment { get; set; } = string.Empty;
   public int Rating { get; set; }
   
}