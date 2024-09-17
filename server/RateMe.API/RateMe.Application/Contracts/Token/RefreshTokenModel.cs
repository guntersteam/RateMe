namespace RateMe.Application.Contracts.Token;

public record RefreshTokenModel(
   string JwtToken,
   string RefreshToken
);