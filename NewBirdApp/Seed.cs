using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewBirdApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewBirdApp
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("UserSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            var roles = new List<AppRole> {
                    new AppRole { Name = "Member"},
                    new AppRole { Name = "Admin"}
                };

            foreach(var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user,"Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "adminLuka",
            };
            await userManager.CreateAsync(admin, "adminPa$$w0rd");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
