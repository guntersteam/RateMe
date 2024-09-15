using System.Runtime.Intrinsics.Arm;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RateMe.Infrastructure;

namespace RateMe.API.Extensions;

public static class ApiExtension
{
   public static void AddApiAuthentication(this IServiceCollection services,
      IConfiguration configuration)
   {
      var jwtOptions = new JwtOptions();
      configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
            };

            options.Events = new JwtBearerEvents
            {
               OnMessageReceived = context =>
               {
                  context.Token = context.Request.Cookies["tasty-cookies"];

                  return Task.CompletedTask;
               }
            };
         });

      services.AddAuthorization();
   }
}