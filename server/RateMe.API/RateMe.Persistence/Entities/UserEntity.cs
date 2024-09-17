using Microsoft.AspNetCore.Identity;
using RateMe.Core.Enums;

namespace RateMe.Persistence.Entities;

public class UserEntity
{
   public Guid Id { get; set; }
   public string Name { get; set; } = string.Empty;
   public string Surname { get; set; } = string.Empty;
   public string Email { get; set; } = string.Empty;
   public string About { get; set; } = string.Empty;
   public string UserName { get; set; } = string.Empty;
   public string HashPassword { get; set; } = string.Empty;
   public Sex Sex { get; set; } = Sex.UnSelected;
   public string AvatarLink { get; set; } = string.Empty;
   public Role Role { get; set; } = Role.User;
   
   public TokenEntity Token { get; set; }
   public List<ReviewEntity> Reviews { get; set; } = [];
   public List<UserTrackEntity> UserTracks { get; set; } = [];
   public List<PlayListEntity> PlayLists { get; set; } = [];
}