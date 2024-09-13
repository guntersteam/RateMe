using RateMe.Application.Contracts.User;
using RateMe.Application.Interfaces;
using RateMe.Application.Interfaces.Auth;
using RateMe.Core.Abstractions;
using RateMe.Core.Abstractions.Services;
using RateMe.Core.Models;
using RateMe.Persistence.Repositories;

namespace RateMe.Application.Services;

public class UserService : IUserService
{
   private readonly IPasswordHasher _passwordHasher;
   private readonly IUserRepository _userRepository;
   private readonly IJwtProvider _jwtProvider;

   public UserService(IPasswordHasher passwordHasher,IUserRepository userRepository, IJwtProvider jwtProvider)
   {
      _passwordHasher = passwordHasher;
      _userRepository = userRepository;
      _jwtProvider = jwtProvider;
   }

   public async Task Register(string userName,string email, string password)
   {
      var passwordHash = _passwordHasher.Generate(password);

      var userResult = User.Create(Guid.NewGuid(), userName: userName, email: email, hashPassword: passwordHash);

      if (string.IsNullOrEmpty(userResult.Error))
      {
         //TODO: intercept
      }

      await _userRepository.Create(userResult.user);
      
   }

   public async Task<string> Login(string userName, string password)
   {
      var response = new LoginResponse();
      var candidate = await _userRepository.GetByEmail(userName);

      var passwordVerifyResult = _passwordHasher.Verify(password, candidate.HashPassword);

      if (candidate == null && passwordVerifyResult == false)
      {
         throw new Exception("Failed to login, Incorrect password");
      }
      
      var token = _jwtProvider.Generate(candidate);

      response.IsLoggedIn = true;
      response.JwtToken = token;
      response.RefreshToken = string.Empty;
      
      return token;
   }
   
}