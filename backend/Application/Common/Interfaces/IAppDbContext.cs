using Domain;
using Microsoft.EntityFrameworkCore;
namespace Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<AppUser> AppUsers { get; }
        DbSet<FoodItem> FoodItems { get; }
        DbSet<HouseholdItems> HouseholdItems { get; }
        DbSet<ShoppingListItem> ShoppingListItems { get; }
        DbSet<StorageGroup> StorageGroups { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}