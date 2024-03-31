using Microsoft.AspNetCore.Identity;

namespace UrlShortener.Models
{
    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int NumOfUrls { get; set; }

        public static explicit operator RoleVM(AppRole role)
        {
            return new RoleVM
            {
                Id = role.Id,
                Name = role.Name,
                NumOfUrls = role.NumOfUrls
            };
        }
    }
}
