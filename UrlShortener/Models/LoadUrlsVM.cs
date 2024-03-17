using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class LoadUrlsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Enabled { get; set; } = true;


        public static explicit operator LoadUrlsVM(AppUrl data)
        {
            return new LoadUrlsVM
            {
                Id = data.Id,
                Url = data.Url,
                Name = data.UrlShort,
                Enabled = data.Enabled
            };
        }
    }
}
