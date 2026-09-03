using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string DisplayName { get; set; }
        
        public required string GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required FridgeGroup Group { get; set; }
    }
}
