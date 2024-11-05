

using RateMe.Application.Contracts.User;

namespace RateMe.Core.Abstractions.Services;

public interface IUserService
{
   Task Register(string userName,string email, string password);
   Task<LoginResponse> Login(string userName, string password);
}