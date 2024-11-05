using RateMe.Application.Contracts.Token;
using RateMe.Application.Contracts.User;

namespace RateMe.Application.Interfaces.Services;

public interface IAuthService
{
   Task Register(string userName,string email, string password);
   Task<LoginResponse> Login(string userName, string password);
   Task<LoginResponse> Refresh(RefreshTokenModel tokenModel);
}