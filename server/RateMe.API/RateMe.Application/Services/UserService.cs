using RateMe.Application.Contracts.User;
using RateMe.Application.Interfaces.Auth;
using RateMe.Application.Interfaces.Services;
using RateMe.Core.Abstractions;
using RateMe.Core.Abstractions.Services;
using RateMe.Core.Models;

namespace RateMe.Application.Services;

public class UserService : IUserService
{
   private readonly IPasswordHasher _passwordHasher;
   private readonly IUserRepository _userRepository;
   private readonly IJwtProvider _jwtProvider;
   private readonly ITokenService _tokenService;

   public UserService(IPasswordHasher passwordHasher,IUserRepository userRepository, IJwtProvider jwtProvider, ITokenService tokenService)
   {
      _passwordHasher = passwordHasher;
      _userRepository = userRepository;
      _jwtProvider = jwtProvider;
      _tokenService = tokenService;
   }

   public async Task Register(string userName,string email, string password)
   {
      var passwordHash = _passwordHasher.Generate(password);

      var userResult = User.Create(Guid.NewGuid(), userName: userName, email: email, hashPassword: passwordHash);

      if (string.IsNullOrEmpty(userResult.Error))
      {
         throw new Exception("Cannot create user model");
      }

      await _userRepository.Create(userResult.user);
      
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
   
}