using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string DisplayName { get; set; }

        public string? GroupId { get; set; }        

        [ForeignKey("GroupId")]
        public virtual StorageGroup? Group { get; set; }        
        public virtual required Storage Storage { get; set; }
        public virtual ICollection<ShoppingList>? ShoppingList { get; set; } = new List<ShoppingList>();
    }
}
