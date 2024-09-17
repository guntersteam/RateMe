using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class TokenRepository : ITokenRepository
{
   private readonly RateMeDbContext _context;

   public TokenRepository(RateMeDbContext context)
   {
      _context = context;
   }


   public async Task<Guid> Create(Token entity)
   {
      var tokenEntity = new TokenEntity
      {
         ExpiresDate = entity.ExpiresDate,
         TokenId = entity.TokenId,
         TokenValue = entity.TokenString,
         UserId = entity.UserId
      };

      await _context.AddAsync(tokenEntity);
      await _context.SaveChangesAsync();

      return tokenEntity.TokenId;
   }

   public async Task<List<Token?>> Get()
   {
      var tokens = await _context.Tokens
         .AsNoTracking()
         .Select(t => Token.Create(t.TokenId, t.UserId, t.TokenValue, t.ExpiresDate).token)
         .ToListAsync();

      return tokens;
   }

   public async Task<Guid> Update(Guid tokenId, string tokenValue, DateTime expiresTime)
   {
      await _context.Tokens
         .Where(t => t.TokenId == tokenId)
         .ExecuteUpdateAsync(s => s
               .SetProperty(t => t.TokenValue, t => tokenValue)
               .SetProperty(t => t.ExpiresDate, t => expiresTime)
            );
      
      return tokenId;
   }

   public async Task<Guid> Delete(Guid id)
   {
      await _context.Tokens
         .Where(t => t.TokenId == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<Token?> GetById(Guid id)
   {
      return await _context.Tokens
         .AsNoTracking()
         .Select(t => Token.Create(t.TokenId, t.UserId, t.TokenValue, t.ExpiresDate).token)
         .FirstOrDefaultAsync(t => t.TokenId == id);
   }

   public async Task<Token?> GetByUserId(Guid id)
   {
      var tokens = await _context.Tokens
         .Select(t => Token.Create(t.TokenId, t.UserId, t.TokenValue, t.ExpiresDate).token)
         .ToListAsync();

      return tokens.FirstOrDefault(t => t.UserId == id);
   }
}