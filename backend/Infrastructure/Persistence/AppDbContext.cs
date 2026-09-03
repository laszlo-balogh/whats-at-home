using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext(options)
    {
        public DbSet<AppUser> AppUsers {get; set;}
        public DbSet<FoodItem> FoodItems {get; set;}
        public DbSet<FridgeGroup> FridgeGroups {get; set;}
        public DbSet<FridgeGroup> FridgeGroups {get; set;}
    }
}