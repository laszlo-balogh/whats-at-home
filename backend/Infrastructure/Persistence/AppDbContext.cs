using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Domain.Common;
using Application.Common.Interfaces;

namespace Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options), IAppDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<HouseholdItems> HouseholdItems { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }
        public DbSet<StorageGroup> StorageGroups { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.Restrict);
     
            modelBuilder.Entity<StorageGroup>()
                .HasOne(g => g.AdminUser)
                .WithOne()
                .HasForeignKey<StorageGroup>(g => g.AdminUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingListItem>()
                .HasOne(i => i.AddedByUser)
                .WithMany()
                .HasForeignKey(i => i.AddedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Storage>()
                .HasOne(s => s.AppUser)
                .WithOne(u => u.Storage)
                .HasForeignKey<Storage>(s => s.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Storage>()
                .HasOne(s => s.Group)
                .WithOne(g => g.Storage)
                .HasForeignKey<Storage>(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingList>()
                .HasOne(sl => sl.AppUser)
                .WithMany(u => u.ShoppingList)
                .HasForeignKey(sl => sl.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BaseItem>().UseTpcMappingStrategy();

            modelBuilder.Entity<FoodItem>().ToTable("FoodItems");
            modelBuilder.Entity<HouseholdItems>().ToTable("HouseholdItems");
            modelBuilder.Entity<ShoppingListItem>().ToTable("ShoppingListItems");
        }
    }
}