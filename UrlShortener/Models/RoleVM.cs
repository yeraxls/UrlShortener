using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static explicit operator RoleVM(IdentityRole role)
        {
            return new RoleVM
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
