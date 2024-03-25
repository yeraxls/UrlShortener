
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IUserRepositoryService
    {
        Task<List<UserForTableVM>> GetUsers();
        Task<UserForTableVM> GetUserById(string userId);
        Task UpdateUser(UserForTableVM user);
        Task DeleteUser(string id);
    }
}