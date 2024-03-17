using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlVM
    {
        [Required(ErrorMessage = "El nombre de la url es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La url es obligatoria")]
        public string Url { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public bool Enabled { get; set; } = true;

        public static explicit operator AppUrl(UrlVM vm)
        {
            return new AppUrl
            {
                UserId = vm.UserId,
                Url = vm.Url,
                UrlShort = $"{vm.Username}/{vm.Name}",
                Enabled = vm.Enabled
            };
        }
    }
}
