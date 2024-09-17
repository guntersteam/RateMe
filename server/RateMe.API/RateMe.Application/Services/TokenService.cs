using System.Security.Cryptography;
using RateMe.Application.Contracts.Token;
using RateMe.Application.Contracts.User;
using RateMe.Application.Interfaces.Auth;
using RateMe.Application.Interfaces.Services;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;
using RateMe.Persistence.Entities;

namespace RateMe.Application.Services;

public class TokenService : ITokenService
{
   private readonly IUserRepository _userRepository;
   private readonly ITokenRepository _tokenRepository;
   private readonly IJwtProvider _jwtProvider;

   public TokenService(IUserRepository userRepository,ITokenRepository tokenRepository,IJwtProvider jwtProvider)
   {
      _userRepository = userRepository;
      _tokenRepository = tokenRepository;
      _jwtProvider = jwtProvider;
   }
   
   public string GenerateRefreshTokenString()
   {
      var randomNumbers = new byte[64];

      using (var randomNumberGenerator = RandomNumberGenerator.Create())
      {
         randomNumberGenerator.GetBytes(randomNumbers);
      }

      return Convert.ToBase64String(randomNumbers);

   }

   public async Task AddOrUpdateToken(Guid userId,string value,DateTime expiresDate)
   {
      var token  = await _tokenRepository.GetByUserId(userId);

      if (token != null)
      {
         await _tokenRepository.Update(token.TokenId, value, expiresDate);
         return;
      }
      
      var tokenResult = Token.Create(Guid.NewGuid(), userId, value, expiresDate);

      if (!string.IsNullOrEmpty(tokenResult.Error))
      {
         throw new Exception("Cannot create token model");
      }

      await _tokenRepository.Create(tokenResult.token);
   }

   public async Task<LoginResponse> Refresh(RefreshTokenModel tokenModel)
   {
      var principal = _jwtProvider.GetPrincipal(tokenModel.JwtToken);

      var loginResponse = new LoginResponse();

      if (principal?.Identity?.Name is null)
         return loginResponse;

      var refreshToken = await _tokenRepository.GetByUserId(Guid.Parse(principal.Identity.Name));


      if (refreshToken is null || refreshToken.TokenString != tokenModel.RefreshToken ||
          refreshToken.ExpiresDate < DateTime.Now)
         return loginResponse;

      var user = await _userRepository.GetById(refreshToken.UserId);
      
      var token = _jwtProvider.Generate(user);

      loginResponse.IsLoggedIn = true;
      loginResponse.JwtToken = token;
      loginResponse.RefreshToken = GenerateRefreshTokenString();

      return loginResponse;
   }
}