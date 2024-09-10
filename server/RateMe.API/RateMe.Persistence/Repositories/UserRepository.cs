using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Core.Enums;
using RateMe.Core.Models;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class UserRepository : IRepository<User>
{
   private readonly RateMeDbContext _context;

   public UserRepository(RateMeDbContext context)
   {
      _context = context;
   }

   public async Task<Guid> Create(User entity)
   {
      var userEntity = new UserEntity
      {
         Id = entity.Id,
         Name = entity.Name,
         Surname = entity.Surname,
         Email = entity.Email,
         About = entity.About,
         UserName = entity.UserName,
         HashPassword = entity.HashPassword,
         Sex = entity.Sex,
         AvatarLink = entity.AvatarLink,
         Role = entity.Role,
      };

      await _context.AddAsync(userEntity);
      await _context.SaveChangesAsync();

      return userEntity.Id;
   }

   public async Task<List<User?>> Get()
   {
      var users = await _context.Users
         .AsNoTracking()
         .Select(u => User.Create(u.Id, u.Email, u.UserName, u.HashPassword, u.Sex, u.AvatarLink, u.Role, u.Surname,
            u.Name, u.About).user)
         .ToListAsync();

      return users;
   }


   public async Task<Guid> Delete(Guid id)
   {
      await _context.Users
         .Where(u => u.Id == id)
         .ExecuteDeleteAsync();

      return id;
   }

   public async Task<User?> GetById(Guid id)
   {
      return await _context.Users
         .AsNoTracking()
         .Select(u => User.Create(u.Id, u.Email, u.UserName, u.HashPassword, u.Sex, u.AvatarLink, u.Role, u.Surname,
            u.Name, u.About).user)
         .FirstOrDefaultAsync(u => u.Id == id);
   }
}