using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class FridgeGroup
    {
        [Key] 
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string GroupName { get; set; }

        public required string AdminUserId { get; set; }

        public virtual ICollection<AppUser> Members { get; set; } = new List<AppUser>();

        public virtual ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();

        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();
    }
}
