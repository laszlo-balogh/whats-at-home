using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class ShoppingListItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public bool IsPurchased { get; set; }

        public string? GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required FridgeGroup Group { get; set; }

        public required string AddedByUserId { get; set; }

        [ForeignKey("AddedByUserId")]
        public virtual required AppUser AddedByUser { get; set; }
    }
}
