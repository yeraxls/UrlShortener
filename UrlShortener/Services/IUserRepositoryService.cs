
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IUserRepositoryService
    {
        Task<List<UserForTableVM>> GetUsers();
    }
}