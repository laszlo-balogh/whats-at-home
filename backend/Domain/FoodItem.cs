using Domain.Common;
using Domain.Enums;

namespace Domain
{
    public class FoodItem : BaseItem
    {
        public string? Unit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public FoodCategory Category { get; set; }
    }
}
