namespace RateMe.Core.Abstractions.Services;

public interface IUserService
{
   Task Register(string userName,string email, string password);
   Task<string> Login(string userName, string password);
}