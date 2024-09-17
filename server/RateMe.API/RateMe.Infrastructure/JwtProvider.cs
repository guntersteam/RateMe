using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateMe.Application.Interfaces;
using RateMe.Application.Interfaces.Auth;
using RateMe.Core.Models;

namespace RateMe.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
   private readonly JwtOptions _options = options.Value;

   public string Generate(User user)
   {
      Claim[] claims = [new("userId", user.Id.ToString())];
      var signingCredentials = new SigningCredentials(
         new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
         SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
         claims: claims,
         signingCredentials: signingCredentials,
         expires: DateTime.UtcNow.AddHours(_options.ExpiresDuration));

      var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

      return tokenValue;
   }

   public ClaimsPrincipal GetPrincipal(string accessToken)
   {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
      
      var validation = new TokenValidationParameters
      {
         IssuerSigningKey = securityKey,
         ValidateIssuer = false,
         ValidateAudience = false,
         ValidateLifetime = false,
         ValidateIssuerSigningKey = true
      };

      return new JwtSecurityTokenHandler().ValidateToken(accessToken, validation, out _);
   }

}