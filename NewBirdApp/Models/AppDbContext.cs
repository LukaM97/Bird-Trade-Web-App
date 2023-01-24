using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewBirdApp.Models
{
    public class AppDbContext:IdentityDbContext<AppUser,AppRole,int, 
        IdentityUserClaim<int>,AppUserRole,IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Bird> Birds { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public AppDbContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();


            builder.Entity<Gender>()
                .HasData(
                    new Gender { Id = 1, Name = "Female" },
                    new Gender { Id = 2, Name = "Male" }
                );
            builder.Entity<Bird>()
                .HasData(
                    new Bird { Id = 1, Name = "Patuljasta patka", GenderId = 2, Quantity = 20 },
                    new Bird { Id = 2, Name = "Patuljasta patka", GenderId = 1, Quantity = 16 },
                    new Bird { Id = 3, Name = "Papagaj rase Aleksandar", GenderId = 2, Quantity = 10 },
                    new Bird { Id = 4, Name = "Papagaj rase Aleksandar", GenderId = 1, Quantity = 12 },
                    new Bird { Id = 5, Name = "Zlatni fazan", GenderId = 2, Quantity = 6 },
                    new Bird { Id = 6, Name = "Zlatni fazan", GenderId = 1, Quantity = 2 }
                );
            builder.Entity<Order>()
                .HasData(
                    new Order { Id = 1, NameOfCustomer = "Aleksandar", SurnameOfCustomer = "Boric", CustomerIdNumber = 1178395612875, AdressOfCustomer = "Bajnatska 20", CustomerCity = "Subotica", BirdId = 1, Amount = 2 },
                    new Order { Id = 2, NameOfCustomer = "Aleksandar", SurnameOfCustomer = "Boric", CustomerIdNumber = 1178395612875, AdressOfCustomer = "Bajnatska 20", CustomerCity = "Subotica", BirdId = 2, Amount = 2 }
                );
        }
    }
}
