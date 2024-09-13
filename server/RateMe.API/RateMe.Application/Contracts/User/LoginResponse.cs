namespace RateMe.Application.Contracts.User;

public record LoginResponse
{
   public bool IsLoggedIn { get; set; } = false;
   public string JwtToken { get; set; }
   public string RefreshToken { get; internal set; }
}