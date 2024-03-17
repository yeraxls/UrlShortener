using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly UrlShortenerDbContext _context;

        public UrlService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUrl(UrlVM urlVM)
        {
            var urlModel = (AppUrl)urlVM;
            var exist = await _context.Queryable<AppUrl>(c => c.UrlShort == urlModel.UrlShort).AnyAsync();
            if (exist)
            {
                return false;
            }
            await _context.Insert<AppUrl>((AppUrl)urlVM);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<LoadUrlsVM>> GetAllUrls(string id)
        {
            return await _context.Queryable<AppUrl>(c => c.UserId == id).Select(c => (LoadUrlsVM)c).ToListAsync();
        }

        public async Task<AppUrl> GetUrl(int id)
        {
            return await _context.Queryable<AppUrl>(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task Delete(AppUrl url)
        {
            await _context.Delete(url);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEnabled(AppUrl url)
        {
            url.Enabled = !url.Enabled;
            await _context.SaveChangesAsync();
        }


    }
}
