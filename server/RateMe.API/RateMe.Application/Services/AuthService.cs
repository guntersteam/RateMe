using RateMe.Application.Contracts.Token;
using RateMe.Application.Contracts.User;
using RateMe.Application.Interfaces.Auth;
using RateMe.Application.Interfaces.Services;
using RateMe.Core.Abstractions;
using RateMe.Core.Models;

namespace RateMe.Application.Services;

public class AuthService: IAuthService
{
   private readonly IPasswordHasher _passwordHasher;
   private readonly IUserRepository _userRepository;
   private readonly IJwtProvider _jwtProvider;
   private readonly ITokenService _tokenService;
   private readonly ITokenRepository _tokenRepository;

   public AuthService(IPasswordHasher passwordHasher, IUserRepository userRepository
      ,IJwtProvider jwtProvider, ITokenService tokenService, ITokenRepository tokenRepository)
   {
      _passwordHasher = passwordHasher;
      _userRepository = userRepository;
      _jwtProvider = jwtProvider;
      _tokenService = tokenService;
      _tokenRepository = tokenRepository;
   }
   
   public async Task Register(string userName, string email, string password)
   {
      var passwordHash = _passwordHasher.Generate(password);

      var userResult = User.Create(Guid.NewGuid(), userName: userName, email: email, hashPassword: passwordHash);

      if (string.IsNullOrEmpty(userResult.Error))
      {
         throw new Exception("Cannot create user model");
      }

      await _userRepository.Create(userResult.user);
      // TODO: add sending activation link on user mail
   }

   public async Task<LoginResponse> Login(string userName, string password)
   {
      var response = new LoginResponse();
      var candidate = await _userRepository.GetByEmail(userName);

      if (candidate == null)
      {
         throw new Exception("user wasn't found");
      }

      var passwordVerifyResult = _passwordHasher.Verify(password, candidate.HashPassword);

      if (!passwordVerifyResult)
      {
         throw new Exception("Failed to login, Incorrect password");
      }
      
      var token = _jwtProvider.Generate(candidate);

      response.IsLoggedIn = true;
      response.JwtToken = token;
      response.RefreshToken = _tokenService.GenerateRefreshTokenString();
      

      await _tokenService.AddOrUpdateToken(candidate.Id, response.RefreshToken, DateTime.UtcNow.AddDays(15));
      
      return response;
   }

   public async Task<LoginResponse> Refresh(RefreshTokenModel tokenModel)
   {
      var principal = _jwtProvider.GetPrincipal(tokenModel.JwtToken);

      var userId = principal.Claims.FirstOrDefault(t => t.Type == "userId");
      
      var loginResponse = new LoginResponse();

      if (userId is null)
         return loginResponse;

      var refreshToken = await _tokenRepository.GetByUserId(Guid.Parse(userId.Value));


      if (refreshToken is null || refreshToken.TokenString != tokenModel.RefreshToken ||
          refreshToken.ExpiresDate < DateTime.Now)
         return loginResponse;

      var user = await _userRepository.GetById(refreshToken.UserId);
      
      var token = _jwtProvider.Generate(user);

      loginResponse.IsLoggedIn = true;
      loginResponse.JwtToken = token;
      loginResponse.RefreshToken = _tokenService.GenerateRefreshTokenString();
      await AddOrUpdateToken(user.Id, loginResponse.RefreshToken, DateTime.UtcNow.AddDays(15));
      return loginResponse;
   }

   private async Task AddOrUpdateToken(Guid userId, string value, DateTime expiresDate)
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
}