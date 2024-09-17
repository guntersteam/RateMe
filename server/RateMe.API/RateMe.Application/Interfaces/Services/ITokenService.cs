using RateMe.Application.Contracts.Token;
using RateMe.Application.Contracts.User;

namespace RateMe.Application.Interfaces.Services;

public interface ITokenService
{
   string GenerateRefreshTokenString();
   Task<LoginResponse> Refresh(RefreshTokenModel tokenModel);
   Task AddOrUpdateToken(Guid userId, string value, DateTime expiresDate);
}