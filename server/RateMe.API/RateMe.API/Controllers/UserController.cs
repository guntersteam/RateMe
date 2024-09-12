using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;
using RateMe.API.Contracts.Users;
using RateMe.Core.Abstractions.Services;

namespace RateMe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   private readonly IUserService _userService;


   public UserController(IUserService userService)
   {
      _userService = userService;
   }

   [HttpPost("register")]
   public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
   {
      await _userService.Register(request.UserName, request.Email, request.Password);

      return Ok();
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
   {
      var token = await _userService.Login(request.UserName,request.Password);
      
      HttpContext.Response.Cookies.Append("tasty-cookies",token);
         
      return Ok(token);

   }
}