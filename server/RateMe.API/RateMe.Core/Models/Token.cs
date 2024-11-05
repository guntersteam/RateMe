namespace RateMe.Core.Models;

public class Token
{
   public Guid TokenId { get; }
   public Guid UserId { get; }
   public string TokenString { get; }
   public DateTime  ExpiresDate { get; }

   public Token(Guid tokenId, Guid userId, string tokenString, DateTime expiresDate)
   {
      TokenId = tokenId;
      UserId = userId;
      TokenString = tokenString;
      ExpiresDate = expiresDate;
   }

   public static (Token token, string Error) Create(Guid tokenId, Guid userId, string tokenString, DateTime expiresDate)
   {
      var errorString = string.Empty;

      if (string.IsNullOrEmpty(tokenString))
      {
         errorString = "Token value can't be empty";
      }

      var token = new Token(tokenId, userId, tokenString, expiresDate);

      return (token, errorString);
   }
}