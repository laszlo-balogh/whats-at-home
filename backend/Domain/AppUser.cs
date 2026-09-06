using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string DisplayName { get; set; }
        
        public required string GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required StorageGroup Group { get; set; }
    }
}
