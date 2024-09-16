using System.Security.Cryptography;

namespace RateMe.Application.Services;

public class TokenService
{

   public string GenerateRefreshTokenString()
   {
      var randomNumbers = new byte[64];

      using (var randomNumberGenerator = RandomNumberGenerator.Create())
      {
         randomNumberGenerator.GetBytes(randomNumbers);
      }

      return Convert.ToBase64String(randomNumbers);

   }
}