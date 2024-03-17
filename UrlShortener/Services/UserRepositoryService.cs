using Microsoft.EntityFrameworkCore;
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
    }
}
