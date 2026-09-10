using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ShoppingList
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public required string Name { get; set; }
        public string? AppUserId { get; set; }
        public string? GroupId { get; set; }
        
        [ForeignKey("GroupId")]
        public virtual StorageGroup? Group { get; set; }
        
        [ForeignKey("AppUserId")]
        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>();

    }
}
