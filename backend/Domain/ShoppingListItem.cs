using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain
{
    public class ShoppingListItem : BaseItem
    {                
        public bool IsPurchased { get; set; }        
     
    }
}
