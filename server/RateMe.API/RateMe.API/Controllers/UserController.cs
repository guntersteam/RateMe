using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using RateMe.API.Contracts.Users;
using RateMe.Application.Contracts.Token;
using RateMe.Application.Interfaces.Services;
using RateMe.Core.Abstractions.Services;

namespace RateMe.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;
   private readonly ITokenService _tokenService;
   private readonly IAuthService _authService;

   public UserController(IUserService userService, ITokenService tokenService,IAuthService authService)
   {
      _userService = userService;
      _tokenService = tokenService;
      _authService = authService;
   }

   [HttpPost("register")]
   public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
   {
      await _authService.Register(request.UserName, request.Email, request.Password);

      return Ok();
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
   {
      var token = await _authService.Login(request.UserName,request.Password);
      
      HttpContext.Response.Cookies.Append("tasty-cookies",token.JwtToken);
         
      return Ok(token);

   }

   [HttpPost("refresh")]
   public async Task<IActionResult> Refresh(RefreshTokenModel refreshTokenModel)
   {
      var loginResult = await _tokenService.Refresh(refreshTokenModel);

      if (loginResult.IsLoggedIn)
      {
         HttpContext.Response.Cookies.Append("tasty-cookies", loginResult.JwtToken);
         return Ok(loginResult);
      }
      
      return Unauthorized();
   }
}