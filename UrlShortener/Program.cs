using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;
using UrlShortener.Inicializators;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UrlDatabase"))
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UrlShortenerDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Users/Login");
    options.AccessDeniedPath = new PathString("/Users/Denied");
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
});

builder.Services.AddTransient<IUserRepositoryService, UserRepositoryService>();
builder.Services.AddTransient<IUrlService, UrlService>();
builder.Services.AddTransient<IRolesService, RolesService>();
builder.Services.AddScoped<IInicializators, Inicializators>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
using(var scope= app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var inicializador = services.GetRequiredService<IInicializators>();
        inicializador.Inicializate();
    }catch (Exception ex){
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError("Migrate failed");
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
