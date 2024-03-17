using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Int32 CountryCode { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
