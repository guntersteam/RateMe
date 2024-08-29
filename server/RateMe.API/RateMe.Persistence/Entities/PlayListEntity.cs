using System.ComponentModel.DataAnnotations.Schema;

namespace RateMe.Persistence.Entities;

public class PlayListEntity
{
   public Guid PlayListId { get; set; }
   public Guid UserId { get; set; }
   public UserEntity User { get; set; }
   public string PlayListName { get; set; } = string.Empty;
   public string ArtistName { get; set; } = string.Empty;
   public List<PlayListTrackEntity> PlayListTracks { get; set; } = [];
}