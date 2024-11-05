namespace RateMe.Infrastructure;

public class JwtOptions
{
   public string SecretKey { get; set; } = string.Empty;
   public int ExpiresDuration { get; set; } = 1;

}