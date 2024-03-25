using Microsoft.EntityFrameworkCore;
using System.Numerics;
using UrlShortener.Context;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UserRepositoryService : IUserRepositoryService
    {
        private readonly UrlShortenerDbContext _context;

        public UserRepositoryService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserForTableVM>> GetUsers()
        {
            var result = await _context.Queryable<AppUser>().Select(c => (UserForTableVM)c).ToListAsync();
            return result;
        }

        public async Task<UserForTableVM> GetUserById(string userId)
        {
            var result = await _context.Queryable<AppUser>(u => u.Id == userId).Select(u => (UserForTableVM)u).FirstOrDefaultAsync();
            return result;
        }
        public async Task UpdateUser(UserForTableVM user)
        {
            var result = await _context.Queryable<AppUser>(u => u.Id == user.Id).FirstOrDefaultAsync();
            result.Name = user.Name;
            result.Lastname = user.Lastname;
            result.UserName = user.UserName;
            result.Email = user.Email ?? "";
            result.Phone = user.Phone;

            await _context.SaveAll();
        }

        public async Task DeleteUser(string id)
        {
            var result = await _context.Queryable<AppUser>(u => u.Id == id).FirstOrDefaultAsync();
            await _context.Delete(result);
            await _context.SaveAll();
        }
    }
}
