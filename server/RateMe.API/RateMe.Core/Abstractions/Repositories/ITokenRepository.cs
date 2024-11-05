using RateMe.Core.Models;

namespace RateMe.Core.Abstractions;

public interface ITokenRepository : IRepository<Token>
{
   Task<Token?> GetByUserId(Guid id);
   Task<Guid> Update(Guid id, string tokenValue, DateTime expiresDate);
}