using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrlShortener.Models;

namespace UrlShortener.Context
{
    public class UrlShortenerDbContext : IdentityDbContext
    {
        public UrlShortenerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsuario { get; set; }
        public DbSet<AppUrl> AppUrl { get; set; }

        public async Task<T> Insert<T>(T elemento) where T : class
        {
            await AddAsync<T>(elemento);
            return elemento;
        }

        public async Task SalvarCambios()
        {
            await SaveChangesAsync();
        }

        public IQueryable<T> Queryable<T>(Expression<Func<T, bool>>? expression = null) where T : class
        {
            if (expression == null)
                return Set<T>();
            return Set<T>().Where(expression);
        }

        public async Task Delete<T>(T elemento) where T : class
        {
            Remove<T>(elemento);
        }
    }
}
