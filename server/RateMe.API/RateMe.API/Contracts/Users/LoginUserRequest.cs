using System.ComponentModel.DataAnnotations;

namespace RateMe.API.Contracts.Users;

public record LoginUserRequest(
   [Required] string UserName,
   [Required] string Password);