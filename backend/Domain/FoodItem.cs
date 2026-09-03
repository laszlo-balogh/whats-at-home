using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class FoodItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public required string Category { get; set; }
        public required string GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required FridgeGroup Group { get; set; }

        public required string AddedByUserId { get; set; }

        [ForeignKey("AddedByUserId")]
        public virtual required AppUser AddedByUser { get; set; }
    }
}
