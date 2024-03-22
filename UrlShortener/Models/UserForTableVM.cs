namespace UrlShortener.Models
{
    public class UserForTableVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public static explicit operator UserForTableVM(AppUser user)
        {
            return new UserForTableVM
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                UserName = user.UserName,
                Email = user.Email??"",
                Phone = user.Phone,
            };
        }

        public static explicit operator AppUser(UserForTableVM user)
        {
            return new AppUser
            {
                Id = user.Id,
                Name = user.Name,
                Lastname = user.Lastname,
                UserName = user.UserName,
                Email = user.Email ?? "",
                Phone = user.Phone,
            };
        }
    }
}
