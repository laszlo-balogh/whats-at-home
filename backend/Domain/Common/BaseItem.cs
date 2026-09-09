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
        public required string StorageId { get; set; }        

        [ForeignKey("StorageId")]
        public virtual required Storage Storage { get; set; }

    }
}