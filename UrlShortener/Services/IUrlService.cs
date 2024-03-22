using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<bool> AddUrl(UrlVM url);
        Task<List<LoadUrlsVM>> GetAllUrls(string id);
        Task<AppUrl> GetUrl(int id);
        Task Delete(AppUrl url);
        Task UpdateEnabled(AppUrl url);
        Task EditUrl(AppUrl url, LoadUrlsVM urlVM);
        Task<AppUrl> GetUrlByName(string nameUrl);
        Task<int> CountUrl(UrlVM urlVM);
    }
}