using System.Security.Claims;
using RateMe.Core.Models;

namespace RateMe.Application.Interfaces.Auth;

public interface IJwtProvider
{
   string Generate(User user);
   ClaimsPrincipal GetPrincipal(string accessToken);
}