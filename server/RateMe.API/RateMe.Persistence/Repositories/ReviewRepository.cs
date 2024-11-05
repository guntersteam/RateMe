using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class ReviewRepository : IRepository<ReviewEntity>
{
   private readonly RateMeDbContext _context;

   public ReviewRepository(RateMeDbContext context)
   {
      _context = context;
   }
   
   public async Task<Guid> Create(ReviewEntity entity)
   {
      var reviewEntity = new ReviewEntity
      {
         ReviewId = entity.ReviewId,
         UserId = entity.UserId,
         TrackId = entity.TrackId,
         Comment = entity.Comment,
         Rating = entity.Rating

      };

      await _context.AddAsync(reviewEntity);
      await _context.SaveChangesAsync();

      return reviewEntity.ReviewId;
   }

   public async Task<List<ReviewEntity?>> Get()
   {
      var reviews = await _context.Reviews
         .AsNoTracking()
         .ToListAsync();

      return reviews;
   }

   public async Task<Guid> Delete(Guid id)
   {
      await _context.Reviews
         .Where(r => r.ReviewId == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<ReviewEntity?> GetById(Guid id)
   {
      return await _context.Reviews
         .AsNoTracking()
         .FirstOrDefaultAsync(r => r.ReviewId == id);
   }
}