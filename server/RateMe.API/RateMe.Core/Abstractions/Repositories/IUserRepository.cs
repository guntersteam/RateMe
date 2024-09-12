using RateMe.Core.Models;

namespace RateMe.Core.Abstractions;

public interface IUserRepository : IRepository<User>
{
   Task<User?> GetByEmail(string userName);
}