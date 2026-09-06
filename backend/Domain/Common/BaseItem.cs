using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class BaseItem
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public int Quantity { get; set; }
        public required string GroupId { get; set; }
        public string? AddedByUserId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required StorageGroup Group { get; set; }

        [ForeignKey("AddedByUserId")]
        public virtual AppUser? AddedByUser { get; set; }
    }
}