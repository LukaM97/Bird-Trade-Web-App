using System;

namespace NewBirdApp.Models.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastOnline { get; set; } = DateTime.Now;
        public string Token { get; set; }
    }
}
