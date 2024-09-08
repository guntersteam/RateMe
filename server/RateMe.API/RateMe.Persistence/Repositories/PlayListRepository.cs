using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class PlayListRepository : IRepository<PlayListEntity>
{
   private readonly RateMeDbContext _context;

   public PlayListRepository(RateMeDbContext context)
   {
      _context = context;
   }

   public async Task<Guid> Create(PlayListEntity entity)
   {
      var playListEntity = new PlayListEntity
      {
         PlayListId = entity.PlayListId,
         UserId = entity.UserId,
         PlayListName = entity.PlayListName,
         ArtistName = entity.ArtistName,
      };

      await _context.AddAsync(playListEntity);
      await _context.SaveChangesAsync();

      return playListEntity.PlayListId;
   }
   

   public async Task<List<PlayListEntity?>> Get()
   {
      var playlists = await _context.PlayLists.AsNoTracking().ToListAsync();

      return playlists;
   }

   public async Task<Guid> Delete(Guid id)
   {
      await _context.PlayLists
         .Where(p => p.PlayListId == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<PlayListEntity?> GetById(Guid id)
   {
      return await _context.PlayLists
         .AsNoTracking()
         .FirstOrDefaultAsync(p => p.PlayListId == id);
   }
}