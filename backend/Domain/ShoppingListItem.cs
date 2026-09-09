using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ShoppingListItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public bool IsPurchased { get; set; }
        public int Quantity { get; set; }
        public string? AddedByUserId { get; set; }
        public required string ShoppingListId { get; set; }
        [ForeignKey("AddedByUserId")]
        public virtual AppUser? AddedByUser { get; set; }
        [ForeignKey("ShoppingListId")]
        public virtual required ShoppingList ShoppingList { get; set; }
    }
}
