using Microsoft.AspNetCore.Identity;

namespace NewBirdApp.Models
{
    public class AppUserRole:IdentityUserRole<int>
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
