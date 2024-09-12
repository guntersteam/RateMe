using RateMe.Core.Models;

namespace RateMe.Application.Interfaces;

public interface IJwtProvider
{
   string Generate(User user);
}