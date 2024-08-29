using RateMe.Core.Models;

namespace RateMe.Persistence.Entities;

public class PlayListTrackEntity
{
   public Guid PlayListTrackId { get; set; }
   public Guid PlayListId { get; set; }
   public PlayListEntity? PlayList { get; set; }
   public Guid TrackId { get; set; }
   public TrackEntity? Track { get; set; }
}