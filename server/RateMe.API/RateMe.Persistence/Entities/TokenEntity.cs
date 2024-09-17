namespace RateMe.Persistence.Entities;

public class TokenEntity
{
   public Guid TokenId { get; set; }
   public Guid UserId { get; set; }
   public UserEntity User { get; set; }
   public string TokenValue { get; set; }
   public DateTime ExpiresDate { get; set; } = DateTime.UtcNow.AddDays(7);
   
}