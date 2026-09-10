using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class StorageGroup
    {
        [Key] 
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string GroupName { get; set; }
        public required string AdminUserId { get; set; }
        [ForeignKey("AdminUserId")]
        public virtual required AppUser AdminUser { get; set; }
        public virtual required Storage Storage { get; set; }
        public virtual ICollection<ShoppingList>? ShoppingList { get; set; } = new List<ShoppingList>();
        public virtual ICollection<AppUser> Members { get; set; } = new List<AppUser>();
    }
}
