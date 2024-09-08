using Microsoft.EntityFrameworkCore;
using RateMe.Core.Abstractions;
using RateMe.Core.Enums;
using RateMe.Persistence.Entities;

namespace RateMe.Persistence.Repositories;

public class UserRepository : IRepository<UserEntity>
{
   private readonly RateMeDbContext _context;

   public UserRepository(RateMeDbContext context)
   {
      _context = context;
   }

   public async Task<Guid> Create(UserEntity entity)
   {
      var userEntity = new UserEntity
      {
         Id = entity.Id,
         Name = entity.Name,
         Surname = entity.Surname,
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

   public async Task<List<UserEntity?>> Get()
   {
      var users = await _context.Users.AsNoTracking().ToListAsync();

      return users;
   }
   

   public async Task<Guid> Delete(Guid id)
   {
       await _context.Users
         .Where(u => u.Id ==  id)
         .ExecuteDeleteAsync();

       return id;
   }

   public async Task<UserEntity?> GetById(Guid id)
   {
      return await _context.Users
         .AsNoTracking()
         .FirstOrDefaultAsync(u => u.Id == id);
   }
}