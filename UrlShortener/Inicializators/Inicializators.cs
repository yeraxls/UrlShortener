using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;
using UrlShortener.Models;

namespace UrlShortener.Inicializators
{
    public class Inicializators : IInicializators
    {
        private readonly UrlShortenerDbContext _db;
        private readonly UserManager<AppUser> _user;
        private readonly RoleManager<AppRole> _role;

        public Inicializators(UrlShortenerDbContext db, UserManager<AppUser> user, RoleManager<AppRole> role)
        {
            _db = db;
            _user = user;
            _role = role;
        }

        public async void Inicializate()
        {
            try
            {

                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }catch (Exception ex)
            {

            }
            if (await _role.RoleExistsAsync("Administrador")) return;

            await _role.CreateAsync(new AppRole { Name = "Administrador", NumOfUrls = 10 });

            //Creación de rol usuario Registrado
            await _role.CreateAsync(new AppRole { Name = "Registrado", NumOfUrls = 3 });

            await _role.CreateAsync(new AppRole { Name = "FreeUser", NumOfUrls = 3 });


            var user = new AppUser
            {
                UserName = "YeraxAdmin",
                Email = "yerayls91@gmail.com",
                Name = "Yeray",
                Lastname = "López Santos",
                CountryCode = 34,
                Phone = "664175705",
                Country = "Spain",
                City = "Toledo",
                DateOfBirth = new DateTime(year:1991, month:4, day:22)
            };
            var result = await _user.CreateAsync(user, "Asd123$");
            await _user.AddToRoleAsync(user, "Administrador");
        }
    }
}
