using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public class AppRole : IdentityRole
    {
        public int NumOfUrls { get; set; }
    }
}
