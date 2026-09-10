using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Storage
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

        public virtual ICollection<BaseItem> Items { get; set; } = new List<BaseItem>();
    }
}
