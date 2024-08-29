namespace RateMe.Core.Models;

public class PlayList
{
   public Guid PlayListId { get; }
   public Guid UserId { get; }
   public string PlayListName { get; } = string.Empty;
   public string ArtistName { get; } = string.Empty;

   public PlayList(Guid playListId,Guid userId,string playListName,string artistName)
   {
      PlayListId = playListId;
      UserId = userId;
      PlayListName = playListName;
      ArtistName = artistName;
   }

   public (PlayList, string Error) Create(Guid playListId, Guid userId, string playListName, string artistName)
   {
      var error = string.Empty;

      if (string.IsNullOrEmpty(playListName))
      {
         error = "Playlist can't be added without name";
      }

      var playList = new PlayList(playListId, userId, playListName, artistName);

      return (playList, error);
   }
   
}