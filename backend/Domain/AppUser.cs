using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public required string DisplayName { get; set; }
        
        public required string GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual required FridgeGroup Group { get; set; }
    }
}
