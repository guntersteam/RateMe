namespace RateMe.API.Contracts.Users;

public record LoginUserRequest(
   string UserName,
   string Password);