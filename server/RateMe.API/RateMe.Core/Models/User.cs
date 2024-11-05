using RateMe.Core.Enums;

namespace RateMe.Core.Models;

public class User
{
   public const int MIN_USERNAME_LENGTH = 2;
   public Guid Id { get; }
   public string Name { get; } = string.Empty;
   public string Surname { get; } = string.Empty;
   public string Email { get; } = string.Empty;
   public string About { get; } = string.Empty;
   public string UserName { get; } = string.Empty;
   public string HashPassword { get; } = string.Empty;
   public Sex Sex { get; } = Sex.UnSelected;
   public string AvatarLink { get; } = string.Empty;
   public Role Role { get; } = Role.User;

   public User(Guid id, string name, string surname, string email, string about, string userName, string hashPassword,
      Sex sex,
      string avatarLink, Role role)
   {
      Id = id;
      Name = name;
      Surname = surname;
      Email = email;
      About = about;
      UserName = userName;
      HashPassword = hashPassword;
      Sex = sex;
      AvatarLink = avatarLink;
      Role = role;
   }

   public static (User user, string Error) Create(Guid id, string email, string userName,
      string hashPassword, Sex sex = Sex.UnSelected, string avatarLink = null, Role role = Role.User,
      string surname = null, string name = null, string about = null)
   {
      var error = string.Empty;

      if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(hashPassword))
      {
         error = "username or password can't be empty";
      }

      if (userName.Length < MIN_USERNAME_LENGTH)
      {
         error = "username can't be smaller than 2 chars";
      }

      var user = new User(id, name, surname, email, about, userName, hashPassword, sex, avatarLink, role);

      return (user, error);
   }
}