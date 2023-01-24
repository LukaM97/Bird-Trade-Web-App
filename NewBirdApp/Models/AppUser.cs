using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace NewBirdApp.Models
{
    public class AppUser:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastOnline { get; set; } = DateTime.Now;
        public string Country { get; set; }
        public string City { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
