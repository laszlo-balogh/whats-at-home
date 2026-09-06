using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Domain.Common;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<HouseholdItems> HouseholdItems { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<StorageGroup> StorageGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
     
            modelBuilder.Entity<StorageGroup>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(g => g.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BaseItem>()
                .HasOne(i => i.AddedByUser)
                .WithMany()
                .HasForeignKey(i => i.AddedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BaseItem>().UseTpcMappingStrategy();

            modelBuilder.Entity<FoodItem>().ToTable("FoodItems");
            modelBuilder.Entity<HouseholdItems>().ToTable("HouseholdItems");
            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItems");
        }
    }
}